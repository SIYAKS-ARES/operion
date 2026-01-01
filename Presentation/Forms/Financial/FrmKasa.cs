using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Financial
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        public string kullanici;

        void musterihareket()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("HareketID", typeof(int));
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("Adet", typeof(int));
                dt.Columns.Add("PersonelAd", typeof(string));
                dt.Columns.Add("MusteriAd", typeof(string));
                dt.Columns.Add("Fiyat", typeof(double));
                dt.Columns.Add("Toplam", typeof(double));
                dt.Columns.Add("FaturaAlici", typeof(string));
                dt.Columns.Add("Tarih", typeof(string));
                dt.Columns.Add("Notlar", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM MusteriHareketler", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                    reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5),
                                    reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6),
                                    reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    reader.IsDBNull(9) ? "" : reader.GetString(9)
                                );
                            }
                        }
                    }
                }
                grdkasamusterihareketler.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdkasamusterihareketler);
                ModernDataGridViewHelper.EnableHoverEffect(grdkasamusterihareketler);
                
                // Para birimi formatı
                if (grdkasamusterihareketler.Columns["Fiyat"] != null)
                {
                    grdkasamusterihareketler.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                }
                if (grdkasamusterihareketler.Columns["Toplam"] != null)
                {
                    grdkasamusterihareketler.Columns["Toplam"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri hareketleri yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmahareket()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("HareketID", typeof(int));
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("Adet", typeof(int));
                dt.Columns.Add("PersonelAd", typeof(string));
                dt.Columns.Add("FirmaAd", typeof(string));
                dt.Columns.Add("Fiyat", typeof(double));
                dt.Columns.Add("Toplam", typeof(double));
                dt.Columns.Add("FaturaAlici", typeof(string));
                dt.Columns.Add("Tarih", typeof(string));
                dt.Columns.Add("Notlar", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM FirmaHareketler", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                    reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5),
                                    reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6),
                                    reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    reader.IsDBNull(9) ? "" : reader.GetString(9)
                                );
                            }
                        }
                    }
                }
                grdkasafirmahareketler.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdkasafirmahareketler);
                ModernDataGridViewHelper.EnableHoverEffect(grdkasafirmahareketler);
                
                // Para birimi formatı
                if (grdkasafirmahareketler.Columns["Fiyat"] != null)
                {
                    grdkasafirmahareketler.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                }
                if (grdkasafirmahareketler.Columns["Toplam"] != null)
                {
                    grdkasafirmahareketler.Columns["Toplam"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Firma hareketleri yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void cikishareket()
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
                    using (var cmd = new SqliteCommand("SELECT * FROM TBL_GIDERLER ORDER BY GiderID ASC", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0),
                                    reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    reader.IsDBNull(3) ? 0.0 : reader.GetDouble(3),
                                    reader.IsDBNull(4) ? 0.0 : reader.GetDouble(4),
                                    reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5),
                                    reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6),
                                    reader.IsDBNull(7) ? 0.0 : reader.GetDouble(7),
                                    reader.IsDBNull(8) ? 0.0 : reader.GetDouble(8),
                                    reader.IsDBNull(9) ? "" : reader.GetString(9)
                                );
                            }
                        }
                    }
                }
                grdkasacikis.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdkasacikis);
                ModernDataGridViewHelper.EnableHoverEffect(grdkasacikis);
                
                // Para birimi formatı
                string[] currencyColumns = { "GiderElektrik", "GiderSu", "GiderDogalgaz", "GiderInternet", "GiderMaaslar", "GiderEkstra" };
                foreach (string colName in currencyColumns)
                {
                    if (grdkasacikis.Columns[colName] != null)
                    {
                        grdkasacikis.Columns[colName].DefaultCellStyle.Format = "C2";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Çıkış hareketleri yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void toplamtutar()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SUM(Tutar) FROM TBL_FATURADETAY", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value && double.TryParse(result.ToString(), out double tutar))
                        {
                            lbltoplamtutar.Text = tutar.ToString("C2");
                        }
                        else
                        {
                            lbltoplamtutar.Text = "0,00 ₺";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Toplam tutar hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void odemeler()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT SUM(GiderElektrik+GiderSu+GiderDogalgaz+GiderInternet+GiderEkstra) FROM TBL_GIDERLER", 
                        connection))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value && double.TryParse(result.ToString(), out double tutar))
                        {
                            lblodemeler.Text = tutar.ToString("C2");
                        }
                        else
                        {
                            lblodemeler.Text = "0,00 ₺";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödemeler hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void personelmaaslari()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SUM(GiderMaaslar) FROM TBL_GIDERLER", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value && double.TryParse(result.ToString(), out double tutar))
                        {
                            lblpersonelmaaslari.Text = tutar.ToString("C2");
                        }
                        else
                        {
                            lblpersonelmaaslari.Text = "0,00 ₺";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Personel maaşları hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void musterisayisi()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        lblmusterisayisi.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri sayısı hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmasayisi()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT COUNT(*) FROM TBL_FIRMALAR", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        lblfirmasayisi.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Firma sayısı hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void personelsayisi()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        lblpersonelsayisi.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Personel sayısı hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void stoksayisi()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT SUM(UrunAdet) FROM TBL_URUNLER", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        lblstoksayisi.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Stok sayısı hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmasehirsayisi()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT COUNT(DISTINCT(FirmaSehir)) FROM TBL_FIRMALAR", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        lblfsehirsayisi.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Firma şehir sayısı hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void musterisehirsayisi()
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT COUNT(DISTINCT(MusteriSehir)) FROM TBL_MUSTERILER", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        lblmsehirsayisi.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri şehir sayısı hesaplama hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            musterihareket();
            firmahareket();
            cikishareket();
            toplamtutar();
            odemeler();
            personelmaaslari();
            musterisayisi();
            firmasayisi();
            firmasehirsayisi();
            musterisehirsayisi();
            personelsayisi();
            stoksayisi();
            lblaktifkullanici.Text = kullanici ?? "";

            // TODO: Chart kontrolleri şimdilik kaldırıldı (DevExpress Charts)
            // İleride standart chart kontrolü eklenebilir
        }
    }
}
