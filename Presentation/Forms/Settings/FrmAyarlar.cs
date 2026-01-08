using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Settings
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        void listele()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("KullaniciAd", typeof(string));
            dt.Columns.Add("KullaniciSifre", typeof(string));

            using (var connection = DatabaseService.GetConnection())
            {
                using (var cmd = new SqliteCommand("SELECT * FROM TBL_ADMIN", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dt.Rows.Add(
                                reader.IsDBNull(0) ? "" : reader.GetString(0),
                                reader.IsDBNull(1) ? "" : reader.GetString(1)
                            );
                        }
                    }
                }
            }
            grdayarlar.DataSource = dt;
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            
            // Dinamik 'AI Belleğini Güncelle' Butonu Ekleme
            operion.Presentation.Controls.ModernButton btnRagSync = new operion.Presentation.Controls.ModernButton();
            btnRagSync.Text = "🧠 AI Belleğini Güncelle";
            btnRagSync.Size = new System.Drawing.Size(320, 44);
            btnRagSync.Location = new System.Drawing.Point(90, 330); // Diğer kontrollerle hizalı, overlap önlendi
            btnRagSync.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary; // Or Primary
            btnRagSync.Click += async (s, args) => await BtnRagSync_Click(s, args);
            
            // Eğer varsa panel içine, yoksa form'a ekle. PnlAyarlar var.
            bool addedToPanel = false;
            foreach(Control c in this.Controls) {
                if(c.Name == "pnlAyarlar") {
                   c.Controls.Add(btnRagSync);
                   btnRagSync.BringToFront();
                   addedToPanel = true;
                   break;
                }
            }
            if(!addedToPanel) this.Controls.Add(btnRagSync);

            txtkullanicad.Text = "";
            txtsifre.Text = "";
        }

        private async Task BtnRagSync_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (operion.Presentation.Controls.ModernButton)sender;
                btn.Enabled = false;
                btn.Text = "Güncelleniyor...";

                // Servisleri oluştur (Dependency Injection olmadığı için manuel)
                var aiService = new operion.Application.Services.AiService();
                var ragService = new operion.Application.Services.RagService(apiKey: null); 
                await ragService.InitializeAsync(); 
                
                var ingestionService = new operion.Application.Services.IngestionService(ragService);

                // İşlemi Başlat
                string result = await ingestionService.IngestAllAsync();

                MessageBox.Show(result, "Senkronizasyon Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                var btn = (operion.Presentation.Controls.ModernButton)sender;
                btn.Text = "🧠 AI Belleğini Güncelle";
                btn.Enabled = true;
            }
        }

        private void grdayarlar_SelectionChanged(object sender, EventArgs e)
        {
            if (grdayarlar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdayarlar.SelectedRows[0];
                if (row.DataBoundItem is DataRowView drv)
                {
                    DataRow dr = drv.Row;
                    txtkullanicad.Text = dr["KullaniciAd"]?.ToString() ?? "";
                    txtsifre.Text = dr["KullaniciSifre"]?.ToString() ?? "";
                }
            }
        }

        private void txtkullanicad_TextChanged(object sender, EventArgs e)
        {
            if (txtkullanicad.Text != "")
            {
                Btnislem.Text = "Güncelle";
            }
            else
            {
                Btnislem.Text = "Kaydet";
            }
        }

        private void Btnislem_Click(object sender, EventArgs e)
        {
            try
            {
                bool hasError = false;

                if (string.IsNullOrWhiteSpace(txtkullanicad.Text))
                {
                    txtkullanicad.HasError = true;
                    txtkullanicad.ErrorMessage = "Kullanıcı adı zorunlu";
                    hasError = true;
                }
                else
                {
                    txtkullanicad.HasError = false;
                }

                if (string.IsNullOrWhiteSpace(txtsifre.Text))
                {
                    txtsifre.HasError = true;
                    txtsifre.ErrorMessage = "Şifre zorunlu";
                    hasError = true;
                }
                else
                {
                    txtsifre.HasError = false;
                }

                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Btnislem.Text == "Kaydet")
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand("INSERT INTO TBL_ADMIN (KullaniciAd, KullaniciSifre) VALUES (@p1, @p2)", connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtkullanicad.Text);
                            cmd.Parameters.AddWithValue("@p2", txtsifre.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Kullanıcı Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                }
                if (Btnislem.Text == "Güncelle")
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand("UPDATE TBL_ADMIN SET KullaniciSifre=@p2 WHERE KullaniciAd=@p1", connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtkullanicad.Text);
                            cmd.Parameters.AddWithValue("@p2", txtsifre.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Kullanıcı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                }

                // Başarılı işlem sonrası buton durumunu resetle
                Btnislem.Text = "Kaydet";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

