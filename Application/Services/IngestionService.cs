using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace operion.Application.Services
{
    public class IngestionService
    {
        private readonly RagService _ragService;
        private readonly int _chunkSize = 512; // Modest chunk size for better context
        private readonly int _overlap = 50;

        public IngestionService(RagService ragService)
        {
            _ragService = ragService;
        }

        // --- SQL Veri İşleme (Strategy 3.A) ---
        
        /// <summary>
        /// Müşteri verilerini özet metin formatına çevirip indeksler.
        /// Gerçek uygulamada burası DB'den (Entity Framework) çekilen verilerle çalışır.
        /// </summary>
        public async Task<int> IngestCustomerDataAsync(IEnumerable<dynamic> customers)
        {
            int count = 0;
            foreach (var customer in customers)
            {
                // Row Serialization Strategy
                // Format: "Müşteri: {Name}, Şehir: {City}, Bakiye: {Balance}, Son İşlem: {LastDate}"
                var sb = new StringBuilder();
                sb.AppendLine($"Müşteri Kaydı: {customer.AdSoyad}");
                sb.AppendLine($"Firma: {customer.FirmaUnvani}");
                sb.AppendLine($"İl/İlçe: {customer.Il}/{customer.Ilce}");
                sb.AppendLine($"Telefon: {customer.Telefon}");
                sb.AppendLine($"Bakiye: {customer.Bakiye}");
                
                string serializedText = sb.ToString();
                string id = $"cust_{customer.Id}";
                string metadata = $"Type: Customer, ID: {customer.Id}";

                await _ragService.SaveInformationAsync(
                    id: id,
                    text: serializedText,
                    title: customer.FirmaUnvani ?? customer.AdSoyad,
                    additionalMetadata: metadata
                );
                count++;
            }
            return count;
        }

        // --- Döküman İşleme ---

        /// <summary>
        /// Belirtilen klasördeki tüm desteklenen dosyaları (md, txt) işler.
        /// </summary>
        public async Task<int> IngestDirectoryAsync(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
            }

            var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".md") || f.EndsWith(".txt"))
                .ToList();

            int totalChunks = 0;

            foreach (var file in files)
            {
                totalChunks += await IngestFileAsync(file);
            }

            return totalChunks;
        }

        /// <summary>
        /// Tek bir dosyayı okur, parçalar ve RAG servisine kaydeder.
        /// </summary>
        public async Task<int> IngestFileAsync(string filePath)
        {
            string text = await File.ReadAllTextAsync(filePath);
            string fileName = Path.GetFileName(filePath);
            string fileId = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName)); // Simple ID

            // 1. Chunking
            var chunks = RecursiveCharacterTextSplitter(text, _chunkSize, _overlap);

            // 2. Indexing
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

        /// <summary>
        /// Metni özyinelemeli olarak anlamlı parçalara böler.
        /// (LangChain RecursiveCharacterTextSplitter mantığı)
        /// </summary>
        private List<string> RecursiveCharacterTextSplitter(string text, int maxTokens, int overlap)
        {
            var chunks = new List<string>();
            
            // Ayrıştırıcı öncelik sırası: Paragraf -> Satır -> Cümle -> Kelime -> Karakter
            string[] separators = new[] { "\n\n", "\n", ". ", " ", "" };

            SplitRecursively(text, maxTokens, overlap, separators, 0, chunks);

            return chunks;
        }

        private void SplitRecursively(string text, int maxTokens, int overlap, string[] separators, int separatorIndex, List<string> output)
        {
            // Token sayımı (yaklaşık: 4 karakter = 1 token)
            int estimatedTokens = text.Length / 4;

            if (estimatedTokens <= maxTokens)
            {
                output.Add(text);
                return;
            }

            // Separator kalmadıysa mecbur bölüyoruz (veya karakter bazlı bölme)
            if (separatorIndex >= separators.Length)
            {
                // Basitçe karakter limitine göre böl
                int charLimit = maxTokens * 4;
                for (int i = 0; i < text.Length; i += charLimit - overlap)
                {
                    int len = Math.Min(charLimit, text.Length - i);
                    output.Add(text.Substring(i, len));
                }
                return;
            }

            string separator = separators[separatorIndex];
            string[] splits;
            
            if (string.IsNullOrEmpty(separator))
            {
                // Karakter bazlı split (son çare)
                splits = text.ToCharArray().Select(c => c.ToString()).ToArray();
            }
            else
            {
                // Split ederken separator'ı korumak isteriz ama basitlik için şimdilik kaybedebiliriz
                // Veya split edip sonuna ekleyebiliriz. C# string.Split separator'ı atar.
                splits = text.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            }

            string currentChunk = "";
            
            foreach (var split in splits)
            {
                string nextSegment = string.IsNullOrEmpty(currentChunk) ? split : currentChunk + separator + split;
                int estimate = nextSegment.Length / 4;

                if (estimate > maxTokens)
                {
                    // Mevcut chunk dolu, önce onu kaydet (varsa)
                    if (!string.IsNullOrEmpty(currentChunk))
                    {
                        // recursive call for currentChunk? No, currentChunk is implicitly < maxTokens because we check before adding
                        output.Add(currentChunk);
                    }
                    
                    // Yeni parça tek başına bile çok büyükse, onu bir alt seviye separator ile böl
                    if ((split.Length / 4) > maxTokens)
                    {
                        SplitRecursively(split, maxTokens, overlap, separators, separatorIndex + 1, output);
                    }
                    else
                    {
                        currentChunk = split;
                    }
                }
                else
                {
                    currentChunk = nextSegment;
                }
            }

            if (!string.IsNullOrEmpty(currentChunk))
            {
                output.Add(currentChunk);
            }
        }
    }
}
