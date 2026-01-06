using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Financial
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
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
                dt.Columns.Add("BankaID", typeof(int));
                dt.Columns.Add("BankaAd", typeof(string));
                dt.Columns.Add("BankaSube", typeof(string));
                dt.Columns.Add("BankaIBAN", typeof(string));
                dt.Columns.Add("BankaHesapNo", typeof(string));
                dt.Columns.Add("BankaYetkili", typeof(string));
                dt.Columns.Add("BankaTarih", typeof(string));
                dt.Columns.Add("BankaHesapTuru", typeof(string));
                dt.Columns.Add("BankaIl", typeof(string));
                dt.Columns.Add("BankaIlce", typeof(string));
                dt.Columns.Add("BankaTelefon", typeof(string));
                dt.Columns.Add("FirmaID", typeof(int));
                dt.Columns.Add("FirmaAd", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM BankaBilgileri", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader["BankaID"] != DBNull.Value ? Convert.ToInt32(reader["BankaID"]) : 0,
                                    reader["BankaAd"] != DBNull.Value ? reader["BankaAd"].ToString() : "",
                                    reader["BankaSube"] != DBNull.Value ? reader["BankaSube"].ToString() : "",
                                    reader["BankaIBAN"] != DBNull.Value ? reader["BankaIBAN"].ToString() : "",
                                    reader["BankaHesapNo"] != DBNull.Value ? reader["BankaHesapNo"].ToString() : "",
                                    reader["BankaYetkili"] != DBNull.Value ? reader["BankaYetkili"].ToString() : "",
                                    reader["BankaTarih"] != DBNull.Value ? reader["BankaTarih"].ToString() : "",
                                    reader["BankaHesapTuru"] != DBNull.Value ? reader["BankaHesapTuru"].ToString() : "",
                                    reader["BankaIl"] != DBNull.Value ? reader["BankaIl"].ToString() : "",
                                    reader["BankaIlce"] != DBNull.Value ? reader["BankaIlce"].ToString() : "",
                                    reader["BankaTelefon"] != DBNull.Value ? reader["BankaTelefon"].ToString() : "",
                                    reader["FirmaID"] != DBNull.Value ? Convert.ToInt32(reader["FirmaID"]) : 0,
                                    reader["FirmaAd"] != DBNull.Value ? reader["FirmaAd"].ToString() : ""
                                );
                            }
                        }
                    }
                }
                grdbankalar.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdbankalar);
                ModernDataGridViewHelper.EnableHoverEffect(grdbankalar);

                // Override to allow horizontal scrolling
                grdbankalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
                cmbbankail.Items.Clear();
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SEHIR FROM TBL_ILLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbbankail.Items.Add(reader.GetString(0));
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
            txtbankaid.Clear();
            txtbankaad.Clear();
            txtbankasube.Clear();
            txtbankaiban.Clear();
            txtbankahesapno.Clear();
            txtbankayetkili.Clear();
            mskbankatarih.Text = "";
            txtbankahesapturu.Clear();
            cmbbankail.SelectedIndex = -1;
            cmbbankailce.SelectedIndex = -1;
            mskbankatel.Text = "";
            cmbbankabifrma.SelectedIndex = -1;
        }

        void firmalistesi()
        {
            try
            {
                cmbbankabifrma.Items.Clear();
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT FirmaID, FirmaAd FROM TBL_FIRMALAR", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbbankabifrma.Items.Add($"{reader.GetInt32(0)} - {reader.GetString(1)}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Firma listesi yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            SehirListele();
            firmalistesi();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtbankaad.Text))
                {
                    txtbankaad.HasError = true;
                    txtbankaad.ErrorMessage = "Banka adı zorunludur";
                    hasError = true;
                }
                else
                {
                    txtbankaad.HasError = false;
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                int? firmaID = null;
                if (!string.IsNullOrEmpty(cmbbankabifrma.Text))
                {
                    string[] parts = cmbbankabifrma.Text.Split('-');
                    if (parts.Length > 0 && int.TryParse(parts[0].Trim(), out int id) && id > 0)
                    {
                        // FirmaID'nin geçerli olduğunu kontrol et
                        using (var connection = DatabaseService.GetConnection())
                        {
                            using (var checkCmd = new SqliteCommand(
                                "SELECT COUNT(*) FROM TBL_FIRMALAR WHERE FirmaID = @firmaID", 
                                connection))
                            {
                                checkCmd.Parameters.AddWithValue("@firmaID", id);
                                var count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (count > 0)
                                {
                                    firmaID = id;
                                }
                                else
                                {
                                    MessageBox.Show("Seçilen firma bulunamadı. Lütfen geçerli bir firma seçin.", "Uyarı", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_BANKALAR (BankaAd,BankaSube,BankaIBAN,BankaHesapNo,BankaYetkili,BankaTarih,BankaHesapTuru,BankaIl,BankaIlce,BankaTelefon,FirmaID) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtbankaad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtbankasube.Text);
                        cmd.Parameters.AddWithValue("@p3", txtbankaiban.Text);
                        cmd.Parameters.AddWithValue("@p4", txtbankahesapno.Text);
                        cmd.Parameters.AddWithValue("@p5", txtbankayetkili.Text);
                        cmd.Parameters.AddWithValue("@p6", mskbankatarih.Text);
                        cmd.Parameters.AddWithValue("@p7", txtbankahesapturu.Text);
                        cmd.Parameters.AddWithValue("@p8", cmbbankail.Text);
                        cmd.Parameters.AddWithValue("@p9", cmbbankailce.Text);
                        cmd.Parameters.AddWithValue("@p10", mskbankatel.Text);
                        cmd.Parameters.AddWithValue("@p11", firmaID.HasValue ? (object)firmaID.Value : DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Banka başarıyla eklendi", "Başarılı", 
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

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtbankaid.Text))
                {
                    MessageBox.Show("Lütfen güncellenecek bankayı seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtbankaad.Text))
                {
                    txtbankaad.HasError = true;
                    txtbankaad.ErrorMessage = "Banka adı zorunludur";
                    hasError = true;
                }
                else
                {
                    txtbankaad.HasError = false;
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? firmaID = null;
                if (!string.IsNullOrEmpty(cmbbankabifrma.Text))
                {
                    string[] parts = cmbbankabifrma.Text.Split('-');
                    if (parts.Length > 0 && int.TryParse(parts[0].Trim(), out int id) && id > 0)
                    {
                        // FirmaID'nin geçerli olduğunu kontrol et
                        using (var connection = DatabaseService.GetConnection())
                        {
                            using (var checkCmd = new SqliteCommand(
                                "SELECT COUNT(*) FROM TBL_FIRMALAR WHERE FirmaID = @firmaID", 
                                connection))
                            {
                                checkCmd.Parameters.AddWithValue("@firmaID", id);
                                var count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (count > 0)
                                {
                                    firmaID = id;
                                }
                                else
                                {
                                    MessageBox.Show("Seçilen firma bulunamadı. Lütfen geçerli bir firma seçin.", "Uyarı", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_BANKALAR SET BankaAd = @p1,BankaSube = @p2,BankaIBAN = @p3,BankaHesapNo = @p4,BankaYetkili = @p5,BankaTarih = @p6,BankaHesapTuru = @p7,BankaIl = @p8,BankaIlce =@p9,BankaTelefon = @p10, FirmaID = @p11 WHERE BankaID = @p12", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtbankaad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtbankasube.Text);
                        cmd.Parameters.AddWithValue("@p3", txtbankaiban.Text);
                        cmd.Parameters.AddWithValue("@p4", txtbankahesapno.Text);
                        cmd.Parameters.AddWithValue("@p5", txtbankayetkili.Text);
                        cmd.Parameters.AddWithValue("@p6", mskbankatarih.Text);
                        cmd.Parameters.AddWithValue("@p7", txtbankahesapturu.Text);
                        cmd.Parameters.AddWithValue("@p8", cmbbankail.Text);
                        cmd.Parameters.AddWithValue("@p9", cmbbankailce.Text);
                        cmd.Parameters.AddWithValue("@p10", mskbankatel.Text);
                        cmd.Parameters.AddWithValue("@p11", firmaID.HasValue ? (object)firmaID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@p12", int.Parse(txtbankaid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Banka bilgileri başarıyla güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtbankaid.Text))
                {
                    MessageBox.Show("Lütfen silinecek bankayı seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silme onayı
                var result = MessageBox.Show(
                    $"{txtbankaad.Text} isimli bankayı silmek istediğinizden emin misiniz?", 
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
                        "DELETE FROM TBL_BANKALAR WHERE BankaID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtbankaid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Banka başarıyla silindi", "Başarılı", 
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

        private void Btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void grdbankalar_SelectionChanged(object sender, EventArgs e)
        {
            if (grdbankalar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdbankalar.SelectedRows[0];
                txtbankaid.Text = row.Cells["BankaID"].Value?.ToString() ?? "";
                txtbankaad.Text = row.Cells["BankaAd"].Value?.ToString() ?? "";
                txtbankasube.Text = row.Cells["BankaSube"].Value?.ToString() ?? "";
                txtbankaiban.Text = row.Cells["BankaIBAN"].Value?.ToString() ?? "";
                txtbankahesapno.Text = row.Cells["BankaHesapNo"].Value?.ToString() ?? "";
                txtbankayetkili.Text = row.Cells["BankaYetkili"].Value?.ToString() ?? "";
                mskbankatarih.Text = row.Cells["BankaTarih"].Value?.ToString() ?? "";
                txtbankahesapturu.Text = row.Cells["BankaHesapTuru"].Value?.ToString() ?? "";
                cmbbankail.Text = row.Cells["BankaIl"].Value?.ToString() ?? "";
                cmbbankailce.Text = row.Cells["BankaIlce"].Value?.ToString() ?? "";
                mskbankatel.Text = row.Cells["BankaTelefon"].Value?.ToString() ?? "";
                cmbbankabifrma.Text = row.Cells["FirmaID"].Value?.ToString() ?? "";
            }
        }

        private void cmbbankail_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbbankailce.Items.Clear();
                if (cmbbankail.SelectedIndex >= 0)
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand(
                            "SELECT ILCE FROM TBL_ILCELER WHERE SEHIR = @p1", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", cmbbankail.SelectedIndex + 1);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cmbbankailce.Items.Add(reader.GetString(0));
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
    }
}
