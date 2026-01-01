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
            txtkullanicad.Text = "";
            txtsifre.Text = "";
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

