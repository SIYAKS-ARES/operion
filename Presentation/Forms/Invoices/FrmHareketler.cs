using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Invoices
{
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        void FHlistele()
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
                                    reader.IsDBNull(0) ? 0 : reader.GetInt32(0), // HareketID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // UrunAd
                                    reader.IsDBNull(2) ? 0 : reader.GetInt32(2), // Adet
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // PersonelAd
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // FirmaAd
                                    reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5), // Fiyat
                                    reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6), // Toplam
                                    reader.IsDBNull(7) ? "" : reader.GetString(7), // FaturaAlici
                                    reader.IsDBNull(8) ? "" : reader.GetString(8), // Tarih
                                    reader.IsDBNull(9) ? "" : reader.GetString(9) // Notlar
                                );
                            }
                        }
                    }
                }
                grdhareketlerfirmalar.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdhareketlerfirmalar);
                ModernDataGridViewHelper.EnableHoverEffect(grdhareketlerfirmalar);
                
                // Para birimi formatı
                if (grdhareketlerfirmalar.Columns["Fiyat"] != null)
                {
                    grdhareketlerfirmalar.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                }
                if (grdhareketlerfirmalar.Columns["Toplam"] != null)
                {
                    grdhareketlerfirmalar.Columns["Toplam"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Firma hareketleri listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void MHlistele()
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
                                    reader.IsDBNull(0) ? 0 : reader.GetInt32(0), // HareketID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // UrunAd
                                    reader.IsDBNull(2) ? 0 : reader.GetInt32(2), // Adet
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // PersonelAd
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // MusteriAd
                                    reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5), // Fiyat
                                    reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6), // Toplam
                                    reader.IsDBNull(7) ? "" : reader.GetString(7), // FaturaAlici
                                    reader.IsDBNull(8) ? "" : reader.GetString(8), // Tarih
                                    reader.IsDBNull(9) ? "" : reader.GetString(9) // Notlar
                                );
                            }
                        }
                    }
                }
                grdhareketlermusteriler.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdhareketlermusteriler);
                ModernDataGridViewHelper.EnableHoverEffect(grdhareketlermusteriler);
                
                // Para birimi formatı
                if (grdhareketlermusteriler.Columns["Fiyat"] != null)
                {
                    grdhareketlermusteriler.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                }
                if (grdhareketlermusteriler.Columns["Toplam"] != null)
                {
                    grdhareketlermusteriler.Columns["Toplam"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri hareketleri listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            FHlistele();
            MHlistele();
        }
    }
}
