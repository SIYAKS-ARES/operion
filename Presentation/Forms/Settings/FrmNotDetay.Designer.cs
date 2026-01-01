namespace operion.Presentation.Forms.Settings
{
    partial class FrmNotDetay
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
            this.pnlNotDetay = new operion.Presentation.Controls.ModernPanel();
            this.rchnotdeyat = new System.Windows.Forms.RichTextBox();
            this.pnlNotDetay.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNotDetay
            // 
            this.pnlNotDetay.Controls.Add(this.rchnotdeyat);
            this.pnlNotDetay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotDetay.Location = new System.Drawing.Point(0, 0);
            this.pnlNotDetay.Name = "pnlNotDetay";
            this.pnlNotDetay.Size = new System.Drawing.Size(800, 450);
            this.pnlNotDetay.TabIndex = 0;
            this.pnlNotDetay.Title = "📝 Not Detayı";
            // 
            // rchnotdeyat
            // 
            this.rchnotdeyat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchnotdeyat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchnotdeyat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rchnotdeyat.Location = new System.Drawing.Point(0, 40);
            this.rchnotdeyat.Name = "rchnotdeyat";
            this.rchnotdeyat.ReadOnly = true;
            this.rchnotdeyat.Size = new System.Drawing.Size(800, 410);
            this.rchnotdeyat.TabIndex = 0;
            this.rchnotdeyat.Text = "";
            // 
            // FrmNotDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlNotDetay);
            this.Name = "FrmNotDetay";
            this.Text = "Not Detayı";
            this.Load += new System.EventHandler(this.FrmNotDetay_Load);
            this.pnlNotDetay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private operion.Presentation.Controls.ModernPanel pnlNotDetay;
        private System.Windows.Forms.RichTextBox rchnotdeyat;
    }
}
