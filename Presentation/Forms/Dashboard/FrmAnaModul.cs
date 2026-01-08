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
using operion.Presentation.Forms.Ai;

namespace operion.Presentation.Forms.Dashboard
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
            
            // Performans için DoubleBuffered aktif et
            this.DoubleBuffered = true;

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
        
        // AI Chat Reference
        private operion.Presentation.Forms.Ai.FrmAiChat? frmAiChat;

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgisini göster
            UpdateUserInfo();

            // AI Chat'i başlat (Sidebar için)
            InitAiChat();
            
            // Ana sayfa formunu otomatik aç
            if (frmanasayfa == null || frmanasayfa.IsDisposed)
            {
                frmanasayfa = new FrmAnaSayfa();
            }
            ShowFormInPanel(frmanasayfa);
            SetActiveMenuItem(menuAnaSayfa);
        }

        private void InitAiChat()
        {
            if (frmAiChat == null || frmAiChat.IsDisposed)
            {
                frmAiChat = new operion.Presentation.Forms.Ai.FrmAiChat();
                frmAiChat.TopLevel = false;
                frmAiChat.FormBorderStyle = FormBorderStyle.None;
                frmAiChat.Dock = DockStyle.Fill;
                
                // pnlAiSidebar controls must exist in Designer!
                pnlAiSidebar.Controls.Add(frmAiChat);
                frmAiChat.Show();
            }
        }

        private void btnAiChat_Click(object? sender, EventArgs e)
        {
            // Toggle Sidebar
            pnlAiSidebar.Visible = !pnlAiSidebar.Visible;
        }

        private void ShowFormInPanel(Form frm)
        {
            // Mevcut içeriği temizle
            pnlMainContent.Controls.Clear();

            // Form ayarları
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Panele ekle ve göster
            pnlMainContent.Controls.Add(frm);
            frm.Show();

            // AI Context Güncelle
            if (frmAiChat != null && !frmAiChat.IsDisposed)
            {
                string contextName = "Genel Bakış";
                if (frm is FrmUrunler) contextName = "Ürün ve Stok Yönetimi";
                else if (frm is FrmMusteriler) contextName = "Müşteri Yönetimi";
                else if (frm is FrmFaturalar) contextName = "Fatura İşlemleri";
                else if (frm is FrmPersoneller) contextName = "Personel Yönetimi";
                else if (frm is FrmKasa) contextName = "Kasa ve Finans";
                else if (frm is FrmGiderler) contextName = "Gider Yönetimi";
                else if (frm is FrmAnaSayfa) contextName = "Ana Dashboard";
                else contextName = frm.Text;

                frmAiChat.SetContext(contextName);
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
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            btnThemeToggle.Text = ThemeManager.IsDarkMode ? "☀️ Light Mode" : "🌙 Dark Mode";
        }

        // --- Navigation Handlers ---

        private void BtnUrunler_Click(object sender, EventArgs e)
        {
            if (frmurunler == null || frmurunler.IsDisposed)
            {
                frmurunler = new FrmUrunler();
            }
            ShowFormInPanel(frmurunler);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnMusteriler_Click(object sender, EventArgs e)
        {
            if (frmmusteriler == null || frmmusteriler.IsDisposed)
            {
                frmmusteriler = new FrmMusteriler();
            }
            ShowFormInPanel(frmmusteriler);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnFirmalar_Click(object sender, EventArgs e)
        {
            if (frmfirmalar == null || frmfirmalar.IsDisposed)
            {
                frmfirmalar = new FrmFirmalar();
            }
            ShowFormInPanel(frmfirmalar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnPersoneller_Click(object sender, EventArgs e)
        {
            if (frmpersoneller == null || frmpersoneller.IsDisposed)
            {
                frmpersoneller = new FrmPersoneller();
            }
            ShowFormInPanel(frmpersoneller);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnRehber_Click(object sender, EventArgs e)
        {
            if (frmrehber == null || frmrehber.IsDisposed)
            {
                frmrehber = new FrmRehber();
            }
            ShowFormInPanel(frmrehber);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnGiderler_Click(object sender, EventArgs e)
        {
            if (frmgiderler == null || frmgiderler.IsDisposed)
            {
                frmgiderler = new FrmGiderler();
            }
            ShowFormInPanel(frmgiderler);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnBankalar_Click(object sender, EventArgs e)
        {
            if (frmbankalar == null || frmbankalar.IsDisposed)
            {
                frmbankalar = new FrmBankalar();
            }
            ShowFormInPanel(frmbankalar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnFaturalar_Click(object sender, EventArgs e)
        {
            if (frmfaturalar == null || frmfaturalar.IsDisposed)
            {
                frmfaturalar = new FrmFaturalar();
            }
            ShowFormInPanel(frmfaturalar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnNotlar_Click(object sender, EventArgs e)
        {
            if (frmnotlar == null || frmnotlar.IsDisposed)
            {
                frmnotlar = new FrmNotlar();
            }
            ShowFormInPanel(frmnotlar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnHareketler_Click(object sender, EventArgs e)
        {
            if (frmhareketler == null || frmhareketler.IsDisposed)
            {
                frmhareketler = new FrmHareketler();
            }
            ShowFormInPanel(frmhareketler);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnRaporlar_Click(object sender, EventArgs e)
        {
            if (frmraporlar == null || frmraporlar.IsDisposed)
            {
                frmraporlar = new FrmRaporlar();
            }
            ShowFormInPanel(frmraporlar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnStoklar_Click(object sender, EventArgs e)
        {
            if (frmstoklar == null || frmstoklar.IsDisposed)
            {
                frmstoklar = new FrmStoklar();
            }
            ShowFormInPanel(frmstoklar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnAyarlar_Click(object sender, EventArgs e)
        {
            if (frmayarlar == null || frmayarlar.IsDisposed)
            {
                frmayarlar = new FrmAyarlar();
            }
            ShowFormInPanel(frmayarlar);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnKasa_Click(object sender, EventArgs e)
        {
            if (frmkasa == null || frmkasa.IsDisposed)
            {
                frmkasa = new FrmKasa();
                frmkasa.kullanici = kullanici;
            }
            ShowFormInPanel(frmkasa);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }

        private void BtnAnaSayfa_Click(object sender, EventArgs e)
        {
            if (frmanasayfa == null || frmanasayfa.IsDisposed)
            {
                frmanasayfa = new FrmAnaSayfa();
            }
            ShowFormInPanel(frmanasayfa);
            SetActiveMenuItem((ToolStripMenuItem)sender);
        }
    }
}
