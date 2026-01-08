using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Settings
{
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("NotID", typeof(int));
                dt.Columns.Add("NotTarih", typeof(string));
                dt.Columns.Add("NotSaat", typeof(string));
                dt.Columns.Add("NotBaslik", typeof(string));
                dt.Columns.Add("NotDetay", typeof(string));
                dt.Columns.Add("NotOlusturan", typeof(string));
                dt.Columns.Add("NotHitap", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_NOTLAR", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // NotID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // NotTarih
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // NotSaat
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // NotBaslik
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // NotDetay
                                    reader.IsDBNull(5) ? "" : reader.GetString(5), // NotOlusturan
                                    reader.IsDBNull(6) ? "" : reader.GetString(6) // NotHitap
                                );
                            }
                        }
                    }
                }
                grdnotlar.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdnotlar);
                ModernDataGridViewHelper.EnableHoverEffect(grdnotlar);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            txtnotlarid.Clear();
            txtnotlarhitap.Clear();
            txtnotlarbaslik.Clear();
            txtnotlarolusturan.Clear();
            msknotlarsaat.Clear();
            msknotlartarih.Clear();
            rchnotlardetay.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void Btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtnotlarbaslik.Text))
                {
                    txtnotlarbaslik.HasError = true;
                    txtnotlarbaslik.ErrorMessage = "Başlık zorunludur";
                    hasError = true;
                }
                else
                {
                    txtnotlarbaslik.HasError = false;
                }
                
                if (string.IsNullOrWhiteSpace(txtnotlarolusturan.Text))
                {
                    txtnotlarolusturan.HasError = true;
                    txtnotlarolusturan.ErrorMessage = "Oluşturan zorunludur";
                    hasError = true;
                }
                else
                {
                    txtnotlarolusturan.HasError = false;
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_NOTLAR (NotTarih,NotSaat,NotBaslik,NotDetay,NotOlusturan,NotHitap) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", msknotlartarih.Text);
                        cmd.Parameters.AddWithValue("@p2", msknotlarsaat.Text);
                        cmd.Parameters.AddWithValue("@p3", txtnotlarbaslik.Text);
                        cmd.Parameters.AddWithValue("@p4", rchnotlardetay.Text);
                        cmd.Parameters.AddWithValue("@p5", txtnotlarolusturan.Text);
                        cmd.Parameters.AddWithValue("@p6", txtnotlarhitap.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Not başarıyla eklendi", "Başarılı", 
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
                if (string.IsNullOrEmpty(txtnotlarid.Text))
                {
                    MessageBox.Show("Lütfen silinecek notu seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silme onayı
                var result = MessageBox.Show(
                    $"'{txtnotlarbaslik.Text}' başlıklı notu silmek istediğinizden emin misiniz?", 
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
                        "DELETE FROM TBL_NOTLAR WHERE NotID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtnotlarid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Not başarıyla silindi", "Başarılı", 
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
                if (string.IsNullOrEmpty(txtnotlarid.Text))
                {
                    MessageBox.Show("Lütfen güncellenecek notu seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtnotlarbaslik.Text))
                {
                    txtnotlarbaslik.HasError = true;
                    txtnotlarbaslik.ErrorMessage = "Başlık zorunludur";
                    hasError = true;
                }
                else
                {
                    txtnotlarbaslik.HasError = false;
                }
                
                if (string.IsNullOrWhiteSpace(txtnotlarolusturan.Text))
                {
                    txtnotlarolusturan.HasError = true;
                    txtnotlarolusturan.ErrorMessage = "Oluşturan zorunludur";
                    hasError = true;
                }
                else
                {
                    txtnotlarolusturan.HasError = false;
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_NOTLAR SET NotTarih = @p1,NotSaat = @p2,NotBaslik = @p3,NotDetay = @p4,NotOlusturan =  @p5,NotHitap = @p6 WHERE NotID = @p7", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", msknotlartarih.Text);
                        cmd.Parameters.AddWithValue("@p2", msknotlarsaat.Text);
                        cmd.Parameters.AddWithValue("@p3", txtnotlarbaslik.Text);
                        cmd.Parameters.AddWithValue("@p4", rchnotlardetay.Text);
                        cmd.Parameters.AddWithValue("@p5", txtnotlarolusturan.Text);
                        cmd.Parameters.AddWithValue("@p6", txtnotlarhitap.Text);
                        cmd.Parameters.AddWithValue("@p7", int.Parse(txtnotlarid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Not başarıyla güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdnotlar_SelectionChanged(object sender, EventArgs e)
        {
            if (grdnotlar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdnotlar.SelectedRows[0];
                txtnotlarid.Text = row.Cells["NotID"].Value?.ToString() ?? "";
                msknotlartarih.Text = row.Cells["NotTarih"].Value?.ToString() ?? "";
                msknotlarsaat.Text = row.Cells["NotSaat"].Value?.ToString() ?? "";
                txtnotlarbaslik.Text = row.Cells["NotBaslik"].Value?.ToString() ?? "";
                rchnotlardetay.Text = row.Cells["NotDetay"].Value?.ToString() ?? "";
                txtnotlarolusturan.Text = row.Cells["NotOlusturan"].Value?.ToString() ?? "";
                txtnotlarhitap.Text = row.Cells["NotHitap"].Value?.ToString() ?? "";
            }
        }

        private void grdnotlar_DoubleClick(object sender, EventArgs e)
        {
            if (grdnotlar.SelectedRows.Count > 0)
            {
                FrmNotDetay frmnotdetay = new FrmNotDetay();
                DataGridViewRow row = grdnotlar.SelectedRows[0];
                frmnotdetay.metin = row.Cells["NotDetay"].Value?.ToString() ?? "";
                frmnotdetay.Show();
            }
        }
    }
}
