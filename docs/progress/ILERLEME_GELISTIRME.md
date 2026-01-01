## AI Mikro-Entegrasyon Görevleri

Bu görevler, Faz 7 (AI Planı) kapsamında belirlenen hızlı kazanımların formlara entegrasyonunu kapsar. Tüm görevler `AiService` ve `PromptBuilder` sınıflarını kullanacaktır.

---

### 1. Form: `FrmUrunler` (Ürün Açıklaması)
* **Zorluk:** Düşük
* **UI Değişiklikleri:**
    * `RchDetay` (`MemoEdit` veya `RichEdit`) alanının yanına `btnAiUrunAciklama` (`SimpleButton`) ekle (ikon: 🤖 veya ✨).
* **İş Akışı (Kod):**
    * `btnAiUrunAciklama_Click` (async void) event'i oluştur.
    * `TxtUrunAd`, `TxtMarka`, `TxtModel` alanlarından verileri al.
    * `_promptBuilder.BuildProductDescriptionPrompt(...)` ile prompt oluştur.
    * `_aiService.GenerateEmailAsync(...)` (veya yeni bir `GenerateAsync` metodu) çağır.
    * Butonu `Enabled = false` yap, bekleme imleci göster.
    * Gelen `response.Content`'i `RchDetay.Text` alanına bas.
    * `try-catch-finally` bloğu ile hata yönetimi ve buton/imleç düzeltmesi yap.

---

### 2. Form: `FrmMusteriler` / `FrmFirmalar` (Müşteri 360° Özeti)
* **Zorluk:** Orta (PII Maskeleme nedeniyle)
* **UI Değişiklikleri:**
    * Formda uygun bir yere (örn: iletişim bilgileri altı) `memoAiMusteriOzet` (`MemoEdit`, `ReadOnly=true`, `Height=100px`) ekle.
    * `GridView`'ın `FocusedRowChanged` event'ine veya "Müşteri Seçildi" event'ine tetikleme eklenecek (veya manuel "Özetle" butonu).
* **İş Akışı (Kod):**
    * `FocusedRowChanged` event'i (async) tetiklendiğinde:
    * `memoAiMusteriOzet.Text = "Müşteri profili yükleniyor..."`
    * Seçili müşterinin ID'sini al.
    * DB'den Toplam Ciro, Sipariş Adedi, Son 3 Notu çek.
    * **Kritik:** `PiiMaskingService.MaskCustomerData(...)` ile veriyi anonimleştir.
    * `_promptBuilder.BuildCustomerSummaryPrompt(...)` çağır.
    * `_aiService` çağır.
    * Gelen yanıtı `memoAiMusteriOzet.Text` alanına bas.
    * Hata durumunda "Özet alınamadı" yaz.

---

### 3. Form: `FrmStoklar` (Stok Analizi)
* **Zorluk:** Düşük
* **UI Değişiklikleri:**
    * `gridControlStoklar`'ın yanındaki `groupControl` içine `memoAiStokDurum` (`MemoEdit`, `ReadOnly=true`) ekle.
    * `GridView`'ın `FocusedRowChanged` event'ine tetikleme eklenecek.
* **İş Akışı (Kod):**
    * `FocusedRowChanged` (async) tetiklendiğinde:
    * `memoAiStokDurum.Text = "Stok durumu analiz ediliyor..."`
    * Seçili ürünün ID'sini al.
    * DB'den mevcut stok ve son 6 aylık satış hareketlerini çek (SQL'de `GROUP BY` ay/yıl).
    * `_promptBuilder.BuildStockAnalysisPrompt(...)` çağır.
    * `_aiService` çağır.
    * Gelen yanıtı (yorum metni) `memoAiStokDurum.Text`'e bas.

---

### 4. Form: `FrmNotlar` (Görev Çıkarıcı)
* **Zorluk:** Orta (PII Riski ve UI onayı)
* **UI Değişiklikleri:**
    * `RchDetay` (`MemoEdit`) alanının üstüne `btnAiGorevCikar` (`SimpleButton`) ekle.
* **İş Akışı (Kod):**
    * `btnAiGorevCikar_Click` (async void) event'i oluştur.
    * **Onay:** `XtraMessageBox.Show("Bu not içeriği analiz için yapay zekaya gönderilecektir. Onaylıyor musunuz? (Hassas veri içermediğinden emin olun)", "Onay", ...)`
    * Kullanıcı onaylarsa:
    * `RchDetay.Text` içeriğini al.
    * `_promptBuilder.BuildTaskExtractionPrompt(...)` çağır.
    * `_aiService` çağır.
    * Gelen görev listesini (response.Content) `RchDetay.Text`'in sonuna `\n\n--- AI Görevleri ---\n{...liste}` olarak ekle veya ayrı bir `MemoEdit`'e bas.

---

### 5. Form: `FrmFaturalar` (Anomali Tespiti)
* **Zorluk:** Orta
* **UI Değişiklikleri:** Yok (Arka planda çalışacak).
* **İş Akışı (Kod):**
    * `GridView` (fatura kalemleri) üzerinde `CellValueChanged` event'ine (veya satır eklendi/kaydedildi event'ine) eklenecek.
    * Eğer değişen hücre "Birim Fiyat" veya "Miktar" ise:
    * `UrunAdi`, `Fiyat`, `Miktar` bilgilerini al.
    * `_promptBuilder.BuildAnomalyDetectionPrompt(...)` çağır.
    * `_aiService` çağır. (Bu çağrı `async` olmalı ama kullanıcıyı bloklamamalı - `await` kullanma, `Task.Run` ile arka planda çalıştır).
    * Dönen yanıt "OLASI HATA" içeriyorsa:
    * `XtraMessageBox.Show("Dikkat! 'Laptop' için '100 TL' fiyatı hatalı olabilir. Lütfen kontrol edin.", "Veri Giriş Uyarısı", ...)`


### 6. Form: Sipariş Modülü eklenecektir.

---

## Durum ve Karar

**Tarih:** 2025-12-09  
**Durum:** Backlog (Uygulanmadı)

### Karar
AI mikro-entegrasyon görevleri şu an için backlog'ta tutulacak. Modernizasyon tamamlandıktan sonra değerlendirilecek.

### Gerekçe
1. Modernizasyon öncelikli (tamamlandı ✅)
2. AI entegrasyonu opsiyonel özellik
3. API key yönetimi ve maliyet değerlendirmesi gerekli
4. Kullanıcı geri bildirimi sonrası karar verilecek

### Öncelik Sırası (Uygulanacaksa)
1. **FrmUrunler** - Ürün Açıklaması (Düşük zorluk, hızlı kazanım)
2. **FrmStoklar** - Stok Analizi (Düşük zorluk)
3. **FrmNotlar** - Görev Çıkarıcı (Orta zorluk, PII riski)
4. **FrmFaturalar** - Anomali Tespiti (Orta zorluk, arka plan)
5. **FrmMusteriler/FrmFirmalar** - Müşteri Özeti (Orta zorluk, PII maskeleme gerekli)

### Notlar
- Tüm görevler `AiService` ve `PromptBuilder` sınıflarını kullanacak
- PII maskeleme (`PiiMaskingService`) kritik öneme sahip
- API key güvenliği için ENV: prefix kullanımı mevcut
- Rate limiting (`AiRateLimiter`) aktif

**Son Güncelleme:** 2025-12-09