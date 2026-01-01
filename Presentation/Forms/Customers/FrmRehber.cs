using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Forms.Settings;

namespace operion.Presentation.Forms.Customers
{
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            try
            {
                // Müşteri Bilgileri Getirme
                DataTable dtmusteri = new DataTable();
                dtmusteri.Columns.Add("MusteriID", typeof(int));
                dtmusteri.Columns.Add("MusteriAd", typeof(string));
                dtmusteri.Columns.Add("MusteriSoyad", typeof(string));
                dtmusteri.Columns.Add("MusteriTelefon", typeof(string));
                dtmusteri.Columns.Add("MusteriTelefon2", typeof(string));
                dtmusteri.Columns.Add("MusteriMail", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT MusteriID,MusteriAd,MusteriSoyad,MusteriTelefon,MusteriTelefon2,MusteriMail FROM TBL_MUSTERILER", 
                        connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dtmusteri.Rows.Add(
                                    reader.GetInt32(0), // MusteriID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // MusteriAd
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // MusteriSoyad
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // MusteriTelefon
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // MusteriTelefon2
                                    reader.IsDBNull(5) ? "" : reader.GetString(5) // MusteriMail
                                );
                            }
                        }
                    }
                }
                grdrehbermusteriler.DataSource = dtmusteri;

                // Firma Bilgileri Getirme
                DataTable dtfirma = new DataTable();
                dtfirma.Columns.Add("FirmaID", typeof(int));
                dtfirma.Columns.Add("FirmaAd", typeof(string));
                dtfirma.Columns.Add("FirmaYetkiliAdSoyad", typeof(string));
                dtfirma.Columns.Add("FirmaTelefon1", typeof(string));
                dtfirma.Columns.Add("FirmaTelefon2", typeof(string));
                dtfirma.Columns.Add("FirmaTelefon3", typeof(string));
                dtfirma.Columns.Add("FirmaMail", typeof(string));
                dtfirma.Columns.Add("FirmaFax", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT FirmaID,FirmaAd,FirmaYetkiliAdSoyad,FirmaTelefon1,FirmaTelefon2,FirmaTelefon3,FirmaMail,FirmaFax FROM TBL_FIRMALAR", 
                        connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dtfirma.Rows.Add(
                                    reader.GetInt32(0), // FirmaID
                                    reader.IsDBNull(1) ? "" : reader.GetString(1), // FirmaAd
                                    reader.IsDBNull(2) ? "" : reader.GetString(2), // FirmaYetkiliAdSoyad
                                    reader.IsDBNull(3) ? "" : reader.GetString(3), // FirmaTelefon1
                                    reader.IsDBNull(4) ? "" : reader.GetString(4), // FirmaTelefon2
                                    reader.IsDBNull(5) ? "" : reader.GetString(5), // FirmaTelefon3
                                    reader.IsDBNull(6) ? "" : reader.GetString(6), // FirmaMail
                                    reader.IsDBNull(7) ? "" : reader.GetString(7) // FirmaFax
                                );
                            }
                        }
                    }
                }
                grdrehberfirmalar.DataSource = dtfirma;

                // Modern DataGridView styling + hover
                ModernDataGridViewHelper.ApplyModernStyle(grdrehbermusteriler);
                ModernDataGridViewHelper.EnableHoverEffect(grdrehbermusteriler);
                ModernDataGridViewHelper.ApplyModernStyle(grdrehberfirmalar);
                ModernDataGridViewHelper.EnableHoverEffect(grdrehberfirmalar);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rehber yükleme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdrehbermusteriler_DoubleClick(object sender, EventArgs e)
        {
            if (grdrehbermusteriler.SelectedRows.Count > 0)
            {
                FrmMail frmmail = new FrmMail();
                DataGridViewRow row = grdrehbermusteriler.SelectedRows[0];
                frmmail.mail = row.Cells["MusteriMail"].Value?.ToString() ?? "";
                frmmail.Show();
            }
        }

        private void grdrehberfirmalar_DoubleClick(object sender, EventArgs e)
        {
            if (grdrehberfirmalar.SelectedRows.Count > 0)
            {
                FrmMail frmmail = new FrmMail();
                DataGridViewRow row = grdrehberfirmalar.SelectedRows[0];
                frmmail.mail = row.Cells["FirmaMail"].Value?.ToString() ?? "";
                frmmail.Show();
            }
        }
    }
}
