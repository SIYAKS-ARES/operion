using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Companies
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
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
                dt.Columns.Add("FirmaID", typeof(int));
                dt.Columns.Add("FirmaAd", typeof(string));
                dt.Columns.Add("FirmaYetkiliGorev", typeof(string));
                dt.Columns.Add("FirmaYetkiliAdSoyad", typeof(string));
                dt.Columns.Add("FirmaYetkiliTC", typeof(string));
                dt.Columns.Add("FirmaSektor", typeof(string));
                dt.Columns.Add("FirmaTelefon1", typeof(string));
                dt.Columns.Add("FirmaTelefon2", typeof(string));
                dt.Columns.Add("FirmaTelefon3", typeof(string));
                dt.Columns.Add("FirmaMail", typeof(string));
                dt.Columns.Add("FirmaFax", typeof(string));
                dt.Columns.Add("FirmaSehir", typeof(string));
                dt.Columns.Add("FirmaIlce", typeof(string));
                dt.Columns.Add("FirmaAdres", typeof(string));
                dt.Columns.Add("FirmaVergiDairesi", typeof(string));
                dt.Columns.Add("FirmaOzelKod", typeof(string));
                dt.Columns.Add("FirmaOzelKod2", typeof(string));
                dt.Columns.Add("FirmaOzelKod3", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_FIRMALAR", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // FirmaID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // FirmaAd
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // FirmaYetkiliGorev
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // FirmaYetkiliAdSoyad
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // FirmaYetkiliTC
                                    reader.IsDBNull(5) ? "" : reader.GetString(5), // FirmaSektor
                                    reader.IsDBNull(6) ? "" : reader.GetString(6), // FirmaTelefon1
                                    reader.IsDBNull(7) ? "" : reader.GetString(7), // FirmaTelefon2
                                    reader.IsDBNull(8) ? "" : reader.GetString(8), // FirmaTelefon3
                                    reader.IsDBNull(9) ? "" : reader.GetString(9), // FirmaMail
                                    reader.IsDBNull(10) ? "" : reader.GetString(10), // FirmaFax
                                    reader.IsDBNull(11) ? "" : reader.GetString(11), // FirmaSehir
                                    reader.IsDBNull(12) ? "" : reader.GetString(12), // FirmaIlce
                                    reader.IsDBNull(13) ? "" : reader.GetString(13), // FirmaAdres
                                    reader.IsDBNull(14) ? "" : reader.GetString(14), // FirmaVergiDairesi
                                    reader.IsDBNull(15) ? "" : reader.GetString(15), // FirmaOzelKod
                                    reader.IsDBNull(16) ? "" : reader.GetString(16), // FirmaOzelKod2
                                    reader.IsDBNull(17) ? "" : reader.GetString(17) // FirmaOzelKod3
                                );
                            }
                        }
                    }
                }
                grdfirmalar.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grdfirmalar);
                ModernDataGridViewHelper.EnableHoverEffect(grdfirmalar);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            txtfirmaid.Text = "";
            txtfirmaad.Text = "";
            txtfirmaygorev.Text = "";
            txtfirmayetkili.Text = "";
            txtfirmaytc.Text = "";
            txtfirmasektor.Text = "";
            mskfirmatel1.Text = "";
            mskfirmatel2.Text = "";
            mskfirmatel3.Text = "";
            txtfirmamail.Text = "";
            mskfirmafax.Text = "";
            cmbfirmail.Text = "";
            cmbfirmailce.Text = "";
            rchfirmaadres.Text = "";
            txtfirmavergidairesi.Text = "";
            rchfirmakod1.Text = "";
            rchfirmakod2.Text = "";
            rchfirmakod3.Text = "";
        }

        void SehirListele()
        {
            try
            {
                cmbfirmail.Items.Clear();
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SEHIR FROM TBL_ILLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbfirmail.Items.Add(reader.GetString(0));
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

        void kodaciklamalar()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR LIMIT 1", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rchfirmakod1.Text = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kod açıklamaları yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            SehirListele();
            kodaciklamalar();
            
            // Scroll Sync (Helper kullanılarak)
            ModernDataGridViewHelper.AttachCustomScrollbar(grdfirmalar, hScrollBar1);
        }


        private void grdfirmalar_SelectionChanged(object sender, EventArgs e)
        {
            if (grdfirmalar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdfirmalar.SelectedRows[0];
                txtfirmaid.Text = row.Cells["FirmaID"].Value?.ToString() ?? "";
                txtfirmaad.Text = row.Cells["FirmaAd"].Value?.ToString() ?? "";
                txtfirmaygorev.Text = row.Cells["FirmaYetkiliGorev"].Value?.ToString() ?? "";
                txtfirmayetkili.Text = row.Cells["FirmaYetkiliAdSoyad"].Value?.ToString() ?? "";
                txtfirmaytc.Text = row.Cells["FirmaYetkiliTC"].Value?.ToString() ?? "";
                txtfirmasektor.Text = row.Cells["FirmaSektor"].Value?.ToString() ?? "";
                mskfirmatel1.Text = row.Cells["FirmaTelefon1"].Value?.ToString() ?? "";
                mskfirmatel2.Text = row.Cells["FirmaTelefon2"].Value?.ToString() ?? "";
                mskfirmatel3.Text = row.Cells["FirmaTelefon3"].Value?.ToString() ?? "";
                txtfirmamail.Text = row.Cells["FirmaMail"].Value?.ToString() ?? "";
                mskfirmafax.Text = row.Cells["FirmaFax"].Value?.ToString() ?? "";
                cmbfirmail.Text = row.Cells["FirmaSehir"].Value?.ToString() ?? "";
                cmbfirmailce.Text = row.Cells["FirmaIlce"].Value?.ToString() ?? "";
                rchfirmaadres.Text = row.Cells["FirmaAdres"].Value?.ToString() ?? "";
                txtfirmavergidairesi.Text = row.Cells["FirmaVergiDairesi"].Value?.ToString() ?? "";
                rchfirmakod1.Text = row.Cells["FirmaOzelKod"].Value?.ToString() ?? "";
                rchfirmakod2.Text = row.Cells["FirmaOzelKod2"].Value?.ToString() ?? "";
                rchfirmakod3.Text = row.Cells["FirmaOzelKod3"].Value?.ToString() ?? "";
            }
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtfirmaad.Text))
            {
                txtfirmaad.HasError = true;
                txtfirmaad.ErrorMessage = "Firma adı gereklidir";
                txtfirmaad.Focus();
                return;
            }

            // Hataları temizle
            txtfirmaad.HasError = false;

            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_FIRMALAR (FirmaAd,FirmaYetkiliGorev,FirmaYetkiliAdSoyad,FirmaYetkiliTC,FirmaSektor,FirmaTelefon1,FirmaTelefon2,FirmaTelefon3,FirmaMail,FirmaFax,FirmaSehir,FirmaIlce,FirmaAdres,FirmaVergiDairesi) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtfirmaad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtfirmaygorev.Text);
                        cmd.Parameters.AddWithValue("@p3", txtfirmayetkili.Text);
                        cmd.Parameters.AddWithValue("@p4", txtfirmaytc.Text);
                        cmd.Parameters.AddWithValue("@p5", txtfirmasektor.Text);
                        cmd.Parameters.AddWithValue("@p6", mskfirmatel1.Text);
                        cmd.Parameters.AddWithValue("@p7", mskfirmatel2.Text);
                        cmd.Parameters.AddWithValue("@p8", mskfirmatel3.Text);
                        cmd.Parameters.AddWithValue("@p9", txtfirmamail.Text);
                        cmd.Parameters.AddWithValue("@p10", mskfirmafax.Text);
                        cmd.Parameters.AddWithValue("@p11", cmbfirmail.Text);
                        cmd.Parameters.AddWithValue("@p12", cmbfirmailce.Text);
                        cmd.Parameters.AddWithValue("@p13", rchfirmaadres.Text);
                        cmd.Parameters.AddWithValue("@p14", txtfirmavergidairesi.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Firma sisteme eklendi", "Başarılı", 
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
                if (string.IsNullOrEmpty(txtfirmaid.Text))
                {
                    MessageBox.Show("Lütfen silinecek firmayı seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay mesajı
                DialogResult result = MessageBox.Show(
                    $"'{txtfirmaad.Text}' firmasını silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "DELETE FROM TBL_FIRMALAR WHERE FirmaID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtfirmaid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Firma silindi", "Başarılı", 
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
            if (string.IsNullOrEmpty(txtfirmaid.Text))
            {
                MessageBox.Show("Lütfen güncellenecek firmayı seçin", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtfirmaad.Text))
            {
                txtfirmaad.HasError = true;
                txtfirmaad.ErrorMessage = "Firma adı gereklidir";
                txtfirmaad.Focus();
                return;
            }

            // Hataları temizle
            txtfirmaad.HasError = false;

            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_FIRMALAR SET FirmaAd= @p1,FirmaYetkiliGorev= @p2,FirmaYetkiliAdSoyad= @p3,FirmaYetkiliTC= @p4,FirmaSektor= @p5,FirmaTelefon1= @p6,FirmaTelefon2= @p7,FirmaTelefon3= @p8,FirmaMail= @p9,FirmaFax= @p10,FirmaSehir= @p11,FirmaIlce= @p12,FirmaAdres= @p13,FirmaVergiDairesi= @p14,FirmaOzelKod= @p15,FirmaOzelKod2= @p16,FirmaOzelKod3= @p17 WHERE FirmaID = @p18", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtfirmaad.Text);
                        cmd.Parameters.AddWithValue("@p2", txtfirmaygorev.Text);
                        cmd.Parameters.AddWithValue("@p3", txtfirmayetkili.Text);
                        cmd.Parameters.AddWithValue("@p4", txtfirmaytc.Text);
                        cmd.Parameters.AddWithValue("@p5", txtfirmasektor.Text);
                        cmd.Parameters.AddWithValue("@p6", mskfirmatel1.Text);
                        cmd.Parameters.AddWithValue("@p7", mskfirmatel2.Text);
                        cmd.Parameters.AddWithValue("@p8", mskfirmatel3.Text);
                        cmd.Parameters.AddWithValue("@p9", txtfirmamail.Text);
                        cmd.Parameters.AddWithValue("@p10", mskfirmafax.Text);
                        cmd.Parameters.AddWithValue("@p11", cmbfirmail.Text);
                        cmd.Parameters.AddWithValue("@p12", cmbfirmailce.Text);
                        cmd.Parameters.AddWithValue("@p13", rchfirmaadres.Text);
                        cmd.Parameters.AddWithValue("@p14", txtfirmavergidairesi.Text);
                        cmd.Parameters.AddWithValue("@p15", rchfirmakod1.Text);
                        cmd.Parameters.AddWithValue("@p16", rchfirmakod2.Text);
                        cmd.Parameters.AddWithValue("@p17", rchfirmakod3.Text);
                        cmd.Parameters.AddWithValue("@p18", int.Parse(txtfirmaid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Firma bilgisi güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbfirmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbfirmailce.Items.Clear();
                if (cmbfirmail.SelectedIndex >= 0)
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand(
                            "SELECT ILCE FROM TBL_ILCELER WHERE SEHIR = @p1", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", cmbfirmail.SelectedIndex + 1);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cmbfirmailce.Items.Add(reader.GetString(0));
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

        private void Btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
