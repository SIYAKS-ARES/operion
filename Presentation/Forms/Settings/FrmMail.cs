using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using operion.Application.Services;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Settings
{
    public partial class FrmMail : Form
    {
        private readonly AiService _aiService = new AiService();
        private readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private readonly AiResponseParser _aiParser = new AiResponseParser();
        private readonly PiiMaskingService _piiMasking = new PiiMaskingService();
        private readonly AiRateLimiter _rateLimiter = new AiRateLimiter();
        private ParsedEmailTemplate? _currentTemplate;

        public FrmMail()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        public string mail;

        private void FrmMail_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                txtmailadres.Text = mail;
            }

            // Feature flag kontrolü
            bool featureEnabled = bool.Parse(ConfigurationManager.AppSettings["FEATURE_AI_EMAIL_ASSISTANT"] ?? "true");
            if (!featureEnabled)
            {
                // AI Assistant panelini gizle
                if (grpAiAsistan != null)
                {
                    grpAiAsistan.Visible = false;
                    this.ClientSize = new System.Drawing.Size(500, 520);
                }
            }
            else
            {
                // AI Assistant panelini göster (Designer'da gizli kalmış olabilir)
                if (grpAiAsistan != null)
                {
                    grpAiAsistan.Visible = true;
                    this.ClientSize = new System.Drawing.Size(950, 520);
                }
            }

            // Varsayılan değerleri ayarla
            if (cmbScenario != null && cmbScenario.Items.Count > 0)
            {
                cmbScenario.SelectedIndex = 0;
            }
            if (cmbTon != null && cmbTon.Items.Count > 0)
            {
                cmbTon.SelectedIndex = 1; // Nötr
            }
            if (cmbUzunluk != null && cmbUzunluk.Items.Count > 0)
            {
                cmbUzunluk.SelectedIndex = 1; // Orta
            }
        }

        private async void Btngonder_Click(object sender, EventArgs e)
        {
            try
            {
                bool hasError = false;

                if (string.IsNullOrWhiteSpace(txtmailadres.Text) || !txtmailadres.Text.Contains("@"))
                {
                    txtmailadres.HasError = true;
                    txtmailadres.ErrorMessage = "Geçerli bir e-posta girin";
                    hasError = true;
                }
                else
                {
                    txtmailadres.HasError = false;
                }

                if (string.IsNullOrWhiteSpace(txtmailkonu.Text))
                {
                    txtmailkonu.HasError = true;
                    txtmailkonu.ErrorMessage = "Konu zorunludur";
                    hasError = true;
                }
                else
                {
                    txtmailkonu.HasError = false;
                }

                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları düzeltin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                /* 
                // Gerçek mail gönderimi (Mock modu için devre dışı bırakıldı)
                MailMessage message = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                
                // App.config'den SMTP ayarlarını oku
                string smtpHost = ConfigurationManager.AppSettings["SMTP_HOST"] ?? "smtp.live.com";
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"] ?? "587");
                bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTP_ENABLE_SSL"] ?? "true");
                int timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_TIMEOUT_MS"] ?? "50000");
                string fromEmail = ConfigurationManager.AppSettings["SMTP_FROM_EMAIL"] ?? "eposta@hotmail.com";
                string fromName = ConfigurationManager.AppSettings["SMTP_FROM_NAME"] ?? "operion Ticari Otomasyon";
                
                // SMTP şifresini güvenli şekilde al (ENV: prefix desteği ile)
                string smtpPassword = GetSmtpPassword();
                
                istemci.Credentials = new System.Net.NetworkCredential(fromEmail, smtpPassword);
                istemci.Port = smtpPort;
                istemci.Host = smtpHost;
                istemci.EnableSsl = enableSsl;
                istemci.Timeout = timeout;
                
                message.To.Add(txtmailadres.Text);
                message.From = new MailAddress(fromEmail, fromName);
                message.Subject = txtmailkonu.Text;
                message.IsBodyHtml = true;
                message.Body = rchmailmesaj.Text;
                
                istemci.Send(message);
                */

                // Mock gönderim simülasyonu
                await Task.Delay(1000); // 1 saniye bekle
                
                MessageBox.Show("E-posta başarıyla gönderildi (Simülasyon).", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorMsg = $"E-posta gönderme hatası: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\nDetay: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// SMTP şifresini güvenli şekilde alır (ENV: prefix desteği ile)
        /// </summary>
        private string GetSmtpPassword()
        {
            string passwordConfig = ConfigurationManager.AppSettings["SMTP_PASSWORD"] ?? "";
            
            if (passwordConfig.StartsWith("ENV:", StringComparison.Ordinal))
            {
                string envVarName = passwordConfig.Substring(4);
                return Environment.GetEnvironmentVariable(envVarName) ?? "";
            }
            
            return passwordConfig;
        }

        /// <summary>
        /// AI şablon önerisi butonu
        /// </summary>
        private async void btnSablonOner_Click(object sender, EventArgs e)
        {
            await GenerateEmailTemplate();
        }

        /// <summary>
        /// Yeniden üret butonu
        /// </summary>
        private async void btnYenidenUret_Click(object sender, EventArgs e)
        {
            await GenerateEmailTemplate();
        }

        /// <summary>
        /// E-posta şablonu oluşturur
        /// </summary>
        private async Task GenerateEmailTemplate()
        {
            try
            {
                // Feature flag kontrolü
                bool featureEnabled = bool.Parse(ConfigurationManager.AppSettings["FEATURE_AI_EMAIL_ASSISTANT"] ?? "true");
                if (!featureEnabled)
                {
                    MessageBox.Show("AI E-posta Asistanı özelliği devre dışı bırakılmış.", "Bilgi",
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

                // UI güncellemeleri
                btnSablonOner.Enabled = false;
                btnYenidenUret.Enabled = false;
                btnGovdeyeAktar.Enabled = false;
                progressBarAi.Visible = true;
                progressBarAi.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "E-posta şablonu oluşturuluyor...";
                txtOnizleme.Clear();
                cmbKonu.Items.Clear();
                cmbKonu.Enabled = false;
                System.Windows.Forms.Application.DoEvents();

                // Müşteri referansını al ve maskele
                string customerEmail = txtmailadres.Text ?? "";
                string customerReference = "[MÜŞTERİ]";
                if (!string.IsNullOrEmpty(customerEmail))
                {
                    // E-posta adresinden müşteri adını çıkar (basit yaklaşım)
                    if (customerEmail.Contains("@"))
                    {
                        string emailName = customerEmail.Split('@')[0];
                        customerReference = _piiMasking.MaskCustomerReference(emailName);
                    }
                }

                // Senaryo, ton ve uzunluk değerlerini al ve enum'a dönüştür
                EmailScenario scenario = ParseScenario(cmbScenario.SelectedItem?.ToString() ?? "Genel Yanıt");
                EmailTone tone = ParseTone(cmbTon.SelectedItem?.ToString() ?? "Nötr");
                EmailLength length = ParseLength(cmbUzunluk.SelectedItem?.ToString() ?? "Orta");

                // Rate limit kaydı
                _rateLimiter.RecordRequest(userId);

                // Context oluştur
                var context = new EmailTemplateContext
                {
                    Scenario = scenario,
                    Tone = tone,
                    Length = length,
                    CustomerReference = customerReference,
                    ProductsInfo = "", // İsteğe bağlı: Ürün bilgisi eklenebilir
                    AdditionalInfo = "" // İsteğe bağlı: Ek bağlam
                };

                // Prompt oluştur
                string prompt = _promptBuilder.BuildEmailTemplatePrompt(context);

                // AI çağrısı
                var startTime = DateTime.Now;
                var aiResponse = await _aiService.GenerateEmailAsync(prompt);
                var duration = (DateTime.Now - startTime).TotalSeconds;

                // Yanıtı parse et
                _currentTemplate = _aiParser.ParseEmailParts(aiResponse.Content);

                if (!_currentTemplate.ParseSuccess || _currentTemplate.SubjectLines.Count == 0 || string.IsNullOrEmpty(_currentTemplate.EmailBody))
                {
                    // Parse başarısız, ham metni göster
                    txtOnizleme.Text = aiResponse.Content;
                    lblStatus.Text = $"Şablon oluşturuldu (Parse uyarısı) - {duration:F1} saniye - {aiResponse.TotalTokens} token";
                }
                else
                {
                    // Başarılı parse
                    // Konu satırlarını dropdown'a ekle
                    cmbKonu.Items.Clear();
                    foreach (var subject in _currentTemplate.SubjectLines)
                    {
                        cmbKonu.Items.Add(subject);
                    }
                    if (cmbKonu.Items.Count > 0)
                    {
                        cmbKonu.SelectedIndex = 0;
                        cmbKonu.Enabled = true;
                    }

                    // E-posta gövdesini göster
                    txtOnizleme.Text = _currentTemplate.EmailBody;
                    btnYenidenUret.Enabled = true;
                    btnGovdeyeAktar.Enabled = true;
                    lblStatus.Text = $"Şablon başarıyla oluşturuldu - {duration:F1} saniye - {aiResponse.TotalTokens} token";
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
                
                MessageBox.Show($"AI servisi hatası: {errorMessage}\n\n" +
                    "Lütfen internet bağlantınızı ve API anahtarınızı kontrol edin.",
                    "AI Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Hata: " + errorMessage;
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
                btnSablonOner.Enabled = true;
                progressBarAi.Visible = false;
                progressBarAi.Style = ProgressBarStyle.Continuous;
            }
        }

        /// <summary>
        /// Gövdeye aktar butonu
        /// </summary>
        private void btnGovdeyeAktar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentTemplate == null)
                {
                    MessageBox.Show("Önce bir şablon oluşturun.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçili konu satırını al
                string selectedSubject = cmbKonu.SelectedItem?.ToString() ?? "";
                if (string.IsNullOrEmpty(selectedSubject) && _currentTemplate.SubjectLines.Count > 0)
                {
                    selectedSubject = _currentTemplate.SubjectLines[0];
                }

                // Form alanlarına aktar
                if (!string.IsNullOrEmpty(selectedSubject))
                {
                    txtmailkonu.Text = selectedSubject;
                }

                if (!string.IsNullOrEmpty(txtOnizleme.Text))
                {
                    rchmailmesaj.Text = txtOnizleme.Text;
                }

                MessageBox.Show("E-posta formuna aktarıldı.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Aktarma hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Senaryo string'ini enum'a dönüştürür
        /// </summary>
        private EmailScenario ParseScenario(string scenario)
        {
            return scenario switch
            {
                "Teklif" => EmailScenario.Teklif,
                "Teşekkür" => EmailScenario.Tesekkur,
                "Ödeme Hatırlatma" => EmailScenario.OdemeHatirlatma,
                "Teslimat Bilgi" => EmailScenario.TeslimatBilgi,
                "Genel Yanıt" => EmailScenario.GenelYanit,
                _ => EmailScenario.GenelYanit
            };
        }

        /// <summary>
        /// Ton string'ini enum'a dönüştürür
        /// </summary>
        private EmailTone ParseTone(string tone)
        {
            return tone switch
            {
                "Resmi" => EmailTone.Resmi,
                "Nötr" => EmailTone.Notr,
                "Samimi" => EmailTone.Samimi,
                "Acil" => EmailTone.Acil,
                _ => EmailTone.Notr
            };
        }

        /// <summary>
        /// Uzunluk string'ini enum'a dönüştürür
        /// </summary>
        private EmailLength ParseLength(string length)
        {
            return length switch
            {
                "Kısa" => EmailLength.Kisa,
                "Orta" => EmailLength.Orta,
                "Uzun" => EmailLength.Uzun,
                _ => EmailLength.Orta
            };
        }
    }
}

