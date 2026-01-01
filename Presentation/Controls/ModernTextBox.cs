using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// Modern TextBox kontrolü
    /// Fluent Design System stilinde
    /// </summary>
    public class ModernTextBox : Panel
    {
        #region Fields

        private TextBox _textBox;
        private Label _placeholderLabel;
        private Label _errorLabel;
        private bool _isFocused = false;
        private bool _hasError = false;
        private string _errorMessage = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// TextBox metni
        /// </summary>
        public override string Text
        {
            get => _textBox.Text;
            set
            {
                _textBox.Text = value;
                UpdatePlaceholderVisibility();
            }
        }

        /// <summary>
        /// Placeholder metni
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get => _placeholderLabel.Text;
            set
            {
                _placeholderLabel.Text = value;
                UpdatePlaceholderVisibility();
            }
        }

        /// <summary>
        /// Hata durumu
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                UpdateBorder();
                _errorLabel.Visible = _hasError && !string.IsNullOrEmpty(_errorMessage);
            }
        }

        /// <summary>
        /// Hata mesajı
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                _errorLabel.Text = value;
                _errorLabel.Visible = _hasError && !string.IsNullOrEmpty(_errorMessage);
            }
        }

        /// <summary>
        /// Şifre modu
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get => _textBox.UseSystemPasswordChar;
            set => _textBox.UseSystemPasswordChar = value;
        }

        /// <summary>
        /// Şifre karakteri
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue('\0')]
        public char PasswordChar
        {
            get => _textBox.PasswordChar;
            set => _textBox.PasswordChar = value;
        }

        /// <summary>
        /// Maksimum uzunluk
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(32767)]
        public int MaxLength
        {
            get => _textBox.MaxLength;
            set => _textBox.MaxLength = value;
        }

        /// <summary>
        /// Çok satırlı
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool Multiline
        {
            get => _textBox.Multiline;
            set
            {
                _textBox.Multiline = value;
                if (value)
                {
                    _textBox.ScrollBars = ScrollBars.Vertical;
                    Height = 80; // Çok satırlı için daha yüksek
                }
                else
                {
                    _textBox.ScrollBars = ScrollBars.None;
                    Height = DesignSystem.ControlSizes.InputHeight;
                }
            }
        }

        /// <summary>
        /// Sadece okunur
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _textBox.ReadOnly;
            set
            {
                _textBox.ReadOnly = value;
                UpdateBorder();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Text değiştiğinde tetiklenir
        /// </summary>
        public new event EventHandler? TextChanged
        {
            add => _textBox.TextChanged += value;
            remove => _textBox.TextChanged -= value;
        }

        /// <summary>
        /// Focus alındığında tetiklenir
        /// </summary>
        public new event EventHandler? GotFocus
        {
            add => _textBox.GotFocus += value;
            remove => _textBox.GotFocus -= value;
        }

        /// <summary>
        /// Focus kaybedildiğinde tetiklenir
        /// </summary>
        public new event EventHandler? LostFocus
        {
            add => _textBox.LostFocus += value;
            remove => _textBox.LostFocus -= value;
        }

        /// <summary>
        /// KeyPress olayı
        /// </summary>
        public new event KeyPressEventHandler? KeyPress
        {
            add => _textBox.KeyPress += value;
            remove => _textBox.KeyPress -= value;
        }

        /// <summary>
        /// KeyDown olayı
        /// </summary>
        public new event KeyEventHandler? KeyDown
        {
            add => _textBox.KeyDown += value;
            remove => _textBox.KeyDown -= value;
        }

        #endregion

        #region Constructor

        public ModernTextBox()
        {
            // Panel ayarları
            SetStyle(ControlStyles.UserPaint | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = DesignSystem.Colors.Surface;
            Height = DesignSystem.ControlSizes.InputHeight;
            Padding = new Padding(DesignSystem.Spacing.S);

            // TextBox oluştur
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = DesignSystem.Colors.Surface,
                ForeColor = DesignSystem.Colors.Text,
                Font = DesignSystem.Fonts.Body,
                Dock = DockStyle.Fill
            };

            // Placeholder Label
            _placeholderLabel = new Label
            {
                Text = "",
                ForeColor = DesignSystem.Colors.TextLight,
                Font = DesignSystem.Fonts.Body,
                BackColor = Color.Transparent,
                Cursor = Cursors.IBeam,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Error Label
            _errorLabel = new Label
            {
                Text = "",
                ForeColor = DesignSystem.Colors.Error,
                Font = DesignSystem.Fonts.Small,
                BackColor = Color.Transparent,
                Dock = DockStyle.Bottom,
                Height = 16,
                Visible = false,
                Padding = new Padding(2, 0, 0, 0)
            };

            // Event handlers
            _textBox.TextChanged += (s, e) => UpdatePlaceholderVisibility();
            _textBox.Enter += (s, e) => { _isFocused = true; Invalidate(); };
            _textBox.Leave += (s, e) => { _isFocused = false; Invalidate(); };
            _placeholderLabel.Click += (s, e) => _textBox.Focus();

            // Tema değişikliği
            ThemeManager.ThemeChanged += (s, e) =>
            {
                _textBox.BackColor = DesignSystem.Colors.Surface;
                _textBox.ForeColor = DesignSystem.Colors.Text;
                _placeholderLabel.ForeColor = DesignSystem.Colors.TextLight;
                BackColor = DesignSystem.Colors.Surface;
                Invalidate();
            };

            // Kontrolleri ekle
            Controls.Add(_errorLabel);
            Controls.Add(_textBox);
            Controls.Add(_placeholderLabel);

            UpdatePlaceholderVisibility();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Focus'u TextBox'a verir
        /// </summary>
        public new void Focus()
        {
            _textBox.Focus();
        }

        /// <summary>
        /// Tüm metni seçer
        /// </summary>
        public void SelectAll()
        {
            _textBox.SelectAll();
        }

        /// <summary>
        /// Temizler
        /// </summary>
        public void Clear()
        {
            _textBox.Clear();
            HasError = false;
            ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Placeholder görünürlüğünü günceller
        /// </summary>
        private void UpdatePlaceholderVisibility()
        {
            _placeholderLabel.Visible = string.IsNullOrEmpty(_textBox.Text);
        }

        /// <summary>
        /// Border'ı günceller
        /// </summary>
        private void UpdateBorder()
        {
            Invalidate();
        }

        #endregion

        #region Painting

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Border rengi belirle
            Color borderColor;
            int borderWidth;

            if (_hasError)
            {
                borderColor = DesignSystem.Colors.Error;
                borderWidth = DesignSystem.Borders.WidthDefault;
            }
            else if (_isFocused)
            {
                borderColor = DesignSystem.Colors.Focus;
                borderWidth = DesignSystem.Borders.WidthFocus;
            }
            else if (ReadOnly)
            {
                borderColor = DesignSystem.Colors.Border;
                borderWidth = DesignSystem.Borders.WidthDefault;
            }
            else
            {
                borderColor = DesignSystem.Colors.Border;
                borderWidth = DesignSystem.Borders.WidthDefault;
            }

            // Border çiz
            int radius = DesignSystem.Borders.RadiusSmall;
            using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, radius))
            {
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    // Border'ın yarısı dışarıda kalmaması için içeri doğru şişiriyoruz
                    float offset = borderWidth / 2f;
                    RectangleF rect = new RectangleF(
                        ClientRectangle.X + offset, 
                        ClientRectangle.Y + offset, 
                        ClientRectangle.Width - borderWidth, 
                        ClientRectangle.Height - borderWidth);
                        
                    using (GraphicsPath innerPath = GetRoundedRectangle(rect, radius))
                    {
                        g.DrawPath(pen, innerPath);
                    }
                }
            }
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
}

