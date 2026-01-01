using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Products
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("MİKTAR", typeof(int));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT UrunAd,SUM(UrunAdet) AS 'MİKTAR' FROM TBL_URUNLER GROUP BY UrunAd", 
                        connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetString(0), // UrunAd
                                    reader.GetInt32(1) // MİKTAR
                                );
                            }
                        }
                    }
                }
                grdstoklar.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdstoklar);
                ModernDataGridViewHelper.EnableHoverEffect(grdstoklar);

                // TODO: Chart kontrolü şimdilik kaldırıldı (DevExpress Charts)
                // İleride standart chart kontrolü eklenebilir
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Stok listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
