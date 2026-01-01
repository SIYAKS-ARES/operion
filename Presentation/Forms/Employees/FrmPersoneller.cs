using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Employees
{
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
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
                dt.Columns.Add("PersonelID", typeof(int));
                dt.Columns.Add("PersonelAd", typeof(string));
                dt.Columns.Add("PersonelSoyad", typeof(string));
                dt.Columns.Add("PersonelTelefon", typeof(string));
                dt.Columns.Add("PersonelTC", typeof(string));
                dt.Columns.Add("PersonelMail", typeof(string));
                dt.Columns.Add("PersonelSehir", typeof(string));
                dt.Columns.Add("PersonelIlce", typeof(string));
                dt.Columns.Add("PersonelAdres", typeof(string));
                dt.Columns.Add("PersonelGorev", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_PERSONELLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // PersonelID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // PersonelAd
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // PersonelSoyad
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // PersonelTelefon
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // PersonelTC
                                    reader.IsDBNull(5) ? "" : reader.GetString(5), // PersonelMail
                                    reader.IsDBNull(6) ? "" : reader.GetString(6), // PersonelSehir
                                    reader.IsDBNull(7) ? "" : reader.GetString(7), // PersonelIlce
                                    reader.IsDBNull(8) ? "" : reader.GetString(8), // PersonelAdres
                                    reader.IsDBNull(9) ? "" : reader.GetString(9) // PersonelGorev
                                );
                            }
                        }
                    }
                }
                grdpersoneller.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdpersoneller);
                ModernDataGridViewHelper.EnableHoverEffect(grdpersoneller);
                
                // Override to allow horizontal scrolling
                grdpersoneller.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
                cmbpersonelil.Items.Clear();
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SEHIR FROM TBL_ILLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbpersonelil.Items.Add(reader.GetString(0));
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
            txtpersonelid.Clear();
            txtpersonelad.Clear();
            txtpersonelsoyad.Clear();
            mskpersoneltc.Text = "";
            txtpersonelgorev.Clear();
            mskpersoneltel1.Text = "";
            txtpersonelmail.Clear();
            cmbpersonelil.SelectedIndex = -1;
            cmbpersonelilce.SelectedIndex = -1;
            rchpersoneladres.Text = "";
            picPersonelFoto.Image = null;
        }

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            listele();
            SehirListele();
            
            // Custom Scrollbar
            ModernDataGridViewHelper.AttachCustomScrollbar(grdpersoneller, hScrollBar1);
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtpersonelad.Text))
                {
                    txtpersonelad.HasError = true;
                    txtpersonelad.ErrorMessage = "Ad zorunludur";
                    hasError = true;
                }
                else
                {
                    txtpersonelad.HasError = false;
                }
                
                if (string.IsNullOrWhiteSpace(txtpersonelsoyad.Text))
                {
                    txtpersonelsoyad.HasError = true;
                    txtpersonelsoyad.ErrorMessage = "Soyad zorunludur";
                    hasError = true;
                }
                else
                {
                    txtpersonelsoyad.HasError = false;
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                byte[] fotoBytes = null;
                if (picPersonelFoto.Image != null)
                {
                    fotoBytes = ImageToByteArray(picPersonelFoto.Image);
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_PERSONELLER (PersonelAd,PersonelSoyad,PersonelTelefon,PersonelTC,PersonelMail,PersonelSehir,PersonelIlce,PersonelAdres,PersonelGorev,PersonelFoto) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtpersonelad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtpersonelsoyad.Text);
                        cmd.Parameters.AddWithValue("@p3", mskpersoneltel1.Text);
                        cmd.Parameters.AddWithValue("@p4", mskpersoneltc.Text);
                        cmd.Parameters.AddWithValue("@p5", txtpersonelmail.Text);
                        cmd.Parameters.AddWithValue("@p6", cmbpersonelil.Text);
                        cmd.Parameters.AddWithValue("@p7", cmbpersonelilce.Text);
                        cmd.Parameters.AddWithValue("@p8", rchpersoneladres.Text);
                        cmd.Parameters.AddWithValue("@p9", txtpersonelgorev.Text);
                        cmd.Parameters.AddWithValue("@p10", fotoBytes ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Personel başarıyla eklendi", "Başarılı", 
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
                if (string.IsNullOrEmpty(txtpersonelid.Text))
                {
                    MessageBox.Show("Lütfen silinecek personeli seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silme onayı
                var result = MessageBox.Show(
                    $"{txtpersonelad.Text} {txtpersonelsoyad.Text} isimli personeli silmek istediğinizden emin misiniz?", 
                    "Silme Onayı", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    return;
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "DELETE FROM TBL_PERSONELLER WHERE PersonelID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtpersonelid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Personel başarıyla silindi", "Başarılı", 
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
            try
            {
                if (string.IsNullOrEmpty(txtpersonelid.Text))
                {
                    MessageBox.Show("Lütfen güncellenecek personeli seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtpersonelad.Text))
                {
                    txtpersonelad.HasError = true;
                    txtpersonelad.ErrorMessage = "Ad zorunludur";
                    hasError = true;
                }
                else
                {
                    txtpersonelad.HasError = false;
                }
                
                if (string.IsNullOrWhiteSpace(txtpersonelsoyad.Text))
                {
                    txtpersonelsoyad.HasError = true;
                    txtpersonelsoyad.ErrorMessage = "Soyad zorunludur";
                    hasError = true;
                }
                else
                {
                    txtpersonelsoyad.HasError = false;
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] fotoBytes = null;
                if (picPersonelFoto.Image != null)
                {
                    fotoBytes = ImageToByteArray(picPersonelFoto.Image);
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_PERSONELLER SET PersonelAd = @p1,PersonelSoyad = @p2,PersonelTelefon = @p3,PersonelTC = @p4,PersonelMail = @p5,PersonelSehir = @p6,PersonelIlce = @p7,PersonelAdres = @p8,PersonelGorev =@p9,PersonelFoto = @p10 WHERE PersonelID = @p11", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtpersonelad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtpersonelsoyad.Text);
                        cmd.Parameters.AddWithValue("@p3", mskpersoneltel1.Text);
                        cmd.Parameters.AddWithValue("@p4", mskpersoneltc.Text);
                        cmd.Parameters.AddWithValue("@p5", txtpersonelmail.Text);
                        cmd.Parameters.AddWithValue("@p6", cmbpersonelil.Text);
                        cmd.Parameters.AddWithValue("@p7", cmbpersonelilce.Text);
                        cmd.Parameters.AddWithValue("@p8", rchpersoneladres.Text);
                        cmd.Parameters.AddWithValue("@p9", txtpersonelgorev.Text);
                        cmd.Parameters.AddWithValue("@p10", fotoBytes ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p11", int.Parse(txtpersonelid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Personel bilgileri başarıyla güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbpersonelil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbpersonelilce.Items.Clear();
                if (cmbpersonelil.SelectedIndex >= 0)
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand(
                            "SELECT ILCE FROM TBL_ILCELER WHERE SEHIR = @p1", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", cmbpersonelil.SelectedIndex + 1);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cmbpersonelilce.Items.Add(reader.GetString(0));
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

        private void grdpersoneller_SelectionChanged(object sender, EventArgs e)
        {
            if (grdpersoneller.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdpersoneller.SelectedRows[0];
                txtpersonelid.Text = row.Cells["PersonelID"].Value?.ToString() ?? "";
                txtpersonelad.Text = row.Cells["PersonelAd"].Value?.ToString() ?? "";
                txtpersonelsoyad.Text = row.Cells["PersonelSoyad"].Value?.ToString() ?? "";
                mskpersoneltel1.Text = row.Cells["PersonelTelefon"].Value?.ToString() ?? "";
                mskpersoneltc.Text = row.Cells["PersonelTC"].Value?.ToString() ?? "";
                txtpersonelmail.Text = row.Cells["PersonelMail"].Value?.ToString() ?? "";
                cmbpersonelil.Text = row.Cells["PersonelSehir"].Value?.ToString() ?? "";
                cmbpersonelilce.Text = row.Cells["PersonelIlce"].Value?.ToString() ?? "";
                txtpersonelgorev.Text = row.Cells["PersonelGorev"].Value?.ToString() ?? "";
                rchpersoneladres.Text = row.Cells["PersonelAdres"].Value?.ToString() ?? "";
                
                // Fotoğrafı yükle
                LoadPersonelFoto(int.Parse(txtpersonelid.Text));
            }
        }

        /// <summary>
        /// Personel fotoğrafını veritabanından yükler
        /// </summary>
        private void LoadPersonelFoto(int personelID)
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT PersonelFoto FROM TBL_PERSONELLER WHERE PersonelID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", personelID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                byte[] fotoBytes = (byte[])reader.GetValue(0);
                                picPersonelFoto.Image = ByteArrayToImage(fotoBytes);
                            }
                            else
                            {
                                picPersonelFoto.Image = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fotoğraf yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                picPersonelFoto.Image = null;
            }
        }

        /// <summary>
        /// Image'i byte array'e dönüştürür
        /// </summary>
        private byte[]? ImageToByteArray(Image image)
        {
            try
            {
                if (image == null)
                    return null;
                    
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fotoğraf dönüştürme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        /// <summary>
        /// Byte array'i Image'e dönüştürür
        /// </summary>
        private Image? ByteArrayToImage(byte[] byteArray)
        {
            try
            {
                if (byteArray == null || byteArray.Length == 0)
                    return null;
                    
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fotoğraf dönüştürme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        /// <summary>
        /// Fotoğraf yükleme butonu
        /// </summary>
        private void btnFotoYukle_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*";
                    ofd.FilterIndex = 1;
                    ofd.RestoreDirectory = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = ofd.FileName;
                        picPersonelFoto.Image = Image.FromFile(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fotoğraf yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
