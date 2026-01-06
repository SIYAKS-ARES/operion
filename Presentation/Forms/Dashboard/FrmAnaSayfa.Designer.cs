using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Dashboard
{
    partial class FrmAnaSayfa
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.cardAzalanStoklar = new ModernPanel();
            this.grd_azalanstoklar = new System.Windows.Forms.DataGridView();
            this.cardAjanda = new ModernPanel();
            this.grd_ajanda = new System.Windows.Forms.DataGridView();
            this.cardFihrist = new ModernPanel();
            this.grd_fihrist = new System.Windows.Forms.DataGridView();
            this.cardSonHareketler = new ModernPanel();
            this.grd_firmahareketler = new System.Windows.Forms.DataGridView();
            this.cardDovizHaberler = new ModernPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.wb_doviz = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstbx_haberler = new System.Windows.Forms.ListBox();
            this.cardAzalanStoklar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_azalanstoklar)).BeginInit();
            this.cardAjanda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_ajanda)).BeginInit();
            this.cardFihrist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_fihrist)).BeginInit();
            this.cardSonHareketler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_firmahareketler)).BeginInit();
            this.cardDovizHaberler.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardAzalanStoklar
            // 
            this.cardAzalanStoklar.BackColor = DesignSystem.Colors.Surface;
            this.cardAzalanStoklar.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.cardAzalanStoklar.Controls.Add(this.grd_azalanstoklar);
            this.cardAzalanStoklar.Location = new System.Drawing.Point(DesignSystem.Spacing.L, DesignSystem.Spacing.L);
            this.cardAzalanStoklar.Name = "cardAzalanStoklar";
            this.cardAzalanStoklar.Size = new System.Drawing.Size(466, 280);
            this.cardAzalanStoklar.TabIndex = 0;
            this.cardAzalanStoklar.Title = "📦 Azalan Stoklar";
            this.cardAzalanStoklar.ShowTitle = true;
            this.cardAzalanStoklar.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.M);
            // 
            // grd_azalanstoklar
            // 
            this.grd_azalanstoklar.AllowUserToAddRows = false;
            this.grd_azalanstoklar.AllowUserToDeleteRows = false;
            this.grd_azalanstoklar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grd_azalanstoklar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grd_azalanstoklar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_azalanstoklar.Location = new System.Drawing.Point(DesignSystem.Spacing.M, DesignSystem.Spacing.M + 30);
            this.grd_azalanstoklar.Name = "grd_azalanstoklar";
            this.grd_azalanstoklar.ReadOnly = true;
            this.grd_azalanstoklar.Size = new System.Drawing.Size(466 - (DesignSystem.Spacing.M * 2), 280 - (DesignSystem.Spacing.M * 2) - 30);
            this.grd_azalanstoklar.TabIndex = 0;
            // 
            // cardAjanda
            // 
            this.cardAjanda.BackColor = DesignSystem.Colors.Surface;
            this.cardAjanda.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.cardAjanda.Controls.Add(this.grd_ajanda);
            this.cardAjanda.Location = new System.Drawing.Point(475 + DesignSystem.Spacing.S, DesignSystem.Spacing.L);
            this.cardAjanda.Name = "cardAjanda";
            this.cardAjanda.Size = new System.Drawing.Size(442, 280);
            this.cardAjanda.TabIndex = 1;
            this.cardAjanda.Title = "📅 Ajanda";
            this.cardAjanda.ShowTitle = true;
            this.cardAjanda.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.M);
            // 
            // grd_ajanda
            // 
            this.grd_ajanda.AllowUserToAddRows = false;
            this.grd_ajanda.AllowUserToDeleteRows = false;
            this.grd_ajanda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grd_ajanda.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grd_ajanda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_ajanda.Location = new System.Drawing.Point(DesignSystem.Spacing.M, DesignSystem.Spacing.M + 30);
            this.grd_ajanda.Name = "grd_ajanda";
            this.grd_ajanda.ReadOnly = true;
            this.grd_ajanda.Size = new System.Drawing.Size(442 - (DesignSystem.Spacing.M * 2), 280 - (DesignSystem.Spacing.M * 2) - 30);
            this.grd_ajanda.TabIndex = 0;
            // 
            // cardFihrist
            // 
            this.cardFihrist.BackColor = DesignSystem.Colors.Surface;
            this.cardFihrist.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.cardFihrist.Controls.Add(this.grd_fihrist);
            this.cardFihrist.Location = new System.Drawing.Point(475 + DesignSystem.Spacing.S, 288 + DesignSystem.Spacing.L);
            this.cardFihrist.Name = "cardFihrist";
            this.cardFihrist.Size = new System.Drawing.Size(442, 251);
            this.cardFihrist.TabIndex = 2;
            this.cardFihrist.Title = "📖 İletişim Rehberi";
            this.cardFihrist.ShowTitle = true;
            this.cardFihrist.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.M);
            // 
            // grd_fihrist
            // 
            this.grd_fihrist.AllowUserToAddRows = false;
            this.grd_fihrist.AllowUserToDeleteRows = false;
            this.grd_fihrist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grd_fihrist.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grd_fihrist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_fihrist.Location = new System.Drawing.Point(DesignSystem.Spacing.M, DesignSystem.Spacing.M + 30);
            this.grd_fihrist.Name = "grd_fihrist";
            this.grd_fihrist.ReadOnly = true;
            this.grd_fihrist.Size = new System.Drawing.Size(442 - (DesignSystem.Spacing.M * 2), 251 - (DesignSystem.Spacing.M * 2) - 30);
            this.grd_fihrist.TabIndex = 0;
            // 
            // cardSonHareketler
            // 
            this.cardSonHareketler.BackColor = DesignSystem.Colors.Surface;
            this.cardSonHareketler.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.cardSonHareketler.Controls.Add(this.grd_firmahareketler);
            this.cardSonHareketler.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 288 + DesignSystem.Spacing.L);
            this.cardSonHareketler.Name = "cardSonHareketler";
            this.cardSonHareketler.Size = new System.Drawing.Size(471, 251);
            this.cardSonHareketler.TabIndex = 3;
            this.cardSonHareketler.Title = "🔄 Son 10 Hareket";
            this.cardSonHareketler.ShowTitle = true;
            this.cardSonHareketler.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.M);
            // 
            // grd_firmahareketler
            // 
            this.grd_firmahareketler.AllowUserToAddRows = false;
            this.grd_firmahareketler.AllowUserToDeleteRows = false;
            this.grd_firmahareketler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grd_firmahareketler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grd_firmahareketler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_firmahareketler.Location = new System.Drawing.Point(DesignSystem.Spacing.M, DesignSystem.Spacing.M + 30);
            this.grd_firmahareketler.Name = "grd_firmahareketler";
            this.grd_firmahareketler.ReadOnly = true;
            this.grd_firmahareketler.Size = new System.Drawing.Size(471 - (DesignSystem.Spacing.M * 2), 251 - (DesignSystem.Spacing.M * 2) - 30);
            this.grd_firmahareketler.TabIndex = 0;
            // 
            // cardDovizHaberler
            // 
            this.cardDovizHaberler.BackColor = DesignSystem.Colors.Surface;
            this.cardDovizHaberler.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.cardDovizHaberler.Controls.Add(this.tabControl1);
            this.cardDovizHaberler.Location = new System.Drawing.Point(923 + DesignSystem.Spacing.S, DesignSystem.Spacing.L);
            this.cardDovizHaberler.Name = "cardDovizHaberler";
            this.cardDovizHaberler.Size = new System.Drawing.Size(447, 512);
            this.cardDovizHaberler.TabIndex = 4;
            this.cardDovizHaberler.Title = "💱 Döviz & Haberler";
            this.cardDovizHaberler.ShowTitle = true;
            this.cardDovizHaberler.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.M);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(DesignSystem.Spacing.M, DesignSystem.Spacing.M + 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447 - (DesignSystem.Spacing.M * 2), 512 - (DesignSystem.Spacing.M * 2) - 30);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Font = DesignSystem.Fonts.Body;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wb_doviz);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "💱 Döviz Kurları";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // wb_doviz
            // 
            this.wb_doviz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb_doviz.Location = new System.Drawing.Point(3, 3);
            this.wb_doviz.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_doviz.Name = "wb_doviz";
            this.wb_doviz.Size = new System.Drawing.Size(433, 478);
            this.wb_doviz.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lstbx_haberler);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "📰 Haberler";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstbx_haberler
            // 
            this.lstbx_haberler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbx_haberler.Font = DesignSystem.Fonts.Body;
            this.lstbx_haberler.FormattingEnabled = true;
            this.lstbx_haberler.HorizontalScrollbar = true;
            this.lstbx_haberler.ItemHeight = 18;
            this.lstbx_haberler.Location = new System.Drawing.Point(3, 3);
            this.lstbx_haberler.Name = "lstbx_haberler";
            this.lstbx_haberler.Size = new System.Drawing.Size(433, 478);
            this.lstbx_haberler.TabIndex = 0;
            // 
            // FrmAnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = DesignSystem.Colors.Background;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.cardDovizHaberler);
            this.Controls.Add(this.cardSonHareketler);
            this.Controls.Add(this.cardFihrist);
            this.Controls.Add(this.cardAjanda);
            this.Controls.Add(this.cardAzalanStoklar);
            this.Name = "FrmAnaSayfa";
            this.Text = "Dashboard - operion";
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1300, 600);
            this.Load += new System.EventHandler(this.FrmAnaSayfa_Load);
            this.cardAzalanStoklar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_azalanstoklar)).EndInit();
            this.cardAjanda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_ajanda)).EndInit();
            this.cardFihrist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_fihrist)).EndInit();
            this.cardSonHareketler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_firmahareketler)).EndInit();
            this.cardDovizHaberler.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private ModernPanel cardAzalanStoklar;
        private System.Windows.Forms.DataGridView grd_azalanstoklar;
        private ModernPanel cardAjanda;
        private System.Windows.Forms.DataGridView grd_ajanda;
        private ModernPanel cardFihrist;
        private System.Windows.Forms.DataGridView grd_fihrist;
        private ModernPanel cardSonHareketler;
        private System.Windows.Forms.DataGridView grd_firmahareketler;
        private ModernPanel cardDovizHaberler;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser wb_doviz;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstbx_haberler;
    }
}
