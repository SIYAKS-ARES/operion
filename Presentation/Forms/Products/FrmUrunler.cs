using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Products
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
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
                dt.Columns.Add("UrunID", typeof(int));
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("UrunMarka", typeof(string));
                dt.Columns.Add("UrunModel", typeof(string));
                dt.Columns.Add("UrunYil", typeof(string));
                dt.Columns.Add("UrunAdet", typeof(int));
                dt.Columns.Add("UrunMaliyet", typeof(decimal));
                dt.Columns.Add("UrunSatisFiyat", typeof(decimal));
                dt.Columns.Add("UrunDetay", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_URUNLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // UrunID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // UrunAd
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // UrunMarka
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // UrunModel
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // UrunYil
                                    reader.IsDBNull(5) ? 0 : reader.GetInt32(5), // UrunAdet
                                    reader.IsDBNull(6) ? 0m : reader.GetDecimal(6), // UrunMaliyet
                                    reader.IsDBNull(7) ? 0m : reader.GetDecimal(7), // UrunSatisFiyat
                                    reader.IsDBNull(8) ? "" : reader.GetString(8) // UrunDetay
                                );
                            }
                        }
                    }
                }
                grdurunler.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grdurunler);
                ModernDataGridViewHelper.EnableHoverEffect(grdurunler);
                
                // Custom Scrollbar
                ModernDataGridViewHelper.AttachCustomScrollbar(grdurunler, hScrollBar1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            txturunid.Text = "";
            txturunad.Text = "";
            txturunalisfiyat.Text = "";
            txturunmarka.Text = "";
            txturunmodel.Text = "";
            txturunsatisfiyat.Text = "";
            mskyil.Text = "";
            rchdetay.Text = "";
            nudadet.Value = 0;
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txturunad.Text))
            {
                txturunad.HasError = true;
                txturunad.ErrorMessage = "Ürün adı gereklidir";
                txturunad.Focus();
                return;
            }

            // Hataları temizle
            txturunad.HasError = false;

            try
            {
                // Verileri kaydetme
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_URUNLER (UrunAd,UrunMarka,UrunModel,UrunYil,UrunAdet,UrunMaliyet,UrunSatisFiyat,UrunDetay) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txturunad.Text);
                        cmd.Parameters.AddWithValue("@p2", txturunmarka.Text);
                        cmd.Parameters.AddWithValue("@p3", txturunmodel.Text);
                        cmd.Parameters.AddWithValue("@p4", mskyil.Text);
                        cmd.Parameters.AddWithValue("@p5", (int)nudadet.Value);
                        cmd.Parameters.AddWithValue("@p6", string.IsNullOrWhiteSpace(txturunalisfiyat.Text) ? 0m : decimal.Parse(txturunalisfiyat.Text));
                        cmd.Parameters.AddWithValue("@p7", string.IsNullOrWhiteSpace(txturunsatisfiyat.Text) ? 0m : decimal.Parse(txturunsatisfiyat.Text));
                        cmd.Parameters.AddWithValue("@p8", rchdetay.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ürün sisteme eklendi", "Başarılı", 
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
                if (string.IsNullOrEmpty(txturunid.Text))
                {
                    MessageBox.Show("Lütfen silinecek ürünü seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay mesajı
                DialogResult result = MessageBox.Show(
                    $"'{txturunad.Text}' ürününü silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                // Verileri silme
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "DELETE FROM TBL_URUNLER WHERE UrunID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txturunid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ürün silindi", "Başarılı", 
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

        private void grdurunler_SelectionChanged(object sender, EventArgs e)
        {
            // DataGridView'deki verileri form kontrollerine yazdırma
            if (grdurunler.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdurunler.SelectedRows[0];
                txturunid.Text = row.Cells["UrunID"].Value?.ToString() ?? "";
                txturunad.Text = row.Cells["UrunAd"].Value?.ToString() ?? "";
                txturunmarka.Text = row.Cells["UrunMarka"].Value?.ToString() ?? "";
                txturunmodel.Text = row.Cells["UrunModel"].Value?.ToString() ?? "";
                mskyil.Text = row.Cells["UrunYil"].Value?.ToString() ?? "";
                
                if (row.Cells["UrunAdet"].Value != null && 
                    row.Cells["UrunAdet"].Value.ToString() != null &&
                    int.TryParse(row.Cells["UrunAdet"].Value.ToString()!, out int adet))
                {
                    nudadet.Value = adet;
                }
                
                txturunalisfiyat.Text = row.Cells["UrunMaliyet"].Value?.ToString() ?? "";
                txturunsatisfiyat.Text = row.Cells["UrunSatisFiyat"].Value?.ToString() ?? "";
                rchdetay.Text = row.Cells["UrunDetay"].Value?.ToString() ?? "";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrEmpty(txturunid.Text))
            {
                MessageBox.Show("Lütfen güncellenecek ürünü seçin", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txturunad.Text))
            {
                txturunad.HasError = true;
                txturunad.ErrorMessage = "Ürün adı gereklidir";
                txturunad.Focus();
                return;
            }

            // Hataları temizle
            txturunad.HasError = false;

            try
            {
                // Verileri güncelleme
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_URUNLER SET UrunAd = @p1,UrunMarka = @p2,UrunModel = @p3,UrunYil = @p4,UrunAdet = @p5,UrunMaliyet = @p6,UrunSatisFiyat = @p7,UrunDetay = @p8 WHERE UrunID = @p9", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txturunad.Text);
                        cmd.Parameters.AddWithValue("@p2", txturunmarka.Text);
                        cmd.Parameters.AddWithValue("@p3", txturunmodel.Text);
                        cmd.Parameters.AddWithValue("@p4", mskyil.Text);
                        cmd.Parameters.AddWithValue("@p5", (int)nudadet.Value);
                        cmd.Parameters.AddWithValue("@p6", string.IsNullOrWhiteSpace(txturunalisfiyat.Text) ? 0m : decimal.Parse(txturunalisfiyat.Text));
                        cmd.Parameters.AddWithValue("@p7", string.IsNullOrWhiteSpace(txturunsatisfiyat.Text) ? 0m : decimal.Parse(txturunsatisfiyat.Text));
                        cmd.Parameters.AddWithValue("@p8", rchdetay.Text);
                        cmd.Parameters.AddWithValue("@p9", int.Parse(txturunid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ürün bilgisi güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
