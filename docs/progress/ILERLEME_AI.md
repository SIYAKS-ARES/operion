## Faz 7 — Mikro-Entegrasyonlar (Hızlı Kazanımlar)

Bu faz, mevcut `AiService` altyapısını kullanarak farklı formlara küçük, hedefe yönelik AI yetenekleri eklemeyi amaçlar.

---

### 1. Senaryo: Ürün Açıklaması Üretici (FrmUrunler)
* **Amaç:** `FrmUrunler`'de, "Detay" alanı için otomatik pazarlama metni oluşturmak.
* **Veri Hazırlama:**
    * **Girdi:** Ürün Adı, Marka, Model, Kategori (varsa).
    * **PII Riski:** Düşük. Ürün adları PII değildir.
* **Prompt Tasarımı (`PromptBuilder.cs` için yeni metot: `BuildProductDescriptionPrompt`):**
    * **Rol:** "Sen bir e-ticaret uzmanısın."
    * **Görev:** "Aşağıdaki bilgilere sahip ürün için 30 kelimeyi geçmeyen, dikkat çekici, Türkçe bir pazarlama açıklaması yaz."
    * **Bağlam:** `Ürün Adı: {ad}`, `Marka: {marka}`
* **Başarı Kriteri:** Kullanıcı, 1-2 saniye içinde akıcı ve anlamlı bir ürün detayı alabilmelidir.

---

### 2. Senaryo: Müşteri 360° Özeti (FrmMusteriler / FrmFirmalar)
* **Amaç:** Seçili müşterinin/firmanın finansal durumunu ve potansiyelini hızlıca özetlemek.
* **Veri Hazırlama:**
    * **Girdi:** Toplam Sipariş Adedi, Toplam Ciro, Son Sipariş Tarihi, Son 3 Not (`TblNotlar`'dan).
    * **PII Riski:** **Yüksek.** Müşteri adı, not içeriği AI'a GÖNDERİLMEMELİ.
    * **Önlem:** `PiiMaskingService` kullanılacak. AI'a sadece "Müşteri 'M-123', Toplam Ciro: 50.000 TL, Sipariş Adedi: 15, Son Notlar: 'memnun', 'fiyat sordu'" gibi anonim veri gönderilecek.
* **Prompt Tasarımı (`PromptBuilder.cs` için: `BuildCustomerSummaryPrompt`):**
    * **Rol:** "Sen bir kıdemli satış analistisin."
    * **Görev:** "Bu müşterinin profilini (Sadık Müşteri, Riskli Müşteri, Yeni Potansiyel, VIP) belirle ve tek cümlelik aksiyon önerisi sun."
    * **Bağlam:** `Veri: {anonim_veri}`
* **Başarı Kriteri:** Satış ekibinin bir müşteriyi aramadan önce 2 saniyede profilini anlamasını sağlamak.

---

### 3. Senaryo: Akıllı Stok Analizi (FrmStoklar)
* **Amaç:** `FrmStoklar`'da seçili ürünün stok durumunu "yorumlamak".
* **Veri Hazırlama:**
    * **Girdi:** Ürün Adı, Mevcut Stok Adedi, Son 6 Aylık Satış Adetleri (örn: `[10, 15, 12, 20, 18, 25]`).
    * **PII Riski:** Düşük.
* **Prompt Tasarımı (`PromptBuilder.cs` için: `BuildStockAnalysisPrompt`):**
    * **Rol:** "Sen bir tedarik zinciri ve stok yönetimi uzmanısın."
    * **Görev:** "Aşağıdaki satış verisi ve mevcut stoka göre bu ürünün durumu nedir? Stoklar kaç ay/gün yeter? Acil sipariş gerekir mi? Satış trendi (artıyor/azalıyor) nedir?"
    * **Bağlam:** `Mevcut Stok: {stok}`, `Aylık Satışlar: {veri_listesi}`
* **Başarı Kriteri:** "Stok tükenmek üzere, trend artıyor" gibi net, aksiyon alınabilir bir içgörü sağlamak.

---

### 4. Senaryo: Görev Çıkarıcı (FrmNotlar)
* **Amaç:** `FrmNotlar`'daki serbest metinlerden otomatik görev (aksiyon) listesi çıkarmak.
* **Veri Hazırlama:**
    * **Girdi:** `MemoEdit` içindeki tam metin.
    * **PII Riski:** **Yüksek.** Notlar müşteri veya personel hakkında hassas bilgi içerebilir.
    * **Önlem:** PII maskelemesi uygulanacak veya bu özellik için kullanıcıdan net bir "veri gönderme onayı" alınacak.
* **Prompt Tasarımı (`PromptBuilder.cs` için: `BuildTaskExtractionPrompt`):**
    * **Görev:** "Bu metni oku. İçindeki 'yapılacak işleri' (aksiyon maddelerini) madde madde listele. Sadece görev listesini ver, yorum ekleme."
    * **Bağlam:** `{not_metni}`
* **Başarı Kriteri:** Dağınık toplantı notlarını organize bir görev listesine dönüştürmek.

---

### 5. Senaryo: Fatura Anomali Tespiti (FrmFaturalar)
* **Amaç:** `FrmFaturalar`'da veri girişi sırasında (veya kaydetmeden önce) bariz hataları yakalamak.
* **Veri Hazırlama:**
    * **Girdi:** Ürün Adı, Ürün Kategorisi, Miktar, Birim Fiyat.
    * **PII Riski:** Düşük.
* **Prompt Tasarımı (`PromptBuilder.cs` için: `BuildAnomalyDetectionPrompt`):**
    * **Rol:** "Sen bir denetçisin."
    * **Görev:** "Bir 'Laptop' ürününün birim fiyatının '100 TL' olması mantıklı mı? Yoksa bu bir veri giriş hatası (örn: 10.000 TL yerine 100 TL yazılmış) olabilir mi? Sadece 'OLASI HATA' veya 'NORMAL' olarak yanıt ver."
    * **Bağlam:** `Ürün: {ad}`, `Fiyat: {fiyat}`
* **Başarı Kriteri:** Kullanıcı 10.000 TL'lik ürünü 100 TL olarak kaydetmeye çalıştığında uyarı almasını sağlamak.