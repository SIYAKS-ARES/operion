# operion Smoke Test Senaryoları

**Tarih:** 2025-12-09  
**Durum:** Modernizasyon tamamlandı (21/21 form)  
**Hedef:** Temel fonksiyonellik ve regresyon testleri

---

## Test Kategorileri

### 1. Form Açılış ve Navigasyon Testleri

#### 1.1 Ana Modül Navigasyonu
- [X] FrmAdmin açılıyor mu? (Login ekranı) (login ekranı açılıyor)
- [X] Giriş yapıldıktan sonra FrmAnaModul açılıyor mu?
- [ ] Ana menüden tüm formlar açılabiliyor mu? (21 form)
  - [x] Bankalar hatası (SQLite Error 1) - Düzeltildi
  - [x] Personeller sayfası kaymaları - Düzeltildi
  - [x] Faturalar sayfası kaymaları - Düzeltildi
  - [x] Giderler sayfası kaymaları - Düzeltildi
  - [x] Kasa sayfası kaymaları - Düzeltildi
  - [x] Notlar sayfası kaymaları - Düzeltildi
  - [x] Ayarlar sayfası kaymaları - Düzeltildi
  - [x] Dashboard (Fihrist -> Rehber) - Düzeltildi
- [ ] AI implementasyonu (Raporlar ve Mail modülünde mevcut)
- [X] Tema toggle (Dark/Light) çalışıyor mu? (Çalışıyor ama açılan pencerelerde form kısımları aydınlık kalıyor hala)
- [X] Logo görüntüleniyor mu?

#### 1.2 Form Açılış Hızları
- [X] Her form 2 saniye içinde açılıyor mu?
- [X] Form açılışında donma var mı? (Donma yok)
- [X] Memory leak var mı? (Form kapatıldıktan sonra) (Hayır herhangi bir memory leak ile karşılaşmadım)

---

### 2. Core İş Modülleri Testleri

#### 2.1 FrmUrunler (Ürünler)
- [X] Ürün listesi yükleniyor mu?
- [X] Yeni ürün eklenebiliyor mu?
- [X] Ürün güncellenebiliyor mu?
- [X] Ürün silinebiliyor mu? (Onay mesajı çıkıyor mu?)
- [X] Inline validation çalışıyor mu? (Ürün adı zorunlu)
- [X] DataGridView hover efekti çalışıyor mu?
- [X] Grid yatay scroll çalışıyor mu? (DisplayedCells) - Eklendi (2026-01-02)

#### 2.2 FrmMusteriler (Müşteriler)
- [X] Müşteri listesi yükleniyor mu?
- [X] Yeni müşteri eklenebiliyor mu?
- [x] TC, Telefon maskeleri çalışıyor mu?
- [X] İl/İlçe ComboBox bağımlılığı çalışıyor mu?
- [x] Inline validation çalışıyor mu? (Ad, Soyad zorunlu)

#### 2.3 FrmFirmalar (Firmalar)
- [X] Firma listesi yükleniyor mu?
- [X] Yeni firma eklenebiliyor mu?
- [X] RichTextBox alanları (Adres, Özel Kodlar) çalışıyor mu?
- [X] Scroll edilebilir form paneli çalışıyor mu?

#### 2.4 FrmPersoneller (Personeller)
- [X] Personel listesi yükleniyor mu?
- [X] Yeni personel eklenebiliyor mu?
- [X] Fotoğraf yükleme çalışıyor mu? (BLOB) - Eklendi (2025-01-XX)
- [X] Form düzeni düzgün mü? (Dikey boşluklar artırıldı) - Düzeltildi (2026-01-02)

---

### 3. Fatura Modülleri Testleri

#### 3.1 FrmFaturalar (Faturalar)
- [ ] Fatura listesi yükleniyor mu? (Liste boş test edemedim ekleyemedim de)
- [ ] Fatura Bilgisi kaydı yapılabiliyor mu?
- [ ] Fatura Detay kaydı yapılabiliyor mu?
- [ ] Otomatik tutar hesaplama çalışıyor mu? (Miktar × Fiyat)
- [ ] DoubleClick ile FrmFaturaUrunDetay açılıyor mu?
- [ ] Inline validation çalışıyor mu? (Seri, Sıra No zorunlu)

#### 3.2 FrmFaturaUrunDetay (Fatura Ürün Detay)
- [ ] Modal dialog olarak açılıyor mu?
- [ ] Fatura detayları görüntüleniyor mu?
- [ ] Para birimi formatı doğru mu? (₺)
- [ ] DoubleClick ile FrmFaturaUrunDuzenleme açılıyor mu?

#### 3.3 FrmFaturaUrunDuzenleme (Fatura Ürün Düzenleme)
- [ ] Modal dialog olarak açılıyor mu?
- [ ] Ürün bilgileri yükleniyor mu?
- [ ] Güncelleme çalışıyor mu?
- [ ] Silme çalışıyor mu? (Onay mesajı)
- [ ] Otomatik tutar hesaplama gerçek zamanlı çalışıyor mu?

#### 3.4 FrmHareketler (Hareketler)
- [ ] TabControl sekmeleri çalışıyor mu? (Firma/Müşteri)
- [ ] Her iki sekmede DataGridView yükleniyor mu?
- [ ] Para birimi formatı doğru mu?
- [ ] VIEW'lar çalışıyor mu? (FirmaHareketler, MusteriHareketler)

---

### 4. Yardımcı Modüller Testleri

#### 4.1 FrmBankalar (Bankalar) 
- [ ] Banka listesi yükleniyor mu?
- [ ] Yeni banka eklenebiliyor mu? (Kayit hatas: SQLite Error 19: 'FOREIGN KEY constraint failed'. hatasını verdi)
- [ ] VIEW kullanımı çalışıyor mu? (BankaBilgileri)





<!-- 
Not: Kullanıcı geri bildirimlerine istinaden aşağıdaki iyileştirmeler yapılmıştır (2026-01-02):
- Her panele basınca yeni pencere açma yerine tam ekran MDI Child kullanımı (Düzeltildi)
- Dark modda beyaz kalan alanlar (Düzeltildi: ThemeManager tüm child kontrollere tema uyguluyor)
- Yazı ve kutucuk kaymaları (Düzeltildi: Tüm formlarda layout iyileştirmesi yapıldı)
-->





#### 4.2 FrmGiderler (Giderler)
- [ ] Gider listesi yükleniyor mu?
- [ ] Yeni gider eklenebiliyor mu?
- [ ] Para birimi formatı doğru mu?

#### 4.3 FrmStoklar (Stoklar)
- [ ] Stok listesi yükleniyor mu?
- [ ] GROUP BY sorgusu çalışıyor mu? (Ürün bazlı toplama)

#### 4.4 FrmKasa (Kasa)
- [ ] Statistik kartları yükleniyor mu? (9 kart)
- [ ] TabControl sekmeleri çalışıyor mu? (Giriş/Çıkış)
- [ ] Para birimi formatı doğru mu?

#### 4.5 FrmNotlar (Notlar)
- [ ] Not listesi yükleniyor mu?
- [ ] Yeni not eklenebiliyor mu?
- [ ] DoubleClick ile FrmNotDetay açılıyor mu?

#### 4.6 FrmNotDetay (Not Detay)
- [ ] Modal dialog olarak açılıyor mu?
- [ ] Not içeriği görüntüleniyor mu?

#### 4.7 FrmRehber (Rehber)
- [ ] TabControl sekmeleri çalışıyor mu? (Müşteriler/Firmalar)
- [ ] DoubleClick ile FrmMail açılıyor mu?

---

### 5. Özel Modüller Testleri

#### 5.1 FrmRaporlar (Raporlar)
- [ ] TabControl sekmeleri çalışıyor mu? (4 sekme)
- [ ] Rapor butonları çalışıyor mu?
- [ ] HTML rapor üretimi çalışıyor mu?
- [ ] Rapor HTML tarayıcıda açılıyor mu?
- [ ] Başarı mesajı gösteriliyor mu?

#### 5.2 FrmMail (Mail)
- [ ] Form açılıyor mu?
- [ ] E-posta adresi validasyonu çalışıyor mu?
- [ ] Konu zorunlu kontrolü çalışıyor mu?
- [ ] App.config'den SMTP ayarları okunuyor mu?
- [ ] E-posta gönderimi çalışıyor mu? (Test SMTP ile)
- [ ] Hata mesajları doğru mu?
- [ ] Başarı mesajı gösteriliyor mu?

**SMTP Test Notları:**
- App.config'de SMTP ayarları yapılandırılmış olmalı
- SMTP_PASSWORD için ENV:SMTP_PASSWORD kullanılabilir
- Test için geçerli SMTP bilgileri gerekli

#### 5.3 FrmAyarlar (Ayarlar)
- [ ] Kullanıcı listesi yükleniyor mu?
- [ ] Yeni kullanıcı eklenebiliyor mu?
- [ ] Kullanıcı güncellenebiliyor mu?
- [ ] Şifre güncelleme çalışıyor mu?
- [ ] Inline validation çalışıyor mu? (Kullanıcı adı, Şifre zorunlu)

---

### 6. Tema ve Tasarım Testleri

#### 6.1 Light/Dark Tema
- [ ] Tema toggle butonu çalışıyor mu?
- [ ] Tüm formlarda tema uygulanıyor mu?
- [ ] Tema geçişi smooth mu? (Donma yok)
- [ ] Tema tercihi kaydediliyor mu?

#### 6.2 Modern UI Bileşenleri
- [ ] ModernButton stilleri doğru mu? (Primary, Secondary, Success, Error)
- [ ] ModernTextBox placeholder'ları görünüyor mu?
- [ ] ModernTextBox inline validation çalışıyor mu?
- [ ] ModernPanel card tasarımı doğru mu?
- [ ] ModernDataGridView hover efekti çalışıyor mu?

---

### 7. Veritabanı Testleri

#### 7.1 Veritabanı Başlatma
- [ ] İlk çalıştırmada veritabanı oluşturuluyor mu?
- [ ] SQL script dosyası bulunuyor mu?
- [ ] Tablolar oluşturuluyor mu?
- [ ] VIEW'lar oluşturuluyor mu?
- [ ] Default admin kullanıcısı ekleniyor mu?

#### 7.2 Veri İşlemleri
- [ ] CRUD işlemleri çalışıyor mu? (Create, Read, Update, Delete)
- [ ] Foreign key constraint'ler çalışıyor mu?
- [ ] Silme işlemlerinde ilişkili kayıt kontrolü yapılıyor mu?

---

### 8. Performans Testleri

#### 8.1 Form Açılış Hızları
- [ ] Tüm formlar 2 saniye içinde açılıyor mu?
- [ ] DataGridView'lar hızlı yükleniyor mu? (100+ kayıt)

#### 8.2 Memory Kullanımı
- [ ] Form kapatıldıktan sonra memory temizleniyor mu?
- [ ] Memory leak var mı? (Uzun süreli kullanım)

---

## Test Senaryoları Özeti

**Toplam Test Senaryosu:** ~80  
**Kritik Senaryolar:** 25  
**Orta Öncelikli:** 35  
**Düşük Öncelikli:** 20

---

## Bilinen Sorunlar ve Notlar

### SMTP Konfigürasyonu
- App.config'de SMTP ayarları yapılandırılmalı
- SMTP_PASSWORD için ENV:SMTP_PASSWORD kullanılabilir
- Test için geçerli SMTP bilgileri gerekli

### NU1510 Uyarısı
- ConfigurationManager paketi AI servisleri tarafından kullanılıyor
- Uyarı görmezden gelinebilir (paket gereklidir)

### WFO1000 Uyarıları
- Terminal build'de WFO1000 uyarısı yok
- Visual Studio cache temizleme gerekebilir

---

## Test Sonuçları

**Test Tarihi:** [Tarih]  
**Test Eden:** [İsim]  
**Sonuç:** [Başarılı/Başarısız]

### Başarılı Senaryolar
- [ ] Liste

### Başarısız Senaryolar
- [ ] Liste

### Notlar
- [ ] Liste

---

**Son Güncelleme:** 2025-12-09

