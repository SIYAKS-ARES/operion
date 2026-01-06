namespace operion.Presentation.Forms.Reports
{
    partial class FrmRaporlar
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BtnMusterilerRapor = new operion.Presentation.Controls.ModernButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BtnFirmalarRapor = new operion.Presentation.Controls.ModernButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BtnGiderlerRapor = new operion.Presentation.Controls.ModernButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.BtnPersonellerRapor = new operion.Presentation.Controls.ModernButton();
            this.tabPageAiOzet = new System.Windows.Forms.TabPage();
            this.btnOzetUret = new operion.Presentation.Controls.ModernButton();
            this.progressBarAi = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblOzetBaslik = new System.Windows.Forms.Label();
            this.txtOzet = new System.Windows.Forms.RichTextBox();
            this.btnKopyalaOzet = new operion.Presentation.Controls.ModernButton();
            this.lblAksiyonBaslik = new System.Windows.Forms.Label();
            this.txtAksiyon = new System.Windows.Forms.RichTextBox();
            this.btnKopyalaAksiyon = new operion.Presentation.Controls.ModernButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPageAiOzet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPageAiOzet);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1364, 514);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BtnMusterilerRapor);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1356, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "📧 Müşteriler Raporu";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnMusterilerRapor
            // 
            this.BtnMusterilerRapor.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnMusterilerRapor.Location = new System.Drawing.Point(20, 20);
            this.BtnMusterilerRapor.Name = "BtnMusterilerRapor";
            this.BtnMusterilerRapor.Size = new System.Drawing.Size(250, 48);
            this.BtnMusterilerRapor.TabIndex = 0;
            this.BtnMusterilerRapor.Text = "Müşteriler Raporunu Oluştur";
            this.BtnMusterilerRapor.Click += new System.EventHandler(this.BtnMusterilerRapor_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BtnFirmalarRapor);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1356, 488);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "🏢 Firmalar Raporu";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnFirmalarRapor
            // 
            this.BtnFirmalarRapor.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnFirmalarRapor.Location = new System.Drawing.Point(20, 20);
            this.BtnFirmalarRapor.Name = "BtnFirmalarRapor";
            this.BtnFirmalarRapor.Size = new System.Drawing.Size(250, 48);
            this.BtnFirmalarRapor.TabIndex = 0;
            this.BtnFirmalarRapor.Text = "Firmalar Raporunu Oluştur";
            this.BtnFirmalarRapor.Click += new System.EventHandler(this.BtnFirmalarRapor_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BtnGiderlerRapor);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1356, 488);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "💰 Giderler Raporu";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // BtnGiderlerRapor
            // 
            this.BtnGiderlerRapor.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnGiderlerRapor.Location = new System.Drawing.Point(20, 20);
            this.BtnGiderlerRapor.Name = "BtnGiderlerRapor";
            this.BtnGiderlerRapor.Size = new System.Drawing.Size(250, 48);
            this.BtnGiderlerRapor.TabIndex = 0;
            this.BtnGiderlerRapor.Text = "Giderler Raporunu Oluştur";
            this.BtnGiderlerRapor.Click += new System.EventHandler(this.BtnGiderlerRapor_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.BtnPersonellerRapor);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1356, 488);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "👤 Personeller Raporu";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // BtnPersonellerRapor
            // 
            this.BtnPersonellerRapor.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnPersonellerRapor.Location = new System.Drawing.Point(20, 20);
            this.BtnPersonellerRapor.Name = "BtnPersonellerRapor";
            this.BtnPersonellerRapor.Size = new System.Drawing.Size(250, 48);
            this.BtnPersonellerRapor.TabIndex = 0;
            this.BtnPersonellerRapor.Text = "Personeller Raporunu Oluştur";
            this.BtnPersonellerRapor.Click += new System.EventHandler(this.BtnPersonellerRapor_Click);
            // 
            // tabPageAiOzet
            // 
            this.tabPageAiOzet.Controls.Add(this.splitContainer1);
            this.tabPageAiOzet.Controls.Add(this.lblStatus);
            this.tabPageAiOzet.Controls.Add(this.progressBarAi);
            this.tabPageAiOzet.Controls.Add(this.btnOzetUret);
            this.tabPageAiOzet.Location = new System.Drawing.Point(4, 22);
            this.tabPageAiOzet.Name = "tabPageAiOzet";
            this.tabPageAiOzet.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAiOzet.Size = new System.Drawing.Size(1356, 488);
            this.tabPageAiOzet.TabIndex = 4;
            this.tabPageAiOzet.Text = "🤖 AI Özeti";
            this.tabPageAiOzet.UseVisualStyleBackColor = true;
            // 
            // btnOzetUret
            // 
            this.btnOzetUret.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.btnOzetUret.Location = new System.Drawing.Point(20, 20);
            this.btnOzetUret.Name = "btnOzetUret";
            this.btnOzetUret.Size = new System.Drawing.Size(200, 44);
            this.btnOzetUret.TabIndex = 0;
            this.btnOzetUret.Text = "Özet Üret";
            this.btnOzetUret.Click += new System.EventHandler(this.btnOzetUret_Click);
            // 
            // progressBarAi
            // 
            this.progressBarAi.Location = new System.Drawing.Point(240, 20);
            this.progressBarAi.Name = "progressBarAi";
            this.progressBarAi.Size = new System.Drawing.Size(300, 23);
            this.progressBarAi.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarAi.TabIndex = 1;
            this.progressBarAi.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 75);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(20, 100);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblOzetBaslik);
            this.splitContainer1.Panel1.Controls.Add(this.txtOzet);
            this.splitContainer1.Panel1.Controls.Add(this.btnKopyalaOzet);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblAksiyonBaslik);
            this.splitContainer1.Panel2.Controls.Add(this.txtAksiyon);
            this.splitContainer1.Panel2.Controls.Add(this.btnKopyalaAksiyon);
            this.splitContainer1.Size = new System.Drawing.Size(1316, 370);
            this.splitContainer1.SplitterDistance = 658;
            this.splitContainer1.TabIndex = 3;
            // 
            // lblOzetBaslik
            // 
            this.lblOzetBaslik.AutoSize = true;
            this.lblOzetBaslik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblOzetBaslik.Location = new System.Drawing.Point(3, 3);
            this.lblOzetBaslik.Name = "lblOzetBaslik";
            this.lblOzetBaslik.Size = new System.Drawing.Size(95, 17);
            this.lblOzetBaslik.TabIndex = 0;
            this.lblOzetBaslik.Text = "Rapor Özeti:";
            // 
            // txtOzet
            // 
            this.txtOzet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOzet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOzet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtOzet.Location = new System.Drawing.Point(3, 25);
            this.txtOzet.Name = "txtOzet";
            this.txtOzet.ReadOnly = true;
            this.txtOzet.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOzet.Size = new System.Drawing.Size(652, 310);
            this.txtOzet.TabIndex = 1;
            this.txtOzet.Text = "";
            // 
            // btnKopyalaOzet
            // 
            this.btnKopyalaOzet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKopyalaOzet.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.btnKopyalaOzet.Location = new System.Drawing.Point(3, 341);
            this.btnKopyalaOzet.Name = "btnKopyalaOzet";
            this.btnKopyalaOzet.Size = new System.Drawing.Size(200, 35);
            this.btnKopyalaOzet.TabIndex = 2;
            this.btnKopyalaOzet.Text = "Panoya Kopyala (Özet)";
            this.btnKopyalaOzet.Click += new System.EventHandler(this.btnKopyalaOzet_Click);
            // 
            // lblAksiyonBaslik
            // 
            this.lblAksiyonBaslik.AutoSize = true;
            this.lblAksiyonBaslik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblAksiyonBaslik.Location = new System.Drawing.Point(3, 3);
            this.lblAksiyonBaslik.Name = "lblAksiyonBaslik";
            this.lblAksiyonBaslik.Size = new System.Drawing.Size(127, 17);
            this.lblAksiyonBaslik.TabIndex = 0;
            this.lblAksiyonBaslik.Text = "Aksiyon Maddeleri:";
            // 
            // txtAksiyon
            // 
            this.txtAksiyon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAksiyon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAksiyon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtAksiyon.Location = new System.Drawing.Point(3, 25);
            this.txtAksiyon.Name = "txtAksiyon";
            this.txtAksiyon.ReadOnly = true;
            this.txtAksiyon.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtAksiyon.Size = new System.Drawing.Size(651, 310);
            this.txtAksiyon.TabIndex = 1;
            this.txtAksiyon.Text = "";
            // 
            // btnKopyalaAksiyon
            // 
            this.btnKopyalaAksiyon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKopyalaAksiyon.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.btnKopyalaAksiyon.Location = new System.Drawing.Point(3, 341);
            this.btnKopyalaAksiyon.Name = "btnKopyalaAksiyon";
            this.btnKopyalaAksiyon.Size = new System.Drawing.Size(200, 35);
            this.btnKopyalaAksiyon.TabIndex = 2;
            this.btnKopyalaAksiyon.Text = "Panoya Kopyala (Aksiyon)";
            this.btnKopyalaAksiyon.Click += new System.EventHandler(this.btnKopyalaAksiyon_Click);
            // 
            // FrmRaporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 514);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmRaporlar";
            this.Text = "Raporlar";
            this.Load += new System.EventHandler(this.FrmRaporlar_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPageAiOzet.ResumeLayout(false);
            this.tabPageAiOzet.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private operion.Presentation.Controls.ModernButton BtnMusterilerRapor;
        private operion.Presentation.Controls.ModernButton BtnFirmalarRapor;
        private operion.Presentation.Controls.ModernButton BtnGiderlerRapor;
        private operion.Presentation.Controls.ModernButton BtnPersonellerRapor;
        private System.Windows.Forms.TabPage tabPageAiOzet;
        private operion.Presentation.Controls.ModernButton btnOzetUret;
        private System.Windows.Forms.ProgressBar progressBarAi;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblOzetBaslik;
        private System.Windows.Forms.RichTextBox txtOzet;
        private operion.Presentation.Controls.ModernButton btnKopyalaOzet;
        private System.Windows.Forms.Label lblAksiyonBaslik;
        private System.Windows.Forms.RichTextBox txtAksiyon;
        private operion.Presentation.Controls.ModernButton btnKopyalaAksiyon;
    }
}

