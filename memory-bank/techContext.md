# operion Teknik Bağlam

## Teknolojiler

### Framework ve Platform
- **.NET 10.0:** Ana framework
- **Windows Forms:** UI framework
- **Target Framework:** `net10.0-windows`
- **Platform:** Windows 10/11 (x64 ve ARM64)

### Veritabanı
- **SQLite:** Yerel veritabanı motoru
- **Entity Framework Core 10.0:** ORM framework
- **Microsoft.Data.Sqlite 10.0.0:** SQLite provider

### NuGet Paketleri

```xml
- Microsoft.Data.Sqlite (10.0.0)
- Microsoft.EntityFrameworkCore.Sqlite (10.0.0)
- Microsoft.EntityFrameworkCore.Design (10.0.0)
- Microsoft.EntityFrameworkCore.Tools (10.0.0)
- Newtonsoft.Json (13.0.4)
- System.Configuration.ConfigurationManager (10.0.0)
- Microsoft.VisualStudio.Services.NuGet.CredentialProvider (0.37.0)
```

## Geliştirme Ortamı

### IDE
- **Visual Studio 2022** veya **Visual Studio 2025** (Preview)
- **Windows Forms Designer:** Form tasarımı için
- **NuGet Package Manager:** Paket yönetimi

### Gerekli İş Yükleri
- .NET masaüstü geliştirme
- .NET 10 SDK
- Windows Forms geliştirme araçları

### Geliştirme Araçları
- Entity Framework Core Tools (migration için)
- NuGet Package Manager
- Windows Forms Designer

## Proje Yapılandırması

### Proje Dosyası (`operion.csproj`)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

### Konfigürasyon Dosyaları
- **App.config:** Uygulama ayarları
- **operion.csproj.user:** Kullanıcı özel ayarlar

## Veritabanı Yapısı

### Entity Framework Core Yaklaşımı
- **Code-First:** Model sınıflarından veritabanı oluşturma
- **DbContext:** `TicariOtomasyonDbContext`
- **Migrations:** Veritabanı şema değişiklikleri

### Model Sınıfları (`Models/`)
- `TblAdmin` - Admin kullanıcıları
- `TblMusteriler` - Müşteri bilgileri
- `TblFirmalar` - Firma bilgileri
- `TblUrunler` - Ürün bilgileri
- `TblStoklar` - Stok bilgileri
- `TblFaturaBilgi` - Fatura başlık bilgileri
- `TblFaturaDetay` - Fatura detay satırları
- `TblGiderler` - Gider kayıtları
- `TblPersoneller` - Personel bilgileri
- `TblBankalar` - Banka bilgileri
- `TblNotlar` - Not kayıtları
- `TblKodlar` - Kod tabloları
- `TblIller` / `TblIlceler` - İl/İlçe bilgileri
- `TblMusteriHareketler` - Müşteri hareketleri
- `TblFirmaHareketler` - Firma hareketleri

## Tasarım Sistemi

### Modern UI Framework
- **Özel Kontroller:** Windows Forms üzerine modern kontroller
- **Tema Sistemi:** Light/Dark mode desteği
- **Renk Paleti:** Microsoft Teams inspired (Modern Mavi)
- **İkonlar:** Fluent Icons (Microsoft Modern)

### Tasarım Bileşenleri
- `DesignSystem.cs` - Renk paleti ve standartlar
- `ThemeManager.cs` - Tema yönetimi
- `IconHelper.cs` - İkon yardımcıları
- `Design/Controls/` - Özel kontroller

## Bağımlılıklar ve Kısıtlamalar

### Platform Kısıtlamaları
- **Sadece Windows:** Windows Forms platform-specific
- **.NET 10:** En yeni framework versiyonu
- **SQLite:** Yerel veritabanı (sunucu gerektirmez)

### Performans Kısıtlamaları
- SQLite tek kullanıcılı veritabanı
- Büyük veri setleri için optimizasyon gerekebilir
- Windows Forms single-threaded UI

## Araç Kullanım Desenleri

### Entity Framework Core
- DbContext pattern
- LINQ sorguları
- Migration kullanımı

### Windows Forms
- Event-driven programming
- Designer-based UI
- Data binding (DataGridView, vb.)

### Konfigürasyon Yönetimi
- `App.config` ile ayar yönetimi
- `System.Configuration.ConfigurationManager` kullanımı
- **AI Ayarları:** App.config'de `appSettings` bölümünde (endpoint, API key, model, vb.)
- **SMTP Ayarları:** App.config'de `appSettings` bölümünde (host, port, SSL, credentials)
- **ENV Prefix Desteği:** Hassas bilgiler için ortam değişkeni desteği (örn: `ENV:SMTP_PASSWORD`)

## Derleme ve Dağıtım

### Build Output
- `bin/Debug/net10.0/` - Debug build
- `bin/Debug/net10.0-windows/` - Windows-specific build
- Executable: `operion.exe`

### Gerekli Dosyalar
- `operion.exe` - Ana uygulama
- `operion.dll` - Ana assembly
- `*.dll` - Bagimliliklar
- `App.config` - Konfigurasyon (AI ve SMTP ayarlari icerir)
- `.env` - API anahtarlari (GEMINI_API_KEY) - **ONEMLI: Bu dosya output dizinine kopyalanmali**
- `DB/TicariOtomasyon_SQLite.sql` - Veritabani scripti
- `logo/operion-logo.jpg` - Logo dosyasi

### Konfigürasyon Notları
- **SMTP:** App.config'de `SMTP_HOST`, `SMTP_PORT`, `SMTP_ENABLE_SSL`, `SMTP_FROM_EMAIL`, `SMTP_PASSWORD` ayarları
- **AI:** App.config'de `AI_ENDPOINT`, `AI_API_KEY`, `AI_MODEL` vb. ayarlar
- **Güvenlik:** Şifreler için `ENV:SMTP_PASSWORD` veya `ENV:OPENAI_API_KEY` formatı kullanılabilir

## AI Entegrasyonu (Opsiyonel)

### AI Servisleri
- `AiService.cs` - AI servis entegrasyonu
- `AiLogger.cs` - AI işlem logları
- `AiRateLimiter.cs` - Rate limiting
- `AiResponseParser.cs` - Yanıt parsing
- `PromptBuilder.cs` - Prompt oluşturma
- `PiiMaskingService.cs` - Kişisel bilgi maskeleme

### ARM Uyumluluğu
- `ARMCompatibilityHelper.cs` - ARM64 desteği

