using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Invoices
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        public string urunid;

        private void FaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            try
            {
                txtfaturaurunid.Text = urunid;
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT * FROM TBL_FATURADETAY WHERE FaturaUrunID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", urunid);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtfaturaurunad.Text = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                txtfaturaurunmiktar.Text = reader.GetInt32(2).ToString();
                                txtfaturaurunfiyat.Text = reader.GetDouble(3).ToString("F2");
                                txtfaturauruntutar.Text = reader.GetDouble(4).ToString("F2");
                            }
                        }
                    }
                }
                
                // Otomatik tutar hesaplama için event handler ekle
                txtfaturaurunmiktar.TextChanged += Txtfaturaurunmiktar_TextChanged;
                txtfaturaurunfiyat.TextChanged += Txtfaturaurunfiyat_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void Txtfaturaurunmiktar_TextChanged(object sender, EventArgs e)
        {
            HesaplaTutar();
        }
        
        private void Txtfaturaurunfiyat_TextChanged(object sender, EventArgs e)
        {
            HesaplaTutar();
        }
        
        private void HesaplaTutar()
        {
            if (double.TryParse(txtfaturaurunfiyat.Text, out double fiyat) && 
                int.TryParse(txtfaturaurunmiktar.Text, out int miktar))
            {
                double tutar = fiyat * miktar;
                txtfaturauruntutar.Text = tutar.ToString("F2");
            }
            else
            {
                txtfaturauruntutar.Text = "";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
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
                HesaplaTutar();
                
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "UPDATE TBL_FATURADETAY SET UrunAd = @p1,Miktar = @p2,Fiyat = @p3,Tutar = @p4 WHERE FaturaUrunID = @p5", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtfaturaurunad.Text);
                        cmd.Parameters.AddWithValue("@p2", int.Parse(txtfaturaurunmiktar.Text));
                        cmd.Parameters.AddWithValue("@p3", double.Parse(txtfaturaurunfiyat.Text));
                        cmd.Parameters.AddWithValue("@p4", double.Parse(txtfaturauruntutar.Text));
                        cmd.Parameters.AddWithValue("@p5", int.Parse(txtfaturaurunid.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Değişiklikler başarıyla kaydedildi", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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
                var result = MessageBox.Show(
                    $"{txtfaturaurunad.Text} isimli ürünü silmek istediğinizden emin misiniz?", 
                    "Silme Onayı", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    using (var connection = DatabaseService.GetConnection())
                    {
                        using (var cmd = new SqliteCommand(
                            "DELETE FROM TBL_FATURADETAY WHERE FaturaUrunID = @p1", 
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@p1", int.Parse(txtfaturaurunid.Text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Ürün başarıyla silindi", "Başarılı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
