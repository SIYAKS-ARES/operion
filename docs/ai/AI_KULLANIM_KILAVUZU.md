# AI Entegrasyonu Kullanım Kılavuzu

## 📋 İçindekiler
1. [Kurulum](#kurulum)
2. [Yapılandırma](#yapılandırma)
3. [Özellikler](#özellikler)
4. [Kullanım](#kullanım)
5. [Güvenlik](#güvenlik)
6. [Sorun Giderme](#sorun-giderme)

## 🚀 Kurulum

### Gereksinimler
- .NET Framework 4.5.2 veya üzeri
- DevExpress 18.1 veya üzeri
- Newtonsoft.Json 13.0.3
- OpenAI API anahtarı veya Azure OpenAI erişimi

### Paket Kurulumu
1. NuGet Package Manager'dan `Newtonsoft.Json` paketini yükleyin:
   ```
   Install-Package Newtonsoft.Json -Version 13.0.3
   ```

2. Proje referanslarını kontrol edin (otomatik eklenir):
   - System.Configuration
   - System.Net.Http

## ⚙️ Yapılandırma

### App.config Ayarları

AI özelliklerini kullanmak için `App.config` dosyasında aşağıdaki ayarları yapılandırın:

#### Temel Ayarlar
```xml
<!-- AI Sağlayıcı -->
<add key="AI_PROVIDER" value="OpenAI" />
<add key="AI_ENDPOINT" value="https://api.openai.com/v1/chat/completions" />
<add key="AI_MODEL" value="gpt-4o-mini" />
```

#### API Anahtarı (Güvenli Yöntem)
**Önerilen:** Çevre değişkeni kullanın
```xml
<add key="AI_API_KEY" value="ENV:OPENAI_API_KEY" />
```

Sistem çevre değişkenini ayarlamak için PowerShell:
```powershell
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-your-api-key-here", "User")
```

**Alternatif:** Doğrudan anahtarı yazın (güvenli değil!)
```xml
<add key="AI_API_KEY" value="sk-your-api-key-here" />
```

#### Performans Ayarları
```xml
<add key="AI_TIMEOUT_MS" value="30000" />          <!-- 30 saniye -->
<add key="AI_RETRY_COUNT" value="3" />              <!-- 3 deneme -->
<add key="AI_MAX_TOKENS" value="2000" />            <!-- Maksimum token -->
```

#### Rate Limiting (Hız Sınırlama)
```xml
<add key="AI_RATE_LIMIT_GLOBAL" value="30" />       <!-- Dakikada 30 istek -->
<add key="AI_RATE_LIMIT_PER_USER" value="10" />     <!-- Kullanıcı başına 10 -->
```

#### Feature Flags (Özellik Anahtarları)
```xml
<add key="FEATURE_AI_REPORT_SUMMARY" value="true" />    <!-- Rapor özeti -->
<add key="FEATURE_AI_EMAIL_ASSISTANT" value="true" />   <!-- E-posta asistanı -->
```

## 🎯 Özellikler

### 1. Rapor Özetleme (FrmRaporlar)

#### Özellik Açıklaması
- Uzun raporları Türkçe doğal dil özeti haline getirir
- Aksiyon maddeleri önerir
- PII (Kişisel Veri) maskeleme ile güvenli işlem

#### Kullanım Adımları
1. **Raporlar** modülünü açın
2. İstediğiniz rapor türünü seçin (Firmalar, Müşteriler, Giderler, Personel)
3. **AI Özeti** sekmesine geçin
4. **Özet Üret** butonuna tıklayın
5. Birkaç saniye içinde özet ve aksiyon maddeleri görüntülenir
6. **Panoya Kopyala** ile metni kopyalayabilirsiniz

#### Desteklenen Rapor Türleri
- ✅ Firmalar Raporu
- ✅ Müşteriler Raporu
- ✅ Giderler Raporu
- ✅ Personeller Raporu

#### Örnek Çıktı
```
Rapor Özeti:
• Toplam 45 firma kaydı bulunmakta, büyük çoğunluğu aktif durumdadır
• Son aydaki işlem hacmi önceki aya göre %15 artış göstermiştir
• En yüksek ciro İstanbul bölgesinden sağlanmıştır

Aksiyon Maddeleri:
1. Pasif firmalarla iletişime geçilmeli ve durumları güncellenmelidir
2. İstanbul dışı bölgelerde pazarlama çalışmaları artırılmalıdır
3. En düşük cirolu 5 firma ile özel indirim görüşmesi yapılabilir
```

### 2. E-posta Asistanı (FrmMail)

#### Özellik Açıklaması
- Profesyonel e-posta şablonları oluşturur
- Farklı senaryo, ton ve uzunluk seçenekleri
- 3 alternatif konu satırı önerisi
- Düzenlenebilir gövde metni

#### Kullanım Adımları
1. **Mail** modülünü açın
2. Alıcı e-posta adresini girin
3. **AI E-posta Asistanı** panelinde:
   - **Senaryo** seçin (Teklif, Teşekkür, Ödeme Hatırlatma vb.)
   - **Ton** belirleyin (Resmi, Nötr, Samimi, Acil)
   - **Uzunluk** seçin (Kısa, Orta, Uzun)
4. **Şablon Öner** butonuna tıklayın
5. Önizlemede konu satırlarını ve gövdeyi inceleyin
6. İsterseniz **Yeniden Üret** ile farklı bir şablon alın
7. **Gövdeye Aktar** ile e-posta formuna aktarın
8. Gerekirse düzenleyin ve **GÖNDER**

#### Senaryo Türleri
- 📧 **Teklif**: Ürün/hizmet teklifi sunumu
- 🙏 **Teşekkür**: Müşteri teşekkür ve takip
- 💰 **Ödeme Hatırlatma**: Nazik ödeme hatırlatması
- 🚚 **Teslimat Bilgi**: Teslimat ve kargo bilgilendirme
- 📝 **Genel Yanıt**: Genel amaçlı yanıt şablonu

#### Ton Seçenekleri
- **Resmi**: Kurumsal ve resmi dil
- **Nötr**: Profesyonel ancak nötr
- **Samimi**: Yakın ve samimi üslup
- **Acil**: Net ve aciliyet vurgulu

## 🔒 Güvenlik

### PII (Kişisel Veri) Koruması
Sistem otomatik olarak şu verileri maskeler:
- ✅ E-posta adresleri → `[EMAIL]`
- ✅ Telefon numaraları → `[TELEFON]`
- ✅ TC Kimlik/Vergi No → `[KIMLIK_NO]`
- ✅ IBAN numaraları → `[IBAN]`
- ✅ Kişi adları → `[KİŞİ_ADI]`

### Veri Minimizasyonu
- Raporlardan maksimum 50 satır gönderilir
- Uzun metinler kısaltılır (max 4-8 KB)
- Gereksiz sütunlar filtrelenir

### Loglama
Tüm AI işlemleri loglanır:
- İstek/yanıt süreleri
- Token kullanımı
- Hata mesajları (PII maskelenmiş)
- Log dosyaları: `Logs/AI/ai_log_YYYYMMDD.log`

## 🐛 Sorun Giderme

### "AI servisi yapılandırılmamış" Hatası
**Çözüm:**
1. `App.config` dosyasında `AI_ENDPOINT` ve `AI_API_KEY` ayarlarını kontrol edin
2. Çevre değişkeni kullanıyorsanız, sistemde tanımlı olduğundan emin olun:
   ```powershell
   [Environment]::GetEnvironmentVariable("OPENAI_API_KEY", "User")
   ```

### "Çok fazla istek gönderildi" Hatası
**Çözüm:**
- Rate limit aşıldı, birkaç saniye bekleyin
- `App.config`'de limit ayarlarını artırabilirsiniz:
  ```xml
  <add key="AI_RATE_LIMIT_GLOBAL" value="60" />
  ```

### "AI yanıtı işlenirken hata oluştu" Hatası
**Çözüm:**
1. Ham yanıtı kontrol edin (hata penceresinde gösterilir)
2. Model değiştirmeyi deneyin (`gpt-4o` yerine `gpt-3.5-turbo`)
3. Prompt'un çok uzun olmadığını kontrol edin

### Timeout Hatası
**Çözüm:**
- Timeout süresini artırın:
  ```xml
  <add key="AI_TIMEOUT_MS" value="60000" />
  ```
- İnternet bağlantınızı kontrol edin

### "Parse Hatası"
**Çözüm:**
1. AI yanıtı beklenen formatta değil
2. Model'i `gpt-4o` olarak değiştirin (daha tutarlı)
3. Prompt şablonlarını kontrol edin

## 📊 Telemetri ve İzleme

### Log Dosyaları
- **Konum**: `Logs/AI/`
- **Format**: `ai_log_YYYYMMDD.log`
- **İçerik**: İstek detayları, süre, token kullanımı, hatalar

### Log Temizleme
Eski loglar otomatik temizlenir (varsayılan: 30 gün)
```csharp
var logger = new AiLogger();
logger.CleanOldLogs(30); // 30 günden eski logları sil
```

### Telemetri İzleme
Her AI işlemi telemetri datasına kaydedilir:
- Özellik adı (RaporOzet, EmailAsistan)
- Kullanıcı eylemi
- Süre (ms)
- Başarı durumu
- Metadata

## 💡 En İyi Uygulamalar

### 1. API Anahtarı Güvenliği
- ❌ **YAPMAYIN**: API anahtarını doğrudan App.config'e yazmayın
- ✅ **YAPIN**: Çevre değişkeni kullanın (`ENV:OPENAI_API_KEY`)
- ✅ **YAPIN**: Production'da Azure Key Vault kullanın

### 2. Maliyet Yönetimi
- ✅ Token limitini makul tutun (max 2000)
- ✅ Cache'i etkinleştirin (15 dakika)
- ✅ Sadece gerekli veriyi gönderin (veri minimizasyonu)

### 3. Performans
- ✅ Async/await kullanın (zaten uygulanmış)
- ✅ Progress bar ile kullanıcı bilgilendirin
- ✅ Timeout değerlerini ortama göre ayarlayın

### 4. Kullanıcı Deneyimi
- ✅ Hata mesajlarını kullanıcı dostu yapın
- ✅ "Yeniden Üret" seçeneği sunun
- ✅ Önizleme imkanı verin

## 🔧 Gelişmiş Yapılandırma

### Azure OpenAI Kullanımı
```xml
<add key="AI_PROVIDER" value="AzureOpenAI" />
<add key="AI_ENDPOINT" value="https://your-resource.openai.azure.com/openai/deployments/your-deployment/chat/completions?api-version=2024-02-15-preview" />
<add key="AI_API_KEY" value="ENV:AZURE_OPENAI_KEY" />
```

### Cache Ayarları
```xml
<add key="AI_CACHE_ENABLED" value="true" />
<add key="AI_CACHE_DURATION_MINUTES" value="15" />
```

### Özellik Kapatma
Geçici olarak bir özelliği devre dışı bırakmak için:
```xml
<add key="FEATURE_AI_REPORT_SUMMARY" value="false" />
<add key="FEATURE_AI_EMAIL_ASSISTANT" value="false" />
```

## 📞 Destek ve İletişim

### Sık Karşılaşılan Sorular
1. **S: Hangi dillerde çalışır?**
   - C: Sadece Türkçe. Sistem promptlarında "Türkçe yanıt ver" direktifi vardır.

2. **S: Offline çalışır mı?**
   - C: Hayır, internet bağlantısı ve API erişimi gereklidir.

3. **S: Maliyeti nedir?**
   - C: OpenAI API kullanım ücretlerine tabidir. Token başına ücretlendirme vardır.

4. **S: Kişisel veriler AI'a gönderiliyor mu?**
   - C: Evet, ancak PII maskeleme ile hassas veriler korunur.

### İletişim
- 📧 Teknik Destek: [E-posta adresi]
- 📚 Dokümantasyon: Bu dosya
- 🐛 Hata Bildirimi: GitHub Issues

## 📝 Değişiklik Günlüğü

### v1.0.0 (2025-10-13)
- ✨ İlk sürüm
- ✅ Rapor özetleme özelliği
- ✅ E-posta asistanı özelliği
- ✅ PII maskeleme
- ✅ Rate limiting
- ✅ Telemetri ve loglama

---

**Not:** Bu özellikler AI teknolojisi kullanır ve %100 doğruluk garanti edilemez. Oluşturulan içeriği mutlaka gözden geçirin ve düzenleyin.

