using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Invoices
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
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
                dt.Columns.Add("FaturaID", typeof(int));
                dt.Columns.Add("FaturaSeri", typeof(string));
                dt.Columns.Add("FaturaSiraNo", typeof(string));
                dt.Columns.Add("FaturaTarih", typeof(string));
                dt.Columns.Add("FaturaSaat", typeof(string));
                dt.Columns.Add("FaturaVergiDairesi", typeof(string));
                dt.Columns.Add("FaturaAlici", typeof(string));
                dt.Columns.Add("FaturaTeslimEden", typeof(string));
                dt.Columns.Add("FaturaTeslimAlan", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_FATURABILGI", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // FaturaID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // FaturaSeri
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // FaturaSiraNo
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // FaturaTarih
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // FaturaSaat
                                    reader.IsDBNull(5) ? "" : reader.GetString(5), // FaturaVergiDairesi
                                    reader.IsDBNull(6) ? "" : reader.GetString(6), // FaturaAlici
                                    reader.IsDBNull(7) ? "" : reader.GetString(7), // FaturaTeslimEden
                                    reader.IsDBNull(8) ? "" : reader.GetString(8) // FaturaTeslimAlan
                                );
                            }
                        }
                    }
                }
                grdfaturalar.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdfaturalar);
                ModernDataGridViewHelper.EnableHoverEffect(grdfaturalar);

                // Override to allow horizontal scrolling
                grdfaturalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            txtfaturaseri.Clear();
            txtfaturasirano.Clear();
            mskfaturatarih.Text = "";
            mskfaturasaat.Text = "";
            txtfaturavergidairesi.Clear();
            txtfaturaalici.Clear();
            txtfaturateden.Clear();
            txtfaturatalan.Clear();
            txtfaturaurunad.Clear();
            txtfaturaurunfiyat.Clear();
            txtfaturaurunid.Clear();
            txtfaturaurunmiktar.Clear();
            txtfaturauruntutar.Clear();
            txtfaturafaturaid.Clear();
            txtfaturaid.Clear();
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtfaturafaturaid.Text))
                {
                    // Yeni fatura bilgisi ekle
                    // Validation
                    bool hasError = false;
                    
                    if (string.IsNullOrWhiteSpace(txtfaturaseri.Text))
                    {
                        txtfaturaseri.HasError = true;
                        txtfaturaseri.ErrorMessage = "Seri zorunludur";
                        hasError = true;
                    }
                    else
                    {
                        txtfaturaseri.HasError = false;
                    }
                    
                    if (string.IsNullOrWhiteSpace(txtfaturasirano.Text))
                    {
                        txtfaturasirano.HasError = true;
                        txtfaturasirano.ErrorMessage = "Sıra No zorunludur";
                        hasError = true;
                    }
                    else
                    {
                        txtfaturasirano.HasError = false;
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
                            "INSERT INTO TBL_FATURABILGI (FaturaSeri,FaturaSiraNo,FaturaTarih,FaturaSaat,FaturaVergiDairesi,FaturaAlici,FaturaTeslimEden,FaturaTeslimAlan) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtfaturaseri.Text);
                            cmd.Parameters.AddWithValue("@p2", txtfaturasirano.Text);
                            cmd.Parameters.AddWithValue("@p3", mskfaturatarih.Text);
                            cmd.Parameters.AddWithValue("@p4", mskfaturasaat.Text);
                            cmd.Parameters.AddWithValue("@p5", txtfaturavergidairesi.Text);
                            cmd.Parameters.AddWithValue("@p6", txtfaturaalici.Text);
                            cmd.Parameters.AddWithValue("@p7", txtfaturateden.Text);
                            cmd.Parameters.AddWithValue("@p8", txtfaturatalan.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Fatura bilgisi başarıyla eklendi", "Başarılı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
                else
                {
                    // Fatura detay ekle
                    // Validation
                    bool hasError = false;
                    
                    if (string.IsNullOrWhiteSpace(txtfaturaurunad.Text))
                    {
                        txtfaturaurunad.HasError = true;
                        txtfaturaurunad.ErrorMessage = "Ürün adı zorunludur";
                        hasError = true;
                    }
                    else
                    {
                        txtfaturaurunad.HasError = false;
                    }
                    
                    if (string.IsNullOrWhiteSpace(txtfaturaurunmiktar.Text) || !int.TryParse(txtfaturaurunmiktar.Text, out _))
                    {
                        txtfaturaurunmiktar.HasError = true;
                        txtfaturaurunmiktar.ErrorMessage = "Geçerli bir miktar giriniz";
                        hasError = true;
                    }
                    else
                    {
                        txtfaturaurunmiktar.HasError = false;
                    }
                    
                    if (string.IsNullOrWhiteSpace(txtfaturaurunfiyat.Text) || !double.TryParse(txtfaturaurunfiyat.Text, out _))
                    {
                        txtfaturaurunfiyat.HasError = true;
                        txtfaturaurunfiyat.ErrorMessage = "Geçerli bir fiyat giriniz";
                        hasError = true;
                    }
                    else
                    {
                        txtfaturaurunfiyat.HasError = false;
                    }
                    
                    if (hasError)
                    {
                        MessageBox.Show("Lütfen zorunlu alanları doğru şekilde doldurun", "Uyarı", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Tutar hesapla
                    if (double.TryParse(txtfaturaurunfiyat.Text, out double fiyat) && 
                        int.TryParse(txtfaturaurunmiktar.Text, out int miktar))
                    {
                        double tutar = fiyat * miktar;
                        txtfaturauruntutar.Text = tutar.ToString("F2");
                    }
                    
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand(
                            "INSERT INTO TBL_FATURADETAY (UrunAd,Miktar,Fiyat,Tutar,FaturaID) VALUES (@p1,@p2,@p3,@p4,@p5)", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtfaturaurunad.Text);
                            cmd.Parameters.AddWithValue("@p2", int.Parse(txtfaturaurunmiktar.Text));
                            cmd.Parameters.AddWithValue("@p3", double.Parse(txtfaturaurunfiyat.Text));
                            cmd.Parameters.AddWithValue("@p4", double.Parse(txtfaturauruntutar.Text));
                            cmd.Parameters.AddWithValue("@p5", int.Parse(txtfaturafaturaid.Text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Fatura detay başarıyla eklendi", "Başarılı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    // Ürün detay alanlarını temizle
                    txtfaturaurunad.Clear();
                    txtfaturaurunmiktar.Clear();
                    txtfaturaurunfiyat.Clear();
                    txtfaturauruntutar.Clear();
                    txtfaturaurunid.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdfaturalar_SelectionChanged(object sender, EventArgs e)
        {
            if (grdfaturalar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdfaturalar.SelectedRows[0];
                txtfaturaid.Text = row.Cells["FaturaID"].Value?.ToString() ?? "";
                txtfaturaseri.Text = row.Cells["FaturaSeri"].Value?.ToString() ?? "";
                txtfaturasirano.Text = row.Cells["FaturaSiraNo"].Value?.ToString() ?? "";
                mskfaturatarih.Text = row.Cells["FaturaTarih"].Value?.ToString() ?? "";
                mskfaturasaat.Text = row.Cells["FaturaSaat"].Value?.ToString() ?? "";
                txtfaturavergidairesi.Text = row.Cells["FaturaVergiDairesi"].Value?.ToString() ?? "";
                txtfaturaalici.Text = row.Cells["FaturaAlici"].Value?.ToString() ?? "";
                txtfaturateden.Text = row.Cells["FaturaTeslimEden"].Value?.ToString() ?? "";
                txtfaturatalan.Text = row.Cells["FaturaTeslimAlan"].Value?.ToString() ?? "";
                txtfaturafaturaid.Text = row.Cells["FaturaID"].Value?.ToString() ?? "";
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtfaturaid.Text))
                {
                    MessageBox.Show("Lütfen silinecek faturayı seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silme onayı
                var result = MessageBox.Show(
                    $"Fatura Seri: {txtfaturaseri.Text}, Sıra No: {txtfaturasirano.Text} olan faturayı silmek istediğinizden emin misiniz?", 
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
                        "DELETE FROM TBL_FATURABILGI WHERE FaturaID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", int.Parse(txtfaturaid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Fatura başarıyla silindi", "Başarılı", 
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

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtfaturaid.Text))
                {
                    MessageBox.Show("Lütfen güncellenecek faturayı seçin", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation
                bool hasError = false;
                
                if (string.IsNullOrWhiteSpace(txtfaturaseri.Text))
                {
                    txtfaturaseri.HasError = true;
                    txtfaturaseri.ErrorMessage = "Seri zorunludur";
                    hasError = true;
                }
                else
                {
                    txtfaturaseri.HasError = false;
                }
                
                if (string.IsNullOrWhiteSpace(txtfaturasirano.Text))
                {
                    txtfaturasirano.HasError = true;
                    txtfaturasirano.ErrorMessage = "Sıra No zorunludur";
                    hasError = true;
                }
                else
                {
                    txtfaturasirano.HasError = false;
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
                        "UPDATE TBL_FATURABILGI SET FaturaSeri =@p1,FaturaSiraNo=@p2,FaturaTarih=@p3,FaturaSaat=@p4,FaturaVergiDairesi=@p5,FaturaAlici=@p6,FaturaTeslimEden=@p7,FaturaTeslimAlan=@p8 WHERE FaturaID = @p9", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtfaturaseri.Text);
                        cmd.Parameters.AddWithValue("@p2", txtfaturasirano.Text);
                        cmd.Parameters.AddWithValue("@p3", mskfaturatarih.Text);
                        cmd.Parameters.AddWithValue("@p4", mskfaturasaat.Text);
                        cmd.Parameters.AddWithValue("@p5", txtfaturavergidairesi.Text);
                        cmd.Parameters.AddWithValue("@p6", txtfaturaalici.Text);
                        cmd.Parameters.AddWithValue("@p7", txtfaturateden.Text);
                        cmd.Parameters.AddWithValue("@p8", txtfaturatalan.Text);
                        cmd.Parameters.AddWithValue("@p9", int.Parse(txtfaturaid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Fatura bilgileri başarıyla güncellendi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdfaturalar_DoubleClick(object sender, EventArgs e)
        {
            if (grdfaturalar.SelectedRows.Count > 0)
            {
                FrmFaturaUrunDetay frmfaturaurundetay = new FrmFaturaUrunDetay();
                DataGridViewRow row = grdfaturalar.SelectedRows[0];
                frmfaturaurundetay.urunid = row.Cells["FaturaID"].Value?.ToString() ?? "";
                frmfaturaurundetay.Show();
            }
        }
    }
}
