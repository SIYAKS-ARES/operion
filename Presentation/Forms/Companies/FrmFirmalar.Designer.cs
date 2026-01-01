using operion.Presentation.Controls;
using operion.Presentation.Controls;

namespace operion.Presentation.Forms.Companies
{
    partial class FrmFirmalar
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
            this.grdfirmalar = new System.Windows.Forms.DataGridView();
            this.panelFormContainer = new System.Windows.Forms.Panel();
            this.panelForm = new ModernPanel();
            this.Btntemizle = new ModernButton();
            this.BtnGuncelle = new ModernButton();
            this.BtnSil = new ModernButton();
            this.Btnkaydet = new ModernButton();
            this.rchfirmakod3 = new System.Windows.Forms.RichTextBox();
            this.rchfirmakod2 = new System.Windows.Forms.RichTextBox();
            this.rchfirmakod1 = new System.Windows.Forms.RichTextBox();
            this.rchfirmaadres = new System.Windows.Forms.RichTextBox();
            this.cmbfirmailce = new System.Windows.Forms.ComboBox();
            this.cmbfirmail = new System.Windows.Forms.ComboBox();
            this.mskfirmafax = new System.Windows.Forms.MaskedTextBox();
            this.mskfirmatel3 = new System.Windows.Forms.MaskedTextBox();
            this.mskfirmatel2 = new System.Windows.Forms.MaskedTextBox();
            this.mskfirmatel1 = new System.Windows.Forms.MaskedTextBox();
            this.txtfirmaytc = new ModernTextBox();
            this.txtfirmavergidairesi = new ModernTextBox();
            this.txtfirmamail = new ModernTextBox();
            this.txtfirmasektor = new ModernTextBox();
            this.txtfirmaygorev = new ModernTextBox();
            this.txtfirmayetkili = new ModernTextBox();
            this.txtfirmaad = new ModernTextBox();
            this.txtfirmaid = new ModernTextBox();
            this.labelControl20 = new System.Windows.Forms.Label();
            this.labelControl19 = new System.Windows.Forms.Label();
            this.labelControl18 = new System.Windows.Forms.Label();
            this.labelControl17 = new System.Windows.Forms.Label();
            this.labelControl16 = new System.Windows.Forms.Label();
            this.labelControl15 = new System.Windows.Forms.Label();
            this.labelControl14 = new System.Windows.Forms.Label();
            this.labelControl13 = new System.Windows.Forms.Label();
            this.labelControl12 = new System.Windows.Forms.Label();
            this.labelControl10 = new System.Windows.Forms.Label();
            this.labelControl6 = new System.Windows.Forms.Label();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.labelKod1 = new System.Windows.Forms.Label();
            this.labelKod2 = new System.Windows.Forms.Label();
            this.labelKod3 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdfirmalar)).BeginInit();
            this.panelFormContainer.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.grdfirmalar);
            this.panelMain.Controls.Add(this.hScrollBar1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.L);
            this.panelMain.Size = new System.Drawing.Size(900, 561);
            this.panelMain.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 561 - 22);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(900 - (DesignSystem.Spacing.L * 2), 22);
            this.hScrollBar1.TabIndex = 1;
            // 
            // grdfirmalar
            // 
            this.grdfirmalar.AllowUserToAddRows = false;
            this.grdfirmalar.AllowUserToDeleteRows = false;
            this.grdfirmalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdfirmalar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdfirmalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdfirmalar.Location = new System.Drawing.Point(DesignSystem.Spacing.L, DesignSystem.Spacing.L);
            this.grdfirmalar.Name = "grdfirmalar";
            this.grdfirmalar.ReadOnly = true;
            this.grdfirmalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdfirmalar.Size = new System.Drawing.Size(900 - (DesignSystem.Spacing.L * 2), 561 - (DesignSystem.Spacing.L * 2) - 22);
            this.grdfirmalar.TabIndex = 0;
            this.grdfirmalar.SelectionChanged += new System.EventHandler(this.grdfirmalar_SelectionChanged);
            // 
            // panelFormContainer
            // 
            this.panelFormContainer.AutoScroll = true;
            this.panelFormContainer.Controls.Add(this.panelForm);
            this.panelFormContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormContainer.Location = new System.Drawing.Point(900, 0);
            this.panelFormContainer.Name = "panelFormContainer";
            this.panelFormContainer.Size = new System.Drawing.Size(470, 561);
            this.panelFormContainer.TabIndex = 1;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = DesignSystem.Colors.Surface;
            this.panelForm.BorderRadius = DesignSystem.Borders.RadiusMedium;
            this.panelForm.Controls.Add(this.labelKod3);
            this.panelForm.Controls.Add(this.labelKod2);
            this.panelForm.Controls.Add(this.labelKod1);
            this.panelForm.Controls.Add(this.Btntemizle);
            this.panelForm.Controls.Add(this.BtnGuncelle);
            this.panelForm.Controls.Add(this.BtnSil);
            this.panelForm.Controls.Add(this.Btnkaydet);
            this.panelForm.Controls.Add(this.rchfirmakod3);
            this.panelForm.Controls.Add(this.rchfirmakod2);
            this.panelForm.Controls.Add(this.rchfirmakod1);
            this.panelForm.Controls.Add(this.rchfirmaadres);
            this.panelForm.Controls.Add(this.cmbfirmailce);
            this.panelForm.Controls.Add(this.cmbfirmail);
            this.panelForm.Controls.Add(this.mskfirmafax);
            this.panelForm.Controls.Add(this.mskfirmatel3);
            this.panelForm.Controls.Add(this.mskfirmatel2);
            this.panelForm.Controls.Add(this.mskfirmatel1);
            this.panelForm.Controls.Add(this.txtfirmaytc);
            this.panelForm.Controls.Add(this.txtfirmavergidairesi);
            this.panelForm.Controls.Add(this.txtfirmamail);
            this.panelForm.Controls.Add(this.txtfirmasektor);
            this.panelForm.Controls.Add(this.txtfirmaygorev);
            this.panelForm.Controls.Add(this.txtfirmayetkili);
            this.panelForm.Controls.Add(this.txtfirmaad);
            this.panelForm.Controls.Add(this.txtfirmaid);
            this.panelForm.Controls.Add(this.labelControl20);
            this.panelForm.Controls.Add(this.labelControl19);
            this.panelForm.Controls.Add(this.labelControl18);
            this.panelForm.Controls.Add(this.labelControl17);
            this.panelForm.Controls.Add(this.labelControl16);
            this.panelForm.Controls.Add(this.labelControl15);
            this.panelForm.Controls.Add(this.labelControl14);
            this.panelForm.Controls.Add(this.labelControl13);
            this.panelForm.Controls.Add(this.labelControl12);
            this.panelForm.Controls.Add(this.labelControl10);
            this.panelForm.Controls.Add(this.labelControl6);
            this.panelForm.Controls.Add(this.labelControl4);
            this.panelForm.Controls.Add(this.labelControl3);
            this.panelForm.Controls.Add(this.labelControl2);
            this.panelForm.Controls.Add(this.labelControl1);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(DesignSystem.Spacing.L);
            this.panelForm.ShowShadow = true;
            this.panelForm.Size = new System.Drawing.Size(470, 850);
            this.panelForm.TabIndex = 0;
            this.panelForm.Title = "🏢 Firma Bilgileri";
            // 
            // Btntemizle
            // 
            this.Btntemizle.ButtonStyle = ButtonStyle.Secondary;
            this.Btntemizle.Location = new System.Drawing.Point(355, 800);
            this.Btntemizle.Name = "Btntemizle";
            this.Btntemizle.Size = new System.Drawing.Size(105, 36);
            this.Btntemizle.TabIndex = 34;
            this.Btntemizle.Text = "Temizle";
            this.Btntemizle.Click += new System.EventHandler(this.Btntemizle_Click);
            // 
            // BtnGuncelle
            // 
            this.BtnGuncelle.ButtonStyle = ButtonStyle.Primary;
            this.BtnGuncelle.Location = new System.Drawing.Point(222, 800);
            this.BtnGuncelle.Name = "BtnGuncelle";
            this.BtnGuncelle.Size = new System.Drawing.Size(129, 40);
            this.BtnGuncelle.TabIndex = 23;
            this.BtnGuncelle.Text = "Güncelle";
            this.BtnGuncelle.Click += new System.EventHandler(this.BtnGuncelle_Click);
            // 
            // BtnSil
            // 
            this.BtnSil.ButtonStyle = ButtonStyle.Error;
            this.BtnSil.Location = new System.Drawing.Point(130, 800);
            this.BtnSil.Name = "BtnSil";
            this.BtnSil.Size = new System.Drawing.Size(86, 40);
            this.BtnSil.TabIndex = 22;
            this.BtnSil.Text = "Sil";
            this.BtnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // Btnkaydet
            // 
            this.Btnkaydet.ButtonStyle = ButtonStyle.Success;
            this.Btnkaydet.Location = new System.Drawing.Point(DesignSystem.Spacing.L, 800);
            this.Btnkaydet.Name = "Btnkaydet";
            this.Btnkaydet.Size = new System.Drawing.Size(111, 40);
            this.Btnkaydet.TabIndex = 19;
            this.Btnkaydet.Text = "Kaydet";
            this.Btnkaydet.Click += new System.EventHandler(this.Btnkaydet_Click);
            // 
            // rchfirmakod3
            // 
            this.rchfirmakod3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rchfirmakod3.Font = DesignSystem.Fonts.Body;
            this.rchfirmakod3.Location = new System.Drawing.Point(176, 730);
            this.rchfirmakod3.Name = "rchfirmakod3";
            this.rchfirmakod3.Size = new System.Drawing.Size(264, 50);
            this.rchfirmakod3.TabIndex = 33;
            this.rchfirmakod3.Text = "";
            // 
            // rchfirmakod2
            // 
            this.rchfirmakod2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rchfirmakod2.Font = DesignSystem.Fonts.Body;
            this.rchfirmakod2.Location = new System.Drawing.Point(176, 660);
            this.rchfirmakod2.Name = "rchfirmakod2";
            this.rchfirmakod2.Size = new System.Drawing.Size(264, 50);
            this.rchfirmakod2.TabIndex = 32;
            this.rchfirmakod2.Text = "";
            // 
            // rchfirmakod1
            // 
            this.rchfirmakod1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rchfirmakod1.Font = DesignSystem.Fonts.Body;
            this.rchfirmakod1.Location = new System.Drawing.Point(176, 590);
            this.rchfirmakod1.Name = "rchfirmakod1";
            this.rchfirmakod1.Size = new System.Drawing.Size(264, 50);
            this.rchfirmakod1.TabIndex = 31;
            this.rchfirmakod1.Text = "";
            // 
            // rchfirmaadres
            // 
            this.rchfirmaadres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rchfirmaadres.Font = DesignSystem.Fonts.Body;
            this.rchfirmaadres.Location = new System.Drawing.Point(176, 520);
            this.rchfirmaadres.Name = "rchfirmaadres";
            this.rchfirmaadres.Size = new System.Drawing.Size(264, 50);
            this.rchfirmaadres.TabIndex = 30;
            this.rchfirmaadres.Text = "";
            // 
            // cmbfirmailce
            // 
            this.cmbfirmailce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbfirmailce.Font = DesignSystem.Fonts.Body;
            this.cmbfirmailce.FormattingEnabled = true;
            this.cmbfirmailce.Location = new System.Drawing.Point(176, 485);
            this.cmbfirmailce.Name = "cmbfirmailce";
            this.cmbfirmailce.Size = new System.Drawing.Size(264, 28);
            this.cmbfirmailce.TabIndex = 29;
            // 
            // cmbfirmail
            // 
            this.cmbfirmail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbfirmail.Font = DesignSystem.Fonts.Body;
            this.cmbfirmail.FormattingEnabled = true;
            this.cmbfirmail.Location = new System.Drawing.Point(176, 450);
            this.cmbfirmail.Name = "cmbfirmail";
            this.cmbfirmail.Size = new System.Drawing.Size(264, 28);
            this.cmbfirmail.TabIndex = 28;
            this.cmbfirmail.SelectedIndexChanged += new System.EventHandler(this.cmbfirmail_SelectedIndexChanged);
            // 
            // mskfirmafax
            // 
            this.mskfirmafax.Font = DesignSystem.Fonts.Body;
            this.mskfirmafax.Location = new System.Drawing.Point(176, 415);
            this.mskfirmafax.Mask = "(999) 000-0000";
            this.mskfirmafax.Name = "mskfirmafax";
            this.mskfirmafax.Size = new System.Drawing.Size(264, 24);
            this.mskfirmafax.TabIndex = 27;
            // 
            // mskfirmatel3
            // 
            this.mskfirmatel3.Font = DesignSystem.Fonts.Body;
            this.mskfirmatel3.Location = new System.Drawing.Point(176, 380);
            this.mskfirmatel3.Mask = "(999) 000-0000";
            this.mskfirmatel3.Name = "mskfirmatel3";
            this.mskfirmatel3.Size = new System.Drawing.Size(264, 24);
            this.mskfirmatel3.TabIndex = 26;
            // 
            // mskfirmatel2
            // 
            this.mskfirmatel2.Font = DesignSystem.Fonts.Body;
            this.mskfirmatel2.Location = new System.Drawing.Point(176, 345);
            this.mskfirmatel2.Mask = "(999) 000-0000";
            this.mskfirmatel2.Name = "mskfirmatel2";
            this.mskfirmatel2.Size = new System.Drawing.Size(264, 24);
            this.mskfirmatel2.TabIndex = 25;
            // 
            // mskfirmatel1
            // 
            this.mskfirmatel1.Font = DesignSystem.Fonts.Body;
            this.mskfirmatel1.Location = new System.Drawing.Point(176, 310);
            this.mskfirmatel1.Mask = "(999) 000-0000";
            this.mskfirmatel1.Name = "mskfirmatel1";
            this.mskfirmatel1.Size = new System.Drawing.Size(264, 24);
            this.mskfirmatel1.TabIndex = 24;
            // 
            // txtfirmaytc
            // 
            this.txtfirmaytc.Location = new System.Drawing.Point(176, 275);
            this.txtfirmaytc.Name = "txtfirmaytc";
            this.txtfirmaytc.PlaceholderText = "Yetkili TC";
            this.txtfirmaytc.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmaytc.TabIndex = 23;
            // 
            // txtfirmavergidairesi
            // 
            this.txtfirmavergidairesi.Location = new System.Drawing.Point(176, 240);
            this.txtfirmavergidairesi.Name = "txtfirmavergidairesi";
            this.txtfirmavergidairesi.PlaceholderText = "Vergi Dairesi";
            this.txtfirmavergidairesi.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmavergidairesi.TabIndex = 22;
            // 
            // txtfirmamail
            // 
            this.txtfirmamail.Location = new System.Drawing.Point(176, 205);
            this.txtfirmamail.Name = "txtfirmamail";
            this.txtfirmamail.PlaceholderText = "email@example.com";
            this.txtfirmamail.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmamail.TabIndex = 21;
            // 
            // txtfirmasektor
            // 
            this.txtfirmasektor.Location = new System.Drawing.Point(176, 170);
            this.txtfirmasektor.Name = "txtfirmasektor";
            this.txtfirmasektor.PlaceholderText = "Sektör";
            this.txtfirmasektor.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmasektor.TabIndex = 20;
            // 
            // txtfirmaygorev
            // 
            this.txtfirmaygorev.Location = new System.Drawing.Point(176, 135);
            this.txtfirmaygorev.Name = "txtfirmaygorev";
            this.txtfirmaygorev.PlaceholderText = "Yetkili Görevi";
            this.txtfirmaygorev.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmaygorev.TabIndex = 4;
            // 
            // txtfirmayetkili
            // 
            this.txtfirmayetkili.Location = new System.Drawing.Point(176, 100);
            this.txtfirmayetkili.Name = "txtfirmayetkili";
            this.txtfirmayetkili.PlaceholderText = "Yetkili Ad Soyad";
            this.txtfirmayetkili.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmayetkili.TabIndex = 1;
            // 
            // txtfirmaad
            // 
            this.txtfirmaad.Location = new System.Drawing.Point(176, 65);
            this.txtfirmaad.Name = "txtfirmaad";
            this.txtfirmaad.PlaceholderText = "Firma Adı";
            this.txtfirmaad.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmaad.TabIndex = 2;
            // 
            // txtfirmaid
            // 
            this.txtfirmaid.Location = new System.Drawing.Point(176, 30);
            this.txtfirmaid.Name = "txtfirmaid";
            this.txtfirmaid.PlaceholderText = "Otomatik";
            this.txtfirmaid.ReadOnly = true;
            this.txtfirmaid.Size = new System.Drawing.Size(264, DesignSystem.ControlSizes.InputHeight);
            this.txtfirmaid.TabIndex = 1;
            // 
            // labelControl20
            // 
            this.labelControl20.AutoSize = true;
            this.labelControl20.Font = DesignSystem.Fonts.Body;
            this.labelControl20.ForeColor = DesignSystem.Colors.Text;
            this.labelControl20.Location = new System.Drawing.Point(99, 523);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(63, 20);
            this.labelControl20.TabIndex = 19;
            this.labelControl20.Text = "Adres:";
            // 
            // labelControl19
            // 
            this.labelControl19.AutoSize = true;
            this.labelControl19.Font = DesignSystem.Fonts.Body;
            this.labelControl19.ForeColor = DesignSystem.Colors.Text;
            this.labelControl19.Location = new System.Drawing.Point(139, 488);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(23, 20);
            this.labelControl19.TabIndex = 18;
            this.labelControl19.Text = "İlçe:";
            // 
            // labelControl18
            // 
            this.labelControl18.AutoSize = true;
            this.labelControl18.Font = DesignSystem.Fonts.Body;
            this.labelControl18.ForeColor = DesignSystem.Colors.Text;
            this.labelControl18.Location = new System.Drawing.Point(118, 453);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(23, 20);
            this.labelControl18.TabIndex = 17;
            this.labelControl18.Text = "İl:";
            // 
            // labelControl17
            // 
            this.labelControl17.AutoSize = true;
            this.labelControl17.Font = DesignSystem.Fonts.Body;
            this.labelControl17.ForeColor = DesignSystem.Colors.Text;
            this.labelControl17.Location = new System.Drawing.Point(117, 418);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(45, 20);
            this.labelControl17.TabIndex = 16;
            this.labelControl17.Text = "Fax:";
            // 
            // labelControl16
            // 
            this.labelControl16.AutoSize = true;
            this.labelControl16.Font = DesignSystem.Fonts.Body;
            this.labelControl16.ForeColor = DesignSystem.Colors.Text;
            this.labelControl16.Location = new System.Drawing.Point(70, 383);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(93, 20);
            this.labelControl16.TabIndex = 15;
            this.labelControl16.Text = "Telefon 3:";
            // 
            // labelControl15
            // 
            this.labelControl15.AutoSize = true;
            this.labelControl15.Font = DesignSystem.Fonts.Body;
            this.labelControl15.ForeColor = DesignSystem.Colors.Text;
            this.labelControl15.Location = new System.Drawing.Point(70, 348);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(93, 20);
            this.labelControl15.TabIndex = 14;
            this.labelControl15.Text = "Telefon 2:";
            // 
            // labelControl14
            // 
            this.labelControl14.AutoSize = true;
            this.labelControl14.Font = DesignSystem.Fonts.Body;
            this.labelControl14.ForeColor = DesignSystem.Colors.Text;
            this.labelControl14.Location = new System.Drawing.Point(72, 278);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(88, 20);
            this.labelControl14.TabIndex = 13;
            this.labelControl14.Text = "Yetkili TC:";
            // 
            // labelControl13
            // 
            this.labelControl13.AutoSize = true;
            this.labelControl13.Font = DesignSystem.Fonts.Body;
            this.labelControl13.ForeColor = DesignSystem.Colors.Text;
            this.labelControl13.Location = new System.Drawing.Point(33, 138);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(127, 20);
            this.labelControl13.TabIndex = 12;
            this.labelControl13.Text = "Yetkili Görevi:";
            // 
            // labelControl12
            // 
            this.labelControl12.AutoSize = true;
            this.labelControl12.Font = DesignSystem.Fonts.Body;
            this.labelControl12.ForeColor = DesignSystem.Colors.Text;
            this.labelControl12.Location = new System.Drawing.Point(41, 103);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(119, 20);
            this.labelControl12.TabIndex = 11;
            this.labelControl12.Text = "Yetkili Ad Soyad:";
            // 
            // labelControl10
            // 
            this.labelControl10.AutoSize = true;
            this.labelControl10.Font = DesignSystem.Fonts.Body;
            this.labelControl10.ForeColor = DesignSystem.Colors.Text;
            this.labelControl10.Location = new System.Drawing.Point(44, 243);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(118, 20);
            this.labelControl10.TabIndex = 10;
            this.labelControl10.Text = "Vergi Dairesi:";
            // 
            // labelControl6
            // 
            this.labelControl6.AutoSize = true;
            this.labelControl6.Font = DesignSystem.Fonts.Body;
            this.labelControl6.ForeColor = DesignSystem.Colors.Text;
            this.labelControl6.Location = new System.Drawing.Point(70, 313);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(93, 20);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "Telefon 1:";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSize = true;
            this.labelControl4.Font = DesignSystem.Fonts.Body;
            this.labelControl4.ForeColor = DesignSystem.Colors.Text;
            this.labelControl4.Location = new System.Drawing.Point(100, 173);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(64, 20);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Sektör:";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Font = DesignSystem.Fonts.Body;
            this.labelControl3.ForeColor = DesignSystem.Colors.Text;
            this.labelControl3.Location = new System.Drawing.Point(119, 208);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(45, 20);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Mail:";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Font = DesignSystem.Fonts.Body;
            this.labelControl2.ForeColor = DesignSystem.Colors.Text;
            this.labelControl2.Location = new System.Drawing.Point(75, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 20);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Firma Adı:";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = DesignSystem.Fonts.Body;
            this.labelControl1.ForeColor = DesignSystem.Colors.Text;
            this.labelControl1.Location = new System.Drawing.Point(134, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 20);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "ID:";
            // 
            // labelKod1
            // 
            this.labelKod1.AutoSize = true;
            this.labelKod1.Font = DesignSystem.Fonts.Body;
            this.labelKod1.ForeColor = DesignSystem.Colors.Text;
            this.labelKod1.Location = new System.Drawing.Point(64, 593);
            this.labelKod1.Name = "labelKod1";
            this.labelKod1.Size = new System.Drawing.Size(96, 20);
            this.labelKod1.TabIndex = 35;
            this.labelKod1.Text = "Özel Kod 1:";
            // 
            // labelKod2
            // 
            this.labelKod2.AutoSize = true;
            this.labelKod2.Font = DesignSystem.Fonts.Body;
            this.labelKod2.ForeColor = DesignSystem.Colors.Text;
            this.labelKod2.Location = new System.Drawing.Point(64, 663);
            this.labelKod2.Name = "labelKod2";
            this.labelKod2.Size = new System.Drawing.Size(96, 20);
            this.labelKod2.TabIndex = 36;
            this.labelKod2.Text = "Özel Kod 2:";
            // 
            // labelKod3
            // 
            this.labelKod3.AutoSize = true;
            this.labelKod3.Font = DesignSystem.Fonts.Body;
            this.labelKod3.ForeColor = DesignSystem.Colors.Text;
            this.labelKod3.Location = new System.Drawing.Point(64, 733);
            this.labelKod3.Name = "labelKod3";
            this.labelKod3.Size = new System.Drawing.Size(96, 20);
            this.labelKod3.TabIndex = 37;
            this.labelKod3.Text = "Özel Kod 3:";
            // 
            // FrmFirmalar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = DesignSystem.Colors.Background;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.panelFormContainer);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmFirmalar";
            this.Text = "Firmalar - operion";
            this.Load += new System.EventHandler(this.FrmFirmalar_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdfirmalar)).EndInit();
            this.panelFormContainer.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView grdfirmalar;
        private System.Windows.Forms.Panel panelFormContainer;
        private ModernPanel panelForm;
        private ModernButton Btntemizle;
        private ModernButton BtnGuncelle;
        private ModernButton BtnSil;
        private ModernButton Btnkaydet;
        private System.Windows.Forms.RichTextBox rchfirmakod3;
        private System.Windows.Forms.RichTextBox rchfirmakod2;
        private System.Windows.Forms.RichTextBox rchfirmakod1;
        private System.Windows.Forms.RichTextBox rchfirmaadres;
        private System.Windows.Forms.ComboBox cmbfirmailce;
        private System.Windows.Forms.ComboBox cmbfirmail;
        private System.Windows.Forms.MaskedTextBox mskfirmafax;
        private System.Windows.Forms.MaskedTextBox mskfirmatel3;
        private System.Windows.Forms.MaskedTextBox mskfirmatel2;
        private System.Windows.Forms.MaskedTextBox mskfirmatel1;
        private ModernTextBox txtfirmaytc;
        private ModernTextBox txtfirmavergidairesi;
        private ModernTextBox txtfirmamail;
        private ModernTextBox txtfirmasektor;
        private ModernTextBox txtfirmaygorev;
        private ModernTextBox txtfirmayetkili;
        private ModernTextBox txtfirmaad;
        private ModernTextBox txtfirmaid;
        private System.Windows.Forms.Label labelControl20;
        private System.Windows.Forms.Label labelControl19;
        private System.Windows.Forms.Label labelControl18;
        private System.Windows.Forms.Label labelControl17;
        private System.Windows.Forms.Label labelControl16;
        private System.Windows.Forms.Label labelControl15;
        private System.Windows.Forms.Label labelControl14;
        private System.Windows.Forms.Label labelControl13;
        private System.Windows.Forms.Label labelControl12;
        private System.Windows.Forms.Label labelControl10;
        private System.Windows.Forms.Label labelControl6;
        private System.Windows.Forms.Label labelControl4;
        private System.Windows.Forms.Label labelControl3;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl1;
        private System.Windows.Forms.Label labelKod1;
        private System.Windows.Forms.Label labelKod2;
        private System.Windows.Forms.Label labelKod3;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
