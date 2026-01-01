Kısa not: İstediğin iki özelliği (Rapor Özeti ve E‑posta Asistanı) için kapsamlı ve adım adım bir iş planı çıkarıyorum; mimari, UI değişiklikleri, güvenlik, loglama, test ve yayına alma dahil.

### Amaç

- Rapor Özetleme (FrmRaporlar): Uzun raporların Türkçe doğal dil özeti + aksiyon maddeleri.
- E‑posta Asistanı (FrmMail): Teklif/yanıt şablonu üretimi, konu satırı ve ton önerisi.

### Varsayımlar

- AI sağlayıcısı: Şimdilik genel LLM (örn. OpenAI/Azure OpenAI). Değiştirilebilir.
- İnternet erişimi mevcut. API anahtarı güvenli saklanacak.
- PII verileri AI’a gönderilmeden önce maskelenecek ve veri minimizasyonu yapılacak.

## Faz 0 — Hazırlık ve Altyapı

- Gereksinimler ve erişim:
  - **AI sağlayıcı seçimi** ve model kararı (metin özetleme için hızlı/ucuz, e‑posta için kalite dengesi).
  - **API anahtarı** edinme, yetki kapsamlarının kontrolü (kurumsal hesap/çevre).
- Konfigürasyon ve güvenlik:
  - `App.config` içine `AI_ENDPOINT`, `AI_API_KEY`, `AI_PROVIDER`, `AI_TIMEOUT_MS`, `AI_RETRY_COUNT`.
  - API anahtar yönetimi: Çevresel değişkenden okuma (“ENV:” prefiksi) + makinede güvenli saklama.
  - PII politikası: isim, e‑posta, telefon, vergi no maskesi; veri minimizasyon kuralları.
- Altyapı sınıfları:
  - `AiService.cs`: HTTP istemcisi, zaman aşımı, retry, hata yönetimi, telemetry.
  - `PromptBuilder.cs`: Senaryo bazlı prompt şablonları (özet, aksiyon, e‑posta şablon).
  - `AiResponseParser.cs`: Sağlayıcı yanıtlarını ayıklama (JSON/Metin → model).
  - `AiRateLimiter.cs` (opsiyonel): Kullanıcı başına/uygulama genelinde çağrı hız limiti.
- Loglama ve izlenebilirlik:
  - İstek/yanıt (maskeli) log’ları: süre, model, karakter/token bedeli (varsa), hata kodu.
  - Feature flag: Her özellik için aç/kapat anahtarı (konfigürasyon bazlı).

## Faz 1 — Rapor Özetleme (FrmRaporlar)

- Veri hazırlama:
  - Rapor veri kaynağı belirleme: `DB`’deki raporu üreten SP/Query.
  - Özetlenecek içerik formatı: başlıklar + toplamlar + metrik özetleri (metin ≤ 4–8 KB).
  - PII maskesi: müşteri/ürün adları yerine ID veya kategori bazlı özet.
- UI/UX:
  - `FrmRaporlar`’a “Özet Üret” butonu.
  - Yanıt alanı: DevExpress `MemoEdit/RichEdit` alanı (sekme: “AI Özeti”).
  - Yükleniyor durumu: `MarqueeProgressBar` + iptal butonu.
  - Hata ve uyarılar: `AlertControl` veya `XtraMessageBox` ile anlamlı mesajlar.
  - “Aksiyon Maddeleri” alt paneli: maddeler halinde, kopyalanabilir.
- İş akışı:
  - Kullanıcı tarih aralığı/filtreleri seçer → Rapor üretilir (var olan akış).
  - “Özet Üret” butonu:
    - Mevcut raporu kısa metin özetine dönüştür (server-side/SP veya client-side).
    - `PromptBuilder.BuildReportSummaryPrompt(data, formatOptions)` ile prompt hazırla.
    - `AiService.SummarizeAsync(prompt)` çağrısı (timeout+retry).
    - `AiResponseParser.ParseSummaryAndActions(response)` ile özet + aksiyon listesi çıkar.
    - UI alanlarını doldur; “Panoya kopyala” butonu.
- Performans ve kısıtlar:
  - Uzun raporları bölümleyip ardışık özet (chunk → ara özet → final özet).
  - Cache: Aynı filtrelerle tekrar istenirse son 15 dk sonuçlarını sun.
- Testler:
  - Fonksiyonel: Türkçe akıcı özet üretiliyor mu? Aksiyon maddeleri net mi?
  - Sınır durumları: Boş rapor, çok büyük rapor, timeout/hata, internet yok.
  - PII doğrulama: Maskelenmemiş veri dışarı çıkmıyor mu?
- Kabul kriterleri:
  - 2–5 madde arası özlü sonuç, sayısal metriklerin korunması (ciro, adet vb.).
  - 3–7 aksiyon maddesi, anlaşılır, tekrar üretilebilir.
  - Hata mesajları kullanıcı dostu, log’lar teknik detaylı.

## Faz 2 — E‑posta Asistanı (FrmMail)

- Senaryolar:
  - Teklif e‑postası üretimi (müşteri, ürün listesi, iskonto, teslimat koşulları).
  - Yanıt şablonu (teşekkür, takip, ödeme hatırlatma).
  - Konu satırı ve ton önerisi (resmi/yumuşak/son tarih hatırlatma).
- Veri hazırlama:
  - `FrmMail` üzerindeki alandan müşteri/şirket adı (maskeli), ürün satırları (ad yerine kategori+kod), fiyat/iskonto (opsiyon), teslim süresi gibi parametreler.
  - Ton/uzunluk/biçim seçenekleri (kısa/orta/uzun; resmi/nötr/samimi).
- UI/UX:
  - `FrmMail`’e “Şablon Öner” butonu.
  - Seçenekler paneli: Ton, uzunluk, konu satırı üret (checkbox), dil (TR).
  - Çıktı alanı: Rich edit; “Gövdeye Aktar”, “Kopyala”, “Yeniden üret” butonları.
  - Önerilen konu satırı alanı (tek satır).
- İş akışı:
  - Kullanıcı e‑posta bağlamını doldurur (müşteri, teklif detayları) → “Şablon Öner”.
  - `PromptBuilder.BuildEmailTemplatePrompt(context, tone, length)` ile prompt hazırla.
  - `AiService.GenerateEmailAsync(prompt)` çağrısı.
  - `AiResponseParser.ParseEmailParts(response)` ile konu/gövde ayrıştır.
  - Önizleme alanına yerleştir → kullanıcı “Gövdeye Aktar” ile editöre geçirir.
- Güvenlik ve gizlilik:
  - Müşteri adı/iletişim bilgileri maskeli. Öneri metni yerleştirilirken gerçek değerler UI katmanında tekrar doldurulur (template placeholders → gerçek veri).
  - “Dışarı veri gönderme” onayı (kurumsal gereksinim varsa).
- Testler:
  - Farklı ton/uzunluk kombinasyonlarında Türkçe dilbilgisi ve akıcılık.
  - Boş/eksik bağlam uyarıları, timeout/hata davranışı.
  - PII maskeleme akışı ve yerelleştirme (TR karakterleri).
- Kabul kriterleri:
  - En az 3 alternatif konu satırı; 1 ana gövde + kısa varyant.
  - Şablonlar iş kurallarına uygun (iskonto/teslim/ödeme maddeleri).
  - Kullanıcı 1 tıkla gövdeye aktarabiliyor, düzenleyebiliyor.

## Faz 3 — Ortak Bileşenler ve Teknik Detaylar

- Hata yönetimi:
  - Ağ hatası, 401/403/429, 5xx için yeniden deneme (exponential backoff).
  - Kullanıcı dostu hata mesajları; ayrıntı log’da.
- Oran sınırlama (Rate limiting):
  - Uygulama genelinde dakikada X çağrı; kullanıcı başına Y çağrı.
- Telemetri ve ölçüm:
  - Çağrı süresi, hata oranı, kullanım sayısı, “kabul edildi/yeniden üretildi”.
  - “Öncesi/sonrası” e-posta gönderim süresinde iyileşme, rapor okuma süresi azalması.
- Maliyet kontrol:
  - Token/karakter boyutu tahmini; belirli eşik üstünde uyarı veya “daha kısa özet” modu.
- Yerelleştirme:
  - Tüm AI metinleri Türkçe yanıt zorunluluğu; promptlara dil yönergesi.

## Faz 4 — Güvenlik, Uyumluluk, Gizlilik

- PII maskeleme kuralları ve testleri.
- Log’larda PII saklanmaması; yalnızca hash’li/anonim referanslar.
- Veri saklama politikası: AI yanıtları kalıcı mı? Varsayılan: geçici+isteğe bağlı kaydetme.

## Faz 5 — Test ve Doğrulama

- Birim testleri:
  - `PromptBuilder` ve `AiResponseParser` için örnek veriyle deterministik testler.
- Entegrasyon testleri:
  - Sağlayıcı sandbox/test anahtarıyla sahte yanıtlar.
- Kullanıcı kabul testleri (UAT):
  - Satış/rapor ekipleriyle 1 haftalık pilot; geri bildirim toplanması.
- Performans:
  - İlk yanıt süresi < 3 sn (özet için 5–8 sn kabul edilebilir).
  - UI donmaması (async/await; progress göstergesi).

## Faz 6 — Yayına Alma ve İzleme

- Aşamalı açılış (feature flag ile yüzde bazlı).
- Geri bildirim mekanizması: “Öneri faydalı mıydı?” kısa anket.
- İzleme panosu: çağrı sayısı, maliyet, hata oranı, onay/ret oranları.
- Dokümantasyon: Kullanıcı kılavuzu (kısa video/gif), IT için yapılandırma notları.

## Zamanlama (örnek, ardışık sprintler)

- Hafta 1: Faz 0 altyapı + güvenlik/loglama.
- Hafta 2: Rapor özetleme temel akış (prompt, UI, parsing, test).
- Hafta 3: E‑posta asistanı temel akış (prompt, UI, parsing, test).
- Hafta 4: İnce ayar, performans, telemetri, UAT, pilot ve aşamalı açılış.

## Teknik Uygulama Notları

- Async kullanım: Ağ çağrıları `async/await`; UI thread’i bloklanmasın.
- Parçalama/özetleme: Büyük raporlar için “böl‑özetle‑birleştir” yaklaşımı.
- Prompt şablonları:
  - Rapor özeti: amaç, metrikler (ciro, marj, adet), tarih aralığı, beklenen çıktı formatı (madde işaretli), dil TR.
  - E‑posta: bağlam alanları, ton/uzunluk parametreleri, konu ve gövdeyi ayrı üretme isteği, TR zorunluluğu.
- Sağlayıcı soyutlaması: `AiService` tek sınıf; sağlayıcı değiştirmeyi kolaylaştırır.

## Başarı Kriterleri (ölçülebilir)

- Rapor özeti:
  - Kullanıcıların rapor başına okuma süresi ≥ %30 azalır.
  - Aksiyon maddelerinin “faydalı” oyu ≥ %70.
- E‑posta asistanı:
  - Teklif e‑postası hazırlama süresi ≥ %40 kısalır.
  - Önerilen konu satırlarının açılma oranı ≥ %10 artar (ölçülebilirse).

Kısa durum: İki özellik için uçtan uca, hiçbir adımı atlamadan kapsamlı planı çıkardım. İstersen önce “Rapor Özetleme”yi PoC olarak hayata geçirip aynı sprintte “E‑posta Asistanı”nı ekleyebiliriz.
