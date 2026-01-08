using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// Modern Panel kontrolü (Card design)
    /// Microsoft Teams ve Notion card tasarımından ilham alınmıştır
    /// </summary>
    public class ModernPanel : Panel
    {
        #region Fields

        private string _title = string.Empty;
        private bool _showTitle = false;
        private bool _showShadow = true;
        private int _borderRadius = DesignSystem.Borders.RadiusMedium;

        #endregion

        #region Properties

        /// <summary>
        /// Panel başlığı
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _showTitle = !string.IsNullOrEmpty(value);
                UpdatePadding();
                Invalidate();
            }
        }

        /// <summary>
        /// Başlık gösterilsin mi?
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                _showTitle = value;
                UpdatePadding();
                Invalidate();
            }
        }

        /// <summary>
        /// Gölge gösterilsin mi?
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool ShowShadow
        {
            get => _showShadow;
            set
            {
                _showShadow = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Border radius
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(12)]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor

        public ModernPanel()
        {
            SetStyle(ControlStyles.UserPaint | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = DesignSystem.Colors.Surface;
            ForeColor = DesignSystem.Colors.Text;
            Padding = new Padding(DesignSystem.Spacing.M);
            BorderStyle = BorderStyle.None;

            // Tema değişikliği
            ThemeManager.ThemeChanged += (s, e) =>
            {
                BackColor = DesignSystem.Colors.Surface;
                ForeColor = DesignSystem.Colors.Text;
                Invalidate();
            };
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            // Kaydırma sırasında görüntü bozulmalarını önlemek için tüm paneli yeniden çiz
            this.Invalidate();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Padding'i başlık durumuna göre günceller
        /// </summary>
        private void UpdatePadding()
        {
            if (_showTitle && !string.IsNullOrEmpty(_title))
            {
                // Başlık varsa üst padding'i artır
                int titleHeight = (int)DesignSystem.Fonts.Heading3.GetHeight() + DesignSystem.Spacing.S;
                Padding = new Padding(
                    DesignSystem.Spacing.M,
                    titleHeight + DesignSystem.Spacing.M,
                    DesignSystem.Spacing.M,
                    DesignSystem.Spacing.M
                );
            }
            else
            {
                Padding = new Padding(DesignSystem.Spacing.M);
            }
        }

        #endregion

        #region Painting

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Arka plan ve border çiz
            using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, _borderRadius))
            {
                // Gölge çiz (opsiyonel)
                if (_showShadow)
                {
                    DrawShadow(g, path);
                }

                // Arka plan
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    g.FillPath(brush, path);
                }

                // Border
                using (Pen pen = new Pen(DesignSystem.Colors.Border, DesignSystem.Borders.WidthDefault))
                {
                    float offset = pen.Width / 2f;
                    RectangleF rect = new RectangleF(
                        ClientRectangle.X + offset,
                        ClientRectangle.Y + offset,
                        ClientRectangle.Width - pen.Width,
                        ClientRectangle.Height - pen.Width);

                    using (GraphicsPath borderPath = GetRoundedRectangle(rect, _borderRadius))
                    {
                        g.DrawPath(pen, borderPath);
                    }
                }
            }

            // Başlık çiz
            if (_showTitle && !string.IsNullOrEmpty(_title))
            {
                DrawTitle(g);
            }
        }

        /// <summary>
        /// Gölge çizer
        /// </summary>
        private void DrawShadow(Graphics g, GraphicsPath path)
        {
            Rectangle shadowRect = ClientRectangle;
            shadowRect.Offset(0, 2);
            
            // Daha yumuşak bir gölge için alpha değerini düşürüyoruz
            Color shadowColor = DesignSystem.WithAlpha(DesignSystem.Shadow.SmallColor, 30);
            
            using (PathGradientBrush brush = new PathGradientBrush(path))
            {
                brush.CenterColor = shadowColor;
                brush.SurroundColors = new[] { Color.Transparent };
                brush.FocusScales = new PointF(0.95f, 0.95f);
                g.FillPath(brush, path);
            }
        }

        /// <summary>
        /// Başlık çizer
        /// </summary>
        private void DrawTitle(Graphics g)
        {
            Rectangle titleRect = new Rectangle(
                DesignSystem.Spacing.M,
                DesignSystem.Spacing.S,
                Width - (DesignSystem.Spacing.M * 2),
                (int)DesignSystem.Fonts.Heading3.GetHeight() + DesignSystem.Spacing.XS
            );

            TextRenderer.DrawText(
                g,
                _title,
                DesignSystem.Fonts.Heading3,
                titleRect,
                ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        /// <summary>
        /// Yuvarlatılmış dikdörtgen path'i döndürür
        /// </summary>
        private GraphicsPath GetRoundedRectangle(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            float diameter = radius * 2f;
            RectangleF arc = new RectangleF(rect.Location, new SizeF(diameter, diameter));

            // Sol üst
            path.AddArc(arc, 180, 90);

            // Sağ üst
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Sağ alt
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Sol alt
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            return GetRoundedRectangle(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), radius);
        }

        #endregion
    }
}

