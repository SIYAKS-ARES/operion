using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Dashboard
{
    partial class FrmAnaModul
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblKullanici;
        private System.Windows.Forms.Label btnAiChat;
        private System.Windows.Forms.Label btnThemeToggle;
        private ModernMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAnaSayfa;
        private System.Windows.Forms.ToolStripMenuItem menuUrunler;
        private System.Windows.Forms.ToolStripMenuItem menuMusteriler;
        private System.Windows.Forms.ToolStripMenuItem menuFirmalar;
        private System.Windows.Forms.ToolStripMenuItem menuPersoneller;
        private System.Windows.Forms.ToolStripMenuItem menuStoklar;
        private System.Windows.Forms.ToolStripMenuItem menuFaturalar;
        private System.Windows.Forms.ToolStripMenuItem menuHareketler;
        private System.Windows.Forms.ToolStripMenuItem menuGiderler;
        private System.Windows.Forms.ToolStripMenuItem menuBankalar;
        private System.Windows.Forms.ToolStripMenuItem menuKasa;
        private System.Windows.Forms.ToolStripMenuItem menuNotlar;
        private System.Windows.Forms.ToolStripMenuItem menuRehber;
        private System.Windows.Forms.ToolStripMenuItem menuRaporlar;
        private System.Windows.Forms.ToolStripMenuItem menuAyarlar;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlAiSidebar;

        // ... (Existing InitializeComponent code) ...

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblKullanici = new System.Windows.Forms.Label();
            this.btnThemeToggle = new System.Windows.Forms.Label();
            this.btnAiChat = new System.Windows.Forms.Label();
            this.menuStrip1 = new ModernMenuStrip();
            this.menuAnaSayfa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUrunler = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMusteriler = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFirmalar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPersoneller = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStoklar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFaturalar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHareketler = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGiderler = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBankalar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKasa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNotlar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRehber = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRaporlar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAyarlar = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlAiSidebar = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = DesignSystem.Colors.Primary;
            this.panelHeader.Controls.Add(this.pbLogo);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblKullanici);
            this.panelHeader.Controls.Add(this.btnAiChat);
            this.panelHeader.Controls.Add(this.btnThemeToggle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1370, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(12, 8);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(44, 44);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = DesignSystem.Fonts.Heading2;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(62, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "operion";
            // 
            // lblKullanici
            // 
            this.lblKullanici.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKullanici.AutoSize = true;
            this.lblKullanici.Font = DesignSystem.Fonts.Body;
            this.lblKullanici.ForeColor = System.Drawing.Color.White;
            this.lblKullanici.Location = new System.Drawing.Point(980, 20);
            this.lblKullanici.Name = "lblKullanici";
            this.lblKullanici.Size = new System.Drawing.Size(80, 20);
            this.lblKullanici.TabIndex = 2;
            this.lblKullanici.Text = "👤 Kullanıcı";
            // 
            // btnAiChat
            // 
            this.btnAiChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAiChat.AutoSize = true;
            this.btnAiChat.Font = DesignSystem.Fonts.Body;
            this.btnAiChat.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnAiChat.Location = new System.Drawing.Point(1080, 20);
            this.btnAiChat.Name = "btnAiChat";
            this.btnAiChat.Size = new System.Drawing.Size(100, 20);
            this.btnAiChat.TabIndex = 4;
            this.btnAiChat.Text = "🤖 AI Chat";
            this.btnAiChat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAiChat.Click += new System.EventHandler(this.btnAiChat_Click);
            // 
            // btnThemeToggle
            // 
            this.btnThemeToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemeToggle.AutoSize = true;
            this.btnThemeToggle.Font = DesignSystem.Fonts.Body;
            this.btnThemeToggle.ForeColor = System.Drawing.Color.White;
            this.btnThemeToggle.Location = new System.Drawing.Point(1200, 20);
            this.btnThemeToggle.Name = "btnThemeToggle";
            this.btnThemeToggle.Size = new System.Drawing.Size(100, 20);
            this.btnThemeToggle.TabIndex = 3;
            this.btnThemeToggle.Text = "🌙 Dark Mode";
            this.btnThemeToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemeToggle.Click += new System.EventHandler(this.btnThemeToggle_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAnaSayfa,
            this.menuUrunler,
            this.menuMusteriler,
            this.menuFirmalar,
            this.menuPersoneller,
            this.menuStoklar,
            this.menuFaturalar,
            this.menuHareketler,
            this.menuGiderler,
            this.menuBankalar,
            this.menuKasa,
            this.menuNotlar,
            this.menuRehber,
            this.menuRaporlar,
            this.menuAyarlar});
            this.menuStrip1.Location = new System.Drawing.Point(0, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 48);
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAnaSayfa
            // 
            this.menuAnaSayfa.Font = DesignSystem.Fonts.Body;
            this.menuAnaSayfa.ForeColor = System.Drawing.Color.White;
            this.menuAnaSayfa.Name = "menuAnaSayfa";
            this.menuAnaSayfa.Size = new System.Drawing.Size(90, 44);
            this.menuAnaSayfa.Text = "🏠 Ana Sayfa";
            this.menuAnaSayfa.Click += new System.EventHandler(this.BtnAnaSayfa_Click);
            // 
            // menuUrunler
            // 
            this.menuUrunler.Font = DesignSystem.Fonts.Body;
            this.menuUrunler.ForeColor = System.Drawing.Color.White;
            this.menuUrunler.Name = "menuUrunler";
            this.menuUrunler.Size = new System.Drawing.Size(75, 44);
            this.menuUrunler.Text = "📦 Ürünler";
            this.menuUrunler.Click += new System.EventHandler(this.BtnUrunler_Click);
            // 
            // menuMusteriler
            // 
            this.menuMusteriler.Font = DesignSystem.Fonts.Body;
            this.menuMusteriler.ForeColor = System.Drawing.Color.White;
            this.menuMusteriler.Name = "menuMusteriler";
            this.menuMusteriler.Size = new System.Drawing.Size(90, 44);
            this.menuMusteriler.Text = "👥 Müşteriler";
            this.menuMusteriler.Click += new System.EventHandler(this.BtnMusteriler_Click);
            // 
            // menuFirmalar
            // 
            this.menuFirmalar.Font = DesignSystem.Fonts.Body;
            this.menuFirmalar.ForeColor = System.Drawing.Color.White;
            this.menuFirmalar.Name = "menuFirmalar";
            this.menuFirmalar.Size = new System.Drawing.Size(78, 44);
            this.menuFirmalar.Text = "🏢 Firmalar";
            this.menuFirmalar.Click += new System.EventHandler(this.BtnFirmalar_Click);
            // 
            // menuPersoneller
            // 
            this.menuPersoneller.Font = DesignSystem.Fonts.Body;
            this.menuPersoneller.ForeColor = System.Drawing.Color.White;
            this.menuPersoneller.Name = "menuPersoneller";
            this.menuPersoneller.Size = new System.Drawing.Size(90, 44);
            this.menuPersoneller.Text = "👤 Personeller";
            this.menuPersoneller.Click += new System.EventHandler(this.BtnPersoneller_Click);
            // 
            // menuStoklar
            // 
            this.menuStoklar.Font = DesignSystem.Fonts.Body;
            this.menuStoklar.ForeColor = System.Drawing.Color.White;
            this.menuStoklar.Name = "menuStoklar";
            this.menuStoklar.Size = new System.Drawing.Size(70, 44);
            this.menuStoklar.Text = "📊 Stoklar";
            this.menuStoklar.Click += new System.EventHandler(this.BtnStoklar_Click);
            // 
            // menuFaturalar
            // 
            this.menuFaturalar.Font = DesignSystem.Fonts.Body;
            this.menuFaturalar.ForeColor = System.Drawing.Color.White;
            this.menuFaturalar.Name = "menuFaturalar";
            this.menuFaturalar.Size = new System.Drawing.Size(83, 44);
            this.menuFaturalar.Text = "📄 Faturalar";
            this.menuFaturalar.Click += new System.EventHandler(this.BtnFaturalar_Click);
            // 
            // menuHareketler
            // 
            this.menuHareketler.Font = DesignSystem.Fonts.Body;
            this.menuHareketler.ForeColor = System.Drawing.Color.White;
            this.menuHareketler.Name = "menuHareketler";
            this.menuHareketler.Size = new System.Drawing.Size(88, 44);
            this.menuHareketler.Text = "🔄 Hareketler";
            this.menuHareketler.Click += new System.EventHandler(this.BtnHareketler_Click);
            // 
            // menuGiderler
            // 
            this.menuGiderler.Font = DesignSystem.Fonts.Body;
            this.menuGiderler.ForeColor = System.Drawing.Color.White;
            this.menuGiderler.Name = "menuGiderler";
            this.menuGiderler.Size = new System.Drawing.Size(75, 44);
            this.menuGiderler.Text = "💰 Giderler";
            this.menuGiderler.Click += new System.EventHandler(this.BtnGiderler_Click);
            // 
            // menuBankalar
            // 
            this.menuBankalar.Font = DesignSystem.Fonts.Body;
            this.menuBankalar.ForeColor = System.Drawing.Color.White;
            this.menuBankalar.Name = "menuBankalar";
            this.menuBankalar.Size = new System.Drawing.Size(79, 44);
            this.menuBankalar.Text = "🏦 Bankalar";
            this.menuBankalar.Click += new System.EventHandler(this.BtnBankalar_Click);
            // 
            // menuKasa
            // 
            this.menuKasa.Font = DesignSystem.Fonts.Body;
            this.menuKasa.ForeColor = System.Drawing.Color.White;
            this.menuKasa.Name = "menuKasa";
            this.menuKasa.Size = new System.Drawing.Size(59, 44);
            this.menuKasa.Text = "💵 Kasa";
            this.menuKasa.Click += new System.EventHandler(this.BtnKasa_Click);
            // 
            // menuNotlar
            // 
            this.menuNotlar.Font = DesignSystem.Fonts.Body;
            this.menuNotlar.ForeColor = System.Drawing.Color.White;
            this.menuNotlar.Name = "menuNotlar";
            this.menuNotlar.Size = new System.Drawing.Size(67, 44);
            this.menuNotlar.Text = "📝 Notlar";
            this.menuNotlar.Click += new System.EventHandler(this.BtnNotlar_Click);
            // 
            // menuRehber
            // 
            this.menuRehber.Font = DesignSystem.Fonts.Body;
            this.menuRehber.ForeColor = System.Drawing.Color.White;
            this.menuRehber.Name = "menuRehber";
            this.menuRehber.Size = new System.Drawing.Size(72, 44);
            this.menuRehber.Text = "📖 Rehber";
            this.menuRehber.Click += new System.EventHandler(this.BtnRehber_Click);
            // 
            // menuRaporlar
            // 
            this.menuRaporlar.Font = DesignSystem.Fonts.Body;
            this.menuRaporlar.ForeColor = System.Drawing.Color.White;
            this.menuRaporlar.Name = "menuRaporlar";
            this.menuRaporlar.Size = new System.Drawing.Size(79, 44);
            this.menuRaporlar.Text = "📈 Raporlar";
            this.menuRaporlar.Click += new System.EventHandler(this.BtnRaporlar_Click);
            // 
            // menuAyarlar
            // 
            this.menuAyarlar.Font = DesignSystem.Fonts.Body;
            this.menuAyarlar.ForeColor = System.Drawing.Color.White;
            this.menuAyarlar.Name = "menuAyarlar";
            this.menuAyarlar.Size = new System.Drawing.Size(75, 44);
            this.menuAyarlar.Text = "⚙️ Ayarlar";
            this.menuAyarlar.Click += new System.EventHandler(this.BtnAyarlar_Click);
            // 
            // pnlAiSidebar
            // 
            this.pnlAiSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAiSidebar.Location = new System.Drawing.Point(1020, 108);
            this.pnlAiSidebar.Name = "pnlAiSidebar";
            this.pnlAiSidebar.Size = new System.Drawing.Size(300, 641);
            this.pnlAiSidebar.TabIndex = 2;
            this.pnlAiSidebar.Visible = false;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 108);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1020, 641);
            this.pnlMainContent.TabIndex = 3;
            // 
            // FrmAnaModul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = DesignSystem.Colors.Background;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlAiSidebar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelHeader);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmAnaModul";
            this.Text = "operion - Ticari Otomasyon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAnaModul_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}

