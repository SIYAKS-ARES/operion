namespace operion.Presentation.Forms.Invoices
{
    partial class FrmHareketler
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdhareketlerfirmalar = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdhareketlermusteriler = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdhareketlerfirmalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdhareketlermusteriler)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1370, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdhareketlerfirmalar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1362, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "🏢 Firma Hareketleri";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdhareketlerfirmalar
            // 
            this.grdhareketlerfirmalar.AllowUserToAddRows = false;
            this.grdhareketlerfirmalar.AllowUserToDeleteRows = false;
            this.grdhareketlerfirmalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdhareketlerfirmalar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdhareketlerfirmalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdhareketlerfirmalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdhareketlerfirmalar.Location = new System.Drawing.Point(3, 3);
            this.grdhareketlerfirmalar.Name = "grdhareketlerfirmalar";
            this.grdhareketlerfirmalar.ReadOnly = true;
            this.grdhareketlerfirmalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdhareketlerfirmalar.Size = new System.Drawing.Size(1356, 529);
            this.grdhareketlerfirmalar.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdhareketlermusteriler);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1362, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "👤 Müşteri Hareketleri";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grdhareketlermusteriler
            // 
            this.grdhareketlermusteriler.AllowUserToAddRows = false;
            this.grdhareketlermusteriler.AllowUserToDeleteRows = false;
            this.grdhareketlermusteriler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdhareketlermusteriler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdhareketlermusteriler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdhareketlermusteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdhareketlermusteriler.Location = new System.Drawing.Point(3, 3);
            this.grdhareketlermusteriler.Name = "grdhareketlermusteriler";
            this.grdhareketlermusteriler.ReadOnly = true;
            this.grdhareketlermusteriler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdhareketlermusteriler.Size = new System.Drawing.Size(1356, 529);
            this.grdhareketlermusteriler.TabIndex = 0;
            // 
            // FrmHareketler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmHareketler";
            this.Text = "HAREKETLER";
            this.Load += new System.EventHandler(this.FrmHareketler_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdhareketlerfirmalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdhareketlermusteriler)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        // TODO: DevExpress XtraTabControl → TabControl dönüştürüldü
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView grdhareketlerfirmalar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView grdhareketlermusteriler;
    }
}
