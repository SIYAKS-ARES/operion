using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Customers
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MusteriID", typeof(int));
                dt.Columns.Add("MusteriAd", typeof(string));
                dt.Columns.Add("MusteriSoyad", typeof(string));
                dt.Columns.Add("MusteriTelefon", typeof(string));
                dt.Columns.Add("MusteriTelefon2", typeof(string));
                dt.Columns.Add("MusteriTC", typeof(string));
                dt.Columns.Add("MusteriMail", typeof(string));
                dt.Columns.Add("MusteriSehir", typeof(string));
                dt.Columns.Add("MusteriIlce", typeof(string));
                dt.Columns.Add("MusteriAdres", typeof(string));
                dt.Columns.Add("MusteriVergiDaire", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_MUSTERILER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // MusteriID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // MusteriAd
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // MusteriSoyad
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // MusteriTelefon
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // MusteriTelefon2
                                    reader.IsDBNull(5) ? "" : reader.GetString(5), // MusteriTC
                                    reader.IsDBNull(6) ? "" : reader.GetString(6), // MusteriMail
                                    reader.IsDBNull(7) ? "" : reader.GetString(7), // MusteriSehir
                                    reader.IsDBNull(8) ? "" : reader.GetString(8), // MusteriIlce
                                    reader.IsDBNull(9) ? "" : reader.GetString(9), // MusteriAdres
                                    reader.IsDBNull(10) ? "" : reader.GetString(10) // MusteriVergiDaire
                                );
                            }
                        }
                    }
                }
                grdmusteriler.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grdmusteriler);
                ModernDataGridViewHelper.EnableHoverEffect(grdmusteriler);
                
                // Custom Scrollbar
                ModernDataGridViewHelper.AttachCustomScrollbar(grdmusteriler, hScrollBar1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SehirListele()
        {
            try
            {
                cmbmusteriil.Items.Clear();
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SEHIR FROM TBL_ILLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbmusteriil.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şehir listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            txtmusteriid.Text = "";
            txtmusteriad.Text = "";
            mskmusteritc.Text = "";
            txtmusterisoyad.Text = "";
            mskmusteritel1.Text = "";
            mskmusteritel2.Text = "";
            txtmusterimail.Text = "";
            cmbmusteriil.Text = "";
            cmbmusteriilce.Text = "";
            rchmusteriadres.Text = "";
            txtmusterivergidairesi.Text = "";
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            SehirListele();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtmusteriad.Text))
            {
                txtmusteriad.HasError = true;
                txtmusteriad.ErrorMessage = "Müşteri adı gereklidir";
                txtmusteriad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtmusterisoyad.Text))
            {
                txtmusterisoyad.HasError = true;
                txtmusterisoyad.ErrorMessage = "Müşteri soyadı gereklidir";
                txtmusterisoyad.Focus();
                return;
            }

            // Hataları temizle
            txtmusteriad.HasError = false;
            txtmusterisoyad.HasError = false;

            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_MUSTERILER (MusteriAd,MusteriSoyad,MusteriTelefon,MusteriTelefon2,MusteriTC,MusteriMail,MusteriSehir,MusteriIlce,MusteriAdres,MusteriVergiDaire) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtmusteriad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtmusterisoyad.Text);
                        cmd.Parameters.AddWithValue("@p3", mskmusteritel1.Text);
                        cmd.Parameters.AddWithValue("@p4", mskmusteritel2.Text);
                        cmd.Parameters.AddWithValue("@p5", mskmusteritc.Text);
                        cmd.Parameters.AddWithValue("@p6", txtmusterimail.Text);
                        cmd.Parameters.AddWithValue("@p7", cmbmusteriil.Text);
                        cmd.Parameters.AddWithValue("@p8", cmbmusteriilce.Text);
                        cmd.Parameters.AddWithValue("@p9", rchmusteriadres.Text);
                        cmd.Parameters.AddWithValue("@p10", txtmusterivergidairesi.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Müşteri sisteme eklendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmusteriid.Text))
                {
                    MessageBox.Show("Lütfen silinecek müşteriyi seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay mesajı
                DialogResult result = MessageBox.Show(
                    $"'{txtmusteriad.Text} {txtmusterisoyad.Text}' müşterisini silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "DELETE FROM TBL_MUSTERILER WHERE MusteriID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtmusteriid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Müşteri silindi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrEmpty(txtmusteriid.Text))
            {
                MessageBox.Show("Lütfen güncellenecek müşteriyi seçin", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtmusteriad.Text))
            {
                txtmusteriad.HasError = true;
                txtmusteriad.ErrorMessage = "Müşteri adı gereklidir";
                txtmusteriad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtmusterisoyad.Text))
            {
                txtmusterisoyad.HasError = true;
                txtmusterisoyad.ErrorMessage = "Müşteri soyadı gereklidir";
                txtmusterisoyad.Focus();
                return;
            }

            // Hataları temizle
            txtmusteriad.HasError = false;
            txtmusterisoyad.HasError = false;

            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_MUSTERILER SET MusteriAd = @p1,MusteriSoyad = @p2,MusteriTelefon = @p3,MusteriTelefon2 = @p4,MusteriTC = @p5,MusteriMail = @p6,MusteriSehir = @p7,MusteriIlce = @p8,MusteriVergiDaire =@p9,MusteriAdres =@p10 WHERE MusteriID = @p11", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtmusteriad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtmusterisoyad.Text);
                        cmd.Parameters.AddWithValue("@p3", mskmusteritel1.Text);
                        cmd.Parameters.AddWithValue("@p4", mskmusteritel2.Text);
                        cmd.Parameters.AddWithValue("@p5", mskmusteritc.Text);
                        cmd.Parameters.AddWithValue("@p6", txtmusterimail.Text);
                        cmd.Parameters.AddWithValue("@p7", cmbmusteriil.Text);
                        cmd.Parameters.AddWithValue("@p8", cmbmusteriilce.Text);
                        cmd.Parameters.AddWithValue("@p9", txtmusterivergidairesi.Text);
                        cmd.Parameters.AddWithValue("@p10", rchmusteriadres.Text);
                        cmd.Parameters.AddWithValue("@p11", int.Parse(txtmusteriid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Müşteri bilgisi güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbmusteriil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbmusteriilce.Items.Clear();
                if (cmbmusteriil.SelectedIndex >= 0)
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand(
                            "SELECT ILCE FROM TBL_ILCELER WHERE SEHIR = @p1", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", cmbmusteriil.SelectedIndex + 1);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cmbmusteriilce.Items.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İlçe listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdmusteriler_SelectionChanged(object sender, EventArgs e)
        {
            // DataGridView'deki verileri form kontrollerine yazdırma
            if (grdmusteriler.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdmusteriler.SelectedRows[0];
                txtmusteriid.Text = row.Cells["MusteriID"].Value?.ToString() ?? "";
                txtmusteriad.Text = row.Cells["MusteriAd"].Value?.ToString() ?? "";
                txtmusterisoyad.Text = row.Cells["MusteriSoyad"].Value?.ToString() ?? "";
                mskmusteritel1.Text = row.Cells["MusteriTelefon"].Value?.ToString() ?? "";
                mskmusteritel2.Text = row.Cells["MusteriTelefon2"].Value?.ToString() ?? "";
                mskmusteritc.Text = row.Cells["MusteriTC"].Value?.ToString() ?? "";
                txtmusterimail.Text = row.Cells["MusteriMail"].Value?.ToString() ?? "";
                cmbmusteriil.Text = row.Cells["MusteriSehir"].Value?.ToString() ?? "";
                cmbmusteriilce.Text = row.Cells["MusteriIlce"].Value?.ToString() ?? "";
                txtmusterivergidairesi.Text = row.Cells["MusteriVergiDaire"].Value?.ToString() ?? "";
                rchmusteriadres.Text = row.Cells["MusteriAdres"].Value?.ToString() ?? "";
            }
        }

        private void Btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
