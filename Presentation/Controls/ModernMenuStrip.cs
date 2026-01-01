using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using operion.Presentation.Controls;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// Modern MenuStrip kontrolü
    /// Microsoft Teams tarzı modern menü tasarımı
    /// </summary>
    public class ModernMenuStrip : MenuStrip
    {
        #region Constructor

        public ModernMenuStrip()
        {
            SetStyle(ControlStyles.UserPaint | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.ResizeRedraw, true);

            // Modern görünüm ayarları
            Renderer = new ModernMenuStripRenderer();
            BackColor = DesignSystem.Colors.Primary;
            ForeColor = Color.White;
            Font = DesignSystem.Fonts.Heading3; // Daha büyük ve kalın font (14pt Bold)
            Height = 60; // Daha yüksek menü (Teams tarzı genişletildi)
            ImageScalingSize = new Size(32, 32); // İkonları büyüt

            // Tema değişikliği
            ThemeManager.ThemeChanged += (s, e) =>
            {
                BackColor = DesignSystem.Colors.Primary;
                ForeColor = Color.White;
                Invalidate();
            };
        }

        #endregion
    }

    /// <summary>
    /// Modern MenuStrip Renderer
    /// </summary>
    public class ModernMenuStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            // Arka plan rengi
            using (SolidBrush brush = new SolidBrush(DesignSystem.Colors.Primary))
            {
                e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

            // Aktif sayfa vurgusu (Tag = "Active" olanlar için)
            bool isActive = e.Item.Tag?.ToString() == "Active";

            if (isActive)
            {
                // Aktif sayfa için arka plan (hafif şeffaf beyaz)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, Color.White)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }

                // Alt çizgi vurgusu
                using (Pen pen = new Pen(Color.White, 5)) // Daha kalın çizgi
                {
                    e.Graphics.DrawLine(pen, 4, e.Item.Height - 4, e.Item.Width - 4, e.Item.Height - 4);
                }
            }
            else if (e.Item.Selected || e.Item.Pressed)
            {
                // Hover/Press durumu
                using (SolidBrush brush = new SolidBrush(DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.15f)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            // Metin rengi
            e.TextColor = Color.White;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            // Ayırıcı çizgi
            Rectangle rect = e.Item.Bounds;
            using (Pen pen = new Pen(DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.2f), 1))
            {
                e.Graphics.DrawLine(pen, rect.Left + 4, rect.Height / 2, rect.Right - 4, rect.Height / 2);
            }
        }
    }
}

