using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Admin
{
    partial class FrmAdmin
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
        private void InitializeComponent()
        {
            // Modern controls
            this.loginCard = new ModernPanel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtkullanicad = new ModernTextBox();
            this.txtsifre = new ModernTextBox();
            this.BtnGirisYap = new ModernButton();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblThemeToggle = new System.Windows.Forms.Label();
            this.loginCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // loginCard
            // 
            this.loginCard.BackColor = DesignSystem.Colors.Surface;
            this.loginCard.BorderRadius = DesignSystem.Borders.RadiusLarge;
            this.loginCard.ShowShadow = true;
            this.loginCard.ShowTitle = false;
            this.loginCard.Location = new System.Drawing.Point(0, 0);
            this.loginCard.Name = "loginCard";
            this.loginCard.Size = new System.Drawing.Size(400, 600);
            this.loginCard.TabIndex = 0;
            this.loginCard.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.XL);
            // Logo container
            this.loginCard.Controls.Add(this.pbLogo);
            this.loginCard.Controls.Add(this.lblTitle);
            this.loginCard.Controls.Add(this.lblSubtitle);
            this.loginCard.Controls.Add(this.txtkullanicad);
            this.loginCard.Controls.Add(this.txtsifre);
            this.loginCard.Controls.Add(this.BtnGirisYap);
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(125, 30);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(150, 150);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = DesignSystem.Fonts.Heading1;
            this.lblTitle.ForeColor = DesignSystem.Colors.Text;
            this.lblTitle.Location = new System.Drawing.Point(32, 210);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(336, 40);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "operion";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Font = DesignSystem.Fonts.Body;
            this.lblSubtitle.ForeColor = DesignSystem.Colors.TextLight;
            this.lblSubtitle.Location = new System.Drawing.Point(32, 250);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(336, 20);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Ticari Otomasyon Sistemi";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtkullanicad
            // 
            this.txtkullanicad.Location = new System.Drawing.Point(32, 300);
            this.txtkullanicad.Name = "txtkullanicad";
            this.txtkullanicad.Size = new System.Drawing.Size(336, DesignSystem.ControlSizes.InputHeight + 6);
            this.txtkullanicad.TabIndex = 3;
            this.txtkullanicad.PlaceholderText = "Kullanıcı Adı";
            this.txtkullanicad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtkullanicad_KeyDown);
            // 
            // txtsifre
            // 
            this.txtsifre.Location = new System.Drawing.Point(32, 355);
            this.txtsifre.Name = "txtsifre";
            this.txtsifre.Size = new System.Drawing.Size(336, DesignSystem.ControlSizes.InputHeight + 6);
            this.txtsifre.TabIndex = 4;
            this.txtsifre.PlaceholderText = "Şifre";
            this.txtsifre.UseSystemPasswordChar = true;
            this.txtsifre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsifre_KeyDown);
            // 
            // BtnGirisYap
            // 
            this.BtnGirisYap.ButtonStyle = ButtonStyle.Primary;
            this.BtnGirisYap.Location = new System.Drawing.Point(32, 420);
            this.BtnGirisYap.Name = "BtnGirisYap";
            this.BtnGirisYap.Size = new System.Drawing.Size(336, 45);
            this.BtnGirisYap.TabIndex = 5;
            this.BtnGirisYap.Text = "Giriş Yap";
            this.BtnGirisYap.UseVisualStyleBackColor = false;
            this.BtnGirisYap.Click += new System.EventHandler(this.BtnGirisYap_Click);
// 
// lblVersion
// 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = DesignSystem.Fonts.Small;
            this.lblVersion.ForeColor = DesignSystem.Colors.TextLight;
            this.lblVersion.Location = new System.Drawing.Point(12, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(80, 15);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "v1.0.0 (2026)";
            // 
            // lblThemeToggle
            // 
            this.lblThemeToggle.AutoSize = true;
            this.lblThemeToggle.Font = DesignSystem.Fonts.Body;
            this.lblThemeToggle.ForeColor = DesignSystem.Colors.Primary;
            this.lblThemeToggle.Location = new System.Drawing.Point(0, 0);
            this.lblThemeToggle.Name = "lblThemeToggle";
            this.lblThemeToggle.Size = new System.Drawing.Size(100, 20);
            this.lblThemeToggle.TabIndex = 8;
            this.lblThemeToggle.Text = "🌙 Dark Mode";
            this.lblThemeToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblThemeToggle.Click += new System.EventHandler(this.lblThemeToggle_Click);
            // 
            // FrmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = DesignSystem.Colors.Background;
            this.ClientSize = new System.Drawing.Size(1386, 600);
            this.Controls.Add(this.lblThemeToggle);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.loginCard);
            this.Font = DesignSystem.Fonts.Body;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "operion - Giriş";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAdmin_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAdmin_FormClosing);
            this.SizeChanged += new System.EventHandler(this.FrmAdmin_SizeChanged);
            this.loginCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ModernPanel loginCard;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private ModernTextBox txtkullanicad;
        private ModernTextBox txtsifre;
        private ModernButton BtnGirisYap;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblThemeToggle;

        private void FrmAdmin_SizeChanged(object sender, System.EventArgs e)
        {
            // Login card'ı merkeze hizala
            loginCard.Location = new System.Drawing.Point(
                (this.ClientSize.Width - loginCard.Width) / 2,
                (this.ClientSize.Height - loginCard.Height) / 2
            );

            // Version label'ı sol alt köşeye
            lblVersion.Location = new System.Drawing.Point(
                DesignSystem.Spacing.L,
                this.ClientSize.Height - lblVersion.Height - DesignSystem.Spacing.L
            );

            // Theme toggle'ı sağ alt köşeye
            lblThemeToggle.Location = new System.Drawing.Point(
                this.ClientSize.Width - lblThemeToggle.Width - DesignSystem.Spacing.L,
                this.ClientSize.Height - lblThemeToggle.Height - DesignSystem.Spacing.L
            );
        }

        private void lblThemeToggle_Click(object sender, System.EventArgs e)
        {
            // Dark mode toggle
            ThemeManager.ToggleTheme();
            
            // Label metnini güncelle
            lblThemeToggle.Text = ThemeManager.IsDarkMode ? "☀️ Light Mode" : "🌙 Dark Mode";
        }
    }
}
