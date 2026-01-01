using System;
using System.Windows.Forms;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Settings
{
    public partial class FrmNotDetay : Form
    {
        public FrmNotDetay()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        public string metin;

        private void FrmNotDetay_Load(object sender, EventArgs e)
        {
            rchnotdeyat.Text = metin;
        }
    }
}
