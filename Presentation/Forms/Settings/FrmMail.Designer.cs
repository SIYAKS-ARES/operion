namespace operion.Presentation.Forms.Settings
{
    partial class FrmMail
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
            this.pnlMail = new operion.Presentation.Controls.ModernPanel();
            this.Btngonder = new operion.Presentation.Controls.ModernButton();
            this.rchmailmesaj = new System.Windows.Forms.RichTextBox();
            this.txtmailkonu = new operion.Presentation.Controls.ModernTextBox();
            this.txtmailadres = new operion.Presentation.Controls.ModernTextBox();
            this.grpAiAsistan = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBarAi = new System.Windows.Forms.ProgressBar();
            this.btnGovdeyeAktar = new operion.Presentation.Controls.ModernButton();
            this.btnYenidenUret = new operion.Presentation.Controls.ModernButton();
            this.btnSablonOner = new operion.Presentation.Controls.ModernButton();
            this.txtOnizleme = new System.Windows.Forms.RichTextBox();
            this.lblOnizleme = new System.Windows.Forms.Label();
            this.cmbKonu = new System.Windows.Forms.ComboBox();
            this.lblKonu = new System.Windows.Forms.Label();
            this.cmbUzunluk = new System.Windows.Forms.ComboBox();
            this.lblUzunluk = new System.Windows.Forms.Label();
            this.cmbTon = new System.Windows.Forms.ComboBox();
            this.lblTon = new System.Windows.Forms.Label();
            this.cmbScenario = new System.Windows.Forms.ComboBox();
            this.lblScenario = new System.Windows.Forms.Label();
            this.pnlMail.SuspendLayout();
            this.grpAiAsistan.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMail
            // 
            this.pnlMail.Controls.Add(this.Btngonder);
            this.pnlMail.Controls.Add(this.rchmailmesaj);
            this.pnlMail.Controls.Add(this.txtmailkonu);
            this.pnlMail.Controls.Add(this.txtmailadres);
            this.pnlMail.Location = new System.Drawing.Point(0, 0);
            this.pnlMail.Name = "pnlMail";
            this.pnlMail.Size = new System.Drawing.Size(500, 520);
            this.pnlMail.TabIndex = 0;
            this.pnlMail.Title = "✉️ Mail Gönder";
            // 
            // Btngonder
            // 
            this.Btngonder.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.Btngonder.Location = new System.Drawing.Point(160, 430);
            this.Btngonder.Name = "Btngonder";
            this.Btngonder.Size = new System.Drawing.Size(180, 44);
            this.Btngonder.TabIndex = 3;
            this.Btngonder.Text = "Gönder";
            this.Btngonder.Click += new System.EventHandler(this.Btngonder_Click);
            // 
            // rchmailmesaj
            // 
            this.rchmailmesaj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchmailmesaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.rchmailmesaj.Location = new System.Drawing.Point(40, 200);
            this.rchmailmesaj.Name = "rchmailmesaj";
            this.rchmailmesaj.Size = new System.Drawing.Size(420, 210);
            this.rchmailmesaj.TabIndex = 2;
            this.rchmailmesaj.Text = "";
            // 
            // txtmailkonu
            // 
            this.txtmailkonu.Location = new System.Drawing.Point(40, 140);
            this.txtmailkonu.Name = "txtmailkonu";
            this.txtmailkonu.PlaceholderText = "Konu *";
            this.txtmailkonu.Size = new System.Drawing.Size(420, 40);
            this.txtmailkonu.TabIndex = 1;
            // 
            // txtmailadres
            // 
            this.txtmailadres.Location = new System.Drawing.Point(40, 90);
            this.txtmailadres.Name = "txtmailadres";
            this.txtmailadres.PlaceholderText = "Alıcı e-posta *";
            this.txtmailadres.Size = new System.Drawing.Size(420, 40);
            this.txtmailadres.TabIndex = 0;
            // 
            // grpAiAsistan
            // 
            this.grpAiAsistan.Controls.Add(this.lblStatus);
            this.grpAiAsistan.Controls.Add(this.progressBarAi);
            this.grpAiAsistan.Controls.Add(this.btnGovdeyeAktar);
            this.grpAiAsistan.Controls.Add(this.btnYenidenUret);
            this.grpAiAsistan.Controls.Add(this.btnSablonOner);
            this.grpAiAsistan.Controls.Add(this.txtOnizleme);
            this.grpAiAsistan.Controls.Add(this.lblOnizleme);
            this.grpAiAsistan.Controls.Add(this.cmbKonu);
            this.grpAiAsistan.Controls.Add(this.lblKonu);
            this.grpAiAsistan.Controls.Add(this.cmbUzunluk);
            this.grpAiAsistan.Controls.Add(this.lblUzunluk);
            this.grpAiAsistan.Controls.Add(this.cmbTon);
            this.grpAiAsistan.Controls.Add(this.lblTon);
            this.grpAiAsistan.Controls.Add(this.cmbScenario);
            this.grpAiAsistan.Controls.Add(this.lblScenario);
            this.grpAiAsistan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.grpAiAsistan.Location = new System.Drawing.Point(510, 0);
            this.grpAiAsistan.Name = "grpAiAsistan";
            this.grpAiAsistan.Size = new System.Drawing.Size(440, 520);
            this.grpAiAsistan.TabIndex = 1;
            this.grpAiAsistan.TabStop = false;
            this.grpAiAsistan.Text = "🤖 AI E-posta Asistanı";
            // 
            // lblScenario
            // 
            this.lblScenario.AutoSize = true;
            this.lblScenario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblScenario.Location = new System.Drawing.Point(20, 35);
            this.lblScenario.Name = "lblScenario";
            this.lblScenario.Size = new System.Drawing.Size(65, 17);
            this.lblScenario.TabIndex = 0;
            this.lblScenario.Text = "Senaryo:";
            // 
            // cmbScenario
            // 
            this.cmbScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScenario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbScenario.FormattingEnabled = true;
            this.cmbScenario.Items.AddRange(new object[] {
            "Teklif",
            "Teşekkür",
            "Ödeme Hatırlatma",
            "Teslimat Bilgi",
            "Genel Yanıt"});
            this.cmbScenario.Location = new System.Drawing.Point(100, 32);
            this.cmbScenario.Name = "cmbScenario";
            this.cmbScenario.Size = new System.Drawing.Size(320, 24);
            this.cmbScenario.TabIndex = 1;
            // 
            // lblTon
            // 
            this.lblTon.AutoSize = true;
            this.lblTon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTon.Location = new System.Drawing.Point(20, 70);
            this.lblTon.Name = "lblTon";
            this.lblTon.Size = new System.Drawing.Size(36, 17);
            this.lblTon.TabIndex = 2;
            this.lblTon.Text = "Ton:";
            // 
            // cmbTon
            // 
            this.cmbTon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbTon.FormattingEnabled = true;
            this.cmbTon.Items.AddRange(new object[] {
            "Resmi",
            "Nötr",
            "Samimi",
            "Acil"});
            this.cmbTon.Location = new System.Drawing.Point(100, 67);
            this.cmbTon.Name = "cmbTon";
            this.cmbTon.Size = new System.Drawing.Size(320, 24);
            this.cmbTon.TabIndex = 3;
            // 
            // lblUzunluk
            // 
            this.lblUzunluk.AutoSize = true;
            this.lblUzunluk.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblUzunluk.Location = new System.Drawing.Point(20, 105);
            this.lblUzunluk.Name = "lblUzunluk";
            this.lblUzunluk.Size = new System.Drawing.Size(64, 17);
            this.lblUzunluk.TabIndex = 4;
            this.lblUzunluk.Text = "Uzunluk:";
            // 
            // cmbUzunluk
            // 
            this.cmbUzunluk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUzunluk.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbUzunluk.FormattingEnabled = true;
            this.cmbUzunluk.Items.AddRange(new object[] {
            "Kısa",
            "Orta",
            "Uzun"});
            this.cmbUzunluk.Location = new System.Drawing.Point(100, 102);
            this.cmbUzunluk.Name = "cmbUzunluk";
            this.cmbUzunluk.Size = new System.Drawing.Size(320, 24);
            this.cmbUzunluk.TabIndex = 5;
            // 
            // lblKonu
            // 
            this.lblKonu.AutoSize = true;
            this.lblKonu.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblKonu.Location = new System.Drawing.Point(20, 140);
            this.lblKonu.Name = "lblKonu";
            this.lblKonu.Size = new System.Drawing.Size(110, 17);
            this.lblKonu.TabIndex = 6;
            this.lblKonu.Text = "Konu Satırı Seç:";
            // 
            // cmbKonu
            // 
            this.cmbKonu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKonu.Enabled = false;
            this.cmbKonu.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbKonu.FormattingEnabled = true;
            this.cmbKonu.Location = new System.Drawing.Point(20, 160);
            this.cmbKonu.Name = "cmbKonu";
            this.cmbKonu.Size = new System.Drawing.Size(400, 24);
            this.cmbKonu.TabIndex = 7;
            // 
            // lblOnizleme
            // 
            this.lblOnizleme.AutoSize = true;
            this.lblOnizleme.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblOnizleme.Location = new System.Drawing.Point(20, 195);
            this.lblOnizleme.Name = "lblOnizleme";
            this.lblOnizleme.Size = new System.Drawing.Size(72, 17);
            this.lblOnizleme.TabIndex = 8;
            this.lblOnizleme.Text = "Önizleme:";
            // 
            // txtOnizleme
            // 
            this.txtOnizleme.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOnizleme.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtOnizleme.Location = new System.Drawing.Point(20, 215);
            this.txtOnizleme.Name = "txtOnizleme";
            this.txtOnizleme.ReadOnly = true;
            this.txtOnizleme.Size = new System.Drawing.Size(400, 150);
            this.txtOnizleme.TabIndex = 9;
            this.txtOnizleme.Text = "";
            // 
            // btnSablonOner
            // 
            this.btnSablonOner.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.btnSablonOner.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSablonOner.Location = new System.Drawing.Point(20, 375);
            this.btnSablonOner.Name = "btnSablonOner";
            this.btnSablonOner.Size = new System.Drawing.Size(130, 40);
            this.btnSablonOner.TabIndex = 10;
            this.btnSablonOner.Text = "Şablon Öner";
            this.btnSablonOner.Click += new System.EventHandler(this.btnSablonOner_Click);
            // 
            // btnYenidenUret
            // 
            this.btnYenidenUret.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.btnYenidenUret.Enabled = false;
            this.btnYenidenUret.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnYenidenUret.Location = new System.Drawing.Point(160, 375);
            this.btnYenidenUret.Name = "btnYenidenUret";
            this.btnYenidenUret.Size = new System.Drawing.Size(130, 40);
            this.btnYenidenUret.TabIndex = 11;
            this.btnYenidenUret.Text = "Yeniden Üret";
            this.btnYenidenUret.Click += new System.EventHandler(this.btnYenidenUret_Click);
            // 
            // btnGovdeyeAktar
            // 
            this.btnGovdeyeAktar.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Success;
            this.btnGovdeyeAktar.Enabled = false;
            this.btnGovdeyeAktar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnGovdeyeAktar.Location = new System.Drawing.Point(300, 375);
            this.btnGovdeyeAktar.Name = "btnGovdeyeAktar";
            this.btnGovdeyeAktar.Size = new System.Drawing.Size(120, 40);
            this.btnGovdeyeAktar.TabIndex = 12;
            this.btnGovdeyeAktar.Text = "Gövdeye Aktar";
            this.btnGovdeyeAktar.Click += new System.EventHandler(this.btnGovdeyeAktar_Click);
            // 
            // progressBarAi
            // 
            this.progressBarAi.Location = new System.Drawing.Point(20, 425);
            this.progressBarAi.Name = "progressBarAi";
            this.progressBarAi.Size = new System.Drawing.Size(400, 23);
            this.progressBarAi.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarAi.TabIndex = 13;
            this.progressBarAi.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblStatus.Location = new System.Drawing.Point(20, 455);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 14);
            this.lblStatus.TabIndex = 14;
            // 
            // FrmMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 520);
            this.Controls.Add(this.grpAiAsistan);
            this.Controls.Add(this.pnlMail);
            this.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAİL";
            this.Load += new System.EventHandler(this.FrmMail_Load);
            this.pnlMail.ResumeLayout(false);
            this.grpAiAsistan.ResumeLayout(false);
            this.grpAiAsistan.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // Modern kontroller - DevExpress'ten dönüştürüldü
        private operion.Presentation.Controls.ModernPanel pnlMail;
        private operion.Presentation.Controls.ModernButton Btngonder;
        private System.Windows.Forms.RichTextBox rchmailmesaj;
        private operion.Presentation.Controls.ModernTextBox txtmailkonu;
        private operion.Presentation.Controls.ModernTextBox txtmailadres;
        private System.Windows.Forms.GroupBox grpAiAsistan;
        private System.Windows.Forms.Label lblScenario;
        private System.Windows.Forms.ComboBox cmbScenario;
        private System.Windows.Forms.Label lblTon;
        private System.Windows.Forms.ComboBox cmbTon;
        private System.Windows.Forms.Label lblUzunluk;
        private System.Windows.Forms.ComboBox cmbUzunluk;
        private System.Windows.Forms.Label lblKonu;
        private System.Windows.Forms.ComboBox cmbKonu;
        private System.Windows.Forms.Label lblOnizleme;
        private System.Windows.Forms.RichTextBox txtOnizleme;
        private operion.Presentation.Controls.ModernButton btnSablonOner;
        private operion.Presentation.Controls.ModernButton btnYenidenUret;
        private operion.Presentation.Controls.ModernButton btnGovdeyeAktar;
        private System.Windows.Forms.ProgressBar progressBarAi;
        private System.Windows.Forms.Label lblStatus;
    }
}

