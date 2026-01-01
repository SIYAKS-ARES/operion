using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Customers
{
    partial class FrmMusteriler
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
            this.grdmusteriler = new System.Windows.Forms.DataGridView();
            this.panelForm = new ModernPanel();
            this.Btntemizle = new ModernButton();
            this.BtnGuncelle = new ModernButton();
            this.BtnSil = new ModernButton();
            this.Btnkaydet = new ModernButton();
            this.rchmusteriadres = new System.Windows.Forms.RichTextBox();
            this.cmbmusteriilce = new System.Windows.Forms.ComboBox();
            this.cmbmusteriil = new System.Windows.Forms.ComboBox();
            this.mskmusteritc = new System.Windows.Forms.MaskedTextBox();
            this.mskmusteritel2 = new System.Windows.Forms.MaskedTextBox();
            this.mskmusteritel1 = new System.Windows.Forms.MaskedTextBox();
            this.txtmusterivergidairesi = new ModernTextBox();
            this.txtmusterimail = new ModernTextBox();
            this.txtmusterisoyad = new ModernTextBox();
            this.txtmusteriad = new ModernTextBox();
            this.txtmusteriid = new ModernTextBox();
            this.labelControl11 = new System.Windows.Forms.Label();
            this.labelControl10 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.grdmusteriler)).BeginInit();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.grdmusteriler);
            this.panelMain.Controls.Add(this.hScrollBar1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.L);
            this.panelMain.Size = new System.Drawing.Size(898, 561);
            this.panelMain.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 561 - 22);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(898 - (DesignSystem.Spacing.L * 2), 22);
            this.hScrollBar1.TabIndex = 1;
            // 
            // grdmusteriler
            // 
            this.grdmusteriler.AllowUserToAddRows = false;
            this.grdmusteriler.AllowUserToDeleteRows = false;
            this.grdmusteriler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdmusteriler.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdmusteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdmusteriler.Location = new System.Drawing.Point(DesignSystem.Spacing.L, DesignSystem.Spacing.L);
            this.grdmusteriler.Name = "grdmusteriler";
            this.grdmusteriler.ReadOnly = true;
            this.grdmusteriler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdmusteriler.Size = new System.Drawing.Size(898 - (DesignSystem.Spacing.L * 2), 561 - (DesignSystem.Spacing.L * 2) - 22);
            this.grdmusteriler.TabIndex = 0;
            this.grdmusteriler.SelectionChanged += new System.EventHandler(this.grdmusteriler_SelectionChanged);
            // 
            // panelForm
            // 
            this.panelForm.BackColor = DesignSystem.Colors.Surface;
            this.panelForm.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.panelForm.Controls.Add(this.Btntemizle);
            this.panelForm.Controls.Add(this.BtnGuncelle);
            this.panelForm.Controls.Add(this.BtnSil);
            this.panelForm.Controls.Add(this.Btnkaydet);
            this.panelForm.Controls.Add(this.rchmusteriadres);
            this.panelForm.Controls.Add(this.cmbmusteriilce);
            this.panelForm.Controls.Add(this.cmbmusteriil);
            this.panelForm.Controls.Add(this.mskmusteritc);
            this.panelForm.Controls.Add(this.mskmusteritel2);
            this.panelForm.Controls.Add(this.mskmusteritel1);
            this.panelForm.Controls.Add(this.txtmusterivergidairesi);
            this.panelForm.Controls.Add(this.txtmusterimail);
            this.panelForm.Controls.Add(this.txtmusterisoyad);
            this.panelForm.Controls.Add(this.txtmusteriad);
            this.panelForm.Controls.Add(this.txtmusteriid);
            this.panelForm.Controls.Add(this.labelControl11);
            this.panelForm.Controls.Add(this.labelControl10);
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
            this.panelForm.Location = new System.Drawing.Point(898, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.L);
            this.panelForm.ShowShadow = true;
            this.panelForm.Size = new System.Drawing.Size(472, 561);
            this.panelForm.TabIndex = 1;
            this.panelForm.Title = "👤 Müşteri Bilgileri";
            // 
            // Btntemizle
            // 
            this.Btntemizle.ButtonStyle = ButtonStyle.Secondary;
            this.Btntemizle.Location = new System.Drawing.Point(355, 504);
            this.Btntemizle.Name = "Btntemizle";
            this.Btntemizle.Size = new System.Drawing.Size(105, 36);
            this.Btntemizle.TabIndex = 34;
            this.Btntemizle.Text = "Temizle";
            this.Btntemizle.UseVisualStyleBackColor = false;
            this.Btntemizle.Click += new System.EventHandler(this.Btntemizle_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = ButtonStyle.Primary;
            this.BtnGuncelle.Location = new System.Drawing.Point(222, 504);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(129, 40);
            this.BtnGuncelle.TabIndex = 23;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.UseVisualStyleBackColor = false;
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(130, 504);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(86, 40);
            this.BtnSil.TabIndex = 22;
            this.BtnSil.Text = "Sil";
            this.BtnSil.UseVisualStyleBackColor = false;
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // Btnkaydet
            // 
            this.Btnkaydet.ButtonStyle = ButtonStyle.Success;
            this.Btnkaydet.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 504);
            this.Btnkaydet.Name = "Btnkaydet";
            this.Btnkaydet.Size = new System.Drawing.Size(111, 40);
            this.Btnkaydet.TabIndex = 19;
            this.Btnkaydet.Text = "Kaydet";
            this.Btnkaydet.UseVisualStyleBackColor = false;
            this.Btnkaydet.Click += new System.EventHandler(this.Btnkaydet_Click);
            // 
            // rchmusteriadres
            // 
            this.rchmusteriadres.Font = DesignSystem.Fonts.Body;
            this.rchmusteriadres.Location = new System.Drawing.Point(176, 420);
            this.rchmusteriadres.Name = "rchmusteriadres";
            this.rchmusteriadres.Size = new System.Drawing.Size(154, 63);
            this.rchmusteriadres.TabIndex = 20;
            this.rchmusteriadres.Text = "";
            this.rchmusteriadres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // cmbmusteriilce
            // 
            this.cmbmusteriilce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmusteriilce.Font = DesignSystem.Fonts.Body;
            this.cmbmusteriilce.FormattingEnabled = true;
            this.cmbmusteriilce.Location = new System.Drawing.Point(176, 358);
            this.cmbmusteriilce.Name = "cmbmusteriilce";
            this.cmbmusteriilce.Size = new System.Drawing.Size(154, 28);
            this.cmbmusteriilce.TabIndex = 32;
            // 
            // cmbmusteriil
            // 
            this.cmbmusteriil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmusteriil.Font = DesignSystem.Fonts.Body;
            this.cmbmusteriil.FormattingEnabled = true;
            this.cmbmusteriil.Location = new System.Drawing.Point(176, 328);
            this.cmbmusteriil.Name = "cmbmusteriil";
            this.cmbmusteriil.Size = new System.Drawing.Size(154, 28);
            this.cmbmusteriil.TabIndex = 31;
            this.cmbmusteriil.SelectedIndexChanged += new System.EventHandler(this.cmbmusteriil_SelectedIndexChanged);
            // 
            // mskmusteritc
            // 
            this.mskmusteritc.Font = DesignSystem.Fonts.Body;
            this.mskmusteritc.Location = new System.Drawing.Point(176, 249);
            this.mskmusteritc.Mask = "00000000000";
            this.mskmusteritc.Name = "mskmusteritc";
            this.mskmusteritc.Size = new System.Drawing.Size(154, 24);
            this.mskmusteritc.TabIndex = 30;
            // 
            // mskmusteritel2
            // 
            this.mskmusteritel2.Font = DesignSystem.Fonts.Body;
            this.mskmusteritel2.Location = new System.Drawing.Point(176, 214);
            this.mskmusteritel2.Mask = "(999) 000-0000";
            this.mskmusteritel2.Name = "mskmusteritel2";
            this.mskmusteritel2.Size = new System.Drawing.Size(154, 24);
            this.mskmusteritel2.TabIndex = 29;
            // 
            // mskmusteritel1
            // 
            this.mskmusteritel1.Font = DesignSystem.Fonts.Body;
            this.mskmusteritel1.Location = new System.Drawing.Point(176, 169);
            this.mskmusteritel1.Mask = "(999) 000-0000";
            this.mskmusteritel1.Name = "mskmusteritel1";
            this.mskmusteritel1.Size = new System.Drawing.Size(154, 24);
            this.mskmusteritel1.TabIndex = 28;
            // 
            // txtmusterivergidairesi
            // 
            this.txtmusterivergidairesi.Location = new System.Drawing.Point(175, 393);
            this.txtmusterivergidairesi.Name = "txtmusterivergidairesi";
            this.txtmusterivergidairesi.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txtmusterivergidairesi.TabIndex = 27;
            this.txtmusterivergidairesi.PlaceholderText = "Vergi Dairesi";
            // 
            // txtmusterimail
            // 
            this.txtmusterimail.Location = new System.Drawing.Point(176, 291);
            this.txtmusterimail.Name = "txtmusterimail";
            this.txtmusterimail.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txtmusterimail.TabIndex = 13;
            this.txtmusterimail.PlaceholderText = "email@example.com";
            // 
            // txtmusterisoyad
            // 
            this.txtmusterisoyad.Location = new System.Drawing.Point(176, 126);
            this.txtmusterisoyad.Name = "txtmusterisoyad";
            this.txtmusterisoyad.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txtmusterisoyad.TabIndex = 6;
            this.txtmusterisoyad.PlaceholderText = "Soyad";
            // 
            // txtmusteriad
            // 
            this.txtmusteriad.Location = new System.Drawing.Point(176, 82);
            this.txtmusteriad.Name = "txtmusteriad";
            this.txtmusteriad.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txtmusteriad.TabIndex = 4;
            this.txtmusteriad.PlaceholderText = "Ad";
            // 
            // txtmusteriid
            // 
            this.txtmusteriid.Location = new System.Drawing.Point(176, 39);
            this.txtmusteriid.Name = "txtmusteriid";
            this.txtmusteriid.ReadOnly = true;
            this.txtmusteriid.Size = new System.Drawing.Size(154, DesignSystem.ControlSizes.InputHeight);
            this.txtmusteriid.TabIndex = 1;
            this.txtmusteriid.PlaceholderText = "Otomatik";
            // 
            // labelControl11
            // 
            this.labelControl11.AutoSize = true;
            this.labelControl11.Font = DesignSystem.Fonts.Body;
            this.labelControl11.ForeColor = DesignSystem.Colors.Text;
            this.labelControl11.Location = new System.Drawing.Point(44, 396);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(118, 20);
            this.labelControl11.TabIndex = 26;
            this.labelControl11.Text = "Vergi Dairesi:";
            // 
            // labelControl10
            // 
            this.labelControl10.AutoSize = true;
            this.labelControl10.Font = DesignSystem.Fonts.Body;
            this.labelControl10.ForeColor = DesignSystem.Colors.Text;
            this.labelControl10.Location = new System.Drawing.Point(99, 438);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(63, 20);
            this.labelControl10.TabIndex = 25;
            this.labelControl10.Text = "Adres:";
            // 
            // labelControl9
            // 
            this.labelControl9.AutoSize = true;
            this.labelControl9.Font = DesignSystem.Fonts.Body;
            this.labelControl9.ForeColor = DesignSystem.Colors.Text;
            this.labelControl9.Location = new System.Drawing.Point(118, 361);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(44, 20);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "İlçe:";
            // 
            // labelControl8
            // 
            this.labelControl8.AutoSize = true;
            this.labelControl8.Font = DesignSystem.Fonts.Body;
            this.labelControl8.ForeColor = DesignSystem.Colors.Text;
            this.labelControl8.Location = new System.Drawing.Point(139, 327);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(23, 20);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "İl:";
            // 
            // labelControl7
            // 
            this.labelControl7.AutoSize = true;
            this.labelControl7.Font = DesignSystem.Fonts.Body;
            this.labelControl7.ForeColor = DesignSystem.Colors.Text;
            this.labelControl7.Location = new System.Drawing.Point(117, 294);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(45, 20);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "Mail:";
            // 
            // labelControl6
            // 
            this.labelControl6.AutoSize = true;
            this.labelControl6.Font = DesignSystem.Fonts.Body;
            this.labelControl6.ForeColor = DesignSystem.Colors.Text;
            this.labelControl6.Location = new System.Drawing.Point(70, 214);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(93, 20);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Telefon 2:";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSize = true;
            this.labelControl5.Font = DesignSystem.Fonts.Body;
            this.labelControl5.ForeColor = DesignSystem.Colors.Text;
            this.labelControl5.Location = new System.Drawing.Point(130, 249);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(32, 20);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "TC:";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSize = true;
            this.labelControl4.Font = DesignSystem.Fonts.Body;
            this.labelControl4.ForeColor = DesignSystem.Colors.Text;
            this.labelControl4.Location = new System.Drawing.Point(74, 172);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(89, 20);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Telefon 1:";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Font = DesignSystem.Fonts.Body;
            this.labelControl3.ForeColor = DesignSystem.Colors.Text;
            this.labelControl3.Location = new System.Drawing.Point(100, 132);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 20);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Soyad:";
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
            // FrmMusteriler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = DesignSystem.Colors.Background;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmMusteriler";
            this.Text = "Müşteriler - operion";
            this.Load += new System.EventHandler(this.FrmMusteriler_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdmusteriler)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView grdmusteriler;
        private ModernPanel panelForm;
        private ModernButton BtnGuncelle;
        private ModernButton BtnSil;
        private System.Windows.Forms.RichTextBox rchmusteriadres;
        private ModernButton Btnkaydet;
        private System.Windows.Forms.Label labelControl9;
        private System.Windows.Forms.Label labelControl8;
        private ModernTextBox txtmusterimail;
        private System.Windows.Forms.Label labelControl7;
        private System.Windows.Forms.Label labelControl6;
        private System.Windows.Forms.Label labelControl5;
        private System.Windows.Forms.Label labelControl4;
        private ModernTextBox txtmusterisoyad;
        private System.Windows.Forms.Label labelControl3;
        private ModernTextBox txtmusteriad;
        private System.Windows.Forms.Label labelControl2;
        private ModernTextBox txtmusteriid;
        private System.Windows.Forms.Label labelControl1;
        private ModernButton Btntemizle;
        private System.Windows.Forms.ComboBox cmbmusteriilce;
        private System.Windows.Forms.ComboBox cmbmusteriil;
        private System.Windows.Forms.MaskedTextBox mskmusteritc;
        private System.Windows.Forms.MaskedTextBox mskmusteritel2;
        private System.Windows.Forms.MaskedTextBox mskmusteritel1;
        private ModernTextBox txtmusterivergidairesi;
        private System.Windows.Forms.Label labelControl11;
        private System.Windows.Forms.Label labelControl10;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
