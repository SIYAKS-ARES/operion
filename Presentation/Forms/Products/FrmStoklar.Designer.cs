namespace operion.Presentation.Forms.Products
{
    partial class FrmStoklar
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
            this.grdstoklar = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdstoklar)).BeginInit();
            this.SuspendLayout();
            // 
            // grdstoklar
            // 
            this.grdstoklar.AllowUserToAddRows = false;
            this.grdstoklar.AllowUserToDeleteRows = false;
            this.grdstoklar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdstoklar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdstoklar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdstoklar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdstoklar.Location = new System.Drawing.Point(0, 0);
            this.grdstoklar.Name = "grdstoklar";
            this.grdstoklar.ReadOnly = true;
            this.grdstoklar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdstoklar.Size = new System.Drawing.Size(1370, 561);
            this.grdstoklar.TabIndex = 0;
            // 
            // FrmStoklar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.grdstoklar);
            this.Name = "FrmStoklar";
            this.Text = "STOKLAR";
            this.Load += new System.EventHandler(this.FrmStoklar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdstoklar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        // TODO: DevExpress Chart kontrolü şimdilik kaldırıldı
        // İleride standart chart kontrolü eklenebilir
        private System.Windows.Forms.DataGridView grdstoklar;
    }
}
