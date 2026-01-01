using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Financial
{
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        void giderlistesi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("GiderID", typeof(int));
                dt.Columns.Add("GiderAy", typeof(string));
                dt.Columns.Add("GiderYil", typeof(string));
                dt.Columns.Add("GiderElektrik", typeof(double));
                dt.Columns.Add("GiderSu", typeof(double));
                dt.Columns.Add("GiderDogalgaz", typeof(double));
                dt.Columns.Add("GiderInternet", typeof(double));
                dt.Columns.Add("GiderMaaslar", typeof(double));
                dt.Columns.Add("GiderEkstra", typeof(double));
                dt.Columns.Add("GiderNotlar", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_GIDERLER", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // GiderID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // GiderAy
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // GiderYil
                                    reader.IsDBNull(3) ? 0.0 : reader.GetDouble(3), // GiderElektrik
                                    reader.IsDBNull(4) ? 0.0 : reader.GetDouble(4), // GiderSu
                                    reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5), // GiderDogalgaz
                                    reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6), // GiderInternet
                                    reader.IsDBNull(7) ? 0.0 : reader.GetDouble(7), // GiderMaaslar
                                    reader.IsDBNull(8) ? 0.0 : reader.GetDouble(8), // GiderEkstra
                                    reader.IsDBNull(9) ? "" : reader.GetString(9) // GiderNotlar
                                );
                            }
                        }
                    }
                }
                grdgiderler.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdgiderler);
                ModernDataGridViewHelper.EnableHoverEffect(grdgiderler);

                // Override to allow horizontal scrolling
                grdgiderler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                
                // Para birimi formatı
                string[] currencyColumns = { "GiderElektrik", "GiderSu", "GiderDogalgaz", "GiderInternet", "GiderMaaslar", "GiderEkstra" };
                foreach (string colName in currencyColumns)
                {
                    if (grdgiderler.Columns[colName] != null)
                    {
                        grdgiderler.Columns[colName].DefaultCellStyle.Format = "C2";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            txtgiderlerid.Clear();
            cmbgiderleray.SelectedIndex = -1;
            cmbgiderleryil.SelectedIndex = -1;
            txtgiderlerelektrik.Clear();
            txtgiderlersu.Clear();
            txtgiderlerdogalgaz.Clear();
            txtgiderlerinternet.Clear();
            txtgiderlermaaslar.Clear();
            txtgiderlerekstra.Clear();
            rchtxtgiderlernotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                bool hasError = false;
                
                if (cmbgiderleray.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen ay seçiniz", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (cmbgiderleryil.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen yıl seçiniz", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Para birimi alanları için validasyon (boş bırakılabilir ama sayı olmalı)
                string[] amountFields = { "txtgiderlerelektrik", "txtgiderlersu", "txtgiderlerdogalgaz", 
                    "txtgiderlerinternet", "txtgiderlermaaslar", "txtgiderlerekstra" };
                
                foreach (string fieldName in amountFields)
                {
                    var field = this.Controls.Find(fieldName, true)[0] as ModernTextBox;
                    if (field != null && !string.IsNullOrWhiteSpace(field.Text))
                    {
                        if (!double.TryParse(field.Text, out _))
                        {
                            field.HasError = true;
                            field.ErrorMessage = "Geçerli bir tutar giriniz";
                            hasError = true;
                        }
                        else
                        {
                            field.HasError = false;
                        }
                    }
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen tutar alanlarını doğru şekilde doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "INSERT INTO TBL_GIDERLER (GiderAy,GiderYil,GiderElektrik,GiderSu,GiderDogalgaz,GiderInternet,GiderMaaslar,GiderEkstra,GiderNotlar) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", cmbgiderleray.Text);
                        cmd.Parameters.AddWithValue("@p2", cmbgiderleryil.Text);
                        cmd.Parameters.AddWithValue("@p3", string.IsNullOrWhiteSpace(txtgiderlerelektrik.Text) ? 0 : decimal.Parse(txtgiderlerelektrik.Text));
                        cmd.Parameters.AddWithValue("@p4", string.IsNullOrWhiteSpace(txtgiderlersu.Text) ? 0 : decimal.Parse(txtgiderlersu.Text));
                        cmd.Parameters.AddWithValue("@p5", string.IsNullOrWhiteSpace(txtgiderlerdogalgaz.Text) ? 0 : decimal.Parse(txtgiderlerdogalgaz.Text));
                        cmd.Parameters.AddWithValue("@p6", string.IsNullOrWhiteSpace(txtgiderlerinternet.Text) ? 0 : decimal.Parse(txtgiderlerinternet.Text));
                        cmd.Parameters.AddWithValue("@p7", string.IsNullOrWhiteSpace(txtgiderlermaaslar.Text) ? 0 : decimal.Parse(txtgiderlermaaslar.Text));
                        cmd.Parameters.AddWithValue("@p8", string.IsNullOrWhiteSpace(txtgiderlerekstra.Text) ? 0 : decimal.Parse(txtgiderlerekstra.Text));
                        cmd.Parameters.AddWithValue("@p9", rchtxtgiderlernotlar.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Giderler başarıyla eklendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistesi();
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
                if (string.IsNullOrEmpty(txtgiderlerid.Text))
                {
                    MessageBox.Show("Lütfen silinecek gideri seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silme onayı
                var result = MessageBox.Show(
                    $"{cmbgiderleray.Text} {cmbgiderleryil.Text} dönemine ait gider kaydını silmek istediğinizden emin misiniz?", 
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
                        "DELETE FROM TBL_GIDERLER WHERE GiderID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtgiderlerid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Gider kaydı başarıyla silindi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistesi();
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
                if (string.IsNullOrEmpty(txtgiderlerid.Text))
                {
                    MessageBox.Show("Lütfen güncellenecek gideri seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation
                bool hasError = false;
                
                if (cmbgiderleray.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen ay seçiniz", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (cmbgiderleryil.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen yıl seçiniz", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Para birimi alanları için validasyon
                string[] amountFields = { "txtgiderlerelektrik", "txtgiderlersu", "txtgiderlerdogalgaz", 
                    "txtgiderlerinternet", "txtgiderlermaaslar", "txtgiderlerekstra" };
                
                foreach (string fieldName in amountFields)
                {
                    var field = this.Controls.Find(fieldName, true)[0] as ModernTextBox;
                    if (field != null && !string.IsNullOrWhiteSpace(field.Text))
                    {
                        if (!double.TryParse(field.Text, out _))
                        {
                            field.HasError = true;
                            field.ErrorMessage = "Geçerli bir tutar giriniz";
                            hasError = true;
                        }
                        else
                        {
                            field.HasError = false;
                        }
                    }
                }
                
                if (hasError)
                {
                    MessageBox.Show("Lütfen tutar alanlarını doğru şekilde doldurun", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_GIDERLER SET GiderAy = @p1,GiderYil = @p2,GiderElektrik = @p3,GiderSu = @p4,GiderDogalgaz = @p5,GiderInternet = @p6,GiderMaaslar = @p7,GiderEkstra = @p8,GiderNotlar =@p9 WHERE GiderID = @p10", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", cmbgiderleray.Text);
                        cmd.Parameters.AddWithValue("@p2", cmbgiderleryil.Text);
                        cmd.Parameters.AddWithValue("@p3", string.IsNullOrWhiteSpace(txtgiderlerelektrik.Text) ? 0 : decimal.Parse(txtgiderlerelektrik.Text));
                        cmd.Parameters.AddWithValue("@p4", string.IsNullOrWhiteSpace(txtgiderlersu.Text) ? 0 : decimal.Parse(txtgiderlersu.Text));
                        cmd.Parameters.AddWithValue("@p5", string.IsNullOrWhiteSpace(txtgiderlerdogalgaz.Text) ? 0 : decimal.Parse(txtgiderlerdogalgaz.Text));
                        cmd.Parameters.AddWithValue("@p6", string.IsNullOrWhiteSpace(txtgiderlerinternet.Text) ? 0 : decimal.Parse(txtgiderlerinternet.Text));
                        cmd.Parameters.AddWithValue("@p7", string.IsNullOrWhiteSpace(txtgiderlermaaslar.Text) ? 0 : decimal.Parse(txtgiderlermaaslar.Text));
                        cmd.Parameters.AddWithValue("@p8", string.IsNullOrWhiteSpace(txtgiderlerekstra.Text) ? 0 : decimal.Parse(txtgiderlerekstra.Text));
                        cmd.Parameters.AddWithValue("@p9", rchtxtgiderlernotlar.Text);
                        cmd.Parameters.AddWithValue("@p10", int.Parse(txtgiderlerid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Gider bilgileri başarıyla güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistesi();
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

        private void grdgiderler_SelectionChanged(object sender, EventArgs e)
        {
            if (grdgiderler.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdgiderler.SelectedRows[0];
                txtgiderlerid.Text = row.Cells["GiderID"].Value?.ToString() ?? "";
                cmbgiderleray.Text = row.Cells["GiderAy"].Value?.ToString() ?? "";
                cmbgiderleryil.Text = row.Cells["GiderYil"].Value?.ToString() ?? "";
                txtgiderlerelektrik.Text = row.Cells["GiderElektrik"].Value?.ToString() ?? "";
                txtgiderlersu.Text = row.Cells["GiderSu"].Value?.ToString() ?? "";
                txtgiderlerdogalgaz.Text = row.Cells["GiderDogalgaz"].Value?.ToString() ?? "";
                txtgiderlerinternet.Text = row.Cells["GiderInternet"].Value?.ToString() ?? "";
                txtgiderlermaaslar.Text = row.Cells["GiderMaaslar"].Value?.ToString() ?? "";
                txtgiderlerekstra.Text = row.Cells["GiderEkstra"].Value?.ToString() ?? "";
                rchtxtgiderlernotlar.Text = row.Cells["GiderNotlar"].Value?.ToString() ?? "";
            }
        }
    }
}
