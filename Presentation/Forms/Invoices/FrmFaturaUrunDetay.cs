using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Invoices
{
    public partial class FrmFaturaUrunDetay : Form
    {
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        public string urunid;

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FaturaUrunID", typeof(int));
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("Miktar", typeof(int));
                dt.Columns.Add("Fiyat", typeof(double));
                dt.Columns.Add("Tutar", typeof(double));
                dt.Columns.Add("FaturaID", typeof(int));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT * FROM TBL_FATURADETAY WHERE FaturaID = @p1", 
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", urunid);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.GetInt32(0), // FaturaUrunID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // UrunAd
                                    reader.GetInt32(2), // Miktar
                                    reader.GetDouble(3), // Fiyat
                                    reader.GetDouble(4), // Tutar
                                    reader.GetInt32(5) // FaturaID
                                );
                            }
                        }
                    }
                }
                grdfaturaurundetay.DataSource = dt;
                
                // Modern DataGridView styling
                ModernDataGridViewHelper.ApplyModernStyle(grdfaturaurundetay);
                ModernDataGridViewHelper.EnableHoverEffect(grdfaturaurundetay);
                
                // Format columns
                if (grdfaturaurundetay.Columns["Fiyat"] != null)
                {
                    grdfaturaurundetay.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                }
                if (grdfaturaurundetay.Columns["Tutar"] != null)
                {
                    grdfaturaurundetay.Columns["Tutar"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void grdfaturaurundetay_DoubleClick(object sender, EventArgs e)
        {
            if (grdfaturaurundetay.SelectedRows.Count > 0)
            {
                FrmFaturaUrunDuzenleme frmfaturaurunduzenleme = new FrmFaturaUrunDuzenleme();
                DataGridViewRow row = grdfaturaurundetay.SelectedRows[0];
                frmfaturaurunduzenleme.urunid = row.Cells["FaturaUrunID"].Value?.ToString() ?? "";
                frmfaturaurunduzenleme.Show();
            }
        }
    }
}
