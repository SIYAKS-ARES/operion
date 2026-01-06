using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Forms.Dashboard;
using System.IO;

namespace operion.Presentation.Forms.Admin
{
    public partial class FrmAdmin : Form
    {
        private System.Windows.Forms.Timer? _fadeInTimer;
        private double _opacity = 0;

        public FrmAdmin()
        {
            InitializeComponent();
            
            // Tema sistemi başlat
            ThemeManager.Initialize();
            ThemeManager.RegisterForm(this);
            
            // Fade-in animasyonu başlat
            this.Opacity = 0;
            _fadeInTimer = new System.Windows.Forms.Timer();
            _fadeInTimer.Interval = 20;
            _fadeInTimer.Tick += FadeInTimer_Tick;
        }

        private void FadeInTimer_Tick(object? sender, EventArgs e)
        {
            _opacity += 0.05;
            if (_opacity >= 1)
            {
                _opacity = 1;
                _fadeInTimer?.Stop();
            }
            this.Opacity = _opacity;
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
            try
            {
                // Varsayılan admin kullanıcısını kontrol et ve ekle
                DatabaseService.EnsureDefaultAdmin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Veritabanı başlatma hatası:\n{ex.Message}\n\n" +
                    "Lütfen veritabanı dosyasını silip uygulamayı yeniden başlatın.\n" +
                    "Dosya konumu: " + DatabaseService.GetDatabasePath(),
                    "Veritabanı Hatası", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            
            // Logo yükle
            LoadLogo();
            
            // Fade-in animasyonu başlat
            _fadeInTimer?.Start();
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(AppContext.BaseDirectory, "logo", "operion-logo.jpg");
                if (File.Exists(logoPath))
                {
                    using (var image = Image.FromFile(logoPath))
                    {
                        // Logo boyutunu ayarla (150x150)
                        pbLogo.Image = new Bitmap(image, new Size(150, 150));
                    }
                }
            }
            catch
            {
                // Logo yüklenemezse sessizce devam et
            }
        }

        private void FrmAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Timer'ı temizle
            _fadeInTimer?.Stop();
            _fadeInTimer?.Dispose();
        }

        private async void BtnGirisYap_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtkullanicad.Text))
            {
                txtkullanicad.HasError = true;
                txtkullanicad.ErrorMessage = "Kullanıcı adı gereklidir";
                txtkullanicad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtsifre.Text))
            {
                txtsifre.HasError = true;
                txtsifre.ErrorMessage = "Şifre gereklidir";
                txtsifre.Focus();
                return;
            }

            // Hataları temizle
            txtkullanicad.HasError = false;
            txtsifre.HasError = false;

            // UI'yı bloklamamak için cursor değişimi ve buton disable
            Cursor = Cursors.WaitCursor;
            BtnGirisYap.Enabled = false;

            try
            {
                // Verify DB exists asynchronously-ish (EnsureDefaultAdmin is sync but fast usually, or check if we can make it async tasks later)
                // For now keep EnsureDefaultAdmin sync as it's a schema check.
                DatabaseService.EnsureDefaultAdmin();
                
                using (var connection = DatabaseService.GetConnection())
                {
                    await connection.OpenAsync(); // Explicitly open async

                    using (var cmd = new SqliteCommand(
                        "SELECT * FROM TBL_ADMIN WHERE KullaniciAd = @p1 AND KullaniciSifre = @p2", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtkullanicad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtsifre.Text);
                        
                        using (var dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                FrmAnaModul frmanamodul = new FrmAnaModul();
                                frmanamodul.kullanici = txtkullanicad.Text;
                                frmanamodul.Show();
                                this.Hide();
                            }
                            else
                            {
                                txtsifre.HasError = true;
                                txtsifre.ErrorMessage = "Kullanıcı adı veya şifre hatalı";
                                txtsifre.SelectAll();
                                txtsifre.Focus();
                            }
                        }
                    }
                }
            }
            catch (SqliteException sqlEx)
            {
                // Tablo yoksa oluşturmayı dene
                if (sqlEx.Message.Contains("no such table", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        DatabaseService.EnsureDefaultAdmin();
                        // Tekrar dene (Async metod olduğu için ve event argümanları aynı olduğundan direkt çağırılabilir)
                        BtnGirisYap_Click(sender, e);
                    }
                    catch (Exception retryEx)
                    {
                        MessageBox.Show(
                            $"Giriş hatası:\n{retryEx.Message}\n\n" +
                            "Veritabanı tablosu oluşturulamadı. Lütfen uygulamayı yeniden başlatın.",
                            "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Giriş hatası: {sqlEx.Message}", 
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giriş hatası: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                BtnGirisYap.Enabled = true;
            }
        }
        
        private void BtnKullaniciEkle_Click(object sender, EventArgs e)
        {
            // Şimdilik basit bir dialog (gelecekte ayrı form yapılabilir)
            MessageBox.Show(
                "Kullanıcı ekleme özelliği ayarlar menüsünden yapılabilir.\n\n" +
                "Varsayılan kullanıcı bilgileri:\n" +
                "Kullanıcı Adı: admin\n" +
                "Şifre: admin",
                "Bilgi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void txtkullanicad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtsifre.Focus();
            }
        }

        private void txtsifre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnGirisYap.PerformClick();
            }
        }
    }
}

