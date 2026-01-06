# operion Proje Gereksinimleri

**Proje:** operion (Ticari Otomasyon)  
**Platform:** .NET 10 Windows Forms  
**Son Güncelleme:** 2025-11-16

---

## Sistem Gereksinimleri

### İşletim Sistemi
- **Windows 11** (ARM64 veya x64)
- **Windows 10** (x64) - Minimum sürüm 1903

### Donanım
- **RAM:** Minimum 4 GB (Önerilen: 8 GB)
- **Disk Alanı:** Minimum 500 MB boş alan
- **İşlemci:** ARM64 veya x64 uyumlu

---

## Geliştirme Ortamı Gereksinimleri

### Visual Studio
- **Visual Studio 2022** veya daha yeni (Community, Professional veya Enterprise)
- **Visual Studio 2025** (Preview) - Önerilen

### Visual Studio İş Yükleri
Aşağıdaki iş yüklerinin yüklü olması gerekir:

1. **.NET masaüstü geliştirme**
   - .NET 10 SDK
   - Windows Forms geliştirme araçları

2. **.NET çoklu platform geliştirme** (Opsiyonel)
   - .NET CLI araçları

### Visual Studio Bileşenleri
- .NET 10.0 SDK
- Windows Forms Designer
- NuGet Paket Yöneticisi

---

## .NET 10 SDK Kurulumu

### Otomatik Kurulum (Önerilen)
Visual Studio Installer üzerinden:
1. Visual Studio Installer'ı açın
2. "Değiştir" butonuna tıklayın
3. ".NET masaüstü geliştirme" iş yükünü seçin
4. ".NET 10.0 SDK" bileşeninin seçili olduğundan emin olun
5. "Değiştir" butonuna tıklayın

### Manuel Kurulum
.NET 10 SDK'yı doğrudan indirmek için:
- [.NET 10 SDK İndirme Sayfası](https://dotnet.microsoft.com/download/dotnet/10.0)

### Komut Satırı ile Kurulum (winget)
```powershell
winget install Microsoft.DotNet.SDK.10
```

---

## NuGet Paketleri

Proje aşağıdaki NuGet paketlerini kullanır (otomatik olarak yüklenir):

- **Microsoft.Data.Sqlite** (10.0.0)
- **Microsoft.EntityFrameworkCore.Sqlite** (10.0.0)
- **Microsoft.EntityFrameworkCore.Design** (10.0.0)
- **Microsoft.EntityFrameworkCore.Tools** (10.0.0)
- **Newtonsoft.Json** (13.0.4)
- **System.Configuration.ConfigurationManager** (10.0.0)

### Paketleri Manuel Yükleme
```powershell
cd operion
dotnet restore
```

---

## Veritabanı Gereksinimleri

### SQLite
- **Microsoft.Data.Sqlite** NuGet paketi ile birlikte gelir
- Ekstra kurulum gerekmez
- ARM64 Windows 11'de tam desteklenir

### Veritabanı Dosyası
- Veritabanı otomatik olarak oluşturulur
- Konum: `bin\Debug\net10.0-windows\Data\TicariOtomasyon.db`
- İlk çalıştırmada SQL script'i otomatik olarak çalıştırılır

---

## Visual Studio Ayarları

### Proje Özellikleri Kontrolü
1. Solution Explorer'da `operion` projesine sağ tıklayın
2. "Properties" seçeneğini seçin
3. Aşağıdaki ayarları kontrol edin:

**Application:**
- Target framework: `net10.0-windows`
- Output type: `Windows Application`

**Build:**
- Platform target: `Any CPU` (ARM64 için otomatik algılanır)
- Prefer 32-bit: **KAPALI**

### NuGet Paket Kaynağı
1. Tools → NuGet Package Manager → Package Manager Settings
2. Package Sources bölümünde "nuget.org" kaynağının aktif olduğundan emin olun

---

## Geliştirme Ortamı Kontrolü

### .NET SDK Versiyonunu Kontrol Etme
```powershell
dotnet --version
```
Beklenen çıktı: `10.0.xxx` veya daha yeni

### Yüklü .NET SDK'ları Listeleme
```powershell
dotnet --list-sdks
```

### Proje Derleme Testi
```powershell
cd operion
dotnet build
```

### Proje Çalıştırma Testi
```powershell
cd operion
dotnet run
```

---

## Sorun Giderme

### "no such table: TBL_ADMIN" Hatası
**Çözüm:**
1. Veritabanı dosyasını silin: `bin\Debug\net10.0-windows\Data\TicariOtomasyon.db`
2. Uygulamayı yeniden başlatın
3. Tablolar otomatik olarak oluşturulacaktır

### .NET 10 SDK Bulunamıyor
**Çözüm:**
1. Visual Studio Installer'ı açın
2. "Değiştir" → ".NET masaüstü geliştirme" → ".NET 10.0 SDK" seçin
3. Kurulumu tamamlayın
4. Visual Studio'yu yeniden başlatın

### NuGet Paketleri Yüklenmiyor
**Çözüm:**
```powershell
cd operion
dotnet nuget locals all --clear
dotnet restore
```

### ARM64 Uyumluluk Sorunları
**Not:** Microsoft.Data.Sqlite ARM64'ü tam destekler. Sorun yaşıyorsanız:
1. Projeyi temizleyin: `dotnet clean`
2. Yeniden derleyin: `dotnet build`
3. Uygulamayı çalıştırın: `dotnet run`

---

## Gerekli Bileşenler Özeti

### Zorunlu
- ✅ Visual Studio 2022 veya daha yeni
- ✅ .NET 10.0 SDK
- ✅ Windows Forms geliştirme araçları
- ✅ NuGet Paket Yöneticisi

### Opsiyonel (Otomatik Yüklenir)
- ✅ Microsoft.Data.Sqlite (NuGet paketi)
- ✅ Entity Framework Core (NuGet paketi)

---

## Kurulum Kontrol Listesi

- [ ] Visual Studio 2022+ yüklü
- [ ] .NET 10.0 SDK yüklü (`dotnet --version` ile kontrol)
- [ ] Windows Forms geliştirme iş yükü yüklü
- [ ] Proje başarıyla derleniyor (`dotnet build`)
- [ ] NuGet paketleri yüklü (`dotnet restore`)
- [ ] Uygulama çalışıyor (`dotnet run`)

---

## Ek Kaynaklar

- [.NET 10 Dokümantasyonu](https://docs.microsoft.com/dotnet/)
- [Microsoft.Data.Sqlite Dokümantasyonu](https://docs.microsoft.com/dotnet/standard/data/sqlite/)
- [Windows Forms Dokümantasyonu](https://docs.microsoft.com/dotnet/desktop/winforms/)

---

**Not:** Bu gereksinimler ARM Windows 11 için optimize edilmiştir. x64 Windows sistemlerde de çalışır.

