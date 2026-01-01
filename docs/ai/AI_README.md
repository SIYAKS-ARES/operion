# 🤖 AI Entegrasyonu - Ticari Otomasyon Sistemi

## 📌 Genel Bakış

Bu proje, Ticari Otomasyon sistemine AI (Yapay Zeka) yetenekleri ekleyen kapsamlı bir entegrasyon çözümüdür. OpenAI/Azure OpenAI API'lerini kullanarak **Rapor Özetleme** ve **E-posta Asistanı** özellikleri sunar.

### ✨ Temel Özellikler

#### 1. 📊 Rapor Özetleme
- Uzun raporları Türkçe özetler (2-5 madde)
- Aksiyon önerileri sunar (3-7 madde)
- Firmalar, Müşteriler, Giderler ve Personel raporlarını destekler
- PII (Kişisel Veri) maskeleme ile güvenli işlem

#### 2. ✉️ E-posta Asistanı
- Profesyonel e-posta şablonları oluşturur
- 5 farklı senaryo (Teklif, Teşekkür, Ödeme Hatırlatma, vb.)
- 4 farklı ton seçeneği (Resmi, Nötr, Samimi, Acil)
- 3 uzunluk seçeneği (Kısa, Orta, Uzun)
- 3 alternatif konu satırı önerir

## 🏗️ Mimari

### Katmanlar
```
┌─────────────────────────────────────┐
│         UI Layer (WinForms)         │
│  - FrmRaporlar (Rapor Özeti)        │
│  - FrmMail (E-posta Asistanı)       │
├─────────────────────────────────────┤
│         Business Logic Layer        │
│  - AiService (API çağrıları)        │
│  - PromptBuilder (Prompt oluşturma) │
│  - AiResponseParser (Parse etme)    │
├─────────────────────────────────────┤
│         Infrastructure Layer        │
│  - PiiMaskingService (Güvenlik)     │
│  - AiRateLimiter (Hız kontrolü)     │
│  - AiLogger (Loglama)               │
├─────────────────────────────────────┤
│         External Services           │
│  - OpenAI API / Azure OpenAI        │
└─────────────────────────────────────┘
```

### Temel Sınıflar

| Sınıf | Sorumluluk | Konum |
|-------|-----------|-------|
| `AiService` | AI API çağrıları, retry, timeout | `Classes/AiService.cs` |
| `PromptBuilder` | Senaryo bazlı prompt şablonları | `Classes/PromptBuilder.cs` |
| `AiResponseParser` | AI yanıtlarını parse etme | `Classes/AiResponseParser.cs` |
| `PiiMaskingService` | Kişisel veri maskeleme | `Classes/PiiMaskingService.cs` |
| `AiRateLimiter` | Hız sınırlama (rate limiting) | `Classes/AiRateLimiter.cs` |
| `AiLogger` | AI işlem loglama | `Classes/AiLogger.cs` |

## 🚀 Hızlı Başlangıç

### 1. Gereksinimler
- .NET Framework 4.5.2+
- DevExpress 18.1+
- Newtonsoft.Json 13.0.3
- OpenAI API anahtarı

### 2. NuGet Paketlerini Yükleyin
```bash
Install-Package Newtonsoft.Json -Version 13.0.3
```

### 3. API Anahtarını Ayarlayın

**Önerilen Yöntem (Güvenli):**
```powershell
# Windows çevre değişkeni oluşturun
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-your-key-here", "User")
```

**App.config:**
```xml
<add key="AI_API_KEY" value="ENV:OPENAI_API_KEY" />
```

### 4. Özellikleri Aktifleştirin
```xml
<add key="FEATURE_AI_REPORT_SUMMARY" value="true" />
<add key="FEATURE_AI_EMAIL_ASSISTANT" value="true" />
```

### 5. Uygulamayı Çalıştırın
- **Raporlar** → **AI Özeti** sekmesi → **Özet Üret**
- **Mail** → **AI E-posta Asistanı** → **Şablon Öner**

## 📁 Dosya Yapısı

```
ticari-otomasyon/
├── Classes/
│   ├── AiService.cs                 # AI API servisi
│   ├── PromptBuilder.cs             # Prompt şablonları
│   ├── AiResponseParser.cs          # Yanıt parser
│   ├── PiiMaskingService.cs         # PII maskeleme
│   ├── AiRateLimiter.cs             # Rate limiting
│   ├── AiLogger.cs                  # Loglama
│   ├── FrmRaporlar.cs               # Rapor formu (AI özeti eklendi)
│   └── FrmMail.cs                   # Mail formu (AI asistan eklendi)
├── App.config                        # AI yapılandırmaları
├── packages.config                   # NuGet paketleri
├── AI_KULLANIM_KILAVUZU.md          # Kullanım kılavuzu
├── AI_TEST_SENARYOLARI.md           # Test senaryoları
├── AI_README.md                      # Bu dosya (mevcut dosya)
└── ai-entegrasyonu.md               # Orijinal plan
```

## ⚙️ Yapılandırma

### Temel Ayarlar
```xml
<appSettings>
  <!-- AI Sağlayıcı -->
  <add key="AI_PROVIDER" value="OpenAI" />
  <add key="AI_ENDPOINT" value="https://api.openai.com/v1/chat/completions" />
  <add key="AI_MODEL" value="gpt-4o-mini" />
  <add key="AI_API_KEY" value="ENV:OPENAI_API_KEY" />
  
  <!-- Performans -->
  <add key="AI_TIMEOUT_MS" value="30000" />
  <add key="AI_RETRY_COUNT" value="3" />
  <add key="AI_MAX_TOKENS" value="2000" />
  
  <!-- Rate Limiting -->
  <add key="AI_RATE_LIMIT_GLOBAL" value="30" />
  <add key="AI_RATE_LIMIT_PER_USER" value="10" />
  
  <!-- Feature Flags -->
  <add key="FEATURE_AI_REPORT_SUMMARY" value="true" />
  <add key="FEATURE_AI_EMAIL_ASSISTANT" value="true" />
</appSettings>
```

### Azure OpenAI için
```xml
<add key="AI_PROVIDER" value="AzureOpenAI" />
<add key="AI_ENDPOINT" value="https://your-resource.openai.azure.com/..." />
<add key="AI_API_KEY" value="ENV:AZURE_OPENAI_KEY" />
```

## 🔒 Güvenlik

### PII (Kişisel Veri) Koruması
Sistem otomatik olarak hassas verileri maskeler:

| Veri Türü | Örnek | Maskelenmiş |
|-----------|-------|-------------|
| E-posta | `ali@firma.com` | `[EMAIL]` |
| Telefon | `0532 123 45 67` | `[TELEFON]` |
| TC Kimlik | `12345678901` | `[KIMLIK_NO]` |
| IBAN | `TR33 0006 1005...` | `[IBAN]` |
| Kişi Adı | `Ahmet Yılmaz` | `[KİŞİ_ADI]` |

### Veri Minimizasyonu
- Maksimum 50 satır rapor verisi gönderilir
- Uzun metinler 4-8 KB ile sınırlanır
- Sadece gerekli sütunlar işlenir

### API Anahtar Güvenliği
- ✅ Çevre değişkeni kullanın (`ENV:` prefix)
- ✅ Production'da Azure Key Vault kullanın
- ❌ App.config'e doğrudan yazmayın

## 📊 Kullanım Örnekleri

### Rapor Özetleme
```csharp
// Otomatik çağrılır (FrmRaporlar'da)
var context = new ReportSummaryContext
{
    ReportType = "Firmalar Raporu",
    StartDate = DateTime.Now.AddMonths(-1),
    EndDate = DateTime.Now,
    Data = maskedReportData
};

var prompt = _promptBuilder.BuildReportSummaryPrompt(context);
var response = await _aiService.SummarizeAsync(prompt);
var parsed = _aiParser.ParseSummaryAndActions(response.Content);

// Sonuç: 2-5 özet maddesi + 3-7 aksiyon maddesi
```

### E-posta Şablonu
```csharp
// Otomatik çağrılır (FrmMail'de)
var context = new EmailTemplateContext
{
    Scenario = EmailScenario.Teklif,
    Tone = EmailTone.Resmi,
    Length = EmailLength.Orta,
    CustomerReference = "MUSTERI_001"
};

var prompt = _promptBuilder.BuildEmailTemplatePrompt(context);
var response = await _aiService.GenerateEmailAsync(prompt);
var parsed = _aiParser.ParseEmailParts(response.Content);

// Sonuç: 3 konu satırı + e-posta gövdesi
```

## 🧪 Test

### Birim Testleri
```csharp
[Test]
public void MaskText_EmailAddress_ShouldMask()
{
    var service = new PiiMaskingService();
    var result = service.MaskText("İletişim: test@example.com");
    
    Assert.That(result, Does.Contain("[EMAIL]"));
}
```

### Manuel Testler
Test senaryolarını görmek için:
📄 [AI_TEST_SENARYOLARI.md](AI_TEST_SENARYOLARI.md) (aynı klasörde)

## 📈 Performans

### Hedefler
- ⚡ Rapor özeti: < 5 saniye (ortalama)
- ⚡ E-posta şablonu: < 3 saniye (ortalama)
- 💾 Bellek kullanımı: < 50 MB artış
- 🔄 Rate limit: 30 istek/dakika (global)

### Optimizasyon
- Async/await kullanımı
- Progress bar ile UX iyileştirmesi
- Cache mekanizması (15 dakika)
- Veri minimizasyonu

## 📝 Loglama

### Log Dosyaları
```
Logs/AI/
├── ai_log_20251013.log          # İşlem logları
├── ai_log_20251014.log
└── telemetry_20251013.log       # Telemetri verileri
```

### Log İçeriği
```
[2025-10-13 14:30:25] AI İstek Logu
Tip: RaporOzet
Prompt Uzunluğu: 2450 karakter
Süre: 3250.50 ms
Başarılı: Evet
--------------------------------------------------
[2025-10-13 14:30:25] AI Yanıt Logu
Tip: RaporOzet
Provider: OpenAI
Prompt Token: 615
Completion Token: 185
Toplam Token: 800
Süre: 3250.50 ms
--------------------------------------------------
```

## 🐛 Sorun Giderme

### Sık Karşılaşılan Hatalar

#### "AI servisi yapılandırılmamış"
**Çözüm:**
```powershell
# Çevre değişkenini kontrol edin
[Environment]::GetEnvironmentVariable("OPENAI_API_KEY", "User")

# Yoksa oluşturun
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-...", "User")
```

#### "Çok fazla istek gönderildi"
**Çözüm:**
- Birkaç saniye bekleyin
- Rate limit ayarlarını artırın (App.config)

#### Timeout Hatası
**Çözüm:**
```xml
<add key="AI_TIMEOUT_MS" value="60000" /> <!-- 60 saniye -->
```

## 📚 Dokümantasyon

- 📘 [Kullanım Kılavuzu](AI_KULLANIM_KILAVUZU.md) - Detaylı kullanım bilgileri (aynı klasörde)
- 📗 [Test Senaryoları](AI_TEST_SENARYOLARI.md) - Test prosedürleri (aynı klasörde)
- 📙 [Orijinal Plan](ai-entegrasyonu.md) - İlk planlama dokümanı

## 🔄 Versiyon Geçmişi

### v1.0.0 (2025-10-13)
- ✨ İlk sürüm yayınlandı
- ✅ Rapor özetleme özelliği
- ✅ E-posta asistanı özelliği
- ✅ PII maskeleme ve güvenlik
- ✅ Rate limiting ve loglama
- ✅ OpenAI/Azure OpenAI desteği

## 🛣️ Yol Haritası

### v1.1 (Planlanan)
- [ ] Fatura özetleme desteği
- [ ] Çoklu dil desteği (İngilizce)
- [ ] Dashboard'da AI insights
- [ ] Otomatik e-posta gönderimi

### v1.2 (Planlanan)
- [ ] Özel model fine-tuning desteği
- [ ] Voice-to-text (sesli rapor notu)
- [ ] Sentiment analizi (müşteri geri bildirimleri)

## 🤝 Katkıda Bulunma

AI entegrasyonunu geliştirmek için:
1. Öneri ve hata bildirimleri için issue açın
2. Yeni özellik için pull request gönderin
3. Testleri eksiksiz yazın
4. Dokümantasyonu güncelleyin

## 📄 Lisans

Bu proje [Proje Lisansı] altında lisanslanmıştır.

## 👨‍💻 Geliştirici Notları

### Yeni Özellik Ekleme
1. `PromptBuilder`'da yeni prompt şablonu oluşturun
2. `AiResponseParser`'a parse fonksiyonu ekleyin
3. UI formunda kullanıcı arayüzü tasarlayın
4. Test senaryolarını yazın
5. Dokümantasyonu güncelleyin

### Örnek: Yeni Senaryo Ekleme (E-posta)
```csharp
// 1. Enum'a ekle (PromptBuilder.cs)
public enum EmailScenario 
{ 
    // ...
    YeniSenaryo 
}

// 2. Prompt şablonu ekle
private string GetScenarioDescription(EmailScenario scenario)
{
    switch (scenario)
    {
        // ...
        case EmailScenario.YeniSenaryo:
            return "Yeni Senaryo Açıklaması";
    }
}

// 3. UI'da seçenek ekle (FrmMail.cs)
cmbScenario.Properties.Items.Add("Yeni Senaryo");
```

## 📞 Destek

- 📧 E-posta: [destek@firma.com]
- 📚 Wiki: [Wiki linki]
- 💬 Slack: [#ai-support]

---

**⚠️ Önemli Not:** Bu özellik AI teknolojisi kullanır ve %100 doğruluk garanti edilemez. Oluşturulan içeriği mutlaka gözden geçirin ve gerektiğinde düzenleyin.

**🎉 Teşekkürler!** AI entegrasyonunu kullandığınız için teşekkür ederiz. İyi çalışmalar!

