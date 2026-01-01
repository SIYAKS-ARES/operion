namespace operion.Presentation.Forms.Employees
{
    partial class FrmPersoneller
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
            this.grdpersoneller = new System.Windows.Forms.DataGridView();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pnlPersonelBilgileri = new operion.Presentation.Controls.ModernPanel();
            this.Btntemizle = new operion.Presentation.Controls.ModernButton();
            this.BtnGuncelle = new operion.Presentation.Controls.ModernButton();
            this.BtnSil = new operion.Presentation.Controls.ModernButton();
            this.Btnkaydet = new operion.Presentation.Controls.ModernButton();
            this.rchpersoneladres = new System.Windows.Forms.RichTextBox();
            this.cmbpersonelilce = new System.Windows.Forms.ComboBox();
            this.cmbpersonelil = new System.Windows.Forms.ComboBox();
            this.mskpersoneltc = new System.Windows.Forms.MaskedTextBox();
            this.mskpersoneltel1 = new System.Windows.Forms.MaskedTextBox();
            this.txtpersonelgorev = new operion.Presentation.Controls.ModernTextBox();
            this.txtpersonelmail = new operion.Presentation.Controls.ModernTextBox();
            this.txtpersonelsoyad = new operion.Presentation.Controls.ModernTextBox();
            this.txtpersonelad = new operion.Presentation.Controls.ModernTextBox();
            this.txtpersonelid = new operion.Presentation.Controls.ModernTextBox();
            this.btnFotoYukle = new operion.Presentation.Controls.ModernButton();
            this.picPersonelFoto = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.picPersonelFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdpersoneller)).BeginInit();
            this.pnlPersonelBilgileri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdpersoneller
            // 
            this.grdpersoneller.AllowUserToAddRows = false;
            this.grdpersoneller.AllowUserToDeleteRows = false;
            this.grdpersoneller.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdpersoneller.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdpersoneller.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdpersoneller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdpersoneller.Location = new System.Drawing.Point(0, 0);
            this.grdpersoneller.Name = "grdpersoneller";
            this.grdpersoneller.ReadOnly = true;
            this.grdpersoneller.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdpersoneller.Size = new System.Drawing.Size(950, 561);
            this.grdpersoneller.TabIndex = 0;
            this.grdpersoneller.SelectionChanged += new System.EventHandler(this.grdpersoneller_SelectionChanged);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 539);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(950, 22);
            this.hScrollBar1.TabIndex = 1;
            // 
            // pnlPersonelBilgileri
            // 
            this.pnlPersonelBilgileri.Controls.Add(this.btnFotoYukle);
            this.pnlPersonelBilgileri.Controls.Add(this.picPersonelFoto);
            this.pnlPersonelBilgileri.Controls.Add(this.Btntemizle);
            this.pnlPersonelBilgileri.Controls.Add(this.BtnGuncelle);
            this.pnlPersonelBilgileri.Controls.Add(this.BtnSil);
            this.pnlPersonelBilgileri.Controls.Add(this.Btnkaydet);
            this.pnlPersonelBilgileri.Controls.Add(this.rchpersoneladres);
            this.pnlPersonelBilgileri.Controls.Add(this.cmbpersonelilce);
            this.pnlPersonelBilgileri.Controls.Add(this.cmbpersonelil);
            this.pnlPersonelBilgileri.Controls.Add(this.mskpersoneltc);
            this.pnlPersonelBilgileri.Controls.Add(this.mskpersoneltel1);
            this.pnlPersonelBilgileri.Controls.Add(this.txtpersonelgorev);
            this.pnlPersonelBilgileri.Controls.Add(this.txtpersonelmail);
            this.pnlPersonelBilgileri.Controls.Add(this.txtpersonelsoyad);
            this.pnlPersonelBilgileri.Controls.Add(this.txtpersonelad);
            this.pnlPersonelBilgileri.Controls.Add(this.txtpersonelid);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl11);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl10);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl9);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl8);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl7);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl5);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl4);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl3);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl2);
            this.pnlPersonelBilgileri.Controls.Add(this.labelControl1);
            this.pnlPersonelBilgileri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPersonelBilgileri.Location = new System.Drawing.Point(0, 0);
            this.pnlPersonelBilgileri.Name = "pnlPersonelBilgileri";
            this.pnlPersonelBilgileri.Size = new System.Drawing.Size(416, 561);
            this.pnlPersonelBilgileri.TabIndex = 5;
            this.pnlPersonelBilgileri.Title = "👤 Personel Bilgileri";
            // 
            // Btntemizle
            // 
            this.Btntemizle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.Btntemizle.Location = new System.Drawing.Point(15, 615);
            this.Btntemizle.Name = "Btntemizle";
            this.Btntemizle.Size = new System.Drawing.Size(340, 40);
            this.Btntemizle.TabIndex = 33;
            this.Btntemizle.Text = "Temizle";
            this.Btntemizle.Click += new System.EventHandler(this.Btntemizle_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnGuncelle.Location = new System.Drawing.Point(239, 570);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(116, 40);
            this.BtnGuncelle.TabIndex = 23;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(127, 570);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(100, 40);
            this.BtnSil.TabIndex = 22;
            this.BtnSil.Text = "Sil";
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // Btnkaydet
            // 
            this.Btnkaydet.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Success;
            this.Btnkaydet.Location = new System.Drawing.Point(15, 570);
            this.Btnkaydet.Name = "Btnkaydet";
            this.Btnkaydet.Size = new System.Drawing.Size(100, 40);
            this.Btnkaydet.TabIndex = 19;
            this.Btnkaydet.Text = "Kaydet";
            this.Btnkaydet.Click += new System.EventHandler(this.Btnkaydet_Click);
            // 
            // rchpersoneladres
            // 
            this.rchpersoneladres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rchpersoneladres.Location = new System.Drawing.Point(110, 490);
            this.rchpersoneladres.Name = "rchpersoneladres";
            this.rchpersoneladres.Size = new System.Drawing.Size(245, 60);
            this.rchpersoneladres.TabIndex = 20;
            this.rchpersoneladres.Text = "";
            // 
            // cmbpersonelilce
            // 
            this.cmbpersonelilce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpersonelilce.FormattingEnabled = true;
            this.cmbpersonelilce.Location = new System.Drawing.Point(110, 440);
            this.cmbpersonelilce.Name = "cmbpersonelilce";
            this.cmbpersonelilce.Size = new System.Drawing.Size(245, 21);
            this.cmbpersonelilce.TabIndex = 32;
            // 
            // cmbpersonelil
            // 
            this.cmbpersonelil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpersonelil.FormattingEnabled = true;
            this.cmbpersonelil.Location = new System.Drawing.Point(110, 390);
            this.cmbpersonelil.Name = "cmbpersonelil";
            this.cmbpersonelil.Size = new System.Drawing.Size(245, 21);
            this.cmbpersonelil.TabIndex = 31;
            this.cmbpersonelil.SelectedIndexChanged += new System.EventHandler(this.cmbpersonelil_SelectedIndexChanged);
            // 
            // mskpersoneltc
            // 
            this.mskpersoneltc.Location = new System.Drawing.Point(110, 290);
            this.mskpersoneltc.Mask = "00000000000";
            this.mskpersoneltc.Name = "mskpersoneltc";
            this.mskpersoneltc.Size = new System.Drawing.Size(245, 20);
            this.mskpersoneltc.TabIndex = 30;
            // 
            // mskpersoneltel1
            // 
            this.mskpersoneltel1.Location = new System.Drawing.Point(110, 240);
            this.mskpersoneltel1.Mask = "(999) 000-0000";
            this.mskpersoneltel1.Name = "mskpersoneltel1";
            this.mskpersoneltel1.Size = new System.Drawing.Size(245, 20);
            this.mskpersoneltel1.TabIndex = 28;
            // 
            // txtpersonelgorev
            // 
            this.txtpersonelgorev.Location = new System.Drawing.Point(110, 340);
            this.txtpersonelgorev.Name = "txtpersonelgorev";
            this.txtpersonelgorev.PlaceholderText = "Görev";
            this.txtpersonelgorev.Size = new System.Drawing.Size(245, 40);
            this.txtpersonelgorev.TabIndex = 21;
            // 
            // txtpersonelid
            // 
            this.txtpersonelid.Location = new System.Drawing.Point(110, 40);
            this.txtpersonelid.Name = "txtpersonelid";
            this.txtpersonelid.PlaceholderText = "ID (Otomatik)";
            this.txtpersonelid.ReadOnly = true;
            this.txtpersonelid.Size = new System.Drawing.Size(125, 40);
            this.txtpersonelid.TabIndex = 1;
            // 
            // txtpersonelad
            // 
            this.txtpersonelad.Location = new System.Drawing.Point(110, 90);
            this.txtpersonelad.Name = "txtpersonelad";
            this.txtpersonelad.PlaceholderText = "Ad *";
            this.txtpersonelad.Size = new System.Drawing.Size(125, 40);
            this.txtpersonelad.TabIndex = 4;
            // 
            // txtpersonelsoyad
            // 
            this.txtpersonelsoyad.Location = new System.Drawing.Point(110, 140);
            this.txtpersonelsoyad.Name = "txtpersonelsoyad";
            this.txtpersonelsoyad.PlaceholderText = "Soyad *";
            this.txtpersonelsoyad.Size = new System.Drawing.Size(125, 40);
            this.txtpersonelsoyad.TabIndex = 6;
            // 
            // picPersonelFoto
            // 
            this.picPersonelFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPersonelFoto.Location = new System.Drawing.Point(250, 40);
            this.picPersonelFoto.Name = "picPersonelFoto";
            this.picPersonelFoto.Size = new System.Drawing.Size(105, 120);
            this.picPersonelFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPersonelFoto.TabIndex = 34;
            this.picPersonelFoto.TabStop = false;
            // 
            // btnFotoYukle
            // 
            this.btnFotoYukle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.btnFotoYukle.Location = new System.Drawing.Point(250, 166);
            this.btnFotoYukle.Name = "btnFotoYukle";
            this.btnFotoYukle.Size = new System.Drawing.Size(105, 30);
            this.btnFotoYukle.TabIndex = 35;
            this.btnFotoYukle.Text = "Fotoğraf Yükle";
            this.btnFotoYukle.Click += new System.EventHandler(this.btnFotoYukle_Click);
            // 
            // txtpersonelmail
            // 
            this.txtpersonelmail.Location = new System.Drawing.Point(110, 190);
            this.txtpersonelmail.Name = "txtpersonelmail";
            this.txtpersonelmail.PlaceholderText = "E-posta";
            this.txtpersonelmail.Size = new System.Drawing.Size(125, 40);
            this.txtpersonelmail.TabIndex = 13;
            // 
            // labelControl11 - ADRES (Label - hidden, using placeholder)
            // 
            this.labelControl11.Location = new System.Drawing.Point(15, 500);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(80, 20);
            this.labelControl11.TabIndex = 25;
            this.labelControl11.Text = "Adres:";
            // 
            // labelControl10 - İLÇE (Label - hidden, using placeholder)
            // 
            this.labelControl10.Location = new System.Drawing.Point(15, 443);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(80, 20);
            this.labelControl10.TabIndex = 24;
            this.labelControl10.Text = "İlçe:";
            // 
            // labelControl9 - İL (Label - hidden, using placeholder)
            // 
            this.labelControl9.Location = new System.Drawing.Point(15, 393);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(80, 20);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "İl:";
            // 
            // labelControl8 - GÖREV (Label - hidden, using placeholder)
            // 
            this.labelControl8.Location = new System.Drawing.Point(15, 348);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(80, 20);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Görev:";
            // 
            // labelControl7 - MAIL (Label - hidden, using placeholder)
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 198);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(80, 20);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "E-posta:";
            // 
            // labelControl5 - TC (Label - hidden, using placeholder)
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 293);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(80, 20);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "TC:";
            // 
            // labelControl4 - TELEFON (Label - hidden, using placeholder)
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 243);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 20);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Telefon:";
            // 
            // labelControl3 - SOYAD (Label - hidden, using placeholder)
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 148);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 20);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Soyad:";
            // 
            // labelControl2 - AD (Label - hidden, using placeholder)
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 98);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 20);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Ad:";
            // 
            // labelControl1 - ID (Label - hidden, using placeholder)
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 48);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ID:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdpersoneller);
            this.splitContainer1.Panel1.Controls.Add(this.hScrollBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlPersonelBilgileri);
            this.splitContainer1.Size = new System.Drawing.Size(1370, 700);
            this.splitContainer1.SplitterDistance = 950;
            this.splitContainer1.TabIndex = 6;
            // 
            // FrmPersoneller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 700);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmPersoneller";
            this.Text = "PERSONELLER";
            this.Load += new System.EventHandler(this.FrmPersoneller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdpersoneller)).EndInit();
            this.pnlPersonelBilgileri.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPersonelFoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        // Modern kontroller - DevExpress'ten dönüştürüldü
        private System.Windows.Forms.DataGridView grdpersoneller;
        private operion.Presentation.Controls.ModernPanel pnlPersonelBilgileri;
        private operion.Presentation.Controls.ModernButton Btntemizle;
        private operion.Presentation.Controls.ModernButton BtnGuncelle;
        private operion.Presentation.Controls.ModernButton BtnSil;
        private operion.Presentation.Controls.ModernButton Btnkaydet;
        private System.Windows.Forms.RichTextBox rchpersoneladres;
        private System.Windows.Forms.ComboBox cmbpersonelilce;
        private System.Windows.Forms.ComboBox cmbpersonelil;
        private System.Windows.Forms.MaskedTextBox mskpersoneltc;
        private System.Windows.Forms.MaskedTextBox mskpersoneltel1;
        private operion.Presentation.Controls.ModernTextBox txtpersonelgorev;
        private operion.Presentation.Controls.ModernTextBox txtpersonelmail;
        private operion.Presentation.Controls.ModernTextBox txtpersonelsoyad;
        private operion.Presentation.Controls.ModernTextBox txtpersonelad;
        private operion.Presentation.Controls.ModernTextBox txtpersonelid;
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
        private System.Windows.Forms.PictureBox picPersonelFoto;
        private operion.Presentation.Controls.ModernButton btnFotoYukle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
