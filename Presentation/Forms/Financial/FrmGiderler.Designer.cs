namespace operion.Presentation.Forms.Financial
{
    partial class FrmGiderler
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
            this.grdgiderler = new System.Windows.Forms.DataGridView();
            this.pnlGiderBilgileri = new operion.Presentation.Controls.ModernPanel();
            this.Btntemizle = new operion.Presentation.Controls.ModernButton();
            this.BtnGuncelle = new operion.Presentation.Controls.ModernButton();
            this.BtnSil = new operion.Presentation.Controls.ModernButton();
            this.Btnkaydet = new operion.Presentation.Controls.ModernButton();
            this.rchtxtgiderlernotlar = new System.Windows.Forms.RichTextBox();
            this.txtgiderlerekstra = new operion.Presentation.Controls.ModernTextBox();
            this.txtgiderlermaaslar = new operion.Presentation.Controls.ModernTextBox();
            this.txtgiderlerinternet = new operion.Presentation.Controls.ModernTextBox();
            this.txtgiderlerdogalgaz = new operion.Presentation.Controls.ModernTextBox();
            this.txtgiderlersu = new operion.Presentation.Controls.ModernTextBox();
            this.txtgiderlerelektrik = new operion.Presentation.Controls.ModernTextBox();
            this.cmbgiderleryil = new System.Windows.Forms.ComboBox();
            this.cmbgiderleray = new System.Windows.Forms.ComboBox();
            this.txtgiderlerid = new operion.Presentation.Controls.ModernTextBox();
            this.labelControl11 = new System.Windows.Forms.Label();
            this.labelControl10 = new System.Windows.Forms.Label();
            this.labelControl9 = new System.Windows.Forms.Label();
            this.labelControl8 = new System.Windows.Forms.Label();
            this.labelControl7 = new System.Windows.Forms.Label();
            this.labelControl5 = new System.Windows.Forms.Label();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.grdgiderler)).BeginInit();
            this.pnlGiderBilgileri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdgiderler
            // 
            this.grdgiderler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdgiderler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdgiderler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdgiderler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdgiderler.Location = new System.Drawing.Point(0, 0);
            this.grdgiderler.Name = "grdgiderler";
            this.grdgiderler.ReadOnly = true;
            this.grdgiderler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdgiderler.Size = new System.Drawing.Size(1000, 561);
            this.grdgiderler.TabIndex = 6;
            this.grdgiderler.SelectionChanged += new System.EventHandler(this.grdgiderler_SelectionChanged);
            // 
            // pnlGiderBilgileri
            // 
            this.pnlGiderBilgileri.Controls.Add(this.Btntemizle);
            this.pnlGiderBilgileri.Controls.Add(this.BtnGuncelle);
            this.pnlGiderBilgileri.Controls.Add(this.BtnSil);
            this.pnlGiderBilgileri.Controls.Add(this.Btnkaydet);
            this.pnlGiderBilgileri.Controls.Add(this.rchtxtgiderlernotlar);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlerekstra);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlermaaslar);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlerinternet);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlerdogalgaz);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlersu);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlerelektrik);
            this.pnlGiderBilgileri.Controls.Add(this.cmbgiderleryil);
            this.pnlGiderBilgileri.Controls.Add(this.cmbgiderleray);
            this.pnlGiderBilgileri.Controls.Add(this.txtgiderlerid);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl11);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl10);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl9);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl8);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl7);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl5);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl4);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl3);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl2);
            this.pnlGiderBilgileri.Controls.Add(this.labelControl1);
            this.pnlGiderBilgileri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGiderBilgileri.Location = new System.Drawing.Point(0, 0);
            this.pnlGiderBilgileri.Name = "pnlGiderBilgileri";
            this.pnlGiderBilgileri.Size = new System.Drawing.Size(366, 561);
            this.pnlGiderBilgileri.AutoScroll = true;
            this.pnlGiderBilgileri.TabIndex = 7;
            this.pnlGiderBilgileri.Title = "💰 Gider Bilgileri";
            // 
            // Btntemizle
            // 
            this.Btntemizle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.Btntemizle.Location = new System.Drawing.Point(20, 650);
            this.Btntemizle.Name = "Btntemizle";
            this.Btntemizle.Size = new System.Drawing.Size(320, 40);
            this.Btntemizle.TabIndex = 33;
            this.Btntemizle.Text = "Temizle";
            this.Btntemizle.Click += new System.EventHandler(this.Btntemizle_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnGuncelle.Location = new System.Drawing.Point(220, 600);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(120, 40);
            this.BtnGuncelle.TabIndex = 23;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(120, 600);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(90, 40);
            this.BtnSil.TabIndex = 22;
            this.BtnSil.Text = "Sil";
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // Btnkaydet
            // 
            this.Btnkaydet.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Success;
            this.Btnkaydet.Location = new System.Drawing.Point(20, 600);
            this.Btnkaydet.Name = "Btnkaydet";
            this.Btnkaydet.Size = new System.Drawing.Size(90, 40);
            this.Btnkaydet.TabIndex = 19;
            this.Btnkaydet.Text = "Kaydet";
            this.Btnkaydet.Click += new System.EventHandler(this.Btnkaydet_Click);
            // 
            // rchtxtgiderlernotlar
            // 
            this.rchtxtgiderlernotlar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rchtxtgiderlernotlar.Location = new System.Drawing.Point(110, 490);
            this.rchtxtgiderlernotlar.Name = "rchtxtgiderlernotlar";
            this.rchtxtgiderlernotlar.Size = new System.Drawing.Size(230, 96);
            this.rchtxtgiderlernotlar.TabIndex = 20;
            this.rchtxtgiderlernotlar.Text = "";
            // 
            // txtgiderlerekstra
            // 
            this.txtgiderlerekstra.Location = new System.Drawing.Point(110, 440);
            this.txtgiderlerekstra.Name = "txtgiderlerekstra";
            this.txtgiderlerekstra.PlaceholderText = "Ekstra (₺)";
            this.txtgiderlerekstra.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlerekstra.TabIndex = 27;
            // 
            // txtgiderlermaaslar
            // 
            this.txtgiderlermaaslar.Location = new System.Drawing.Point(110, 390);
            this.txtgiderlermaaslar.Name = "txtgiderlermaaslar";
            this.txtgiderlermaaslar.PlaceholderText = "Maaşlar (₺)";
            this.txtgiderlermaaslar.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlermaaslar.TabIndex = 26;
            // 
            // txtgiderlerinternet
            // 
            this.txtgiderlerinternet.Location = new System.Drawing.Point(110, 340);
            this.txtgiderlerinternet.Name = "txtgiderlerinternet";
            this.txtgiderlerinternet.PlaceholderText = "Internet (₺)";
            this.txtgiderlerinternet.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlerinternet.TabIndex = 25;
            // 
            // txtgiderlerdogalgaz
            // 
            this.txtgiderlerdogalgaz.Location = new System.Drawing.Point(110, 290);
            this.txtgiderlerdogalgaz.Name = "txtgiderlerdogalgaz";
            this.txtgiderlerdogalgaz.PlaceholderText = "Doğalgaz (₺)";
            this.txtgiderlerdogalgaz.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlerdogalgaz.TabIndex = 24;
            // 
            // txtgiderlersu
            // 
            this.txtgiderlersu.Location = new System.Drawing.Point(110, 240);
            this.txtgiderlersu.Name = "txtgiderlersu";
            this.txtgiderlersu.PlaceholderText = "Su (₺)";
            this.txtgiderlersu.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlersu.TabIndex = 23;
            // 
            // txtgiderlerelektrik
            // 
            this.txtgiderlerelektrik.Location = new System.Drawing.Point(110, 190);
            this.txtgiderlerelektrik.Name = "txtgiderlerelektrik";
            this.txtgiderlerelektrik.PlaceholderText = "Elektrik (₺)";
            this.txtgiderlerelektrik.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlerelektrik.TabIndex = 22;
            // 
            // cmbgiderleryil
            // 
            this.cmbgiderleryil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbgiderleryil.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbgiderleryil.FormattingEnabled = true;
            this.cmbgiderleryil.Location = new System.Drawing.Point(110, 140);
            this.cmbgiderleryil.Name = "cmbgiderleryil";
            this.cmbgiderleryil.Size = new System.Drawing.Size(230, 26);
            this.cmbgiderleryil.TabIndex = 21;
            // 
            // cmbgiderleray
            // 
            this.cmbgiderleray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbgiderleray.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbgiderleray.FormattingEnabled = true;
            this.cmbgiderleray.Location = new System.Drawing.Point(110, 90);
            this.cmbgiderleray.Name = "cmbgiderleray";
            this.cmbgiderleray.Size = new System.Drawing.Size(230, 26);
            this.cmbgiderleray.TabIndex = 20;
            // 
            // txtgiderlerid
            // 
            this.txtgiderlerid.Location = new System.Drawing.Point(110, 40);
            this.txtgiderlerid.Name = "txtgiderlerid";
            this.txtgiderlerid.PlaceholderText = "ID (Otomatik)";
            this.txtgiderlerid.ReadOnly = true;
            this.txtgiderlerid.Size = new System.Drawing.Size(230, 40);
            this.txtgiderlerid.TabIndex = 1;
            // 
            // labelControl11
            // 
            this.labelControl11.AutoSize = true;
            this.labelControl11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl11.Location = new System.Drawing.Point(20, 450);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(71, 18);
            this.labelControl11.TabIndex = 26;
            this.labelControl11.Text = "EKSTRA : ";
            // 
            // labelControl10
            // 
            this.labelControl10.AutoSize = true;
            this.labelControl10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl10.Location = new System.Drawing.Point(20, 500);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(72, 18);
            this.labelControl10.TabIndex = 25;
            this.labelControl10.Text = "NOTLAR : ";
            // 
            // labelControl9
            // 
            this.labelControl9.AutoSize = true;
            this.labelControl9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl9.Location = new System.Drawing.Point(20, 400);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(81, 18);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "MAAŞLAR : ";
            // 
            // labelControl8
            // 
            this.labelControl8.AutoSize = true;
            this.labelControl8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl8.Location = new System.Drawing.Point(20, 350);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(86, 18);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "İNTERNET : ";
            // 
            // labelControl7
            // 
            this.labelControl7.AutoSize = true;
            this.labelControl7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl7.Location = new System.Drawing.Point(20, 300);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(86, 18);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "DOĞALGAZ : ";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSize = true;
            this.labelControl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl5.Location = new System.Drawing.Point(20, 250);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 18);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "SU : ";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSize = true;
            this.labelControl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl4.Location = new System.Drawing.Point(20, 200);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(86, 18);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "ELEKTRİK : ";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl3.Location = new System.Drawing.Point(20, 150);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 18);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "YIL : ";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Location = new System.Drawing.Point(20, 100);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 18);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "AY : ";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Location = new System.Drawing.Point(20, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ID : ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdgiderler);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlGiderBilgileri);
            this.splitContainer1.Size = new System.Drawing.Size(1370, 561);
            this.splitContainer1.SplitterDistance = 1000;
            this.splitContainer1.TabIndex = 8;
            // 
            // FrmGiderler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmGiderler";
            this.Text = "GİDERLER";
            this.Load += new System.EventHandler(this.FrmGiderler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdgiderler)).EndInit();
            this.pnlGiderBilgileri.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Modern kontroller - DevExpress'ten dönüştürüldü
        private System.Windows.Forms.DataGridView grdgiderler;
        private operion.Presentation.Controls.ModernPanel pnlGiderBilgileri;
        private operion.Presentation.Controls.ModernButton Btntemizle;
        private operion.Presentation.Controls.ModernButton BtnGuncelle;
        private operion.Presentation.Controls.ModernButton BtnSil;
        private operion.Presentation.Controls.ModernButton Btnkaydet;
        private System.Windows.Forms.RichTextBox rchtxtgiderlernotlar;
        private operion.Presentation.Controls.ModernTextBox txtgiderlerekstra;
        private operion.Presentation.Controls.ModernTextBox txtgiderlermaaslar;
        private operion.Presentation.Controls.ModernTextBox txtgiderlerinternet;
        private operion.Presentation.Controls.ModernTextBox txtgiderlerdogalgaz;
        private operion.Presentation.Controls.ModernTextBox txtgiderlersu;
        private operion.Presentation.Controls.ModernTextBox txtgiderlerelektrik;
        private System.Windows.Forms.ComboBox cmbgiderleryil;
        private System.Windows.Forms.ComboBox cmbgiderleray;
        private operion.Presentation.Controls.ModernTextBox txtgiderlerid;
        private System.Windows.Forms.Label labelControl11;
        private System.Windows.Forms.Label labelControl10;
        private System.Windows.Forms.Label labelControl9;
        private System.Windows.Forms.Label labelControl8;
        private System.Windows.Forms.Label labelControl7;
        private System.Windows.Forms.Label labelControl5;
        private System.Windows.Forms.Label labelControl4;
        private System.Windows.Forms.Label labelControl3;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
