namespace operion.Presentation.Forms.Invoices
{
    partial class FrmFaturaUrunDetay
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
            this.grdfaturaurundetay = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdfaturaurundetay)).BeginInit();
            this.SuspendLayout();
            // 
            // grdfaturaurundetay
            // 
            this.grdfaturaurundetay.AllowUserToAddRows = false;
            this.grdfaturaurundetay.AllowUserToDeleteRows = false;
            this.grdfaturaurundetay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdfaturaurundetay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdfaturaurundetay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdfaturaurundetay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdfaturaurundetay.Location = new System.Drawing.Point(0, 0);
            this.grdfaturaurundetay.Name = "grdfaturaurundetay";
            this.grdfaturaurundetay.ReadOnly = true;
            this.grdfaturaurundetay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdfaturaurundetay.Size = new System.Drawing.Size(800, 450);
            this.grdfaturaurundetay.TabIndex = 0;
            this.grdfaturaurundetay.DoubleClick += new System.EventHandler(this.grdfaturaurundetay_DoubleClick);
            // 
            // FrmFaturaUrunDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.grdfaturaurundetay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFaturaUrunDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📄 Fatura Ürün Detayları";
            this.Load += new System.EventHandler(this.FrmFaturaUrunDetay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdfaturaurundetay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdfaturaurundetay;
    }
}
