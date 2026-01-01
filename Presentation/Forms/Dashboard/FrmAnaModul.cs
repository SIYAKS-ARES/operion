using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using operion.Presentation.Controls;
using operion.Presentation.Forms.Products;
using operion.Presentation.Forms.Customers;
using operion.Presentation.Forms.Companies;
using operion.Presentation.Forms.Employees;
using operion.Presentation.Forms.Invoices;
using operion.Presentation.Forms.Financial;
using operion.Presentation.Forms.Reports;
using operion.Presentation.Forms.Settings;

namespace operion.Presentation.Forms.Dashboard
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
            
            // Tema sistemi başlat
            ThemeManager.Initialize();
            ThemeManager.RegisterForm(this);
        }

        public string kullanici = "";

        // Form referansları
        private FrmUrunler? frmurunler;
        private FrmMusteriler? frmmusteriler;
        private FrmFirmalar? frmfirmalar;
        private FrmPersoneller? frmpersoneller;
        private FrmRehber? frmrehber;
        private FrmGiderler? frmgiderler;
        private FrmBankalar? frmbankalar;
        private FrmFaturalar? frmfaturalar;
        private FrmNotlar? frmnotlar;
        private FrmHareketler? frmhareketler;
        private FrmRaporlar? frmraporlar;
        private FrmStoklar? frmstoklar;
        private FrmAyarlar? frmayarlar;
        private FrmKasa? frmkasa;
        private FrmAnaSayfa? frmanasayfa;

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgisini göster
            UpdateUserInfo();
            
            // Ana sayfa formunu otomatik aç
            if (frmanasayfa == null || frmanasayfa.IsDisposed)
            {
                frmanasayfa = new FrmAnaSayfa();
                frmanasayfa.MdiParent = this;
                frmanasayfa.WindowState = FormWindowState.Maximized;
                frmanasayfa.Show();
                SetActiveMenuItem(menuAnaSayfa);
            }
        }

        private void SetActiveMenuItem(ToolStripMenuItem activeItem)
        {
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Tag = null;
                }
            }
            activeItem.Tag = "Active";
            menuStrip1.Invalidate();
        }

        private void UpdateUserInfo()
        {
            if (!string.IsNullOrEmpty(kullanici))
            {
                lblKullanici.Text = $"👤 {kullanici}";
            }
            
            // Logo yükle
            LoadLogo();
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(AppContext.BaseDirectory, "logo", "operion-logo.jpg");
                if (File.Exists(logoPath))
                {
                    using (var image = Image.FromFile(logoPath))
                    {
                        pbLogo.Image = new Bitmap(image, new Size(44, 44));
                    }
                }
            }
            catch
            {
                // Logo yüklenemezse sessizce devam et
            }
        }

        private void btnThemeToggle_Click(object? sender, EventArgs e)
        {
            ThemeManager.ToggleTheme();
            btnThemeToggle.Text = ThemeManager.IsDarkMode ? "☀️ Light Mode" : "🌙 Dark Mode";
        }

        // TODO: DevExpress XtraBars.ItemClick → MenuStrip/ToolStrip Click event değiştirildi
        private void BtnUrunler_Click(object sender, EventArgs e)
        {
            if (frmurunler == null || frmurunler.IsDisposed)
            {
                frmurunler = new FrmUrunler();
                frmurunler.MdiParent = this;
                frmurunler.WindowState = FormWindowState.Maximized;
                frmurunler.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmurunler.BringToFront();
        }

        private void BtnMusteriler_Click(object sender, EventArgs e)
        {
            if (frmmusteriler == null || frmmusteriler.IsDisposed)
            {
                frmmusteriler = new FrmMusteriler();
                frmmusteriler.MdiParent = this;
                frmmusteriler.WindowState = FormWindowState.Maximized;
                frmmusteriler.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmmusteriler.BringToFront();
        }

        private void BtnFirmalar_Click(object sender, EventArgs e)
        {
            if (frmfirmalar == null || frmfirmalar.IsDisposed)
            {
                frmfirmalar = new FrmFirmalar();
                frmfirmalar.MdiParent = this;
                frmfirmalar.WindowState = FormWindowState.Maximized;
                frmfirmalar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmfirmalar.BringToFront();
        }

        private void BtnPersoneller_Click(object sender, EventArgs e)
        {
            if (frmpersoneller == null || frmpersoneller.IsDisposed)
            {
                frmpersoneller = new FrmPersoneller();
                frmpersoneller.MdiParent = this;
                frmpersoneller.WindowState = FormWindowState.Maximized;
                frmpersoneller.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmpersoneller.BringToFront();
        }

        private void BtnRehber_Click(object sender, EventArgs e)
        {
            if (frmrehber == null || frmrehber.IsDisposed)
            {
                frmrehber = new FrmRehber();
                frmrehber.MdiParent = this;
                frmrehber.WindowState = FormWindowState.Maximized;
                frmrehber.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmrehber.BringToFront();
        }

        private void BtnGiderler_Click(object sender, EventArgs e)
        {
            if (frmgiderler == null || frmgiderler.IsDisposed)
            {
                frmgiderler = new FrmGiderler();
                frmgiderler.MdiParent = this;
                frmgiderler.WindowState = FormWindowState.Maximized;
                frmgiderler.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmgiderler.BringToFront();
        }

        private void BtnBankalar_Click(object sender, EventArgs e)
        {
            if (frmbankalar == null || frmbankalar.IsDisposed)
            {
                frmbankalar = new FrmBankalar();
                frmbankalar.MdiParent = this;
                frmbankalar.WindowState = FormWindowState.Maximized;
                frmbankalar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmbankalar.BringToFront();
        }

        private void BtnFaturalar_Click(object sender, EventArgs e)
        {
            if (frmfaturalar == null || frmfaturalar.IsDisposed)
            {
                frmfaturalar = new FrmFaturalar();
                frmfaturalar.MdiParent = this;
                frmfaturalar.WindowState = FormWindowState.Maximized;
                frmfaturalar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmfaturalar.BringToFront();
        }

        private void BtnNotlar_Click(object sender, EventArgs e)
        {
            if (frmnotlar == null || frmnotlar.IsDisposed)
            {
                frmnotlar = new FrmNotlar();
                frmnotlar.MdiParent = this;
                frmnotlar.WindowState = FormWindowState.Maximized;
                frmnotlar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmnotlar.BringToFront();
        }

        private void BtnHareketler_Click(object sender, EventArgs e)
        {
            if (frmhareketler == null || frmhareketler.IsDisposed)
            {
                frmhareketler = new FrmHareketler();
                frmhareketler.MdiParent = this;
                frmhareketler.WindowState = FormWindowState.Maximized;
                frmhareketler.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmhareketler.BringToFront();
        }

        private void BtnRaporlar_Click(object sender, EventArgs e)
        {
            if (frmraporlar == null || frmraporlar.IsDisposed)
            {
                frmraporlar = new FrmRaporlar();
                frmraporlar.MdiParent = this;
                frmraporlar.WindowState = FormWindowState.Maximized;
                frmraporlar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmraporlar.BringToFront();
        }

        private void BtnStoklar_Click(object sender, EventArgs e)
        {
            if (frmstoklar == null || frmstoklar.IsDisposed)
            {
                frmstoklar = new FrmStoklar();
                frmstoklar.MdiParent = this;
                frmstoklar.WindowState = FormWindowState.Maximized;
                frmstoklar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmstoklar.BringToFront();
        }

        private void BtnAyarlar_Click(object sender, EventArgs e)
        {
            if (frmayarlar == null || frmayarlar.IsDisposed)
            {
                frmayarlar = new FrmAyarlar();
                frmayarlar.MdiParent = this;
                frmayarlar.WindowState = FormWindowState.Maximized;
                frmayarlar.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmayarlar.BringToFront();
        }

        private void BtnKasa_Click(object sender, EventArgs e)
        {
            if (frmkasa == null || frmkasa.IsDisposed)
            {
                frmkasa = new FrmKasa();
                frmkasa.MdiParent = this;
                frmkasa.kullanici = kullanici;
                frmkasa.WindowState = FormWindowState.Maximized;
                frmkasa.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmkasa.BringToFront();
        }

        private void BtnAnaSayfa_Click(object sender, EventArgs e)
        {
            if (frmanasayfa == null || frmanasayfa.IsDisposed)
            {
                frmanasayfa = new FrmAnaSayfa();
                frmanasayfa.MdiParent = this;
                frmanasayfa.WindowState = FormWindowState.Maximized;
                frmanasayfa.Show();
            }
            SetActiveMenuItem((ToolStripMenuItem)sender);
            frmanasayfa.BringToFront();
        }
    }
}

