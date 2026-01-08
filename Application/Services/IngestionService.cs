using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using operion.Infrastructure.Data;

namespace operion.Application.Services
{
    public class IngestionService
    {
        private readonly RagService _ragService;
        private readonly int _chunkSize = 512;
        private readonly int _overlap = 50;

        public IngestionService(RagService ragService)
        {
            _ragService = ragService;
        }

        // --- MASTER SYNC METHOD ---
        public async Task<string> IngestAllAsync()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Veri senkronizasyonu başladı...");

            // 1. Notlar
            int notesCount = await IngestNotesAsync();
            sb.AppendLine($"✅ {notesCount} adet Not işlendi.");

            // 2. Ürünler
            int productsCount = await IngestProductsAsync();
            sb.AppendLine($"✅ {productsCount} adet Ürün işlendi.");

            // 3. Müşteriler (Opsiyonel chunks)
            // int custCount = await IngestCustomersViaDbAsync();
            // sb.AppendLine($"✅ {custCount} adet Müşteri işlendi.");

            sb.AppendLine("Senkronizasyon tamamlandı.");
            return sb.ToString();
        }

        // --- 1. NOTLARIN İŞLENMESİ ---
        public async Task<int> IngestNotesAsync()
        {
            int count = 0;
            using (var connection = DatabaseService.GetConnection())
            {
                // Baglantı zaten açıksa tekrar açmaya gerek yok ama GetConnection yeni açıyor mu kontrol edelim.
                // DatabaseService.GetConnection() genelde open döner.
                
                string query = "SELECT NotID, NotTarih, NotSaat, NotBaslik, NotDetay, NotOlusturan, NotHitap FROM TBL_NOTLAR";
                
                using (var command = new Microsoft.Data.Sqlite.SqliteCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var notId = reader["NotID"].ToString();
                        var notTarih = reader["NotTarih"]?.ToString();
                        var notSaat = reader["NotSaat"]?.ToString();
                        var notBaslik = reader["NotBaslik"]?.ToString();
                        var notDetay = reader["NotDetay"]?.ToString();
                        var notOlusturan = reader["NotOlusturan"]?.ToString();
                        var notHitap = reader["NotHitap"]?.ToString();

                        // Format: "Not Başlığı: X, Tarih: Y, Detay: Z"
                        var sb = new StringBuilder();
                        sb.AppendLine($"Belge Türü: Ajanda Notu / Toplantı Kaydı");
                        sb.AppendLine($"Tarih: {notTarih} {notSaat}");
                        sb.AppendLine($"Konu: {notBaslik}");
                        sb.AppendLine($"İlgili Kişi: {notHitap}");
                        sb.AppendLine($"Oluşturan: {notOlusturan}");
                        sb.AppendLine($"İçerik: {notDetay}");

                        string serializedText = sb.ToString();
                        string id = $"note_{notId}";
                        string metadata = $"Type: Note, ID: {notId}, Date: {notTarih}";

                        await _ragService.SaveInformationAsync(
                            id: id,
                            text: serializedText,
                            title: notBaslik ?? "Not",
                            additionalMetadata: metadata
                        );
                        count++;
                    }
                }
            }
            return count;
        }

        // --- 2. ÜRÜNLERİN İŞLENMESİ ---
        public async Task<int> IngestProductsAsync()
        {
            int count = 0;
            using (var connection = DatabaseService.GetConnection())
            {
                string query = "SELECT UrunID, UrunAd, UrunMarka, UrunModel, UrunYil, UrunAdet, UrunSatisFiyat, UrunDetay FROM TBL_URUNLER";
                
                using (var command = new Microsoft.Data.Sqlite.SqliteCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var urunId = reader["UrunID"].ToString();
                        var urunAd = reader["UrunAd"]?.ToString();
                        var urunMarka = reader["UrunMarka"]?.ToString();
                        var urunModel = reader["UrunModel"]?.ToString();
                        var urunYil = reader["UrunYil"]?.ToString();
                        var urunAdet = reader["UrunAdet"]?.ToString();
                        var urunSatisFiyat = reader["UrunSatisFiyat"]?.ToString();
                        var urunDetay = reader["UrunDetay"]?.ToString(); // Önemli kısım
                        
                         // Format: "Ürün: X, Marka: Y, Özellikler: Z"
                        var sb = new StringBuilder();
                        sb.AppendLine($"Belge Türü: Ürün Kataloğu / Stok Kartı");
                        sb.AppendLine($"Ürün Adı: {urunAd} {urunMarka} {urunModel}");
                        sb.AppendLine($"Kategori/Yıl: {urunYil} Model");
                        sb.AppendLine($"Stok Adedi: {urunAdet}");
                        sb.AppendLine($"Satış Fiyatı: {urunSatisFiyat}"); // Currency format removed for simplicity or manual add
                        sb.AppendLine($"Ürün Açıklaması ve Özellikleri: {urunDetay}");

                        string serializedText = sb.ToString();
                        string id = $"prod_{urunId}";
                        string metadata = $"Type: Product, ID: {urunId}, Brand: {urunMarka}";

                        await _ragService.SaveInformationAsync(
                            id: id,
                            text: serializedText,
                            title: $"{urunMarka} {urunAd}",
                            additionalMetadata: metadata
                        );
                        count++;
                    }
                }
            }
            return count;
        }

        // --- HELPER: FILE CHUNKING (Legacy support for files) ---
        public async Task<int> IngestFileAsync(string filePath)
        {
            if (!File.Exists(filePath)) return 0;

            string text = await File.ReadAllTextAsync(filePath);
            string fileName = Path.GetFileName(filePath);
            string fileId = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName));

            var chunks = RecursiveCharacterTextSplitter(text, _chunkSize, _overlap);
            int chunkIndex = 0;
            foreach (var chunk in chunks)
            {
                string chunkId = $"{fileId}_{chunkIndex}";
                string metadata = $"File: {fileName}, Chunk: {chunkIndex}";
                
                await _ragService.SaveInformationAsync(
                    id: chunkId,
                    text: chunk,
                    title: fileName,
                    additionalMetadata: metadata
                );
                chunkIndex++;
            }
            return chunks.Count;
        }

        private List<string> RecursiveCharacterTextSplitter(string text, int maxTokens, int overlap)
        {
            var chunks = new List<string>();
            string[] separators = new[] { "\n\n", "\n", ". ", " ", "" };
            SplitRecursively(text, maxTokens, overlap, separators, 0, chunks);
            return chunks;
        }

        private void SplitRecursively(string text, int maxTokens, int overlap, string[] separators, int separatorIndex, List<string> output)
        {
            int estimatedTokens = text.Length / 4;
            if (estimatedTokens <= maxTokens)
            {
                output.Add(text);
                return;
            }

            if (separatorIndex >= separators.Length)
            {
                int charLimit = maxTokens * 4;
                for (int i = 0; i < text.Length; i += charLimit - overlap)
                {
                    int len = Math.Min(charLimit, text.Length - i);
                    output.Add(text.Substring(i, len));
                }
                return;
            }

            string separator = separators[separatorIndex];
            string[] splits = string.IsNullOrEmpty(separator) 
                ? text.ToCharArray().Select(c => c.ToString()).ToArray() 
                : text.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            string currentChunk = "";
            foreach (var split in splits)
            {
                string nextSegment = string.IsNullOrEmpty(currentChunk) ? split : currentChunk + separator + split;
                if ((nextSegment.Length / 4) > maxTokens)
                {
                    if (!string.IsNullOrEmpty(currentChunk)) output.Add(currentChunk);
                    
                    if ((split.Length / 4) > maxTokens)
                        SplitRecursively(split, maxTokens, overlap, separators, separatorIndex + 1, output);
                    else
                        currentChunk = split;
                }
                else
                {
                    currentChunk = nextSegment;
                }
            }
            if (!string.IsNullOrEmpty(currentChunk)) output.Add(currentChunk);
        }
    }
}
