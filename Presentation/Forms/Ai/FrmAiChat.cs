using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using operion.Application.Services;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Ai
{
    public partial class FrmAiChat : Form
    {
        private RagService _ragService;
        private AiService _aiService;
        private RetrievalService _retrievalService;
        private PromptBuilder _promptBuilder;

        public FrmAiChat()
        {
            InitializeComponent();
            
            // Services - In a real app, use Dependency Injection
            _ragService = new RagService();
            _aiService = new AiService();
            // RetrievalService and PromptBuilder will be initialized after RagService async init
            _promptBuilder = new PromptBuilder();
            
            // Theme setup
            ThemeManager.RegisterForm(this);
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            this.BackColor = DesignSystem.Colors.Background;
            txtInput.BackColor = DesignSystem.Colors.Surface;
            txtInput.ForeColor = DesignSystem.Colors.Text;
            btnSend.BackColor = DesignSystem.Colors.Primary;
            btnSend.ForeColor = System.Drawing.Color.White;
            rtbChat.BackColor = DesignSystem.Colors.Surface;
            rtbChat.ForeColor = DesignSystem.Colors.Text;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessageAsync();
        }

        private async void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await SendMessageAsync();
            }
        }

        private async Task SendMessageAsync()
        {
            string userQuery = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userQuery)) return;

            // 1. UI Update (User Message)
            AppendMessage("Siz", userQuery, true);
            txtInput.Clear();
            btnSend.Enabled = false;

            try
            {
                // 2. Retrieval & Generation Strategy
                // 2. Retrieval & Generation Strategy
                // Önce yerel (hazır) cevapları kontrol et - Hız ve Maliyet için
                string? localResponse = await CheckLocalResponsesAsync(userQuery);
                if (!string.IsNullOrEmpty(localResponse))
                {
                    // Yerel cevap varsa direkt göster, API'ye gitme
                     rtbChat.AppendText($"AI:\n{localResponse}\n{new string('-', 30)}\n\n");
                     rtbChat.ScrollToCaret();
                     return;
                }

                var loadingMsg = AppendMessage("AI", "Düşünüyor...", false);
                
                string finalResponse = "";

                // STRATEGY: Try SQL for quantitative questions
                var trCulture = new System.Globalization.CultureInfo("tr-TR");
                string qLower = userQuery.ToLower(trCulture);

                bool trySql = qLower.Contains("kaç") || 
                              qLower.Contains("listele") || 
                              qLower.Contains("stok") ||
                              qLower.Contains("fiyat") ||
                              qLower.Contains("bakiye") ||
                              qLower.Contains("toplam") ||
                              qLower.Contains("telefon") ||
                              qLower.Contains("mail") ||
                              qLower.Contains("adres") ||
                              qLower.Contains("borç") ||
                              qLower.Contains("alacak") ||
                              qLower.Contains("kim") ||
                              qLower.Contains("nedir") ||
                              qLower.Contains("bilgi");

                if (trySql)
                {
                    // A. SQL Generation (Phase 4)
                    var _sqlService = new SqlGenerationService(_aiService, new DatabaseSchemaService());
                    string? sql = await _sqlService.GenerateSqlAsync(userQuery);
                    
                    if (!string.IsNullOrEmpty(sql) && _sqlService.IsSafeSql(sql))
                    {
                        string dataResult = await _sqlService.ExecuteQueryAsync(sql);
                        
                        // If data found, summarize it
                        if (!dataResult.Contains("Sorgu sonuç döndürmedi"))
                        {
                            string summaryPrompt = $"Kullanıcı Sorusu: {userQuery}\n\nVeritabanı Sonucun: {dataResult}\n\nBu sonucu kullanıcıya doğal dilde özetle.";
                            var summary = await _aiService.SummarizeAsync(summaryPrompt);
                            finalResponse = summary.Content; // SQL detayını gizledik
                        }
                    }
                }

                // If SQL didn't work or wasn't tried, Standard RAG (Phase 3)
                if (string.IsNullOrEmpty(finalResponse))
                {
                    // Get relevant context
                    var contexts = await _retrievalService.RetrieveContextAsync(userQuery);
                
                    // Prompt Building - Include Screen Context
                    string scopedQuery = $"[Aktif Ekran: {_currentContext}] " + userQuery;
                    string prompt = _promptBuilder.BuildRagPrompt(scopedQuery, contexts);
                
                    // Generation (LLM)
                    var response = await _aiService.SummarizeAsync(prompt);
                    finalResponse = response.Content;
                }
                
                // 5. UI Update (AI Response)
                rtbChat.Text = rtbChat.Text.Replace("Düşünüyor...", finalResponse); 
            }
            catch (Exception ex)
            {
                AppendMessage("Sistem", $"Hata: {ex.Message}", false);
            }
            finally
            {
                btnSend.Enabled = true;
                txtInput.Focus();
            }
        }

        private string AppendMessage(string sender, string message, bool isUser)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{sender}:");
            sb.AppendLine(message);
            sb.AppendLine(new string('-', 30));
            sb.AppendLine();
            
            rtbChat.AppendText(sb.ToString());
            rtbChat.ScrollToCaret();
            
            return sb.ToString(); 
        }

        private async Task<string> GetCurrencyRatesAsync()
        {
            try
            {
                // TCMB'den güncel kurları çek
                string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
                var client = new System.Net.Http.HttpClient();
                // XML olduğu için string olarak çekip parse edelim
                var xmlStr = await client.GetStringAsync(url);
                
                var xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(xmlStr);

                string dolar = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling")?.InnerText ?? "Bilgi Yok";
                string euro = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling")?.InnerText ?? "Bilgi Yok";

                return $"Güncel Döviz Kurları (TCMB):\n\n🇺🇸 Dolar (USD): {dolar} TL\n🇪🇺 Euro (EUR): {euro} TL";
            }
            catch (Exception ex)
            {
                return $"Döviz bilgisi alınamadı: {ex.Message}";
            }
        }

         /// <summary>
        /// Basit selamlaşma ve soruları yerel olarak yanıtlar (API tasarrufu ve hız için)
        /// </summary>
        private async Task<string?> CheckLocalResponsesAsync(string query)
        {
            var q = query.ToLower(new System.Globalization.CultureInfo("tr-TR")).Trim();

            // Döviz Kontrolü
            if (q.Contains("dolar ne kadar") || q.Contains("euro ne kadar") || q.Contains("döviz") || q.Contains("kur kaç"))
            {
                return await GetCurrencyRatesAsync();
            }

            // Yetkinlik Soruları
             if (q.Contains("hangi konuda") || q.Contains("neler biliyorsun") || q.Contains("ne biliyorsun"))
            {
                  return "Veritabanınızdaki **Stoklar**, **Müşteriler**, **Personeller** ve **Faturalar** hakkında bilgi sahibiyim.\n\nAyrıca:\n- 'Dolar ne kadar?' diyerek güncel kurları öğrenebilir,\n- 'En çok satan ürün hangisi?' diyerek analiz yaptırabilir,\n- 'Ahmet isimli müşterinin telefonu ne?' gibi nokta atışı sorular sorabilirsiniz.";
            }

            // Sadece selamlaşma ise (örn. uzunluk < 30) cevap ver. 
            // Uzun cümleler içinde "merhaba" geçiyorsa muhtemelen bir soru cümlesidir.
            if (q.Length > 40) return null;

            // Tam eşleşmeler veya içerir kontrolleri
            if (q == "merhaba" || q == "selam" || q.StartsWith("merhaba ") || q.StartsWith("selam "))
                return "Merhaba! Size nasıl yardımcı olabilirim?";

            if (q.Contains("nasılsın") || q.Contains("nasilsin"))
                return "Ben bir yapay zeka asistanıyım, her zaman çalışmaya hazırım! Siz nasılsınız?";

            if (q.Contains("günaydın") || q.Contains("gunaydin"))
                return "Günaydın! Güne başlamak için harika bir zaman.";

            if (q.Contains("iyi akşamlar") || q.Contains("iyi aksamlar"))
                return "İyi akşamlar. Mesai bitse de ben buradayım.";

            // "Teşekkür" ve "İyiyim" kontrolü
            // "Bende iyiyim teşekkürler" gibi cümleleri yakalamak için Contains kullanıyoruz.
            // Ancak "Teşekkürler, stokları listele" gibi durumları engellemek için uzunluk kontrolü şart.
            if (q.Contains("teşekkür") || q.Contains("tesekkur") || q.Contains("sağol") || q.Contains("sagol"))
            {
                 if (q.Length < 40)
                     return "Rica ederim, her zaman yardımcı olmaktan mutluluk duyarım. Bugün size nasıl yardımcı olabilirim?";
            }

            if (q.Contains("iyiyim") || q.Contains("süperim") || q.Contains("harikayım"))
            {
                if (q.Length < 30)
                     return "Bunu duyduğuma sevindim! Bugün size nasıl yardımcı olabilirim?";
            }

            if (q.Contains("kimsin") || q.Contains("adın ne") || q.Contains("sen kimsin"))
            {
               if (q.Length < 35)
                  return "Ben Operion Ticari Otomasyon Asistanıyım. Size verilerinizle ilgili yardımcı olmak için buradayım.";
            }

            if (q.Contains("ne iş yapıyorsun") || q.Contains("ne işe yarıyorsun") || q.Contains("görevin ne") || q.Contains("neler yapabilirsin"))
            {
               if (q.Length < 50)
                  return "Veritabanınızdaki stokları, müşterileri ve faturaları analiz edebilirim. Bana 'Dolar ne kadar?' veya 'En çok satan ürün hangisi?' gibi sorular sorabilirsiniz.";
            }

            return null;
        }

        private string _currentContext = "Genel Bakış";

        public void SetContext(string contextName)
        {
            _currentContext = contextName;
            // Opsiyonel: Kullanıcıya bilgi ver
            // AppendMessage("Sistem", $"Bağlam değiştirildi: {_currentContext}", false); 
        }

        private async void FrmAiChat_Load(object sender, EventArgs e)
        {
            AppendMessage("Sistem", "Operion AI Asistan başlatılıyor...", false);
            btnSend.Enabled = false; // Disable until ready

            try
            {
                await _ragService.InitializeAsync();
                _retrievalService = new RetrievalService(_ragService, _aiService);
                
                 AppendMessage("Sistem", "Operion AI Asistan'a hoş geldiniz. Verilerinizle ilgili sorular sorabilirsiniz.", false);
                 btnSend.Enabled = true;
                 txtInput.Focus();
            }
            catch (Exception ex)
            {
                AppendMessage("Sistem", $"BAŞLATMA HATASI: {ex.Message}\nLütfen internet bağlantınızı ve yapılandırmayı kontrol edin.", false);
                // Log exception if possible
                System.Diagnostics.Debug.WriteLine($"AI Init Error: {ex}");
            }
        }
    }
}
