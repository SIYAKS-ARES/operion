# Operion - Ticari Otomasyon Sistemi

Operion, küçük ve orta ölçekli işletmelerin (KOBİ) dijital dönüşümünü sağlamak amacıyla geliştirilmiş, .NET 10 tabanlı, modern ve kapsamlı bir ticari otomasyon yazılımıdır. Geleneksel ERP (Kurumsal Kaynak Planlama) fonksiyonlarını, Üretken Yapay Zeka (Generative AI) teknolojileri ile birleştirerek işletmelere akıllı bir yönetim deneyimi sunar.

**Sürüm:** 1.0.0+3
**Mimari:** .NET 10 Windows Forms (x64 / ARM64)
**Veritabanı:** SQLite (Entity Framework Core)

---

## Proje Hakkında

Operion, performans ve taşınabilirlik odaklı tasarlanmıştır. "Single-File Application" (Tek Dosya Uygulaması) mimarisi sayesinde, hedef bilgisayarda herhangi bir ön kurulum (.NET Runtime vb.) gerektirmeden çalışabilir. Modern Windows Forms bileşenleri ile Windows 11 tasarım diline uygun, kullanıcı dostu bir arayüz sunar.

### Temel Fonksiyonlar

Uygulama, bir işletmenin ihtiyaç duyacağı temel modülleri tek bir platformda toplar:

*   **Cari Hesap Yönetimi:** Müşteri ve tedarikçi firmaların kaydı, bakiye takibi, borç/alacak yönetimi ve detaylı hesap ekstreleri.
*   **Stok ve Depo Yönetimi:** Ürün kartları, barkod desteği, kritik stok seviyesi takibi, alış/satış fiyat analizleri ve görsel ürün kataloğu.
*   **Fatura İşlemleri:** Alış ve satış faturalarının oluşturulması, otomatik stok düşümü, KDV hesaplamaları ve fatura yazdırma.
*   **Finansal Yönetimi:** Kasa ve banka hesaplarının takibi, günlük nakit akışı raporlaması, gelir/gider kalemlerinin işlenmesi.
*   **Personel Yönetimi:** Çalışan özlük bilgileri, departman yönetimi ve iletişim kayıtları.
*   **Raporlama Modülü:** Grafik destekli finansal raporlar, stok durum raporları ve cari hareket analizleri.

---

## Yapay Zeka Entegrasyonu (RAG Mimarisi)

Operion, rakiplerinden farklı olarak "Retrieval-Augmented Generation" (RAG) mimarisini kullanır. Bu teknoloji, işletmenize ait verilerin güvenli bir şekilde yapay zeka tarafından analiz edilmesini ve doğal dil ile sorgulanabilmesini sağlar.

**Kullanılan Teknolojiler:**
*   **Orkestrasyon:** Microsoft Semantic Kernel
*   **LLM (Büyük Dil Modeli):** Google Gemini 1.5 Flash
*   **Vektör Veritabanı:** Hibrit Hafıza Yönetimi (In-Memory + SQLite)

### AI Yetenekleri

1.  **Doğal Dil ile Veri Analizi (Text-to-SQL):**
    Kullanıcılar karmaşık rapor ekranları yerine doğal dil ile soru sorabilirler.
    *   *Örnek Sorular:* "Geçen ay en çok ciro yapan 5 müşteriyi listele.", "Stok miktarı 10'un altında olan ürünler hangileri?", "Ahmet Yılmaz'ın toplam borcu ne kadar?"

2.  **Akıllı Dökümantasyon Arama:**
    Yardım dosyaları ve kullanım kılavuzları üzerinde vektörel arama yapar. Klasik anahtar kelime aramasının ötesinde, kullanıcının niyetini anlayarak ilgili yardım konusunu getirir.

3.  **Akıllı E-Posta Asistanı:**
    Müşterilere gönderilecek tahsilat, teklif veya bilgilendirme mailleri için bağlama uygun taslak metinler oluşturur. Ton (Resmi, Samimi) ve uzunluk ayarı yapılabilir.

4.  **Otomatik Rapor Özetleme:**
    Karmaşık tablolardan oluşan finansal raporları analiz eder ve yöneticiler için "Yönetici Özeti" (Executive Summary) çıkarır.

---

## Teknik Altyapı ve Mimari

Proje, sürdürülebilirlik ve genişletilebilirlik ilkeleri gözetilerek **Katmanlı Mimari (N-Tier Architecture)** ile geliştirilmiştir.

### Katmanlar

1.  **Presentation Layer (Sunum Katmanı):**
    Windows Forms tabanlı kullanıcı arayüzü. Modern UI kontrolleri, tema yönetimi (Aydınlık/Koyu Mod) ve kullanıcı etkileşim kodlarını içerir.

2.  **Application Layer (Uygulama Katmanı):**
    İş mantığının ve servislerin bulunduğu katmandır. Yapay zeka servisleri, raporlama motorları ve veri doğrulama kuralları burada işlenir.
    *   *Servisler:* AiService, DatabaseService, IngestionService, RagService.

3.  **Data Layer (Veri Katmanı):**
    Veritabanı erişiminden sorumlu katmandır. Entity Framework Core kullanılarak veritabanı bağımsızlığı (abstraction) sağlanmıştır, ancak performans için SQLite optimize edilmiştir.
    *   *Özellikler:* Code-First yaklaşımı, Transaction yönetimi, Otomatik Migration.

### Güvenlik Önlemleri

*   **PII Masking:** Yapay zeka servislerine gönderilen verilerde Kişisel Tanımlanabilir Bilgiler (TCKN, Telefon, E-Mail) otomatik olarak maskelenir.
*   **SQL Injection Koruması:** Tüm veritabanı sorguları parametrik yapıda hazırlanmıştır. AI tarafından üretilen SQL sorguları, salt-okunur (SELECT only) modda çalıştırılır ve kara liste kontrolünden geçer.

---

## Kurulum ve Kullanım

### Son Kullanıcı İçin

Operion, kurulum gerektirmeyen taşınabilir (portable) bir uygulamadır.

1.  Yayınlanan son sürümü (operion.exe) indirin.
2.  Dosyayı bilgisayarınızda istediğiniz bir klasöre kopyalayın.
3.  Uygulamayı çalıştırın. Veritabanı dosyaları otomatik olarak oluşturulacaktır.
4.  **Varsayılan Giriş Bilgileri:**
    *   Kullanıcı Adı: `admin`
    *   Şifre: `admin`

### Geliştiriciler İçin

Projeyi geliştirmek veya derlemek için aşağıdaki gereksinimlerin sağlanması gerekir:

*   **IDE:** Visual Studio 2022 (v17.10+) veya Visual Studio 2025.
*   **SDK:** .NET 10.0 SDK.
*   **İş Yükleri:** .NET Masaüstü Geliştirme.

#### Projenin Derlenmesi

1.  Repoyu yerel makinenize klonlayın.
2.  `operion.sln` dosyasını Visual Studio ile açın.
3.  Proje kök dizininde bir `.env` dosyası oluşturun ve Gemini API anahtarınızı tanımlayın:
    `GEMINI_API_KEY=AI_API_ANAHTARINIZ`
4.  Projeyi "Debug" veya "Release" modunda derleyin.

#### Yayınlama (Publish) Komutu

Tek dosya halinde çıktı almak için terminal üzerinden şu komutu kullanabilirsiniz:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:DebugType=None /p:ProductName="operion"
```

---

## Lisans ve Katkı

Bu proje açık kaynaklıdır ve topluluk katkılarına açıktır. Katkıda bulunmak için lütfen [KURALLAR.md](docs/requirements/KURALLAR.md) dosyasındaki kod standartlarını inceleyiniz. Proje MIT Lisansı ile dağıtılmaktadır.
