using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// Modern buton kontrolü
    /// Microsoft Teams ve Fluent Design System'den ilham alınmıştır
    /// </summary>
    public class ModernButton : Button
    {
        #region Fields

        private ButtonStyle _buttonStyle = ButtonStyle.Primary;
        private bool _isHovered = false;
        private bool _isPressed = false;
        private Image? _icon;
        private ContentAlignment _iconAlignment = ContentAlignment.MiddleLeft;
        private int _iconSize = DesignSystem.ControlSizes.ButtonIconSize;
        private int _iconSpacing = DesignSystem.Spacing.S;

        #endregion

        #region Properties

        /// <summary>
        /// Buton stili
        /// </summary>
        [Category("Appearance")]
        [Description("Buton görünüm stili")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ButtonStyle ButtonStyle
        {
            get => _buttonStyle;
            set
            {
                _buttonStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Buton ikonu
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(null)]
        public Image? Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                Invalidate();
            }
        }

        /// <summary>
        /// İkon hizalama
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment IconAlignment
        {
            get => _iconAlignment;
            set
            {
                _iconAlignment = value;
                Invalidate();
            }
        }

        /// <summary>
        /// İkon boyutu
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(16)]
        public int IconSize
        {
            get => _iconSize;
            set
            {
                _iconSize = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor

        public ModernButton()
        {
            // Varsayılan ayarlar
            SetStyle(ControlStyles.UserPaint | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Font = DesignSystem.Fonts.Button;
            ForeColor = Color.White;
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            MinimumSize = new Size(DesignSystem.ControlSizes.ButtonMinWidth, DesignSystem.ControlSizes.ButtonHeight);
            Size = new Size(DesignSystem.ControlSizes.ButtonMinWidth, DesignSystem.ControlSizes.ButtonHeight);
            Padding = new Padding(DesignSystem.Spacing.M, DesignSystem.Spacing.S, DesignSystem.Spacing.M, DesignSystem.Spacing.S);
        }

        #endregion

        #region Event Handlers

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _isPressed = false;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion

        #region Painting

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Renkleri belirle
            Color backgroundColor = GetBackgroundColor();
            Color textColor = GetTextColor();

            // Arka plan çiz
            using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, DesignSystem.Borders.RadiusSmall))
            {
                using (SolidBrush brush = new SolidBrush(backgroundColor))
                {
                    g.FillPath(brush, path);
                }
            }

            // Border çiz (Secondary stil için)
            if (_buttonStyle == ButtonStyle.Secondary && Enabled)
            {
                int radius = DesignSystem.Borders.RadiusSmall;
                using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, radius))
                {
                    using (Pen pen = new Pen(DesignSystem.Colors.Primary, DesignSystem.Borders.WidthDefault))
                    {
                        float offset = pen.Width / 2f;
                        RectangleF rect = new RectangleF(
                            ClientRectangle.X + offset,
                            ClientRectangle.Y + offset,
                            ClientRectangle.Width - pen.Width,
                            ClientRectangle.Height - pen.Width);

                        using (GraphicsPath innerPath = GetRoundedRectangle(rect, radius))
                        {
                            g.DrawPath(pen, innerPath);
                        }
                    }
                }
            }

            // İkon ve metin çiz
            Rectangle contentRect = new Rectangle(
                Padding.Left,
                Padding.Top,
                Width - Padding.Horizontal,
                Height - Padding.Vertical
            );

            if (_icon != null)
            {
                DrawIconAndText(g, contentRect, textColor);
            }
            else
            {
                DrawText(g, contentRect, textColor);
            }
        }

        /// <summary>
        /// Arka plan rengini döndürür
        /// </summary>
        private Color GetBackgroundColor()
        {
            if (!Enabled)
            {
                return DesignSystem.Colors.SurfaceHover;
            }

            switch (_buttonStyle)
            {
                case ButtonStyle.Primary:
                    if (_isPressed)
                        return DesignSystem.Colors.Secondary;
                    if (_isHovered)
                        return DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.1f);
                    return DesignSystem.Colors.Primary;

                case ButtonStyle.Secondary:
                    if (_isPressed)
                        return DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.9f);
                    if (_isHovered)
                        return DesignSystem.Colors.SurfaceHover;
                    return DesignSystem.Colors.Surface;

                case ButtonStyle.Icon:
                    if (_isPressed)
                        return DesignSystem.Colors.SurfaceHover;
                    if (_isHovered)
                        return DesignSystem.Lighten(DesignSystem.Colors.Primary, 0.95f);
                    return Color.Transparent;

                case ButtonStyle.Success:
                    if (_isPressed)
                        return DesignSystem.Darken(DesignSystem.Colors.Success, 0.2f);
                    if (_isHovered)
                        return DesignSystem.Lighten(DesignSystem.Colors.Success, 0.1f);
                    return DesignSystem.Colors.Success;

                case ButtonStyle.Error:
                    if (_isPressed)
                        return DesignSystem.Darken(DesignSystem.Colors.Error, 0.2f);
                    if (_isHovered)
                        return DesignSystem.Lighten(DesignSystem.Colors.Error, 0.1f);
                    return DesignSystem.Colors.Error;

                default:
                    return DesignSystem.Colors.Primary;
            }
        }

        /// <summary>
        /// Metin rengini döndürür
        /// </summary>
        private Color GetTextColor()
        {
            if (!Enabled)
            {
                return DesignSystem.Colors.TextDisabled;
            }

            switch (_buttonStyle)
            {
                case ButtonStyle.Primary:
                case ButtonStyle.Success:
                case ButtonStyle.Error:
                    return Color.White;

                case ButtonStyle.Secondary:
                case ButtonStyle.Icon:
                    return DesignSystem.Colors.Primary;

                default:
                    return Color.White;
            }
        }

        /// <summary>
        /// Sadece metin çizer
        /// </summary>
        private void DrawText(Graphics g, Rectangle rect, Color textColor)
        {
            TextRenderer.DrawText(
                g,
                Text,
                Font,
                rect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        /// <summary>
        /// İkon ve metin çizer
        /// </summary>
        private void DrawIconAndText(Graphics g, Rectangle rect, Color textColor)
        {
            if (_icon == null) return;

            // İkon boyutları
            int iconDisplaySize = _iconSize;
            Rectangle iconRect = new Rectangle(
                rect.X,
                rect.Y + (rect.Height - iconDisplaySize) / 2,
                iconDisplaySize,
                iconDisplaySize
            );

            // Metin boyutları
            Size textSize = TextRenderer.MeasureText(g, Text, Font);
            
            // İkon ve metin toplam genişliği
            int totalWidth = iconDisplaySize + _iconSpacing + textSize.Width;

            // Ortalanmış başlangıç pozisyonu
            int startX = rect.X + (rect.Width - totalWidth) / 2;

            // İkon konumu
            iconRect.X = startX;

            // Metin konumu
            Rectangle textRect = new Rectangle(
                startX + iconDisplaySize + _iconSpacing,
                rect.Y,
                textSize.Width,
                rect.Height
            );

            // İkon çiz
            if (Enabled)
            {
                g.DrawImage(_icon, iconRect);
            }
            else
            {
                // Disabled ikon (gri tonlamalı)
                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    var matrix = new System.Drawing.Imaging.ColorMatrix
                    {
                        Matrix33 = 0.3f // Opacity
                    };
                    attributes.SetColorMatrix(matrix);
                    g.DrawImage(_icon, iconRect, 0, 0, _icon.Width, _icon.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            // Metin çiz
            TextRenderer.DrawText(
                g,
                Text,
                Font,
                textRect,
                textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        /// <summary>
        /// Yuvarlatılmış dikdörtgen path'i döndürür
        /// </summary>
        private GraphicsPath GetRoundedRectangle(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
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

    /// <summary>
    /// Buton stilleri
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// Ana buton (dolgu, beyaz metin)
        /// </summary>
        Primary,

        /// <summary>
        /// İkincil buton (çerçeve, renkli metin)
        /// </summary>
        Secondary,

        /// <summary>
        /// İkon butonu (şeffaf arka plan)
        /// </summary>
        Icon,

        /// <summary>
        /// Başarı butonu (yeşil)
        /// </summary>
        Success,

        /// <summary>
        /// Hata/Silme butonu (kırmızı)
        /// </summary>
        Error
    }
}

