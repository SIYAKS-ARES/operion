namespace operion.Presentation.Forms.Customers
{
    partial class FrmRehber
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
            this.grdrehbermusteriler = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdrehberfirmalar = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdrehbermusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdrehberfirmalar)).BeginInit();
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
            this.tabPage1.Controls.Add(this.grdrehbermusteriler);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1362, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "👥 Müşteriler";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdrehbermusteriler
            // 
            this.grdrehbermusteriler.AllowUserToAddRows = false;
            this.grdrehbermusteriler.AllowUserToDeleteRows = false;
            this.grdrehbermusteriler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdrehbermusteriler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdrehbermusteriler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdrehbermusteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdrehbermusteriler.Location = new System.Drawing.Point(3, 3);
            this.grdrehbermusteriler.Name = "grdrehbermusteriler";
            this.grdrehbermusteriler.ReadOnly = true;
            this.grdrehbermusteriler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdrehbermusteriler.Size = new System.Drawing.Size(1356, 529);
            this.grdrehbermusteriler.TabIndex = 0;
            this.grdrehbermusteriler.DoubleClick += new System.EventHandler(this.grdrehbermusteriler_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdrehberfirmalar);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1362, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "🏢 Firmalar";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grdrehberfirmalar
            // 
            this.grdrehberfirmalar.AllowUserToAddRows = false;
            this.grdrehberfirmalar.AllowUserToDeleteRows = false;
            this.grdrehberfirmalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdrehberfirmalar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.grdrehberfirmalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdrehberfirmalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdrehberfirmalar.Location = new System.Drawing.Point(3, 3);
            this.grdrehberfirmalar.Name = "grdrehberfirmalar";
            this.grdrehberfirmalar.ReadOnly = true;
            this.grdrehberfirmalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdrehberfirmalar.Size = new System.Drawing.Size(1356, 529);
            this.grdrehberfirmalar.TabIndex = 0;
            this.grdrehberfirmalar.DoubleClick += new System.EventHandler(this.grdrehberfirmalar_DoubleClick);
            // 
            // FrmRehber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmRehber";
            this.Text = "REHBER";
            this.Load += new System.EventHandler(this.FrmRehber_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdrehbermusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdrehberfirmalar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView grdrehbermusteriler;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView grdrehberfirmalar;
    }
}
