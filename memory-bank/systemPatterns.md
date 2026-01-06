# operion Sistem Mimarisi ve Desenler

## Mimari Yapı

### N-Tier Architecture

```
UI Layer (Windows Forms)
    ↓
Services Layer (Business Logic)
    ↓
    ↓
Data Layer (Entity Framework Core)
    ↓
Database (SQLite)
```

### AI RAG Pipeline
```
User Query -> FrmAiChat
    ↓
RagService (Orchestrator)
    ↓
RetrievalService (Hybrid Search + Re-ranking)
    ↓
Vector Store (Semantic Kernel Memory) / SQL Database
    ↓
LLM (Gemini) -> Answer
```

### Klasör Yapısı

```
operion/
├── Classes/          # Form sınıfları (UI Layer)
├── Models/           # Veri modelleri (Entity Framework)
├── Services/         # İş mantığı servisleri
├── Data/             # Veritabanı context ve konfigürasyon
├── Design/           # Tasarım sistemi ve kontroller
│   ├── Controls/     # Özel kontroller (ModernButton, ModernTextBox, vb.)
│   ├── ThemeManager.cs
│   └── DesignSystem.cs
├── DB/               # SQL script dosyaları
└── Properties/       # Uygulama özellikleri ve kaynaklar
```

## Tasarım Desenleri

### 1. Repository Pattern (Örtülü)
- Entity Framework Core DbContext üzerinden veri erişimi
- `TicariOtomasyonDbContext` merkezi veri erişim noktası

### 2. Service Layer Pattern
- İş mantığı `Services/` klasöründe ayrılmış
- Örnekler: `DatabaseService`, `AiService`, `ReportViewerHelper`

- Özel kontroller `Design/Controls/` klasöründe
- Tema yönetimi `ThemeManager` ile merkezi
- Tasarım sistemi `DesignSystem` ile standartlaştırılmış

### 4. RAG Pattern
- **Ingestion:** Veri hazırlık (`IngestionService`) -> Chunking -> Embedding -> Storage
- **Retrieval:** Arama (`RetrievalService`) -> Vector Search -> Re-ranking -> Context Construction
- **Generation:** Yanıt (`AiService`) -> Prompt Engineering -> LLM -> Response
- **Safeguard:** Güvenlik (`SqlGenerationService.IsSafeSql`, `TokenUsageService`)

### 4. Form Pattern
- **Host:** `FrmAnaModul` (Single Window Host - Panel Embedding)
- **Page:** Child formlar (`FrmUrunler`, vb.) - `TopLevel=false`, `Dock=Fill`, `FormBorderStyle=None`
- **Sidebar:** AI Chat (`FrmAiChat`) - Sağ panelde (`pnlAiSidebar`) yerleşik, Collapsible
- Her form için üç dosya:
  - `FrmXxx.cs` - Kod dosyası
  - `FrmXxx.Designer.cs` - Tasarım dosyası
  - `FrmXxx.resx` - Kaynak dosyası

## Önemli Bileşenler

### Modern UI Kontrolleri

- **ModernButton:** Modern görünümlü buton kontrolü
- **ModernTextBox:** Gelişmiş metin kutusu
- **ModernDataGridViewHelper:** DataGridView yardımcı sınıfı
- **ModernPanel:** Modern panel kontrolü
- **ModernMenuStrip:** Modern menü çubuğu

### Tema Sistemi

- **ThemeManager:** Light/Dark tema yönetimi
- **DesignSystem:** Renk paleti ve tasarım standartları
- **IconHelper:** İkon yönetimi (Fluent Icons)

### Veritabanı Yapısı

- **TicariOtomasyonDbContext:** Entity Framework Core DbContext
- **SQLite:** Yerel veritabanı çözümü
- **Migration:** Code-first yaklaşımı

## Kritik Uygulama Yolları

### Form Yükleme Akışı

1. Form constructor çağrılır
2. `InitializeComponent()` ile UI oluşturulur
3. `Load` event'inde veri yükleme yapılır
4. Modern tema uygulanır (`ThemeManager.ApplyTheme()`)

### Veri İşlemleri

1. Kullanıcı aksiyonu (buton tıklama, vb.)
2. Form event handler tetiklenir
3. Service katmanı çağrılır (gerekirse)
4. DbContext üzerinden veritabanı işlemi
5. UI güncellemesi

### Tema Değiştirme

1. Kullanıcı tema değiştirir (Ayarlar)
2. `ThemeManager.SetTheme()` çağrılır
3. Tüm açık formlara tema uygulanır
4. Ayarlar kaydedilir

## Bileşen İlişkileri

### Form → Service → Data

```
FrmUrunler.cs
    ↓ (kullanır)
DatabaseService
    ↓ (kullanır)
TicariOtomasyonDbContext
    ↓ (erişir)
SQLite Database
```

### Design System → Forms

```
DesignSystem.cs (renkler, stiller)
    ↓ (kullanır)
ThemeManager.cs (tema uygulama)
    ↓ (kullanır)
Modern Controls (ModernButton, vb.)
    ↓ (kullanılır)
Forms (FrmXxx.cs)
```

## Güvenlik ve Performans

### Veri Güvenliği
- Entity Framework Core ile parametreli sorgular (SQL injection koruması)
- PII (Kişisel Bilgi) maskeleme servisi (`PiiMaskingService`)

### Performans Optimizasyonları
- Lazy loading (Entity Framework Core)
- Asenkron işlemler (AI servisleri için)
- Rate limiting (AI servisleri için `AiRateLimiter`)

## Genişletilebilirlik

### Yeni Form Ekleme
1. `Classes/` klasörüne form dosyalarını ekle
2. Modern kontrolleri kullan
3. `ThemeManager` ile tema desteği ekle
4. `DesignSystem` renklerini kullan

### Yeni Servis Ekleme
1. `Services/` klasörüne servis sınıfını ekle
2. Gerekirse `DatabaseService` kullan
3. Dependency injection pattern'i takip et

### Konfigürasyon Yönetimi
- **App.config:** AI ve SMTP ayarları için merkezi konfigürasyon
- **ConfigurationManager:** App.config'den ayar okuma için kullanılır
- **ENV Prefix:** Hassas bilgiler için ortam değişkeni desteği (`ENV:SMTP_PASSWORD`)
- **SMTP:** FrmMail.cs App.config'den SMTP ayarlarını okur
- **AI:** AiService, AiLogger, AiRateLimiter App.config'den AI ayarlarını okur

## Son Güncellemeler (2025-12-09)

### Konfigürasyon İyileştirmeleri
- ✅ SMTP ayarları App.config'e eklendi
- ✅ FrmMail.cs App.config'den SMTP ayarlarını okuyor
- ✅ ENV: prefix desteği eklendi (güvenli credential yönetimi)
- ✅ Test senaryoları dokümanı oluşturuldu (TEST_SENARYOLARI.md)
- ✅ NU1510 uyarısı dokümante edildi (ConfigurationManager paketi kullanılıyor)

## Durum Notu (2025-01-XX)

### Tamamlananlar
- ✅ Tüm 21 form modernize edildi; tema/modern UI tüm katmanda uygulanıyor.
- ✅ WFO1000 designer uyarıları giderildi; terminal build temiz.
- ✅ SMTP konfigürasyonu tamamlandı (App.config + ENV: prefix desteği)
- ✅ Test senaryoları dokümanı hazır (TEST_SENARYOLARI.md)

### Test Öncesi Kalan İşler
- 🔴 Layout sorunları (7 form) - Anchor/Dock/Padding kontrolü gerekli
- 🔴 Dark mode uygulama - ThemeManager.ApplyTheme iyileştirmesi gerekli
- 🔴 Form açılış davranışı - WindowState = Maximized eklenmeli
- 🟠 Veritabanı VIEW sorunları - BankaBilgileri VIEW kontrolü
- 🟠 Dashboard özellikleri - Döviz kurları, haberler, "Fihrist" ismi

### Notlar
- NU1510 (ConfigurationManager) uyarısı görülebilir; kaldırma veya bastırma değerlendirilmelidir.
- AI implementasyonu planlara göre tamamlanmış görünüyor (kod kontrolü gerekli).

