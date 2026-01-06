using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// Tema yöneticisi - Light ve Dark mode arasında geçiş yapar
    /// Tüm uygulamadaki formları ve kontrolleri günceller
    /// </summary>
    public static class ThemeManager
    {
        #region Events

        /// <summary>
        /// Tema değiştiğinde tetiklenir
        /// </summary>
        public static event EventHandler? ThemeChanged;

        #endregion

        #region Properties

        private static bool _isDarkMode = false;

        /// <summary>
        /// Dark mode aktif mi?
        /// </summary>
        public static bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    SaveThemePreference();
                    OnThemeChanged();
                }
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Tema sistemini başlatır ve kaydedilmiş tercihi yükler
        /// </summary>
        public static void Initialize()
        {
            LoadThemePreference();
        }

        #endregion

        #region Theme Preference

        /// <summary>
        /// Tema tercihini dosyadan yükler
        /// </summary>
        private static void LoadThemePreference()
        {
            try
            {
                string preferencePath = GetPreferencePath();
                if (File.Exists(preferencePath))
                {
                    string content = File.ReadAllText(preferencePath);
                    _isDarkMode = content.Trim().ToLower() == "dark";
                }
                else
                {
                    // Varsayılan: Light mode
                    _isDarkMode = false;
                }
            }
            catch
            {
                // Hata durumunda varsayılan light mode kullan
                _isDarkMode = false;
            }
        }

        /// <summary>
        /// Tema tercihini dosyaya kaydeder
        /// </summary>
        private static void SaveThemePreference()
        {
            try
            {
                string preferencePath = GetPreferencePath();
                string? directory = Path.GetDirectoryName(preferencePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(preferencePath, _isDarkMode ? "dark" : "light");
            }
            catch
            {
                // Hata durumunda sessizce devam et
            }
        }

        /// <summary>
        /// Tema tercih dosyasının yolunu döndürür
        /// </summary>
        private static string GetPreferencePath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "operion", "theme.config");
        }

        #endregion

        #region Theme Toggle

        /// <summary>
        /// Tema değiştir (Light ↔ Dark)
        /// </summary>
        public static void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
        }

        /// <summary>
        /// Light mode'a geç
        /// </summary>
        public static void SetLightMode()
        {
            IsDarkMode = false;
        }

        /// <summary>
        /// Dark mode'a geç
        /// </summary>
        public static void SetDarkMode()
        {
            IsDarkMode = true;
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Tema değişikliğini bildirir
        /// </summary>
        private static void OnThemeChanged()
        {
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        #endregion

        #region Apply Theme to Controls

        /// <summary>
        /// Forma tema uygular (tüm child control'leri dahil) - GÜVENLİ VERSİYON
        /// </summary>
        /// <summary>
        /// Forma tema uygular (tüm child control'leri dahil) - GÜVENLİ VERSİYON
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            if (form == null || form.IsDisposed) return;

            try 
            {
                // Layout işlemlerini durdur (performans ve görsel düzgünlük için)
                form.SuspendLayout();

                // Form arka planı
                form.BackColor = DesignSystem.Colors.Background;
                form.ForeColor = DesignSystem.Colors.Text;
                if (form.IsMdiChild)
                {
                    form.WindowState = FormWindowState.Maximized;
                }

                // Tüm child control'lere tema uygula
                ApplyThemeToControls(form.Controls);

                // Layout işlemlerini devam ettir
                form.ResumeLayout(false);
                form.PerformLayout();
            }
            catch (Exception ex)
            {
                // Kritik hata oluşursa uygulamayı çökertme, logla
                System.Diagnostics.Debug.WriteLine($"Tema uygulama hatası: {ex.Message}");
                // Hata durumunda da layout'u serbest bırakmaya çalış
                try { form.ResumeLayout(false); } catch { }
            }
        }

        /// <summary>
        /// Control koleksiyonuna tema uygular (recursive) - GÜVENLİ VERSİYON
        /// </summary>
        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            if (controls == null) return;

            // Koleksiyon modified hatasını önlemek için tersten for döngüsü veya kopya kullan
            // Bazı kontroller özellik değişince koleksiyonu güncelleyebilir
            try
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    // Index out of range koruması
                    if (i >= controls.Count) break;
                    
                    Control control = controls[i];
                    if (control == null || control.IsDisposed) continue;

                    ApplyThemeToControl(control);

                    // Alt kontrollere de uygula
                    if (control.HasChildren)
                    {
                        ApplyThemeToControls(control.Controls);
                    }
                }
            }
            catch
            {
                // Koleksiyon erişim hatası olursa yut, kritik değil
            }
        }

        /// <summary>
        /// Tek bir kontrole tema uygular
        /// </summary>
        private static void ApplyThemeToControl(Control control)
        {
            if (control == null || control.IsDisposed) return;

            try
            {
                switch (control)
                {
                    case TabPage tabPage:
                        // TabPage, Panel'den türediği için Panel'den önce kontrol edilmeli
                        tabPage.BackColor = DesignSystem.Colors.Background;
                        tabPage.ForeColor = DesignSystem.Colors.Text;
                        break;

                case Panel panel:
                    panel.BackColor = DesignSystem.Colors.Surface;
                    panel.ForeColor = DesignSystem.Colors.Text;
                    break;

                case GroupBox groupBox:
                    groupBox.BackColor = DesignSystem.Colors.Surface;
                    groupBox.ForeColor = DesignSystem.Colors.Text;
                    break;

                case ComboBox comboBox:
                    comboBox.BackColor = DesignSystem.Colors.Surface;
                    comboBox.ForeColor = DesignSystem.Colors.Text;
                    break;

                case DataGridView dataGridView:
                    dataGridView.BackgroundColor = DesignSystem.Colors.Background;
                    dataGridView.ForeColor = DesignSystem.Colors.Text;
                    dataGridView.GridColor = DesignSystem.Colors.Border;
                    dataGridView.DefaultCellStyle.BackColor = DesignSystem.Colors.Surface;
                    dataGridView.DefaultCellStyle.ForeColor = DesignSystem.Colors.Text;
                    dataGridView.DefaultCellStyle.SelectionBackColor = DesignSystem.Colors.Primary;
                    dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = DesignSystem.Colors.SurfaceHover;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.8f);
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = DesignSystem.Colors.Text;
                    break;

                case Label label:
                    label.ForeColor = DesignSystem.Colors.Text;
                    break;

                case Button button:
                    // Button'lar custom styling alacak, şimdilik basit tema
                    button.ForeColor = DesignSystem.Colors.Text;
                    break;

                case TabControl tabControl:
                    tabControl.BackColor = DesignSystem.Colors.Background;
                    tabControl.ForeColor = DesignSystem.Colors.Text;
                    break;

                case MenuStrip menuStrip:
                    menuStrip.BackColor = DesignSystem.Colors.Primary;
                    menuStrip.ForeColor = Color.White;
                    break;

                case RichTextBox richTextBox:
                    richTextBox.BackColor = DesignSystem.Colors.Surface;
                    richTextBox.ForeColor = DesignSystem.Colors.Text;
                    richTextBox.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case MaskedTextBox maskedTextBox:
                    maskedTextBox.BackColor = DesignSystem.Colors.Surface;
                    maskedTextBox.ForeColor = DesignSystem.Colors.Text;
                    break;

                case ListBox listBox:
                    listBox.BackColor = DesignSystem.Colors.Surface;
                    listBox.ForeColor = DesignSystem.Colors.Text;
                    break;

                case CheckBox checkBox:
                    checkBox.ForeColor = DesignSystem.Colors.Text;
                    break;

                case RadioButton radioButton:
                    radioButton.ForeColor = DesignSystem.Colors.Text;
                    break;

                case MdiClient mdiClient:
                    mdiClient.BackColor = DesignSystem.Colors.Background;
                    break;

                case TextBox textBox:
                    textBox.BackColor = DesignSystem.Colors.Surface;
                    textBox.ForeColor = DesignSystem.Colors.Text;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    break;

                default:
                    // Diğer kontroller için temel tema
                    control.ForeColor = DesignSystem.Colors.Text;
                    if (control.BackColor != Color.Transparent)
                    {
                        control.BackColor = DesignSystem.Colors.Surface;
                    }
                    break;
            }
            }
            catch
            {
                // Kontrol bazlı hataları yut
            }
        }

        #endregion

        #region Auto Apply Theme

        /// <summary>
        /// Form'un Load olayına bağlanarak otomatik tema uygular
        /// </summary>
        public static void RegisterForm(Form form)
        {
            if (form == null) return;

            // Form load olduğunda tema uygula
            form.Load += (s, e) => ApplyTheme(form);

            // Tema değiştiğinde formu güncelle
            ThemeChanged += (s, e) => 
            {
                if (!form.IsDisposed)
                {
                    form.Invoke(new Action(() => ApplyTheme(form)));
                }
            };
        }

        #endregion
    }
}

