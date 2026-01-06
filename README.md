# operion

**Ticari Otomasyon Sistemi**

operion, küçük ve orta ölçekli işletmeler için tasarlanmış kapsamlı bir ticari otomasyon sistemidir. Sistem, müşteri yönetimi, stok takibi, fatura işlemleri, gider yönetimi ve raporlama gibi temel ticari işlemleri tek bir platformda birleştirir.

## Teknik Bilgiler

- **Platform:** .NET 10.0 Windows Forms
- **Veritabanı:** SQLite (Entity Framework Core 10.0)
- **Mimari:** N-tier Architecture (Clean Architecture benzeri)
- **Tasarım:** Modern UI (2026 standartlarına uygun)

## Proje Yapısı

```
operion/
├── Application/          # Uygulama katmanı (Business Logic)
│   ├── Interfaces/      # Interface tanımları
│   └── Services/        # Servis implementasyonları
├── Core/                # Çekirdek yardımcılar
│   ├── Helpers/         # Yardımcı sınıflar
│   └── Utilities/       # Utility fonksiyonları
├── Domain/              # Domain modelleri
│   └── Entities/        # Entity Framework modelleri
├── Infrastructure/      # Altyapı katmanı
│   ├── Data/           # Veritabanı context ve konfigürasyon
│   └── External/       # Harici servis entegrasyonları
├── Presentation/        # UI katmanı
│   ├── Controls/       # Özel kontroller
│   └── Forms/          # Windows Forms
├── Tests/              # Test projeleri
│   ├── Application/    # Servis testleri
│   ├── Functional/     # Fonksiyonel testler
│   ├── Integration/    # Entegrasyon testleri
│   └── Security/       # Güvenlik testleri
├── docs/               # Dokümantasyon
│   ├── ai/             # AI entegrasyon dokümantasyonu
│   └── progress/       # İlerleme raporları
└── memory-bank/        # LLM Memory Bank
```

## Özellikler

- **Müşteri Yönetimi:** Müşteri bilgileri, hareketler ve rehber yönetimi
- **Stok Yönetimi:** Ürün ve stok takibi
- **Fatura İşlemleri:** Fatura oluşturma, düzenleme ve takibi
- **Firma Yönetimi:** Firma bilgileri ve hareket takibi
- **Gider Yönetimi:** İşletme giderlerinin takibi
- **Kasa Yönetimi:** Nakit akış takibi
- **Raporlama:** Detaylı iş raporları
- **Notlar:** İş notları ve hatırlatıcılar
- **Personel Yönetimi:** Personel bilgileri ve takibi
- **Banka Yönetimi:** Banka hesapları ve işlemleri
- **AI Asistan (RAG):** Dökümanlarda arama yapabilen ve veritabanı sorgulayabilen ("Stokta ne kadar mal var?") akıllı asistan.
- **RAG Mimarisi:** Hibrit arama, Re-ranking ve Semantik Chunking özellikleri.

## Sistem Gereksinimleri

- Windows 10/11 (x64 veya ARM64)
- .NET 10.0 Runtime
- Visual Studio 2022 (geliştirme için)

## Kurulum

Detaylı kurulum talimatları için [docs/GEREKSINIMLER.md](docs/GEREKSINIMLER.md) dosyasına bakın.

## Dokümantasyon

Tüm dokümantasyon `docs/` klasöründe organize edilmiştir:

### Ana Dokümantasyon
- [GEREKSINIMLER.md](docs/GEREKSINIMLER.md) - Sistem gereksinimleri ve kurulum
- [MASTER_DOCUMENT.md](docs/MASTER_DOCUMENT.md) - Kapsamlı master dokümantasyon
- [KURALLAR.md](docs/KURALLAR.md) - Proje kuralları
- [TEST_SENARYOLARI.md](docs/TEST_SENARYOLARI.md) - Test senaryoları

### AI Entegrasyonu
- [AI README](docs/ai/AI_README.md) - AI entegrasyonu genel bakış
- [AI Kullanım Kılavuzu](docs/ai/AI_KULLANIM_KILAVUZU.md) - AI özelliklerini kullanım
- [AI Implementasyon Raporu](docs/ai/AI_IMPLEMENTASYON_RAPORU.md) - Teknik detaylar
- [AI Test Senaryoları](docs/ai/AI_TEST_SENARYOLARI.md) - AI test senaryoları

### İlerleme Raporları
- [İlerleme - AI](docs/progress/ILERLEME_AI.md)
- [İlerleme - Tasarım](docs/progress/ILERLEME_TASARIM.md)
- [İlerleme - Geliştirme](docs/progress/ILERLEME_GELISTIRME.md)
- [İlerleme - Hatalar](docs/progress/ILERLEME_HATALAR.md)

## Geliştirme

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run
```

### Test

```bash
dotnet test
```

## Lisans

Bu proje [LICENSE](LICENSE) dosyasında belirtilen lisans koşulları altında lisanslanmıştır.

## Katkıda Bulunma

Projeye katkıda bulunmak için lütfen önce [docs/KURALLAR.md](docs/KURALLAR.md) dosyasını okuyun.
