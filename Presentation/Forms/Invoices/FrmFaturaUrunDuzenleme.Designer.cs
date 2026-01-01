namespace operion.Presentation.Forms.Invoices
{
    partial class FrmFaturaUrunDuzenleme
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
            this.pnlFaturaUrunDuzenleme = new operion.Presentation.Controls.ModernPanel();
            this.BtnSil = new operion.Presentation.Controls.ModernButton();
            this.BtnGuncelle = new operion.Presentation.Controls.ModernButton();
            this.txtfaturauruntutar = new operion.Presentation.Controls.ModernTextBox();
            this.txtfaturaurunfiyat = new operion.Presentation.Controls.ModernTextBox();
            this.txtfaturaurunmiktar = new operion.Presentation.Controls.ModernTextBox();
            this.txtfaturaurunad = new operion.Presentation.Controls.ModernTextBox();
            this.txtfaturaurunid = new operion.Presentation.Controls.ModernTextBox();
            this.labelControl5 = new System.Windows.Forms.Label();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.pnlFaturaUrunDuzenleme.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFaturaUrunDuzenleme
            // 
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.BtnSil);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.BtnGuncelle);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.txtfaturauruntutar);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.txtfaturaurunfiyat);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.txtfaturaurunmiktar);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.txtfaturaurunad);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.txtfaturaurunid);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.labelControl5);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.labelControl4);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.labelControl3);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.labelControl2);
            this.pnlFaturaUrunDuzenleme.Controls.Add(this.labelControl1);
            this.pnlFaturaUrunDuzenleme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFaturaUrunDuzenleme.Location = new System.Drawing.Point(0, 0);
            this.pnlFaturaUrunDuzenleme.Name = "pnlFaturaUrunDuzenleme";
            this.pnlFaturaUrunDuzenleme.Size = new System.Drawing.Size(450, 300);
            this.pnlFaturaUrunDuzenleme.TabIndex = 0;
            this.pnlFaturaUrunDuzenleme.Title = "✏️ Fatura Ürün Düzenleme";
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(250, 240);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(150, 40);
            this.BtnSil.TabIndex = 11;
            this.BtnSil.Text = "Sil";
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Success;
            this.BtnGuncelle.Location = new System.Drawing.Point(50, 240);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(180, 40);
            this.BtnGuncelle.TabIndex = 10;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // txtfaturauruntutar
            // 
            this.txtfaturauruntutar.Location = new System.Drawing.Point(110, 198);
            this.txtfaturauruntutar.Name = "txtfaturauruntutar";
            this.txtfaturauruntutar.PlaceholderText = "Tutar (Otomatik)";
            this.txtfaturauruntutar.ReadOnly = true;
            this.txtfaturauruntutar.Size = new System.Drawing.Size(290, 40);
            this.txtfaturauruntutar.TabIndex = 9;
            // 
            // txtfaturaurunfiyat
            // 
            this.txtfaturaurunfiyat.Location = new System.Drawing.Point(110, 150);
            this.txtfaturaurunfiyat.Name = "txtfaturaurunfiyat";
            this.txtfaturaurunfiyat.PlaceholderText = "Fiyat *";
            this.txtfaturaurunfiyat.Size = new System.Drawing.Size(290, 40);
            this.txtfaturaurunfiyat.TabIndex = 8;
            // 
            // txtfaturaurunmiktar
            // 
            this.txtfaturaurunmiktar.Location = new System.Drawing.Point(110, 102);
            this.txtfaturaurunmiktar.Name = "txtfaturaurunmiktar";
            this.txtfaturaurunmiktar.PlaceholderText = "Miktar *";
            this.txtfaturaurunmiktar.Size = new System.Drawing.Size(290, 40);
            this.txtfaturaurunmiktar.TabIndex = 7;
            // 
            // txtfaturaurunad
            // 
            this.txtfaturaurunad.Location = new System.Drawing.Point(110, 54);
            this.txtfaturaurunad.Name = "txtfaturaurunad";
            this.txtfaturaurunad.PlaceholderText = "Ürün Adı *";
            this.txtfaturaurunad.Size = new System.Drawing.Size(290, 40);
            this.txtfaturaurunad.TabIndex = 6;
            // 
            // txtfaturaurunid
            // 
            this.txtfaturaurunid.Location = new System.Drawing.Point(110, 18);
            this.txtfaturaurunid.Name = "txtfaturaurunid";
            this.txtfaturaurunid.PlaceholderText = "Ürün ID (Otomatik)";
            this.txtfaturaurunid.ReadOnly = true;
            this.txtfaturaurunid.Size = new System.Drawing.Size(290, 40);
            this.txtfaturaurunid.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 205);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(80, 20);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Tutar:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 157);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 20);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Fiyat:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 109);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 20);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Miktar:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 20);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Ürün Adı:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Ürün ID:";
            // 
            // FrmFaturaUrunDuzenleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.pnlFaturaUrunDuzenleme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFaturaUrunDuzenleme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "✏️ Fatura Ürün Düzenleme";
            this.Load += new System.EventHandler(this.FaturaUrunDuzenleme_Load);
            this.pnlFaturaUrunDuzenleme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Modern kontroller - DevExpress'ten dönüştürüldü
        private operion.Presentation.Controls.ModernPanel pnlFaturaUrunDuzenleme;
        private operion.Presentation.Controls.ModernButton BtnSil;
        private operion.Presentation.Controls.ModernButton BtnGuncelle;
        private operion.Presentation.Controls.ModernTextBox txtfaturauruntutar;
        private operion.Presentation.Controls.ModernTextBox txtfaturaurunfiyat;
        private operion.Presentation.Controls.ModernTextBox txtfaturaurunmiktar;
        private operion.Presentation.Controls.ModernTextBox txtfaturaurunad;
        private operion.Presentation.Controls.ModernTextBox txtfaturaurunid;
        private System.Windows.Forms.Label labelControl5;
        private System.Windows.Forms.Label labelControl4;
        private System.Windows.Forms.Label labelControl3;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl1;
    }
}
