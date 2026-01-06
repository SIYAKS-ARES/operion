# Operion RAG (Retrieval-Augmented Generation) Stratejisi v2.0

## 1. Yönetici Özeti (Executive Summary)
Bu belge, Operion projesine **Kurumsal Seviyede (Production-Grade)** RAG yetenekleri kazandırmak için gereken teknik mimariyi, güvenlik protokollerini ve implementasyon detaylarını kapsar. Basit bir "sorgula-cevapla" yapısının ötesinde, **Hybrid Search**, **Re-ranking**, **Role-Based Access Control (RBAC)** ve **Evaluation** süreçlerini içeren bütüncül bir strateji hedeflenmiştir.

## 2. Mimari Genel Bakış

RAG sistemi iki ana boru hattından (pipeline) oluşur:

```mermaid
graph TD
    subgraph Ingestion["Veri Hazırlık (Ingestion Pipeline)"]
        Raw[Ham Veri (PDF, SQL, MD)] --> Chunking[Akıllı Parçalama (Chunking)]
        Chunking --> EmbeddingModel[Embedding Model (Gecko/OpenAI/Local)]
        EmbeddingModel --> VectorDB[(Vektör Veritabanı)]
    end

    subgraph Retrieval["Sorgu ve Yanıt (Inference Pipeline)"]
        UserQuery[Kullanıcı Sorusu] --> QueryExp[Sorgu Genişletme]
        QueryExp --> HybridSearch{Hibrit Arama}
        HybridSearch -->|Vektör (Semantik)| VectorDB
        HybridSearch -->|Keyword (BM25)| FullTextSearch[Full-Text Index]
        
        VectorDB & FullTextSearch --> Reranker[Re-ranking]
        Reranker --> TopK[En İyi 5 Bağlam]
        TopK --> ContextConst[Prompt İnşası]
        ContextConst --> LLM[LLM (Gemini)]
        LLM --> Answer[Nihai Yanıt]
    end
```

## 3. Veri Hazırlık Stratejisi (Ingestion)

### A. Parçalama (Chunking) Stratejisi
Basit satır tabanlı bölme yerine **Semantik Chunking** kullanılacaktır.
- **Dökümanlar (Markdown/Help):**
  - Strateji: `RecursiveCharacterTextSplitter`.
  - Boyut: 512 Token.
  - Örtüşme (Overlap): 50 Token (Bağlam kopukluğunu önlemek için).
  - *Neden?* Yardım dökümanları genelde konu başlıklarına göre ayrılır, bu boyut bir konuyu kapsamak için idealdir.
- **Veritabanı Kayıtları (SQL Rows):**
  - Strateji: Kayıt bazlı serileştirme (Row serialization).
  - Format: `Müşteri Adı: X, Bakiye: Y, Son İşlem: Z` formatında metne çevrilip embedding alınır.
  - *Öneri:* Sadece "Özet" alanları vektörleştirilmeli, ham sayısal veri (fatura tutarları) Text-to-SQL ile sorgulanmalıdır.

### B. Embedding Model Seçimi
Proje .NET olduğu için ve bulut bağımlılığını yönetmek adına:
1. **Google Gemini Embedding API (`models/embedding-001`):** Mevcut API key ile çalışır, yüksek performanslı. **(Önerilen)**
2. **Local ONNX (all-MiniLM-L6-v2):** Çevrimdışı çalışma gereksinimi varsa. (Ekstra boyut: ~100MB).

### C. Vektör Veritabanı (Vector Store)
Windows Forms uygulamasının dağıtım kolaylığı kritiktir.
1. **SQLite-vss (veya benzeri):** SQLite üzerine vektör eklentisi. Dağıtımı en kolayı.
2. **Qdrant (Docker):** En performanslısı ama kullanıcı makinesinde Docker gerektirir. (V1 için aşırı yük olabilir).
3. **In-Memory (Volatile):** Döküman seti küçükse (<1000 sayfa) en hızlı ve masrafsız çözüm. Uygulama her açılışta index'i yeniden yükler veya dosya sisteminden binary olarak okur.
   - **Karar:** Başlangıç için **In-Memory + Disk Persistency** (Semantic Kernel File Store) kullanılacak. Veri büyürse SQLite vektör eklentisine geçilecek.

## 4. Gelişmiş Erişim Stratejisi (Retrieval)

### A. Hibrit Arama (Hybrid Search)
Sadece vektör araması, "Fatura No: 12345" gibi kesin (exact match) aramalarda başarısız olabilir.
- **Vektör Araması:** "Faturamı nasıl iptal ederim?" (Kavramsal).
- **Keyword Araması:** "12345 nolu fatura" (Kesin).
- **Strateji:** İki sonuç kümesini ağırlıklı olarak birleştir (Reciprocal Rank Fusion - RRF).

### B. Re-ranking (Opsiyonel - Faz 2)
Vektör aramasından dönen ilk 20 sonuç, daha "akıllı" ama yavaş bir model (Cross-Encoder) ile yeniden sıralanarak en alakalı 5 tanesi seçilir. Bu, yanıt kalitesini ciddi oranda artırır.

## 5. Güvenlik ve Gizlilik (Security & Privacy)

Kurumsal yazılımlarda en kritik maddedir.

### A. Veri Maskeleme (PII Masking)
LLM'e veya Embedding API'ye gönderilmeden önce kişisel veriler maskelenmelidir.
- Telefon, T.C. Kimlik, E-posta desenleri Regex ile yakalanıp `[TEL]`, `[EMAIL]` olarak değiştirilmelidir.
- Sistemimizde zaten `PiiMaskingService` mevcuttur, bu RAG pipeline'ına entegre edilecektir.

### B. Yetkilendirme (RBAC)
Kullanıcı A, Kullanıcı B'nin oluşturduğu "Özel Notları" ARATMAMALIDIR.
- **Metadata Filtering:** Her chunk, oluşturulurken metadata ile etiketlenir (`{ "user_id": 5, "role": "sales" }`).
- **Sorgu Anı:** Vektör sorgusu atılırken filtre eklenir: `WHERE user_id = 5 OR is_public = true`.

## 6. Değerlendirme ve Test (Evaluation)

RAG sisteminin kalitesi hislerle değil, metriklerle ölçülmelidir.

### A. Golden Dataset
Operion için 50 soruluk "Soru - Beklenen Cevap - Beklenen Döküman ID" seti oluşturulacaktır.

### B. Otomatik Metrikler (RAGAS Yaklaşımı)
1. **Context Precision:** Getirilen dökümanlar soruyla ne kadar alakalı?
2. **Answer Faithfulness:** Üretilen cevap, sadece verilen bağlama mı dayanıyor (Yoksa halüsinasyon mu görüyor)?

## 7. Maliyet Analizi (Cost Estimation)

- **Embedding:** Çok ucuzdur. (Tüm dökümantasyon indexleme maliyeti genelde kuruşlar mertebesindedir).
- **Generation:** Giriş tokenları (Query + Retrieved Context) maliyeti oluşturur.
- **Optimizasyon:**
  - Context window'u gereksiz doldurmamak (Top-K = 5 yeterli).
  - Sık sorulan soruları Cache'lemek (Semantic Caching).

## 8. Uygulama Planı (Revize Edilmiş)

1. **Semantic Kernel Kurulumu:** Nuget paketleri.
2. **Ingestion Modülü:** Markdown dosyalarını okuyup, parçalayıp, embedding alıp `vector_store.json` olarak kaydeden bir "Indexer" görevi.
3. **Retrieval Modülü:** Kullanıcı sorusunu alıp, embedding alıp, `vector_store` içinde Cosine Similarity ile arayan servis.
4. **Guardrails:** Prompt içinde "Bilmiyorsan uydurma" talimatları ve PII filtresi entegrasyonu.
