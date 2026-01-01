using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Products
{
    partial class FrmUrunler
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.grdurunler = new System.Windows.Forms.DataGridView();
            this.panelForm = new ModernPanel();
            this.Btntemizle = new ModernButton();
            this.BtnGuncelle = new ModernButton();
            this.BtnSil = new ModernButton();
            this.Btnkaydet = new ModernButton();
            this.nudadet = new System.Windows.Forms.NumericUpDown();
            this.rchdetay = new System.Windows.Forms.RichTextBox();
            this.txturunsatisfiyat = new ModernTextBox();
            this.txturunalisfiyat = new ModernTextBox();
            this.txturunmodel = new ModernTextBox();
            this.txturunmarka = new ModernTextBox();
            this.txturunad = new ModernTextBox();
            this.mskyil = new System.Windows.Forms.MaskedTextBox();
            this.txturunid = new ModernTextBox();
            this.labelControl9 = new System.Windows.Forms.Label();
            this.labelControl8 = new System.Windows.Forms.Label();
            this.labelControl7 = new System.Windows.Forms.Label();
            this.labelControl6 = new System.Windows.Forms.Label();
            this.labelControl5 = new System.Windows.Forms.Label();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdurunler)).BeginInit();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudadet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.grdurunler);
            this.panelMain.Controls.Add(this.hScrollBar1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.L);
            this.panelMain.Size = new System.Drawing.Size(998, 561);
            this.panelMain.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 561 - 22);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(998 - (DesignSystem.Spacing.L * 2), 22);
            this.hScrollBar1.TabIndex = 1;
            // 
            // grdurunler
            // 
            this.grdurunler.AllowUserToAddRows = false;
            this.grdurunler.AllowUserToDeleteRows = false;
            this.grdurunler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdurunler.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdurunler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdurunler.Location = new System.Drawing.Point(DesignSystem.Spacing.L, DesignSystem.Spacing.L);
            this.grdurunler.Name = "grdurunler";
            this.grdurunler.ReadOnly = true;
            this.grdurunler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdurunler.Size = new System.Drawing.Size(998 - (DesignSystem.Spacing.L * 2), 561 - (DesignSystem.Spacing.L * 2) - 22);
            this.grdurunler.TabIndex = 0;
            this.grdurunler.SelectionChanged += new System.EventHandler(this.grdurunler_SelectionChanged);
            // 
            // panelForm
            // 
            this.panelForm.BackColor = DesignSystem.Colors.Surface;
            this.panelForm.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.panelForm.Controls.Add(this.Btntemizle);
            this.panelForm.Controls.Add(this.BtnGuncelle);
            this.panelForm.Controls.Add(this.BtnSil);
            this.panelForm.Controls.Add(this.Btnkaydet);
            this.panelForm.Controls.Add(this.nudadet);
            this.panelForm.Controls.Add(this.rchdetay);
            this.panelForm.Controls.Add(this.txturunsatisfiyat);
            this.panelForm.Controls.Add(this.txturunalisfiyat);
            this.panelForm.Controls.Add(this.txturunmodel);
            this.panelForm.Controls.Add(this.txturunmarka);
            this.panelForm.Controls.Add(this.txturunad);
            this.panelForm.Controls.Add(this.mskyil);
            this.panelForm.Controls.Add(this.txturunid);
            this.panelForm.Controls.Add(this.labelControl9);
            this.panelForm.Controls.Add(this.labelControl8);
            this.panelForm.Controls.Add(this.labelControl7);
            this.panelForm.Controls.Add(this.labelControl6);
            this.panelForm.Controls.Add(this.labelControl5);
            this.panelForm.Controls.Add(this.labelControl4);
            this.panelForm.Controls.Add(this.labelControl3);
            this.panelForm.Controls.Add(this.labelControl2);
            this.panelForm.Controls.Add(this.labelControl1);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(998, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.L);
            this.panelForm.ShowShadow = true;
            this.panelForm.ShowTitle = true;
            this.panelForm.Size = new System.Drawing.Size(372, 561);
            this.panelForm.TabIndex = 1;
            this.panelForm.Title = "📦 Ürün Bilgileri";
            // 
            // Btntemizle
            // 
            this.Btntemizle.ButtonStyle = ButtonStyle.Secondary;
            this.Btntemizle.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 487);
            this.Btntemizle.Name = "Btntemizle";
            this.Btntemizle.Size = new System.Drawing.Size(111, 36);
            this.Btntemizle.TabIndex = 34;
            this.Btntemizle.Text = "Temizle";
            this.Btntemizle.UseVisualStyleBackColor = false;
            this.Btntemizle.Click += new System.EventHandler(this.Btntemizle_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = ButtonStyle.Primary;
            this.BtnGuncelle.Location = new System.Drawing.Point(222, 439);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(131, 40);
            this.BtnGuncelle.TabIndex = 23;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.UseVisualStyleBackColor = false;
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(121, 437);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(90, 40);
            this.BtnSil.TabIndex = 22;
            this.BtnSil.Text = "Sil";
            this.BtnSil.UseVisualStyleBackColor = false;
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // Btnkaydet
            // 
            this.Btnkaydet.ButtonStyle = ButtonStyle.Success;
            this.Btnkaydet.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 439);
            this.Btnkaydet.Name = "Btnkaydet";
            this.Btnkaydet.Size = new System.Drawing.Size(102, 40);
            this.Btnkaydet.TabIndex = 19;
            this.Btnkaydet.Text = "Kaydet";
            this.Btnkaydet.UseVisualStyleBackColor = false;
            this.Btnkaydet.Click += new System.EventHandler(this.Btnkaydet_Click);
            // 
            // nudadet
            // 
            this.nudadet.Font = DesignSystem.Fonts.Body;
            this.nudadet.Location = new System.Drawing.Point(176, 247);
            this.nudadet.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudadet.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudadet.Name = "nudadet";
            this.nudadet.Size = new System.Drawing.Size(154, 24);
            this.nudadet.TabIndex = 21;
            // 
            // rchdetay
            // 
            this.rchdetay.Font = DesignSystem.Fonts.Body;
            this.rchdetay.Location = new System.Drawing.Point(176, 358);
            this.rchdetay.Name = "rchdetay";
            this.rchdetay.Size = new System.Drawing.Size(154, 63);
            this.rchdetay.TabIndex = 20;
            this.rchdetay.Text = "";
            this.rchdetay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // txturunsatisfiyat
            // 
            this.txturunsatisfiyat.Location = new System.Drawing.Point(176, 324);
            this.txturunsatisfiyat.Name = "txturunsatisfiyat";
            this.txturunsatisfiyat.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txturunsatisfiyat.TabIndex = 15;
            this.txturunsatisfiyat.PlaceholderText = "0.00";
            // 
            // txturunalisfiyat
            // 
            this.txturunalisfiyat.Location = new System.Drawing.Point(176, 291);
            this.txturunalisfiyat.Name = "txturunalisfiyat";
            this.txturunalisfiyat.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txturunalisfiyat.TabIndex = 13;
            this.txturunalisfiyat.PlaceholderText = "0.00";
            // 
            // txturunmodel
            // 
            this.txturunmodel.Location = new System.Drawing.Point(176, 169);
            this.txturunmodel.Name = "txturunmodel";
            this.txturunmodel.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txturunmodel.TabIndex = 8;
            this.txturunmodel.PlaceholderText = "Model";
            // 
            // txturunmarka
            // 
            this.txturunmarka.Location = new System.Drawing.Point(176, 126);
            this.txturunmarka.Name = "txturunmarka";
            this.txturunmarka.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txturunmarka.TabIndex = 6;
            this.txturunmarka.PlaceholderText = "Marka";
            // 
            // txturunad
            // 
            this.txturunad.Location = new System.Drawing.Point(176, 82);
            this.txturunad.Name = "txturunad";
            this.txturunad.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txturunad.TabIndex = 4;
            this.txturunad.PlaceholderText = "Ürün Adı";
            // 
            // mskyil
            // 
            this.mskyil.Font = DesignSystem.Fonts.Body;
            this.mskyil.Location = new System.Drawing.Point(176, 208);
            this.mskyil.Mask = "0000";
            this.mskyil.Name = "mskyil";
            this.mskyil.Size = new System.Drawing.Size(154, 24);
            this.mskyil.TabIndex = 2;
            // 
            // txturunid
            // 
            this.txturunid.Location = new System.Drawing.Point(176, 39);
            this.txturunid.Name = "txturunid";
            this.txturunid.ReadOnly = true;
            this.txturunid.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txturunid.TabIndex = 1;
            this.txturunid.PlaceholderText = "Otomatik";
            // 
            // labelControl9
            // 
            this.labelControl9.AutoSize = true;
            this.labelControl9.Font = DesignSystem.Fonts.Body;
            this.labelControl9.ForeColor = DesignSystem.Colors.Text;
            this.labelControl9.Location = new System.Drawing.Point(103, 361);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(60, 20);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "Detay:";
            // 
            // labelControl8
            // 
            this.labelControl8.AutoSize = true;
            this.labelControl8.Font = DesignSystem.Fonts.Body;
            this.labelControl8.ForeColor = DesignSystem.Colors.Text;
            this.labelControl8.Location = new System.Drawing.Point(67, 327);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(96, 20);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Satış Fiyat:";
            // 
            // labelControl7
            // 
            this.labelControl7.AutoSize = true;
            this.labelControl7.Font = DesignSystem.Fonts.Body;
            this.labelControl7.ForeColor = DesignSystem.Colors.Text;
            this.labelControl7.Location = new System.Drawing.Point(78, 294);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(85, 20);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "Alış Fiyat:";
            // 
            // labelControl6
            // 
            this.labelControl6.AutoSize = true;
            this.labelControl6.Font = DesignSystem.Fonts.Body;
            this.labelControl6.ForeColor = DesignSystem.Colors.Text;
            this.labelControl6.Location = new System.Drawing.Point(131, 208);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(32, 20);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Yıl:";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSize = true;
            this.labelControl5.Font = DesignSystem.Fonts.Body;
            this.labelControl5.ForeColor = DesignSystem.Colors.Text;
            this.labelControl5.Location = new System.Drawing.Point(112, 249);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(51, 20);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Adet:";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSize = true;
            this.labelControl4.Font = DesignSystem.Fonts.Body;
            this.labelControl4.ForeColor = DesignSystem.Colors.Text;
            this.labelControl4.Location = new System.Drawing.Point(101, 175);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 20);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Model:";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Font = DesignSystem.Fonts.Body;
            this.labelControl3.ForeColor = DesignSystem.Colors.Text;
            this.labelControl3.Location = new System.Drawing.Point(100, 132);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 20);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Marka:";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Font = DesignSystem.Fonts.Body;
            this.labelControl2.ForeColor = DesignSystem.Colors.Text;
            this.labelControl2.Location = new System.Drawing.Point(135, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 20);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Ad:";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = DesignSystem.Fonts.Body;
            this.labelControl1.ForeColor = DesignSystem.Colors.Text;
            this.labelControl1.Location = new System.Drawing.Point(141, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ID:";
            // 
            // FrmUrunler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = DesignSystem.Colors.Background;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmUrunler";
            this.Text = "Ürünler - operion";
            this.Load += new System.EventHandler(this.FrmUrunler_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdurunler)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudadet)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView grdurunler;
        private ModernPanel panelForm;
        private System.Windows.Forms.NumericUpDown nudadet;
        private System.Windows.Forms.RichTextBox rchdetay;
        private ModernButton Btnkaydet;
        private System.Windows.Forms.Label labelControl9;
        private ModernTextBox txturunsatisfiyat;
        private System.Windows.Forms.Label labelControl8;
        private ModernTextBox txturunalisfiyat;
        private System.Windows.Forms.Label labelControl7;
        private System.Windows.Forms.Label labelControl6;
        private System.Windows.Forms.Label labelControl5;
        private ModernTextBox txturunmodel;
        private System.Windows.Forms.Label labelControl4;
        private ModernTextBox txturunmarka;
        private System.Windows.Forms.Label labelControl3;
        private ModernTextBox txturunad;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.MaskedTextBox mskyil;
        private ModernTextBox txturunid;
        private System.Windows.Forms.Label labelControl1;
        private ModernButton BtnGuncelle;
        private ModernButton BtnSil;
        private ModernButton Btntemizle;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
