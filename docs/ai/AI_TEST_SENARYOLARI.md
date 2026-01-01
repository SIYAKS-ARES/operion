# AI Entegrasyonu Test Senaryoları

## 📋 Test Kategorileri
1. [Birim Testleri](#birim-testleri)
2. [Entegrasyon Testleri](#entegrasyon-testleri)
3. [Fonksiyonel Testler](#fonksiyonel-testler)
4. [Güvenlik Testleri](#güvenlik-testleri)
5. [Performans Testleri](#performans-testleri)
6. [Kullanıcı Kabul Testleri (UAT)](#kullanıcı-kabul-testleri)

---

## 🧪 Birim Testleri

### 1. PII Maskeleme Testleri

#### Test 1.1: E-posta Maskeleme
```
Girdi: "Müşteri: ali.yilmaz@gmail.com ile iletişime geçildi"
Beklenen: "Müşteri: [EMAIL] ile iletişime geçildi"
Durum: ✅ Başarılı / ❌ Başarısız
```

#### Test 1.2: Telefon Maskeleme
```
Girdi: "İletişim: 0532 123 45 67"
Beklenen: "İletişim: [TELEFON]"
Durum: ✅ Başarılı / ❌ Başarısız
```

#### Test 1.3: TC Kimlik Maskeleme
```
Girdi: "TC No: 12345678901"
Beklenen: "TC No: [KIMLIK_NO]"
Durum: ✅ Başarılı / ❌ Başarısız
```

#### Test 1.4: IBAN Maskeleme
```
Girdi: "IBAN: TR33 0006 1005 1978 6457 8413 26"
Beklenen: "IBAN: [IBAN]"
Durum: ✅ Başarılı / ❌ Başarısız
```

### 2. Prompt Builder Testleri

#### Test 2.1: Rapor Özeti Prompt Oluşturma
```csharp
var context = new ReportSummaryContext
{
    ReportType = "Firmalar Raporu",
    StartDate = new DateTime(2025, 09, 01),
    EndDate = new DateTime(2025, 10, 01),
    Data = "Test rapor verisi..."
};

var prompt = new PromptBuilder().BuildReportSummaryPrompt(context);

Kontrol:
- Prompt Türkçe direktif içeriyor mu? ✅
- Rapor türü ve tarih aralığı doğru mu? ✅
- Çıktı formatı belirtilmiş mi? ✅
```

#### Test 2.2: E-posta Şablonu Prompt Oluşturma
```csharp
var context = new EmailTemplateContext
{
    Scenario = EmailScenario.Teklif,
    Tone = EmailTone.Resmi,
    Length = EmailLength.Orta,
    CustomerReference = "MUSTERI_001"
};

var prompt = new PromptBuilder().BuildEmailTemplatePrompt(context);

Kontrol:
- Senaryo açıklaması var mı? ✅
- Ton ve uzunluk belirtilmiş mi? ✅
- Konu satırı formatı isteniyor mu? ✅
```

### 3. Response Parser Testleri

#### Test 3.1: Rapor Özeti Parse Etme
```csharp
var aiResponse = @"
## ÖZET:
• Toplam 45 kayıt bulunmaktadır
• Ciroda %15 artış var

## AKSİYON:
1. Pasif firmalarla görüşme
2. Pazarlama artırılmalı
";

var parsed = new AiResponseParser().ParseSummaryAndActions(aiResponse);

Kontrol:
- parsed.ParseSuccess == true ✅
- parsed.SummaryPoints.Count == 2 ✅
- parsed.ActionItems.Count == 2 ✅
```

#### Test 3.2: E-posta Parse Etme
```csharp
var aiResponse = @"
### Konu Satırları:
1. Teklif Sunumu
2. Özel İndirim Fırsatı
3. Hızlı Yanıt Gerekli

### E-posta Gövdesi:
Sayın Müşterimiz,
...
";

var parsed = new AiResponseParser().ParseEmailParts(aiResponse);

Kontrol:
- parsed.ParseSuccess == true ✅
- parsed.SubjectLines.Count == 3 ✅
- parsed.EmailBody != null ✅
```

### 4. Rate Limiter Testleri

#### Test 4.1: Global Limit Kontrolü
```csharp
var limiter = new AiRateLimiter();
// Global limit: 30/dakika

for (int i = 0; i < 30; i++)
{
    Assert.IsTrue(limiter.CanMakeRequest());
    limiter.RecordRequest();
}

Assert.IsFalse(limiter.CanMakeRequest()); // 31. istek red edilmeli
```

#### Test 4.2: Kullanıcı Bazlı Limit
```csharp
var limiter = new AiRateLimiter();
// Kullanıcı limit: 10/dakika

for (int i = 0; i < 10; i++)
{
    Assert.IsTrue(limiter.CanMakeRequest("user123"));
    limiter.RecordRequest("user123");
}

Assert.IsFalse(limiter.CanMakeRequest("user123")); // 11. istek red
Assert.IsTrue(limiter.CanMakeRequest("user456")); // Farklı kullanıcı OK
```

---

## 🔗 Entegrasyon Testleri

### 1. AI Service Mock Testleri

#### Test 1.1: Başarılı API Çağrısı (Mock)
```csharp
// Mock AI response
var mockResponse = new AiResponse
{
    Content = "Test özet içeriği...",
    TotalTokens = 150,
    Provider = "OpenAI"
};

// Test
var service = new AiService();
var result = await service.SummarizeAsync("Test prompt");

Kontrol:
- result != null ✅
- result.Content içerik var mı? ✅
- result.TotalTokens > 0 ✅
```

#### Test 1.2: Timeout Senaryosu
```csharp
// App.config: AI_TIMEOUT_MS = 1000 (1 saniye)
var service = new AiService();

try
{
    var result = await service.SummarizeAsync("çok uzun prompt...");
    Assert.Fail("Timeout exception bekleniyor");
}
catch (TaskCanceledException)
{
    // Beklenen davranış ✅
}
```

#### Test 1.3: Retry Mekanizması
```csharp
// App.config: AI_RETRY_COUNT = 3
// İlk 2 çağrı başarısız, 3. başarılı olacak (mock)

var service = new AiService();
var result = await service.SummarizeAsync("test");

Kontrol:
- 3 deneme yapıldı mı? ✅
- Sonuç başarılı mı? ✅
```

### 2. Database Entegrasyonu

#### Test 2.1: Rapor Verisi Çekme
```csharp
// FrmRaporlar'dan gerçek veri
var reportData = DboTicariOtomasyonDataSet.TBL_FIRMALAR;

Kontrol:
- reportData != null ✅
- reportData.Rows.Count > 0 ✅
- Sütunlar doğru mu? ✅
```

---

## ✅ Fonksiyonel Testler

### 1. FrmRaporlar - AI Özeti

#### Test 1.1: Özet Üretme (Happy Path)
**Adımlar:**
1. FrmRaporlar'ı aç
2. "Firmalar Raporları" sekmesini seç
3. "AI Özeti" sekmesine geç
4. "Özet Üret" butonuna tıkla

**Beklenen Sonuç:**
- ✅ Progress bar gösterilir
- ✅ 3-10 saniye içinde özet oluşturulur
- ✅ Özet maddeleri gösterilir (2-5 madde)
- ✅ Aksiyon maddeleri gösterilir (3-7 madde)
- ✅ "AI Özeti" sekmesi otomatik açılır
- ✅ Status mesajı: "Özet başarıyla oluşturuldu (X saniye - Y token)"

#### Test 1.2: Boş Rapor Senaryosu
**Adımlar:**
1. Veri olmayan bir rapor seç
2. "Özet Üret"

**Beklenen Sonuç:**
- ✅ Uyarı mesajı: "Rapor verisi bulunamadı"
- ❌ AI çağrısı yapılmaz

#### Test 1.3: Rate Limit Aşımı
**Adımlar:**
1. Hızlıca 10+ kez "Özet Üret" tıkla

**Beklenen Sonuç:**
- ✅ "Çok fazla istek gönderildi. Lütfen X saniye bekleyin" mesajı
- ✅ Bekle düğmesi devre dışı

#### Test 1.4: İnternet Yok Senaryosu
**Adımlar:**
1. İnternet bağlantısını kes
2. "Özet Üret"

**Beklenen Sonuç:**
- ✅ Hata mesajı gösterilir
- ✅ Retry 3 kez dener
- ✅ Sonuçta kullanıcıya anlamlı hata mesajı

#### Test 1.5: Panoya Kopyalama
**Adımlar:**
1. Özet üret
2. "Panoya Kopyala" (Özet)
3. "Panoya Kopyala" (Aksiyon)

**Beklenen Sonuç:**
- ✅ Clipboard'a kopyalanır
- ✅ Başarı mesajı gösterilir
- ✅ Notepad'e yapıştırılabilir

### 2. FrmMail - E-posta Asistanı

#### Test 2.1: Şablon Oluşturma (Happy Path)
**Adımlar:**
1. FrmMail'i aç
2. E-posta adresi gir: test@example.com
3. Senaryo: Teklif, Ton: Resmi, Uzunluk: Orta
4. "Şablon Öner"

**Beklenen Sonuç:**
- ✅ 3 konu satırı önerisi
- ✅ E-posta gövdesi önizlemede
- ✅ "Gövdeye Aktar" aktif olur
- ✅ "Yeniden Üret" aktif olur

#### Test 2.2: Gövdeye Aktarma
**Adımlar:**
1. Şablon oluştur
2. Konu satırı seç (2. seçenek)
3. "Gövdeye Aktar"

**Beklenen Sonuç:**
- ✅ Seçili konu "Konu" alanına yazılır
- ✅ Gövde "Mesaj" alanına yazılır
- ✅ Başarı mesajı gösterilir
- ✅ Manuel düzenleme yapılabilir

#### Test 2.3: Yeniden Üretme
**Adımlar:**
1. Şablon oluştur
2. "Yeniden Üret"

**Beklenen Sonuç:**
- ✅ Yeni bir şablon oluşturulur
- ✅ Farklı konu satırları gelir (çoğunlukla)
- ✅ Farklı gövde metni gelir

#### Test 2.4: Farklı Senaryo Testleri
**Test Edilecek Kombinasyonlar:**
- Teklif + Resmi + Kısa ✅
- Teşekkür + Samimi + Orta ✅
- Ödeme Hatırlatma + Acil + Kısa ✅
- Teslimat Bilgi + Nötr + Uzun ✅
- Genel Yanıt + Resmi + Orta ✅

**Her biri için kontrol:**
- Ton'a uygun dil kullanılıyor mu?
- Uzunluk beklentiye uygun mu?
- Senaryo içeriği doğru mu?

---

## 🔒 Güvenlik Testleri

### 1. PII Koruma Testleri

#### Test 1.1: E-posta Maskeleme (Canlı Test)
```
Girdi Rapor: "Müşteri: ahmet.yilmaz@firma.com, Tel: 0532 123 45 67"
AI'a Gönderilen: "[EMAIL] maskelenmiş olmalı, [TELEFON] maskelenmiş olmalı"
Kontrol: AI yanıtında gerçek e-posta/telefon YOK ✅
```

#### Test 1.2: Kimlik Bilgisi Koruması
```
Girdi: "TC: 12345678901, IBAN: TR33 0006 1005 1978 6457 8413 26"
AI'a Gönderilen: "[KIMLIK_NO] ve [IBAN] maskelenmiş olmalı"
Log Kontrolü: Logda gerçek TC/IBAN YOK ✅
```

### 2. API Anahtar Güvenliği

#### Test 2.1: Çevre Değişkeni Okuma
```csharp
// App.config: <add key="AI_API_KEY" value="ENV:OPENAI_API_KEY" />
var service = new AiService();

Kontrol:
- API anahtarı çevre değişkeninden okunuyor mu? ✅
- Hatalı varsa uyarı veriliyor mu? ✅
```

#### Test 2.2: Log Güvenliği
```
Senaryo: AI çağrısı yap ve log dosyasını kontrol et

Log Kontrolü:
- API anahtarı logda YOK ✅
- Endpoint'te key parametresi YOK ✅
- PII verileri maskelenmiş ✅
```

---

## ⚡ Performans Testleri

### 1. Yanıt Süresi Testleri

#### Test 1.1: Rapor Özeti Süresi
```
Test: 10 farklı rapor için özet üret
Hedef: Ortalama < 5 saniye
Sonuç: ___ saniye (ortalama)
Durum: ✅ Başarılı / ❌ Başarısız
```

#### Test 1.2: E-posta Şablonu Süresi
```
Test: 10 farklı senaryo için şablon üret
Hedef: Ortalama < 3 saniye
Sonuç: ___ saniye (ortalama)
Durum: ✅ Başarılı / ❌ Başarısız
```

### 2. Yük Testleri

#### Test 2.1: Ardışık İstekler
```
Test: 30 istek/dakika gönder (rate limit: 30)
Beklenen: İlk 30 başarılı, 31. red
Sonuç: ___
```

#### Test 2.2: Eşzamanlı Kullanıcılar
```
Test: 5 kullanıcı aynı anda farklı raporlar özetle
Beklenen: Hepsi başarılı (rate limit izin verirse)
Sonuç: ___
```

### 3. Bellek ve Kaynak Kullanımı

#### Test 3.1: Bellek Sızıntısı
```
Test: 100 kez rapor özeti oluştur
Başlangıç RAM: ___ MB
Bitiş RAM: ___ MB
Artış: ___ MB
Beklenen: < 50 MB artış
```

---

## 👥 Kullanıcı Kabul Testleri (UAT)

### Senaryo 1: Satış Elemanı - Hızlı Rapor Özeti

**Persona:** Satış elemanı, hızlıca rapor özetine ihtiyacı var

**Adımlar:**
1. Müşteri raporunu aç
2. AI özeti oluştur
3. Önemli bulguları kopyala
4. Yöneticiye e-posta gönder

**Başarı Kriterleri:**
- ✅ Toplam süre < 1 dakika
- ✅ Özet anlamlı ve kullanışlı
- ✅ Kullanıcı memnun (anket: 4/5+)

### Senaryo 2: Muhasebe - Ödeme Hatırlatma E-postası

**Persona:** Muhasebe elemanı, nazik ödeme hatırlatması gönderecek

**Adımlar:**
1. Mail formunu aç
2. Müşteri e-postasını seç
3. Senaryo: Ödeme Hatırlatma, Ton: Nötr
4. Şablon oluştur ve incele
5. Gerekirse düzenle
6. Gönder

**Başarı Kriterleri:**
- ✅ E-posta profesyonel ve nazik
- ✅ Kullanıcı manuel yazım süresini %50+ kısaltıyor
- ✅ Dilbilgisi hatası yok

### Senaryo 3: Yönetici - Aylık Rapor Özeti

**Persona:** Genel Müdür, aylık raporları hızlıca gözden geçirmek istiyor

**Adımlar:**
1. Firmalar, Müşteriler, Giderler, Personel raporlarını aç
2. Her biri için AI özeti üret
3. Aksiyon maddelerini topla
4. Yönetim toplantısında sun

**Başarı Kriterleri:**
- ✅ 4 rapor özeti toplam < 5 dakika
- ✅ Aksiyon maddeleri stratejik ve değerli
- ✅ Toplantıda kullanılabilir kalitede

---

## 📊 Test Raporu Şablonu

### Test Özeti
| Kategori | Toplam Test | Başarılı | Başarısız | Oran |
|----------|-------------|----------|-----------|------|
| Birim Testleri | ___ | ___ | ___ | ___% |
| Entegrasyon | ___ | ___ | ___ | ___% |
| Fonksiyonel | ___ | ___ | ___ | ___% |
| Güvenlik | ___ | ___ | ___ | ___% |
| Performans | ___ | ___ | ___ | ___% |
| UAT | ___ | ___ | ___ | ___% |
| **TOPLAM** | **___** | **___** | **___** | **___%** |

### Kritik Hatalar
1. [Hata ID] - [Açıklama] - [Öncelik: Yüksek/Orta/Düşük]
2. ...

### İyileştirme Önerileri
1. [Öneri açıklaması]
2. ...

### Onay
- Test Sorumlusu: _______________
- Tarih: _______________
- Durum: ☐ Onaylandı / ☐ Revize Gerekli

---

## 🚀 Test Otomasyonu (Gelecek)

### Birim Test Örneği (NUnit)
```csharp
[TestFixture]
public class PiiMaskingTests
{
    private PiiMaskingService _maskingService;

    [SetUp]
    public void Setup()
    {
        _maskingService = new PiiMaskingService();
    }

    [Test]
    public void MaskText_EmailAddress_ShouldMask()
    {
        var input = "İletişim: test@example.com";
        var result = _maskingService.MaskText(input);
        
        Assert.That(result, Does.Contain("[EMAIL]"));
        Assert.That(result, Does.Not.Contain("test@example.com"));
    }

    [Test]
    public void MaskText_PhoneNumber_ShouldMask()
    {
        var input = "Tel: 0532 123 45 67";
        var result = _maskingService.MaskText(input);
        
        Assert.That(result, Does.Contain("[TELEFON]"));
        Assert.That(result, Does.Not.Contain("0532"));
    }
}
```

### Entegrasyon Test Örneği (Mock)
```csharp
[TestFixture]
public class AiServiceTests
{
    [Test]
    public async Task SummarizeAsync_ValidPrompt_ReturnsResponse()
    {
        // Arrange
        var service = new AiService();
        var prompt = "Test prompt";

        // Act
        var result = await service.SummarizeAsync(prompt);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result.Content);
        Assert.Greater(result.TotalTokens, 0);
    }
}
```

---

**Son Güncelleme:** 2025-10-13
**Versiyon:** 1.0
**Test Sorumlusu:** [İsim]

