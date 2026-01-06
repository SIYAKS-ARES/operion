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
            sb.AppendLine("- ID (Integer): Benzersiz kimlik");
            sb.AppendLine("- AD (String): Müşteri adı");
            sb.AppendLine("- SOYAD (String): Müşteri soyadı");
            sb.AppendLine("- TELEFON (String): Telefon numarası");
            sb.AppendLine("- SILINDI (Boolean): Silinme durumu (0: Aktif, 1: Silinmiş)");
            sb.AppendLine("");

            sb.AppendLine("TABLO: TBL_STOKLAR (Ürünler)");
            sb.AppendLine("- ID (Integer): Benzersiz kimlik");
            sb.AppendLine("- URUNAD (String): Ürün adı");
            sb.AppendLine("- OBJE (String): Ürün markası veya grubu");
            sb.AppendLine("- ADET (Integer): Stok adedi");
            sb.AppendLine("- SATISFIYAT (Decimal): Satış fiyatı");
            sb.AppendLine("- DETAY (String): Ürün detayları");
            sb.AppendLine("- YIL (String): Üretim yılı");
            sb.AppendLine("");

             sb.AppendLine("TABLO: TBL_FIRMALAR (Tedarikçiler/Firmalar)");
            sb.AppendLine("- ID (Integer): Benzersiz kimlik");
            sb.AppendLine("- AD (String): Firma adı");
            sb.AppendLine("- YETKILISTATU (String): Yetkili ünvanı");
            sb.AppendLine("- YETKILIADSOYAD (String): Yetkili kişi");
            sb.AppendLine("- SEKTOR (String): Faaliyet sektörü");
            sb.AppendLine("- TELEFON1 (String): Birinci telefon");
            sb.AppendLine("- IL (String): İl");
            sb.AppendLine("- ILCE (String): İlçe");
            sb.AppendLine("");

            sb.AppendLine("TABLO: TBL_PERSONELLER (Çalışanlar)");
            sb.AppendLine("- ID (Integer): Benzersiz kimlik");
            sb.AppendLine("- AD (String): Personel adı");
            sb.AppendLine("- SOYAD (String): Personel soyadı");
            sb.AppendLine("- GOREV (String): Görevi");
            sb.AppendLine("- IL (String): İl");
            sb.AppendLine("");

            // Add other tables as needed based on the project requirements
            
            return sb.ToString();
        }

        public List<string> GetTableNames()
        {
            return new List<string> { "TBL_MUSTERILER", "TBL_STOKLAR", "TBL_FIRMALAR", "TBL_PERSONELLER" };
        }
    }
}
