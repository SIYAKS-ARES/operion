# Memory Bank - Operion Ticari Otomasyon Projesi

Bu dosya, proje taşıma süreci boyunca öğrenilen önemli bilgileri, kararları, sorunları ve çözümleri içerir.

---

## 🎯 Proje Bilgileri

### Kaynak Proje
- **Adı:** Ticari_Otomasyon
- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Veritabanı:** System.Data.SQLite 1.0.118
- **3. Taraf Kütüphaneler:** DevExpress 25.1, Newtonsoft.Json 13.0.3

### Hedef Proje
- **Adı:** operion
- **Framework:** .NET 10
- **UI:** Windows Forms (Standart kontroller)
- **Veritabanı:** Microsoft.Data.Sqlite 10.0.0 (ARM destekli)
- **ORM:** Entity Framework Core 10.0.0
- **3. Taraf Kütüphaneler:** Newtonsoft.Json 13.0.4, System.Configuration.ConfigurationManager 10.0.0
- **Durum:** ✅ Başarıyla tamamlandı, uygulama çalışıyor (modernizasyon 21/21, 2025-12-09)

### Ortam
- **Ana Bilgisayar:** MacBook Pro M3 (ARM)
- **Sanallaştırma:** Parallels (eski sürüm)
- **İşletim Sistemi:** Windows 11 ARM
- **Kısıtlama:** Nested Virtualization YOK (Docker, Hyper-V, MS SQL Server çalışmıyor)

---

## 📋 Kritik Kararlar

### 1. DevExpress Kullanımı
**Karar:** DevExpress bileşenleri şimdilik beklemeye alındı (kullanıcının DevExpress'i yok)  
**Tarih:** [[TARIH]]  
**Alternatif:** Standart Windows Forms kontrolleri kullanılacak

**Dönüşüm Tablosu:**
- `DevExpress.XtraGrid.GridControl` → `DataGridView`
- `DevExpress.XtraGrid.Views.Grid.GridView` → `DataGridView`
- `DevExpress.XtraEditors.TextEdit` → `TextBox`
- `DevExpress.XtraEditors.ComboBoxEdit` → `ComboBox`
- `DevExpress.XtraBars.BarManager` → `MenuStrip` / `ToolStrip`
- `DevExpress.XtraEditors.SimpleButton` → `Button`
- `DevExpress.XtraEditors.GroupControl` → `GroupBox`

**Kayıp Özellikler:**
- GridControl'ün gelişmiş filtering/sorting özellikleri
- XtraBars'ın modern görünümü
- Özel formatting ve styling

### 2. Veritabanı Geçişi
**Karar:** System.Data.SQLite → Microsoft.Data.Sqlite  
**Tarih:** [[TARIH]]  
**Neden:** ARM native desteği, Microsoft'un resmi paketi

**Önemli API Değişiklikleri:**
- `SQLiteConnection` → `SqliteConnection`
- `SQLiteCommand` → `SqliteCommand`
- `SQLiteDataAdapter` → **YOK** (DataTable.Load(SqliteDataReader) kullanılacak)
- `SQLiteDataReader` → `SqliteDataReader`

**Connection String Formatı:**
```csharp
"Data Source=path;Mode=ReadWrite;Cache=Shared"
```

### 3. Entity Framework Core Kullanımı
**Karar:** EF Core kullanılacak (Code First yaklaşımı)  
**Tarih:** [[TARIH]]  
**Neden:** Modern yaklaşım, OOP prensipleri, LINQ desteği

**Not:** Geçiş döneminde DataTable kullanımı devam edebilir, ileride EF Core LINQ sorgularına geçilecek.

### 4. BLOB Desteği
**Karar:** Görsel veriler (ürün resmi, personel fotoğrafı) BLOB olarak veritabanında saklanacak  
**Tarih:** [[TARIH]]  
**Eklenen Kolonlar:**
- `TBL_URUNLER.UrunResim` (BLOB/byte[])
- `TBL_PERSONELLER.PersonelFoto` (BLOB/byte[])
- `TBL_FIRMALAR.FirmaLogo` (BLOB/byte[] - opsiyonel)

### 5. ReportViewer Alternatifi
**Karar:** ReportViewer ARM uyumlu değil, alternatif çözüm gerekli  
**Tarih:** [[TARIH]]  
**Seçenekler:**
1. PDFSharp veya QuestPDF kullanarak PDF export
2. HTML/XML tabanlı rapor çözümü
3. Geçici olarak raporları devre dışı bırak

**Durum:** Henüz karar verilmedi

### 6. Configuration API
**Karar:** Henüz karar verilmedi  
**Seçenekler:**
1. `System.Configuration.ConfigurationManager` (Windows Forms için hala çalışır)
2. `Microsoft.Extensions.Configuration` (Modern yaklaşım)

---

## 🔧 Teknik Notlar

### Veritabanı Bağlantı Kodu Örneği

**ESKİ KOD (.NET Framework 4.8):**
```csharp
using System.Data.SQLite;

SQLiteConnection baglan = new SQLiteConnection($"Data Source={dbPath};Version=3;");
baglan.Open();
```

**YENİ KOD (.NET 10):**
```csharp
using Microsoft.Data.Sqlite;

var connection = new SqliteConnection($"Data Source={dbPath};Mode=ReadWrite;Cache=Shared");
connection.Open();
```

### DataAdapter Alternatifi

**ESKİ KOD:**
```csharp
SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM TBL_URUNLER", bgl.baglanti());
DataTable dt = new DataTable();
da.Fill(dt);
```

**YENİ KOD:**
```csharp
DataTable dt = new DataTable();
using var connection = new SqliteConnection(connectionString);
connection.Open();
using var command = new SqliteCommand("SELECT * FROM TBL_URUNLER", connection);
using var reader = command.ExecuteReader();
dt.Load(reader);
```

### GridControl → DataGridView Dönüşümü

**ESKİ KOD (DevExpress):**
```csharp
DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
txturunid.Text = dr["UrunID"].ToString();
```

**YENİ KOD (DataGridView):**
```csharp
if (dataGridView1.SelectedRows.Count > 0)
{
    var row = dataGridView1.SelectedRows[0];
    txturunid.Text = row.Cells["UrunID"].Value?.ToString() ?? "";
}
```

### BLOB (Image) Saklama

**Veritabanına Kaydetme:**
```csharp
byte[] imageBytes = ImageToByteArray(pictureBox1.Image);
command.Parameters.Add("@UrunResim", SqliteType.Blob).Value = imageBytes;
```

**Veritabanından Okuma:**
```csharp
if (reader["UrunResim"] != DBNull.Value)
{
    byte[] imageBytes = (byte[])reader["UrunResim"];
    pictureBox1.Image = ByteArrayToImage(imageBytes);
}
```

---

## 🐛 Bilinen Sorunlar ve Çözümleri

### Sorun 1: SQLiteDataAdapter Yok
**Açıklama:** Microsoft.Data.Sqlite'de DataAdapter sınıfı yok  
**Çözüm:** DataTable.Load(SqliteDataReader) kullan  
**Durum:** Çözüldü

### Sorun 2: DevExpress Kontrol API Farklılıkları
**Açıklama:** DevExpress kontrollerinin API'leri standart kontrollerden farklı  
**Çözüm:** Tüm formlarda DevExpress kontrolleri standart kontrollerle değiştirilecek  
**Durum:** Devam ediyor

### Sorun 3: ReportViewer ARM Uyumsuzluğu
**Açıklama:** Microsoft.ReportViewer.WinForms ARM Windows'ta çalışmıyor  
**Çözüm:** Alternatif rapor çözümü araştırılacak  
**Durum:** Bekliyor

---

## 📚 Referanslar ve Kaynaklar

### Microsoft Dokümantasyonu
- [.NET Framework'tan .NET'a geçiş](https://learn.microsoft.com/dotnet/core/porting/framework-overview)
- [Microsoft.Data.Sqlite](https://learn.microsoft.com/dotnet/standard/data/sqlite/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [Windows Forms .NET](https://learn.microsoft.com/dotnet/desktop/winforms/)

### ARM Windows 11 Uyumluluğu
- .NET 10 native ARM64 desteği sunar
- Microsoft.Data.Sqlite ARM64 native desteği var
- ReportViewer ARM uyumlu değil

### DevExpress Alternatifleri
- Standart Windows Forms kontrolleri
- Modern görünüm için Windows UI Library kullanılabilir

---

## 💡 Öneriler ve İpuçları

1. **Adım Adım Taşıma:** Her modülü ayrı ayrı taşı ve test et
2. **Yedekleme:** Her faz öncesi commit yap
3. **Test Listesi:** Her form için test senaryosu hazırla
4. **Error Logging:** Hataları logla ve kategorize et
5. **Incremental Build:** Küçük parçalar halinde derle ve test et

---

## 📝 Notlar ve Düşünceler

- Proje modüler yapıda, her form bağımsız olarak taşınabilir
- DevExpress bağımlılıkları sadece UI katmanında, business logic'te yok
- AI servisleri DevExpress bağımlılığı içermiyor, kolayca taşınabilir
- Veritabanı yapısı zaten SQLite, geçiş kolay olmalı

---

## 🔄 Güncelleme Geçmişi

### 2025-11-17 - TASARIM MODERNİZASYONU DEVAM EDİYOR 🎨
- ✅ 12/21 form modernize edildi (%57)
- ✅ Modern UI bileşenleri geliştirildi (ModernButton, ModernTextBox, ModernPanel)
- ✅ Design System ve Theme Manager eklendi
- ✅ Core formlar + Fatura modülü + Yardımcı modüller başlangıcı tamamlandı
- ✅ İlerleme: %57 (9 form kaldı)

**Modernize Edilen Formlar:**
1. FrmAdmin ✅
2. FrmAnaModul ✅
3. FrmAnaSayfa ✅
4. FrmUrunler ✅
5. FrmMusteriler ✅
6. FrmFirmalar ✅
7. FrmPersoneller ✅
8. FrmFaturalar ✅
9. FrmFaturaUrunDetay ✅
10. FrmFaturaUrunDuzenleme ✅
11. FrmHareketler ✅
12. FrmBankalar ✅

### 2025-11-16 17:20 - PROJE TAMAMLANDI ✅
- ✅ Tüm 8 faz tamamlandı
- ✅ 21 form detaylı olarak taşındı
- ✅ Derleme başarılı (0 hata)
- ✅ Uygulama başarıyla çalıştırıldı
- ✅ İlerleme: %99 (Sadece manuel testler kaldı)

### 2025-11-16 (Başlangıç)
- Memory Bank oluşturuldu
- Proje bilgileri eklendi
- Kritik kararlar dokümante edildi
- Faz 1-6 tamamlandı
- Faz 7-8 başlatıldı ve tamamlandı

---

## 🎓 Öğrenilen Dersler

### Başarılı Stratejiler
1. **Modüler Taşıma:** Form form, modül modül taşımak hataları minimize etti
2. **DevExpress Alternatifi:** Standart Windows Forms kontrolleri yeterli oldu
3. **SQLite Seçimi:** ARM uyumluluğu için mükemmel çözüm
4. **Entity Framework Core:** Modern ORM yaklaşımı projeyi güçlendirdi
5. **İlerleme Takibi:** ILERLEME.md ve MEMORY_BANK.md sayesinde hiçbir şey gözden kaçmadı

### Karşılaşılan Zorluklar ve Çözümler
1. **DevExpress → Standart Kontroller:** Tüm formlar başarıyla dönüştürüldü
2. **SqliteDataAdapter Eksikliği:** DataTable.Load(reader) ile çözüldü
3. **ARM Uyumluluk:** ReportViewer yerine HTML raporlar oluşturuldu
4. **Nested Virtualization:** SQLite kullanarak Docker/Hyper-V ihtiyacı ortadan kalktı

### Teknik İyileştirmeler
- `AppContext.BaseDirectory` ile platform bağımsız path yönetimi
- Static `DatabaseService` metodları ile temiz mimari
- Nullable reference types ile daha güvenli kod
- .NET 10 ile modern özellikler

