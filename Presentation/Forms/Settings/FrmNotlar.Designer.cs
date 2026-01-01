namespace operion.Presentation.Forms.Settings
{
    partial class FrmNotlar
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
            this.grdnotlar = new System.Windows.Forms.DataGridView();
            this.pnlNotBilgileri = new operion.Presentation.Controls.ModernPanel();
            this.Btntemizle = new operion.Presentation.Controls.ModernButton();
            this.BtnGuncelle = new operion.Presentation.Controls.ModernButton();
            this.BtnSil = new operion.Presentation.Controls.ModernButton();
            this.Btnkaydet = new operion.Presentation.Controls.ModernButton();
            this.rchnotlardetay = new System.Windows.Forms.RichTextBox();
            this.txtnotlarhitap = new operion.Presentation.Controls.ModernTextBox();
            this.txtnotlarolusturan = new operion.Presentation.Controls.ModernTextBox();
            this.txtnotlarbaslik = new operion.Presentation.Controls.ModernTextBox();
            this.txtnotlarid = new operion.Presentation.Controls.ModernTextBox();
            this.msknotlarsaat = new System.Windows.Forms.MaskedTextBox();
            this.msknotlartarih = new System.Windows.Forms.MaskedTextBox();
            this.labelControl6 = new System.Windows.Forms.Label();
            this.labelControl5 = new System.Windows.Forms.Label();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdnotlar)).BeginInit();
            this.pnlNotBilgileri.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdnotlar
            // 
            this.grdnotlar.AllowUserToAddRows = false;
            this.grdnotlar.AllowUserToDeleteRows = false;
            this.grdnotlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdnotlar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdnotlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdnotlar.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdnotlar.Location = new System.Drawing.Point(0, 0);
            this.grdnotlar.Name = "grdnotlar";
            this.grdnotlar.ReadOnly = true;
            this.grdnotlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdnotlar.Size = new System.Drawing.Size(900, 561);
            this.grdnotlar.TabIndex = 0;
            this.grdnotlar.SelectionChanged += new System.EventHandler(this.grdnotlar_SelectionChanged);
            this.grdnotlar.DoubleClick += new System.EventHandler(this.grdnotlar_DoubleClick);
            // 
            // pnlNotBilgileri
            // 
            this.pnlNotBilgileri.Controls.Add(this.Btntemizle);
            this.pnlNotBilgileri.Controls.Add(this.BtnGuncelle);
            this.pnlNotBilgileri.Controls.Add(this.BtnSil);
            this.pnlNotBilgileri.Controls.Add(this.Btnkaydet);
            this.pnlNotBilgileri.Controls.Add(this.rchnotlardetay);
            this.pnlNotBilgileri.Controls.Add(this.txtnotlarhitap);
            this.pnlNotBilgileri.Controls.Add(this.txtnotlarolusturan);
            this.pnlNotBilgileri.Controls.Add(this.txtnotlarbaslik);
            this.pnlNotBilgileri.Controls.Add(this.txtnotlarid);
            this.pnlNotBilgileri.Controls.Add(this.msknotlarsaat);
            this.pnlNotBilgileri.Controls.Add(this.msknotlartarih);
            this.pnlNotBilgileri.Controls.Add(this.labelControl6);
            this.pnlNotBilgileri.Controls.Add(this.labelControl5);
            this.pnlNotBilgileri.Controls.Add(this.labelControl4);
            this.pnlNotBilgileri.Controls.Add(this.labelControl3);
            this.pnlNotBilgileri.Controls.Add(this.labelControl2);
            this.pnlNotBilgileri.Controls.Add(this.labelControl1);
            this.pnlNotBilgileri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotBilgileri.Location = new System.Drawing.Point(900, 0);
            this.pnlNotBilgileri.Name = "pnlNotBilgileri";
            this.pnlNotBilgileri.Size = new System.Drawing.Size(470, 561);
            this.pnlNotBilgileri.AutoScroll = true;
            this.pnlNotBilgileri.TabIndex = 1;
            this.pnlNotBilgileri.Title = "📝 Not Bilgileri";
            // 
            // Btntemizle
            // 
            this.Btntemizle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Secondary;
            this.Btntemizle.Location = new System.Drawing.Point(20, 650);
            this.Btntemizle.Name = "Btntemizle";
            this.Btntemizle.Size = new System.Drawing.Size(330, 40);
            this.Btntemizle.TabIndex = 16;
            this.Btntemizle.Text = "Temizle";
            this.Btntemizle.Click += new System.EventHandler(this.Btntemizle_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.BtnGuncelle.Location = new System.Drawing.Point(230, 600);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(120, 40);
            this.BtnGuncelle.TabIndex = 15;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(130, 600);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(90, 40);
            this.BtnSil.TabIndex = 14;
            this.BtnSil.Text = "Sil";
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // Btnkaydet
            // 
            this.Btnkaydet.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Success;
            this.Btnkaydet.Location = new System.Drawing.Point(20, 600);
            this.Btnkaydet.Name = "Btnkaydet";
            this.Btnkaydet.Size = new System.Drawing.Size(100, 40);
            this.Btnkaydet.TabIndex = 13;
            this.Btnkaydet.Text = "Kaydet";
            this.Btnkaydet.Click += new System.EventHandler(this.Btnkaydet_Click);
            // 
            // rchnotlardetay
            // 
            this.rchnotlardetay.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rchnotlardetay.Location = new System.Drawing.Point(20, 320);
            this.rchnotlardetay.Name = "rchnotlardetay";
            this.rchnotlardetay.Size = new System.Drawing.Size(430, 260);
            this.rchnotlardetay.TabIndex = 12;
            this.rchnotlardetay.Text = "";
            // 
            // txtnotlarhitap
            // 
            this.txtnotlarhitap.Location = new System.Drawing.Point(90, 240);
            this.txtnotlarhitap.Name = "txtnotlarhitap";
            this.txtnotlarhitap.PlaceholderText = "Hitap";
            this.txtnotlarhitap.Size = new System.Drawing.Size(340, 40);
            this.txtnotlarhitap.TabIndex = 11;
            // 
            // txtnotlarolusturan
            // 
            this.txtnotlarolusturan.Location = new System.Drawing.Point(90, 190);
            this.txtnotlarolusturan.Name = "txtnotlarolusturan";
            this.txtnotlarolusturan.PlaceholderText = "Oluşturan *";
            this.txtnotlarolusturan.Size = new System.Drawing.Size(340, 40);
            this.txtnotlarolusturan.TabIndex = 10;
            // 
            // txtnotlarbaslik
            // 
            this.txtnotlarbaslik.Location = new System.Drawing.Point(90, 90);
            this.txtnotlarbaslik.Name = "txtnotlarbaslik";
            this.txtnotlarbaslik.PlaceholderText = "Başlık *";
            this.txtnotlarbaslik.Size = new System.Drawing.Size(340, 40);
            this.txtnotlarbaslik.TabIndex = 9;
            // 
            // txtnotlarid
            // 
            this.txtnotlarid.Location = new System.Drawing.Point(90, 40);
            this.txtnotlarid.Name = "txtnotlarid";
            this.txtnotlarid.PlaceholderText = "ID (Otomatik)";
            this.txtnotlarid.ReadOnly = true;
            this.txtnotlarid.Size = new System.Drawing.Size(340, 40);
            this.txtnotlarid.TabIndex = 1;
            // 
            // msknotlarsaat
            // 
            this.msknotlarsaat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.msknotlarsaat.Location = new System.Drawing.Point(260, 140);
            this.msknotlarsaat.Mask = "00:00";
            this.msknotlarsaat.Name = "msknotlarsaat";
            this.msknotlarsaat.Size = new System.Drawing.Size(170, 24);
            this.msknotlarsaat.TabIndex = 8;
            // 
            // msknotlartarih
            // 
            this.msknotlartarih.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.msknotlartarih.Location = new System.Drawing.Point(90, 140);
            this.msknotlartarih.Mask = "00/00/0000";
            this.msknotlartarih.Name = "msknotlartarih";
            this.msknotlartarih.Size = new System.Drawing.Size(160, 24);
            this.msknotlartarih.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.AutoSize = true;
            this.labelControl6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl6.Location = new System.Drawing.Point(20, 250);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(50, 18);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "HİTAP:";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSize = true;
            this.labelControl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl5.Location = new System.Drawing.Point(20, 300);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 18);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "DETAY:";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSize = true;
            this.labelControl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl4.Location = new System.Drawing.Point(20, 200);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(100, 18);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "OLUŞTURAN:";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl3.Location = new System.Drawing.Point(20, 100);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 18);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "BAŞLIK:";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Location = new System.Drawing.Point(280, 150);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 18);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "SAAT:";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Location = new System.Drawing.Point(20, 150);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "TARİH:";
            // 
            // FrmNotlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.pnlNotBilgileri);
            this.Controls.Add(this.grdnotlar);
            this.Name = "FrmNotlar";
            this.Text = "NOTLAR";
            this.Load += new System.EventHandler(this.FrmNotlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdnotlar)).EndInit();
            this.pnlNotBilgileri.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Modern kontroller - DevExpress'ten dönüştürüldü
        private System.Windows.Forms.DataGridView grdnotlar;
        private operion.Presentation.Controls.ModernPanel pnlNotBilgileri;
        private operion.Presentation.Controls.ModernButton Btntemizle;
        private operion.Presentation.Controls.ModernButton BtnGuncelle;
        private operion.Presentation.Controls.ModernButton BtnSil;
        private operion.Presentation.Controls.ModernButton Btnkaydet;
        private System.Windows.Forms.RichTextBox rchnotlardetay;
        private operion.Presentation.Controls.ModernTextBox txtnotlarhitap;
        private operion.Presentation.Controls.ModernTextBox txtnotlarolusturan;
        private operion.Presentation.Controls.ModernTextBox txtnotlarbaslik;
        private operion.Presentation.Controls.ModernTextBox txtnotlarid;
        private System.Windows.Forms.MaskedTextBox msknotlarsaat;
        private System.Windows.Forms.MaskedTextBox msknotlartarih;
        private System.Windows.Forms.Label labelControl6;
        private System.Windows.Forms.Label labelControl5;
        private System.Windows.Forms.Label labelControl4;
        private System.Windows.Forms.Label labelControl3;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl1;
    }
}
