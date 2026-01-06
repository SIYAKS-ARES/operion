using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace operion.Application.Services
{
    public interface IDatabaseSchemaService
    {
        string GetSchemaDefinition();
        List<string> GetTableNames();
    }

    public class DatabaseSchemaService : IDatabaseSchemaService
    {
        // Define the schema in a format the LLM can understand (e.g., simplified DDL or XML)
        public string GetSchemaDefinition()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Aşağıdaki veritabanı şemasını kullan:");
            sb.AppendLine("");

            sb.AppendLine("TABLO: TBL_MUSTERILER (Müşteriler)");
            sb.AppendLine("- MusteriID (Integer): Benzersiz kimlik");
            sb.AppendLine("- MusteriAd (String): Müşteri adı");
            sb.AppendLine("- MusteriSoyad (String): Müşteri soyadı");
            sb.AppendLine("- MusteriTelefon (String): Telefon numarası");
            sb.AppendLine("- MusteriSehir (String): Şehir");
            sb.AppendLine("- MusteriIlce (String): İlçe");
            sb.AppendLine("");
            
            // TBL_STOKLAR (Already fixed in previous step, checking context to ensure this block aligns)
            // ... (Skipping TBL_URUNLER/TBL_STOKLAR as they are below this block in the file I'm targeting) ... 
            // Wait, I need to be careful with line numbers.
            // My previous edit replaced TBL_STOKLAR (lines 32-40) with TBL_URUNLER. 
            // Now I am looking at lines 24-30 (MUSTERILER) and 42-59 (FIRMALAR/PERSONELLER).
            // Since TBL_URUNLER is now in the middle, I should target the blocks separately or be careful.
            
            // Let's do TBL_MUSTERILER first (Lines 24-30)
            // Then TBL_FIRMALAR (Lines 42-51 originally, but shifted due to previous edit)
            // It's safer to read the file again or do a MultiReplace?
            // I'll do a MultiReplace for safety.

            sb.AppendLine("TABLO: TBL_URUNLER (Ürünler ve Stoklar)");
            sb.AppendLine("- UrunID (Integer): Benzersiz kimlik");
            sb.AppendLine("- UrunAd (String): Ürün adı (Örnek: Laptop, Mouse)");
            sb.AppendLine("- UrunMarka (String): Marka (Örnek: Dell, Logitech)");
            sb.AppendLine("- UrunModel (String): Model");
            sb.AppendLine("- UrunAdet (Integer): Stoktaki mevcut miktar");
            sb.AppendLine("- UrunMaliyet (Decimal): Alış fiyatı/Maliyet");
            sb.AppendLine("- UrunSatisFiyat (Decimal): Satış fiyatı");
            sb.AppendLine("- UrunDetay (String): Açıklama");
            sb.AppendLine("");

             // TBL_STOKLAR is a secondary table, mostly for charts/categories in this specific schema
            sb.AppendLine("TABLO: TBL_STOKLAR (Stok Türleri)");
            sb.AppendLine("- StokID (Integer): ID");
            sb.AppendLine("- StokTur (String): Kategori Adı");
            sb.AppendLine("- StokAdet (String): Kategori Adedi (Sayısal veri string olarak tutuluyor olabilir)");
            sb.AppendLine("");

             sb.AppendLine("TABLO: TBL_FIRMALAR (Tedarikçiler/Firmalar)");
            sb.AppendLine("- FirmaID (Integer): Benzersiz kimlik");
            sb.AppendLine("- FirmaAd (String): Firma adı");
            sb.AppendLine("- FirmaYetkiliAdSoyad (String): Yetkili kişi");
            sb.AppendLine("- FirmaSektor (String): Faaliyet sektörü");
            sb.AppendLine("- FirmaTelefon1 (String): Birinci telefon");
            sb.AppendLine("- FirmaSehir (String): İl");
            sb.AppendLine("- FirmaIlce (String): İlçe");
            sb.AppendLine("");

            sb.AppendLine("TABLO: TBL_PERSONELLER (Çalışanlar)");
            sb.AppendLine("- PersonelID (Integer): Benzersiz kimlik");
            sb.AppendLine("- PersonelAd (String): Personel adı");
            sb.AppendLine("- PersonelSoyad (String): Personel soyadı");
            sb.AppendLine("- PersonelGorev (String): Görevi");
            sb.AppendLine("- PersonelSehir (String): İl");
            sb.AppendLine("- PersonelMail (String): E-posta adresi");
            sb.AppendLine("");

            return sb.ToString();
        }

        public List<string> GetTableNames()
        {
            return new List<string> { "TBL_MUSTERILER", "TBL_URUNLER", "TBL_STOKLAR", "TBL_FIRMALAR", "TBL_PERSONELLER" };
        }
    }
}
