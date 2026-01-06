using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Reports
{
    public partial class FrmRaporlar : Form
    {
        private readonly AiService _aiService = new AiService();
        private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private readonly AiResponseParser _aiParser = new AiResponseParser();
        private readonly PiiMaskingService _piiMasking = new PiiMaskingService();
        private readonly AiRateLimiter _rateLimiter = new AiRateLimiter();

        public FrmRaporlar()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        /// <summary>
        /// ReportViewer yerine HTML rapor oluştur (ARM uyumluluk için)
        /// </summary>
        public void GenerateHtmlReport(string reportType)
        {
            try
            {
                string title = GetReportTitle(reportType);
                string htmlContent = ReportViewerHelper.GenerateHtmlReport(reportType, title);
                
                // HTML raporu kaydet ve aç
                string fileName = $"{reportType}_Raporu_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                ReportViewerHelper.SaveHtmlReport(htmlContent, fileName);
                ReportViewerHelper.OpenHtmlReport(htmlContent);

                MessageBox.Show("Rapor oluşturuldu ve görüntülendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rapor oluşturma hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetReportTitle(string reportType)
        {
            return reportType.ToLowerInvariant() switch
            {
                "firmalar" => "Firmalar Raporu",
                "musteriler" => "Müşteriler Raporu",
                "giderler" => "Giderler Raporu",
                "personeller" => "Personeller Raporu",
                _ => "Genel Rapor"
            };
        }

        private void BtnFirmalarRapor_Click(object sender, EventArgs e)
        {
            GenerateHtmlReport("firmalar");
        }

        private void BtnMusterilerRapor_Click(object sender, EventArgs e)
        {
            GenerateHtmlReport("musteriler");
        }

        private void BtnGiderlerRapor_Click(object sender, EventArgs e)
        {
            GenerateHtmlReport("giderler");
        }

        private void BtnPersonellerRapor_Click(object sender, EventArgs e)
        {
            GenerateHtmlReport("personeller");
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            // Feature flag kontrolü
            bool featureEnabled = bool.Parse(ConfigurationManager.AppSettings["FEATURE_AI_REPORT_SUMMARY"] ?? "true");
            if (!featureEnabled)
            {
                // AI Özeti sekmesini gizle
                if (tabPageAiOzet != null && tabControl1.TabPages.Contains(tabPageAiOzet))
                {
                    tabControl1.TabPages.Remove(tabPageAiOzet);
                }
            }
        }

        /// <summary>
        /// Rapor verilerini AI için hazırlar
        /// </summary>
        private string PrepareReportDataForAi(string reportType)
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    string sql = GetReportSql(reportType);
                    using (var command = new SqliteCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        // DataTable'a dönüştür
                        var dataTable = ReportDataFormatter.DataReaderToDataTable(reader, maxRows: 50);
                        
                        // Formatla
                        string formattedData = ReportDataFormatter.FormatReportDataForAi(dataTable, maxRows: 50, maxColumnLength: 50);
                        
                        // PII maskeleme uygula
                        string maskedData = _piiMasking.PrepareReportDataForAi(formattedData, includeCustomerNames: false);
                        
                        return maskedData;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Rapor verisi hazırlanırken hata: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Rapor tipine göre SQL sorgusu döndürür
        /// </summary>
        private string GetReportSql(string reportType)
        {
            return reportType.ToLowerInvariant() switch
            {
                "firmalar" => "SELECT * FROM TBL_FIRMALAR ORDER BY FirmaAd LIMIT 50",
                "musteriler" => "SELECT * FROM TBL_MUSTERILER ORDER BY MusteriAd LIMIT 50",
                "giderler" => "SELECT * FROM TBL_GIDERLER ORDER BY GiderID LIMIT 50",
                "personeller" => "SELECT * FROM TBL_PERSONELLER ORDER BY PersonelAd LIMIT 50",
                _ => throw new ArgumentException($"Bilinmeyen rapor tipi: {reportType}")
            };
        }

        /// <summary>
        /// Seçili rapor tipini döndürür
        /// </summary>
        private string GetSelectedReportType()
        {
            int selectedIndex = tabControl1.SelectedIndex;
            if (selectedIndex == 0) return "musteriler";
            if (selectedIndex == 1) return "firmalar";
            if (selectedIndex == 2) return "giderler";
            if (selectedIndex == 3) return "personeller";
            return "musteriler"; // Varsayılan
        }

        /// <summary>
        /// AI özet üretme butonu
        /// </summary>
        private async void btnOzetUret_Click(object sender, EventArgs e)
        {
            try
            {
                // Feature flag kontrolü
                bool featureEnabled = bool.Parse(ConfigurationManager.AppSettings["FEATURE_AI_REPORT_SUMMARY"] ?? "true");
                if (!featureEnabled)
                {
                    MessageBox.Show("AI Rapor Özeti özelliği devre dışı bırakılmış.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // AI yapılandırma kontrolü
                if (!_aiService.IsConfigured())
                {
                    MessageBox.Show("AI servisi yapılandırılmamış. Lütfen App.config dosyasını kontrol edin.\n\n" +
                        "GEMINI_API_KEY ortam değişkeninin ayarlandığından emin olun.",
                        "Yapılandırma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Rate limit kontrolü
                string userId = "default"; // TODO: Gerçek kullanıcı ID'si
                if (!_rateLimiter.CanMakeRequest(userId))
                {
                    int waitSeconds = (int)Math.Ceiling(_rateLimiter.GetWaitTime(userId).TotalSeconds);
                    MessageBox.Show($"Çok fazla istek gönderildi. Lütfen {waitSeconds} saniye bekleyin.",
                        "Rate Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Rapor tipini al
                string reportType = GetSelectedReportType();
                if (tabControl1.SelectedIndex == 4) // AI Özeti sekmesindeyse, önceki sekmeyi kullan
                {
                    reportType = "musteriler"; // Varsayılan
                }

                // UI güncellemeleri
                btnOzetUret.Enabled = false;
                progressBarAi.Visible = true;
                progressBarAi.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "Rapor verisi hazırlanıyor...";
                txtOzet.Clear();
                txtAksiyon.Clear();
                System.Windows.Forms.Application.DoEvents();

                // Rapor verisini hazırla
                string reportData = PrepareReportDataForAi(reportType);
                
                if (string.IsNullOrWhiteSpace(reportData) || reportData.Contains("Rapor verisi bulunamadı"))
                {
                    MessageBox.Show("Rapor verisi bulunamadı. Önce bir rapor oluşturun.",
                        "Veri Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "AI özeti oluşturuluyor...";
                System.Windows.Forms.Application.DoEvents();

                // Rate limit kaydı
                _rateLimiter.RecordRequest(userId);

                // Prompt oluştur
                var context = new ReportSummaryContext
                {
                    ReportType = GetReportTitle(reportType),
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now,
                    Data = reportData
                };

                string prompt = _promptBuilder.BuildReportSummaryPrompt(context);

                // AI çağrısı
                var startTime = DateTime.Now;
                var aiResponse = await _aiService.SummarizeAsync(prompt);
                var duration = (DateTime.Now - startTime).TotalSeconds;

                // Yanıtı parse et
                var parsed = _aiParser.ParseSummaryAndActions(aiResponse.Content);

                if (!parsed.ParseSuccess)
                {
                    // Parse başarısız, ham metni göster
                    txtOzet.Text = aiResponse.Content;
                    txtAksiyon.Text = "Aksiyon maddeleri otomatik çıkarılamadı. Lütfen özeti kontrol edin.";
                    lblStatus.Text = $"Özet oluşturuldu (Parse uyarısı) - {duration:F1} saniye - {aiResponse.TotalTokens} token";
                }
                else
                {
                    // Başarılı parse
                    txtOzet.Text = string.Join("\n", parsed.SummaryPoints.Select((p, i) => $"• {p}"));
                    txtAksiyon.Text = string.Join("\n", parsed.ActionItems.Select((p, i) => $"{i + 1}. {p}"));
                    lblStatus.Text = $"Özet başarıyla oluşturuldu - {duration:F1} saniye - {aiResponse.TotalTokens} token";
                }

                // AI Özeti sekmesine geç
                if (tabControl1.TabPages.Contains(tabPageAiOzet))
                {
                    tabControl1.SelectedTab = tabPageAiOzet;
                }
            }
            catch (AiServiceException ex)
            {
                string errorMessage = ex.Message;
                if (errorMessage.Contains("Geçersiz API anahtarı") || errorMessage.Contains("Unauthorized"))
                {
                    errorMessage = "Geçersiz Gemini API anahtarı. Lütfen GEMINI_API_KEY ortam değişkenini kontrol edin.";
                }
                else if (errorMessage.Contains("quota") || errorMessage.Contains("Quota") || errorMessage.Contains("Forbidden"))
                {
                    errorMessage = "Gemini API quota aşıldı. Lütfen Google Cloud Console'dan quota durumunuzu kontrol edin.";
                }
                else if (errorMessage.Contains("Rate limit") || errorMessage.Contains("TooManyRequests"))
                {
                    errorMessage = "Gemini API rate limit aşıldı. Lütfen bir süre bekleyip tekrar deneyin.";
                }
                
                string originalError = errorMessage;
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nTeknik Detay: {ex.InnerException.Message}";
                }

                MessageBox.Show($"AI servisi hatası: {errorMessage}\n\n" +
                    "Lütfen internet bağlantınızı ve API anahtarınızı kontrol edin.",
                    "AI Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Hata: " + originalError;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                string errorMessage = ex.Message;
                if (errorMessage.Contains("Geçersiz API anahtarı") || errorMessage.Contains("Unauthorized"))
                {
                    errorMessage = "Geçersiz Gemini API anahtarı. Lütfen GEMINI_API_KEY ortam değişkenini kontrol edin.";
                }
                else if (errorMessage.Contains("quota") || errorMessage.Contains("Quota"))
                {
                    errorMessage = "Gemini API quota aşıldı. Lütfen Google Cloud Console'dan quota durumunuzu kontrol edin.";
                }
                
                MessageBox.Show($"Gemini API hatası: {errorMessage}",
                    "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Hata: " + errorMessage;
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show("AI yanıtı zaman aşımına uğradı. Lütfen tekrar deneyin.",
                    "Zaman Aşımı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Text = "Zaman aşımı hatası";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Hata: " + ex.Message;
            }
            finally
            {
                btnOzetUret.Enabled = true;
                progressBarAi.Visible = false;
                progressBarAi.Style = ProgressBarStyle.Continuous;
            }
        }

        /// <summary>
        /// Özeti panoya kopyala
        /// </summary>
        private void btnKopyalaOzet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOzet.Text))
                {
                    Clipboard.SetText(txtOzet.Text);
                    MessageBox.Show("Özet panoya kopyalandı.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kopyalanacak özet bulunamadı.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Panoya kopyalama hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aksiyon maddelerini panoya kopyala
        /// </summary>
        private void btnKopyalaAksiyon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAksiyon.Text))
                {
                    Clipboard.SetText(txtAksiyon.Text);
                    MessageBox.Show("Aksiyon maddeleri panoya kopyalandı.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kopyalanacak aksiyon maddesi bulunamadı.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Panoya kopyalama hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

