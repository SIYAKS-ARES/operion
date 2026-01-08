# Operion Release Guide

Bu döküman, **operion** uygulamasının **v1.0.0+1** sürümüyle standalone (bağımsız) bir `.exe` olarak nasıl yayınlanacağını açıklar.

## 1. Versiyonlama

Yayın öncesi versiyon numarasının `Properties/AssemblyInfo.cs` dosyasında doğru ayarlandığından emin olun:

```csharp
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0.0+1")]
```

## 2. Yayınlama (Publish) Komutu

Uygulamayı tek bir `.exe` dosyası olarak paketlemek için aşağıdaki komutu terminalde çalıştırın. Bu komut, uygulamanın çalışması için gereken tüm .NET bağımlılıklarını içine gömer (`SelfContained`), böylece hedef bilgisayarda .NET yüklü olmasına gerek kalmaz.

### Windows x64 (Genel Kullanım İçin)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:DebugType=None /p:ProductName="operion"
```

### Parametrelerin Anlamı:
- `-c Release`: Release modunda derler (optimize edilmiş kod).
- `-r win-x64`: Windows 64-bit işletim sistemi için hedefler.
- `--self-contained true`: .NET Runtime'ı uygulamanın içine gömer. Hedef makinede kurulum gerektirmez.
- `-p:PublishSingleFile=true`: Tüm dosyaları tek bir `.exe` içinde birleştirir.
- `-p:IncludeNativeLibrariesForSelfExtract=true`: Native kütüphaneleri (SQLite vb.) exe içine dahil eder.
- `-p:DebugType=None`: Debug sembollerini (.pdb) hariç tutar, dosya boyutunu küçültür.

## 3. Çıktı Dosyaları

Komut başarıyla tamamlandığında, `operion.exe` dosyası şu dizinde oluşacaktır:

`bin\Release\net10.0-windows\win-x64\publish\`

Bu klasörün içeriği dağıtım için hazırdır. Klasör şunları içermelidir:
1. `operion.exe` (Ana uygulama)
2. `Data\` klasörü (Veritabanı başlangıç dosyaları varsa)
3. `App.config` (Konfigürasyon dosyası - exe ile birleşmiş olabilir veya yanında durabilir, `.config` olarak)
4. `.env` (API anahtarları için - **DİKKAT:** Müşteriye verirken bu dosyayı temizlediğinizden veya şifrelediğinizden emin olun)

> [!NOTE]
> Veitabanı kurulum dosyası (`TicariOtomasyon_SQLite.sql`) artık **gömülü (embedded)** olduğu için dışarıda olmasına gerek yoktur. `operion.exe` ilk açılışta veritabanı yoksa otomatik kurar.

## 4. Test Etme

Oluşturulan `operion.exe` dosyasını:
1. Başka bir klasöre kopyalayın.
2. Çift tıklayarak çalıştırın.
3. Uygulamanın sorunsuz açıldığını doğrulayın.
