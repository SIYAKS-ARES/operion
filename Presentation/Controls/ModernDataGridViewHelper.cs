using System.Drawing;
using System.Windows.Forms;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// DataGridView için modern stil uygulayan helper sınıfı
    /// Microsoft Teams ve Fluent Design System stilinde
    /// </summary>
    public static class ModernDataGridViewHelper
    {
        /// <summary>
        /// DataGridView'e modern stil uygular
        /// </summary>
        public static void ApplyModernStyle(DataGridView dataGridView)
        {
            if (dataGridView == null) return;

            // Temel ayarlar
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.ScrollBars = ScrollBars.Both;

            // Renkler (tema desteği)
            dataGridView.BackgroundColor = DesignSystem.Colors.Background;
            dataGridView.GridColor = DesignSystem.Colors.Border;

            // Header stil
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.9f);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = DesignSystem.Colors.Text;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = DesignSystem.Fonts.BodyBold;
            dataGridView.ColumnHeadersDefaultCellStyle.Padding = new Padding(DesignSystem.Spacing.S);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.9f);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = DesignSystem.Colors.Text;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.ColumnHeadersHeight = DesignSystem.ControlSizes.DataGridHeaderHeight;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Satır stil
            dataGridView.DefaultCellStyle.BackColor = DesignSystem.Colors.Surface;
            dataGridView.DefaultCellStyle.ForeColor = DesignSystem.Colors.Text;
            dataGridView.DefaultCellStyle.Font = DesignSystem.Fonts.Body;
            dataGridView.DefaultCellStyle.Padding = new Padding(DesignSystem.Spacing.S, DesignSystem.Spacing.XS, DesignSystem.Spacing.S, DesignSystem.Spacing.XS);
            dataGridView.DefaultCellStyle.SelectionBackColor = DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.8f);
            dataGridView.DefaultCellStyle.SelectionForeColor = DesignSystem.Colors.Text;
            dataGridView.RowTemplate.Height = DesignSystem.ControlSizes.DataGridRowHeight;

            // Alternatif satır rengi
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = DesignSystem.Colors.SurfaceHover;
            dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = DesignSystem.Colors.Text;
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.8f);
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = DesignSystem.Colors.Text;

            // Tema değişikliği event'i
            ThemeManager.ThemeChanged += (s, e) =>
            {
                if (!dataGridView.IsDisposed)
                {
                    ApplyModernStyle(dataGridView);
                }
            };
        }

        /// <summary>
        /// DataGridView'e hover efekti ekler
        /// </summary>
        public static void EnableHoverEffect(DataGridView dataGridView)
        {
            if (dataGridView == null) return;

            int lastRowIndex = -1;

            dataGridView.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex != lastRowIndex)
                {
                    lastRowIndex = e.RowIndex;
                    
                    // Önceki hover'ı temizle
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            row.DefaultCellStyle.BackColor = row.Index % 2 == 0 
                                ? DesignSystem.Colors.Surface 
                                : DesignSystem.Colors.SurfaceHover;
                        }
                    }

                    // Yeni hover
                    if (e.RowIndex < dataGridView.Rows.Count)
                    {
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = 
                            DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.95f);
                    }
                }
            };

            dataGridView.MouseLeave += (s, e) =>
            {
                lastRowIndex = -1;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.DefaultCellStyle.BackColor = row.Index % 2 == 0 
                        ? DesignSystem.Colors.Surface 
                        : DesignSystem.Colors.SurfaceHover;
                }
            };
        }

        /// <summary>
        /// DataGridView kolonuna icon ekler
        /// </summary>
        public static void AddIconColumn(DataGridView dataGridView, string columnName, string headerText, int width = 40)
        {
            if (dataGridView == null) return;

            DataGridViewImageColumn column = new DataGridViewImageColumn
            {
                Name = columnName,
                HeaderText = headerText,
                Width = width,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Resizable = DataGridViewTriState.False
            };

            dataGridView.Columns.Add(column);
        }

        /// <summary>
        /// DataGridView'de satır rengini değiştirir (özel durumlar için)
        /// </summary>
        public static void SetRowColor(DataGridViewRow row, Color color)
        {
            if (row == null) return;

            row.DefaultCellStyle.BackColor = color;
            row.DefaultCellStyle.SelectionBackColor = DesignSystem.Darken(color, 0.1f);
        }

        /// <summary>
        /// Stok durumuna göre satır rengini ayarlar
        /// </summary>
        public static void ApplyStockColorCoding(DataGridView dataGridView, string stockColumnName, int lowStockThreshold = 20)
        {
            if (dataGridView == null) return;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[stockColumnName].Value != null)
                {
                    if (int.TryParse(row.Cells[stockColumnName].Value.ToString(), out int stock))
                    {
                        if (stock <= 0)
                        {
                            // Stok yok - Kırmızı vurgu
                            SetRowColor(row, DesignSystem.Lighten(DesignSystem.Colors.Error, 0.9f));
                        }
                        else if (stock <= lowStockThreshold)
                        {
                            // Düşük stok - Sarı vurgu
                            SetRowColor(row, DesignSystem.Lighten(DesignSystem.Colors.Warning, 0.9f));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// DataGridView ile HScrollBar'ı senkronize eder
        /// </summary>
        public static void AttachCustomScrollbar(DataGridView grid, HScrollBar scrollbar)
        {
            if (grid == null || scrollbar == null) return;

            // Native scrollbar'ı sadece dikey yap
            grid.ScrollBars = ScrollBars.Vertical;

            // Event handler
            void UpdateScrollbar()
            {
                try 
                {
                    if (grid.ColumnCount == 0) return;
                    
                    int totalWidth = 0;
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        totalWidth += col.Width;
                    }
                    
                    int visibleWidth = grid.ClientSize.Width;
                    
                    if (totalWidth > visibleWidth)
                    {
                        scrollbar.Enabled = true;
                        // Maximum değeri LargeChange kadar artırılmalı
                        scrollbar.LargeChange = visibleWidth;
                        scrollbar.Maximum = totalWidth;
                        
                        // Mevcut değer sınırların dışındaysa düzelt
                        if (scrollbar.Value > scrollbar.Maximum - scrollbar.LargeChange + 1)
                        {
                            int newValue = scrollbar.Maximum - scrollbar.LargeChange + 1;
                            if (newValue < 0) newValue = 0;
                            scrollbar.Value = newValue;
                        }
                    }
                    else
                    {
                        scrollbar.Enabled = false;
                        scrollbar.Maximum = 0;
                    }
                } 
                catch { }
            }

            // Scroll events
            scrollbar.Scroll += (s, ev) => 
            {
                try {
                    if(grid.HorizontalScrollingOffset != ev.NewValue)
                    {
                        grid.HorizontalScrollingOffset = ev.NewValue;
                    }
                } catch { }
            };

            grid.Scroll += (s, ev) =>
            {
                if(ev.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                   try {
                       if(scrollbar.Value != ev.NewValue) 
                           scrollbar.Value = ev.NewValue;
                   } catch { }
                }
            };

            grid.ColumnWidthChanged += (s, ev) => UpdateScrollbar();
            grid.Resize += (s, ev) => UpdateScrollbar();
            grid.DataSourceChanged += (s, ev) => UpdateScrollbar();
            
            // Initial call
            UpdateScrollbar();
        }
    }
}

