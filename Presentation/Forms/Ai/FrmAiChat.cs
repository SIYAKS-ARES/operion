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
                // Önce yerel (hazır) cevapları kontrol et - Hız ve Maliyet için
                string localResponse = CheckLocalResponses(userQuery);
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
                bool trySql = userQuery.ToLower().Contains("kaç") || 
                              userQuery.ToLower().Contains("listele") || 
                              userQuery.ToLower().Contains("stok") ||
                              userQuery.ToLower().Contains("fiyat") ||
                              userQuery.ToLower().Contains("bakiye") ||
                              userQuery.ToLower().Contains("toplam");

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
                            finalResponse = $"{summary.Content}\n\n[Teknik detay - SQL]:\n{sql}";
                        }
                    }
                }

                // If SQL didn't work or wasn't tried, Standard RAG (Phase 3)
                if (string.IsNullOrEmpty(finalResponse))
                {
                    // Get relevant context
                    var contexts = await _retrievalService.RetrieveContextAsync(userQuery);
                
                    // Prompt Building
                    string prompt = _promptBuilder.BuildRagPrompt(userQuery, contexts);
                
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

         /// <summary>
        /// Basit selamlaşma ve soruları yerel olarak yanıtlar (API tasarrufu ve hız için)
        /// </summary>
        private string? CheckLocalResponses(string query)
        {
            var q = query.ToLower(new System.Globalization.CultureInfo("tr-TR")).Trim();

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

            // "Teşekkür" kontrolü - Sadece teşekkür ediyorsa yakala
            if (q.StartsWith("teşekkür") || q.StartsWith("tesekkur") || q.StartsWith("sağol") || q.StartsWith("sagol"))
            {
                 // Eğer "Teşekkürler ama stoklar ne durumda?" giib bir şeyse null dön
                 if (q.Length < 25)
                     return "Rica ederim, her zaman yardımcı olmaktan mutluluk duyarım.";
            }

            if (q.Contains("kimsin") || q.Contains("adın ne") || q.Contains("sen kimsin"))
            {
               if (q.Length < 35)
                  return "Ben Operion Ticari Otomasyon Asistanıyım. Size verilerinizle ilgili yardımcı olmak için buradayım.";
            }

            return null;
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
