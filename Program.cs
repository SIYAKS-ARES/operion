using System;
using System.Windows.Forms;
using operion.Application.Services;
using operion.Presentation.Forms.Admin;

namespace operion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// .NET 10 Windows Forms uygulamasi
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // ARM uyumluluk kontrol�
            try
            {
                ARMCompatibilityHelper.CheckSystemCompatibility();
                ARMCompatibilityHelper.EnableARMAlternatives();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ARM uyumluluk kontrol� hatasi: {ex.Message}", 
                    "Uyari", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // SQLite veritabanini baslat
            try
            {
                DatabaseService.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabani baslatma hatasi: {ex.Message}\n\n" +
                    "Uygulama devam edemez. L�tfen hatalari kontrol edin.", 
                    "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ana formu baslat
            System.Windows.Forms.Application.Run(new FrmAdmin());
        }
    }
}
