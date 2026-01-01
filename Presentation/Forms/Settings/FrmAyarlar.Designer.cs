namespace operion.Presentation.Forms.Settings
{
    partial class FrmAyarlar
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
            this.grdayarlar = new System.Windows.Forms.DataGridView();
            this.pnlAyarlar = new operion.Presentation.Controls.ModernPanel();
            this.Btnislem = new operion.Presentation.Controls.ModernButton();
            this.txtsifre = new operion.Presentation.Controls.ModernTextBox();
            this.txtkullanicad = new operion.Presentation.Controls.ModernTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdayarlar)).BeginInit();
            this.pnlAyarlar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdayarlar
            // 
            this.grdayarlar.AllowUserToAddRows = false;
            this.grdayarlar.AllowUserToDeleteRows = false;
            this.grdayarlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdayarlar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdayarlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdayarlar.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdayarlar.Location = new System.Drawing.Point(0, 0);
            this.grdayarlar.Name = "grdayarlar";
            this.grdayarlar.ReadOnly = true;
            this.grdayarlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdayarlar.Size = new System.Drawing.Size(900, 561);
            this.grdayarlar.TabIndex = 1;
            this.grdayarlar.SelectionChanged += new System.EventHandler(this.grdayarlar_SelectionChanged);
            // 
            // pnlAyarlar
            // 
            this.pnlAyarlar.Controls.Add(this.Btnislem);
            this.pnlAyarlar.Controls.Add(this.txtsifre);
            this.pnlAyarlar.Controls.Add(this.txtkullanicad);
            this.pnlAyarlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAyarlar.Location = new System.Drawing.Point(900, 0);
            this.pnlAyarlar.Name = "pnlAyarlar";
            this.pnlAyarlar.Size = new System.Drawing.Size(470, 561);
            this.pnlAyarlar.TabIndex = 2;
            this.pnlAyarlar.Title = "⚙️ Kullanıcı Ayarları";
            // 
            // Btnislem
            // 
            this.Btnislem.ButtonStyle = operion.Presentation.Controls.ButtonStyle.Primary;
            this.Btnislem.Location = new System.Drawing.Point(90, 210);
            this.Btnislem.Name = "Btnislem";
            this.Btnislem.Size = new System.Drawing.Size(320, 44);
            this.Btnislem.TabIndex = 3;
            this.Btnislem.Text = "Kaydet";
            this.Btnislem.Click += new System.EventHandler(this.Btnislem_Click);
            // 
            // txtsifre
            // 
            this.txtsifre.Location = new System.Drawing.Point(90, 150);
            this.txtsifre.Name = "txtsifre";
            this.txtsifre.PlaceholderText = "Şifre *";
            this.txtsifre.Size = new System.Drawing.Size(320, 40);
            this.txtsifre.TabIndex = 2;
            // 
            // txtkullanicad
            // 
            this.txtkullanicad.Location = new System.Drawing.Point(90, 90);
            this.txtkullanicad.Name = "txtkullanicad";
            this.txtkullanicad.PlaceholderText = "Kullanıcı Adı *";
            this.txtkullanicad.Size = new System.Drawing.Size(320, 40);
            this.txtkullanicad.TabIndex = 1;
            this.txtkullanicad.TextChanged += new System.EventHandler(this.txtkullanicad_TextChanged);
            // 
            // FrmAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.pnlAyarlar);
            this.Controls.Add(this.grdayarlar);
            this.Name = "FrmAyarlar";
            this.Text = "AYARLAR";
            this.Load += new System.EventHandler(this.FrmAyarlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdayarlar)).EndInit();
            this.pnlAyarlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView grdayarlar;
        private operion.Presentation.Controls.ModernPanel pnlAyarlar;
        private operion.Presentation.Controls.ModernButton Btnislem;
        private operion.Presentation.Controls.ModernTextBox txtsifre;
        private operion.Presentation.Controls.ModernTextBox txtkullanicad;
    }
}

