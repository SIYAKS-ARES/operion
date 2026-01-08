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

            // --- MUSTERILER ---
            sb.AppendLine("TABLO: TBL_MUSTERILER (Müşteriler)");
            sb.AppendLine("- MusteriID (Integer): Benzersiz kimlik (Primary Key)");
            sb.AppendLine("- MusteriAd (String): Müşteri adı");
            sb.AppendLine("- MusteriSoyad (String): Müşteri soyadı");
            sb.AppendLine("- MusteriTelefon (String): Telefon numarası (Format: 05xx xxx xx xx)");
            sb.AppendLine("- MusteriTelefon2 (String): İkinci telefon");
            sb.AppendLine("- MusteriTC (String): TC Kimlik Numarası");
            sb.AppendLine("- MusteriMail (String): E-posta adresi");
            sb.AppendLine("- MusteriSehir (String): Şehir");
            sb.AppendLine("- MusteriIlce (String): İlçe");
            sb.AppendLine("- MusteriAdres (String): Açık adres bilgisi");
            sb.AppendLine("- MusteriVergiDaire (String): Vergi dairesi");
            sb.AppendLine("");

            // --- URUNLER ---
            sb.AppendLine("TABLO: TBL_URUNLER (Ürünler ve Stoklar)");
            sb.AppendLine("- UrunID (Integer): Benzersiz kimlik (Primary Key)");
            sb.AppendLine("- UrunAd (String): Ürün adı (Örnek: Laptop, Ram, Mouse)");
            sb.AppendLine("- UrunMarka (String): Marka");
            sb.AppendLine("- UrunModel (String): Model");
            sb.AppendLine("- UrunYil (String): Üretim yılı");
            sb.AppendLine("- UrunAdet (Integer): Stoktaki mevcut miktar");
            sb.AppendLine("- UrunMaliyet (Double): Alış fiyatı");
            sb.AppendLine("- UrunSatisFiyat (Double): Satış fiyatı");
            sb.AppendLine("- UrunDetay (String): Açıklama");
            sb.AppendLine("");

            // --- STOKLAR (KATEGORILER) ---
            sb.AppendLine("TABLO: TBL_STOKLAR (Stok Türleri / Kategoriler)");
            sb.AppendLine("- StokID (Integer): ID");
            sb.AppendLine("- StokTur (String): Kategori Adı");
            sb.AppendLine("- StokAdet (String): (Genellikle kullanılmaz, UrunAdet kullanılır)");
            sb.AppendLine("");

            // --- FIRMALAR ---
            sb.AppendLine("TABLO: TBL_FIRMALAR (Tedarikçiler/Kurumsal Firmalar)");
            sb.AppendLine("- FirmaID (Integer): Benzersiz kimlik");
            sb.AppendLine("- FirmaAd (String): Firma adı");
            sb.AppendLine("- FirmaYetkiliAdSoyad (String): Yetkili kişi");
            sb.AppendLine("- FirmaSektor (String): Sektör");
            sb.AppendLine("- FirmaTelefon1 (String): Telefon 1");
            sb.AppendLine("- FirmaTelefon2 (String): Telefon 2");
            sb.AppendLine("- FirmaTelefon3 (String): Telefon 3");
            sb.AppendLine("- FirmaMail (String): E-posta");
            sb.AppendLine("- FirmaSehir (String): İl");
            sb.AppendLine("- FirmaIlce (String): İlçe");
            sb.AppendLine("- FirmaAdres (String): Adres");
            sb.AppendLine("");

            // --- PERSONELLER ---
            sb.AppendLine("TABLO: TBL_PERSONELLER (Çalışanlar)");
            sb.AppendLine("- PersonelID (Integer): ID");
            sb.AppendLine("- PersonelAd (String): Ad");
            sb.AppendLine("- PersonelSoyad (String): Soyad");
            sb.AppendLine("- PersonelTC (String): TC Kimlik Numarası");
            sb.AppendLine("- PersonelGorev (String): Görev/Departman");
            sb.AppendLine("- PersonelMail (String): E-posta");
            sb.AppendLine("- PersonelTelefon (String): Telefon");
            sb.AppendLine("- PersonelSehir (String): İl");
            sb.AppendLine("- PersonelIlce (String): İlçe");
            sb.AppendLine("- PersonelAdres (String): Adres");
            sb.AppendLine("");

            // --- BANKALAR ---
            sb.AppendLine("TABLO: TBL_BANKALAR (Banka Hesapları)");
            sb.AppendLine("- BankaID (Integer): ID");
            sb.AppendLine("- BankaAd (String): Banka Adı");
            sb.AppendLine("- BankaSube (String): Şube");
            sb.AppendLine("- BankaIBAN (String): IBAN");
            sb.AppendLine("- BankaHesapNo (String): Hesap No");
            sb.AppendLine("- BankaYetkili (String): Yetkili");
            sb.AppendLine("- FirmaID (Integer): Hesabın bağlı olduğu firma (Varsa)");
            sb.AppendLine("");

            // --- NOTLAR ---
            sb.AppendLine("TABLO: TBL_NOTLAR (Ajanda)");
            sb.AppendLine("- NotID (Integer): ID");
            sb.AppendLine("- NotBaslik (String): Başlık");
            sb.AppendLine("- NotDetay (String): İçerik");
            sb.AppendLine("- NotTarih (String): Tarih");
            sb.AppendLine("- NotSaat (String): Saat");
            sb.AppendLine("- NotOlusturan (String): Oluşturan Kişi");
            sb.AppendLine("");

            // --- FATURABILGI ---
            sb.AppendLine("TABLO: TBL_FATURABILGI (Faturalar - Başlık)");
            sb.AppendLine("- FaturaID (Integer): ID");
            sb.AppendLine("- FaturaSeri (String): Seri");
            sb.AppendLine("- FaturaSiraNo (String): Sıra No");
            sb.AppendLine("- FaturaTarih (String): Tarih");
            sb.AppendLine("- FaturaSaat (String): Saat");
            sb.AppendLine("- FaturaAlici (String): Müşteri/Firma Adı");
            sb.AppendLine("- FaturaTeslimAlan (String): Teslim Alan");
            sb.AppendLine("- FaturaTeslimEden (String): Teslim Eden");
            sb.AppendLine("");

            // --- FATURADETAY ---
            sb.AppendLine("TABLO: TBL_FATURADETAY (Fatura Kalemleri)");
            sb.AppendLine("- FaturaUrunID (Integer): ID");
            sb.AppendLine("- UrunAd (String): Ürün Adı");
            sb.AppendLine("- Miktar (Integer): Adet");
            sb.AppendLine("- Fiyat (Double): Birim Fiyat");
            sb.AppendLine("- Tutar (Double): Satır Toplamı");
            sb.AppendLine("- FaturaID (Integer): Bağlı Fatura ID");
            sb.AppendLine("");

            // --- MUSTERI HAREKETLER ---
            sb.AppendLine("TABLO: TBL_MUSTERIHAREKETLER (Satış Hareketleri)");
            sb.AppendLine("- HareketID (Integer): ID");
            sb.AppendLine("- UrunID (Integer): Satılan Ürün (Join: TBL_URUNLER)");
            sb.AppendLine("- MusteriID (Integer): Alan Müşteri (Join: TBL_MUSTERILER)");
            sb.AppendLine("- PersonelID (Integer): Satan Personel (Join: TBL_PERSONELLER)");
            sb.AppendLine("- FaturaID (Integer): Varsa Fatura ID");
            sb.AppendLine("- Adet (Integer): Satış Adedi");
            sb.AppendLine("- Fiyat (Double): Birim Fiyat");
            sb.AppendLine("- Toplam (Double): Toplam Tutar (Adet * Fiyat)");
            sb.AppendLine("- Tarih (String): İşlem Tarihi");
            sb.AppendLine("");

            // --- GIDERLER ---
            sb.AppendLine("TABLO: TBL_GIDERLER (İşletme Giderleri)");
            sb.AppendLine("- GiderID (Integer): ID");
            sb.AppendLine("- GiderAy (String): Ay");
            sb.AppendLine("- GiderYil (String): Yıl");
            sb.AppendLine("- GiderElektrik (Double): Elektrik");
            sb.AppendLine("- GiderSu (Double): Su");
            sb.AppendLine("- GiderMaaslar (Double): Maaşlar");
            sb.AppendLine("- GiderEkstra (Double): Ekstra");
            sb.AppendLine("");

            // --- İPUÇLARI VE KURALLAR ---
            sb.AppendLine("İPUÇLARI VE JOIN KURALLARI:");
            sb.AppendLine("1. Bir müşterinin telefonunu bulmak için: `SELECT MusteriTelefon FROM TBL_MUSTERILER WHERE MusteriAd LIKE '%Ali%'`");
            sb.AppendLine("2. En çok satış yapılan ürün: `SELECT T2.UrunAd, SUM(T1.Adet) as ToplamSatis FROM TBL_MUSTERIHAREKETLER T1 JOIN TBL_URUNLER T2 ON T1.UrunID = T2.UrunID GROUP BY T2.UrunAd ORDER BY ToplamSatis DESC LIMIT 1`");
            sb.AppendLine("3. Toplam Ciro: `SELECT SUM(Toplam) FROM TBL_MUSTERIHAREKETLER`");
            sb.AppendLine("4. Personel Satış Performansı: `SELECT T2.PersonelAd, SUM(T1.Toplam) FROM TBL_MUSTERIHAREKETLER T1 JOIN TBL_PERSONELLER T2 ON T1.PersonelID = T2.PersonelID GROUP BY T2.PersonelAd`");
            sb.AppendLine("5. Bir müşterinin adresi: `SELECT MusteriAdres FROM TBL_MUSTERILER ...`");

            return sb.ToString();
        }

        public List<string> GetTableNames()
        {
            return new List<string> { 
                "TBL_MUSTERILER", "TBL_URUNLER", "TBL_STOKLAR", "TBL_FIRMALAR", 
                "TBL_PERSONELLER", "TBL_BANKALAR", "TBL_NOTLAR", "TBL_FATURABILGI", 
                "TBL_FATURADETAY", "TBL_MUSTERIHAREKETLER", "TBL_FIRMAHAREKETLER",
                "TBL_GIDERLER"
            };
        }
    }
}
