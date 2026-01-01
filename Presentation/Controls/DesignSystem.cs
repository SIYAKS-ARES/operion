using System;
using System.Drawing;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// operion Tasarım Sistemi - Renk, Font, Spacing ve diğer tasarım sabitleri
    /// Microsoft Teams ve Notion'dan ilham alınarak oluşturulmuştur
    /// </summary>
    public static class DesignSystem
    {
        #region Renk Paleti

        /// <summary>
        /// Light Mode Renk Paleti
        /// </summary>
        public static class LightColors
        {
            // Ana Renkler
            public static Color Primary = ColorTranslator.FromHtml("#0078D4");      // Microsoft Blue
            public static Color Secondary = ColorTranslator.FromHtml("#106EBE");    // Koyu Mavi
            public static Color Accent = ColorTranslator.FromHtml("#50E6FF");       // Açık Mavi
            public static Color Teal = ColorTranslator.FromHtml("#008575");         // Turkuaz (Logo)

            // Durum Renkleri
            public static Color Success = ColorTranslator.FromHtml("#107C10");      // Yeşil
            public static Color Warning = ColorTranslator.FromHtml("#FFB900");      // Sarı
            public static Color Error = ColorTranslator.FromHtml("#E81123");        // Kırmızı
            public static Color Info = ColorTranslator.FromHtml("#0078D4");         // Mavi (Primary)

            // Yüzey Renkleri
            public static Color Background = ColorTranslator.FromHtml("#F3F4F6");   // Ana arka plan
            public static Color Surface = ColorTranslator.FromHtml("#FFFFFF");      // Panel, Card arka planı
            public static Color SurfaceHover = ColorTranslator.FromHtml("#F9FAFB"); // Hover durumu

            // Metin Renkleri
            public static Color Text = ColorTranslator.FromHtml("#1F2937");         // Ana metin
            public static Color TextLight = ColorTranslator.FromHtml("#6B7280");    // Yardımcı metin
            public static Color TextDisabled = ColorTranslator.FromHtml("#9CA3AF"); // Devre dışı metin

            // Border ve Outline
            public static Color Border = ColorTranslator.FromHtml("#E5E7EB");       // Çerçeveler
            public static Color BorderDark = ColorTranslator.FromHtml("#D1D5DB");   // Koyu çerçeve
            public static Color Focus = ColorTranslator.FromHtml("#50E6FF");        // Focus outline
        }

        /// <summary>
        /// Dark Mode Renk Paleti
        /// </summary>
        public static class DarkColors
        {
            // Ana Renkler
            public static Color Primary = ColorTranslator.FromHtml("#4A9EFF");      // Açık Mavi
            public static Color Secondary = ColorTranslator.FromHtml("#357ABD");    // Orta Mavi
            public static Color Accent = ColorTranslator.FromHtml("#64D2FF");       // Açık Cyan
            public static Color Teal = ColorTranslator.FromHtml("#10B5A0");         // Açık Turkuaz

            // Durum Renkleri
            public static Color Success = ColorTranslator.FromHtml("#6CCB5F");      // Açık Yeşil
            public static Color Warning = ColorTranslator.FromHtml("#FFC83D");      // Açık Sarı
            public static Color Error = ColorTranslator.FromHtml("#F1707E");        // Açık Kırmızı
            public static Color Info = ColorTranslator.FromHtml("#4A9EFF");         // Açık Mavi

            // Yüzey Renkleri
            public static Color Background = ColorTranslator.FromHtml("#0F1419");   // Ana arka plan (Notion benzeri)
            public static Color Surface = ColorTranslator.FromHtml("#1A1F26");      // Panel, Card arka planı
            public static Color SurfaceAlt = ColorTranslator.FromHtml("#242B35");   // Alternatif yüzey
            public static Color SurfaceHover = ColorTranslator.FromHtml("#2D3748"); // Hover durumu

            // Metin Renkleri
            public static Color Text = ColorTranslator.FromHtml("#E4E4E7");         // Ana metin
            public static Color TextLight = ColorTranslator.FromHtml("#9CA3AF");    // Yardımcı metin
            public static Color TextDisabled = ColorTranslator.FromHtml("#6B7280"); // Devre dışı metin

            // Border ve Outline
            public static Color Border = ColorTranslator.FromHtml("#2D3748");       // Çerçeveler
            public static Color BorderDark = ColorTranslator.FromHtml("#374151");   // Koyu çerçeve
            public static Color Focus = ColorTranslator.FromHtml("#64D2FF");        // Focus outline
        }

        /// <summary>
        /// Mevcut temaya göre renk döndürür
        /// </summary>
        public static class Colors
        {
            public static Color Primary => ThemeManager.IsDarkMode ? DarkColors.Primary : LightColors.Primary;
            public static Color Secondary => ThemeManager.IsDarkMode ? DarkColors.Secondary : LightColors.Secondary;
            public static Color Accent => ThemeManager.IsDarkMode ? DarkColors.Accent : LightColors.Accent;
            public static Color Teal => ThemeManager.IsDarkMode ? DarkColors.Teal : LightColors.Teal;

            public static Color Success => ThemeManager.IsDarkMode ? DarkColors.Success : LightColors.Success;
            public static Color Warning => ThemeManager.IsDarkMode ? DarkColors.Warning : LightColors.Warning;
            public static Color Error => ThemeManager.IsDarkMode ? DarkColors.Error : LightColors.Error;
            public static Color Info => ThemeManager.IsDarkMode ? DarkColors.Info : LightColors.Info;

            public static Color Background => ThemeManager.IsDarkMode ? DarkColors.Background : LightColors.Background;
            public static Color Surface => ThemeManager.IsDarkMode ? DarkColors.Surface : LightColors.Surface;
            public static Color SurfaceAlt => ThemeManager.IsDarkMode ? DarkColors.SurfaceAlt : LightColors.SurfaceHover;
            public static Color SurfaceHover => ThemeManager.IsDarkMode ? DarkColors.SurfaceHover : LightColors.SurfaceHover;

            public static Color Text => ThemeManager.IsDarkMode ? DarkColors.Text : LightColors.Text;
            public static Color TextLight => ThemeManager.IsDarkMode ? DarkColors.TextLight : LightColors.TextLight;
            public static Color TextDisabled => ThemeManager.IsDarkMode ? DarkColors.TextDisabled : LightColors.TextDisabled;

            public static Color Border => ThemeManager.IsDarkMode ? DarkColors.Border : LightColors.Border;
            public static Color BorderDark => ThemeManager.IsDarkMode ? DarkColors.BorderDark : LightColors.BorderDark;
            public static Color Focus => ThemeManager.IsDarkMode ? DarkColors.Focus : LightColors.Focus;
        }

        #endregion

        #region Typography

        /// <summary>
        /// Font tanımlamaları
        /// </summary>
        public static class Fonts
        {
            // Font Family
            public const string FontFamily = "Segoe UI";
            public const string FallbackFont = "Tahoma";

            // Font Sizes
            public const float Heading1Size = 24f;
            public const float Heading2Size = 18f;
            public const float Heading3Size = 14f;
            public const float BodySize = 11f;
            public const float SmallSize = 9f;
            public const float ButtonSize = 11f;

            // Font Instances
            private static Font? _heading1;
            private static Font? _heading2;
            private static Font? _heading3;
            private static Font? _body;
            private static Font? _bodyBold;
            private static Font? _small;
            private static Font? _button;

            public static Font Heading1 => _heading1 ??= new Font(FontFamily, Heading1Size, FontStyle.Bold);
            public static Font Heading2 => _heading2 ??= new Font(FontFamily, Heading2Size, FontStyle.Bold);
            public static Font Heading3 => _heading3 ??= new Font(FontFamily, Heading3Size, FontStyle.Bold);
            public static Font Body => _body ??= new Font(FontFamily, BodySize, FontStyle.Regular);
            public static Font BodyBold => _bodyBold ??= new Font(FontFamily, BodySize, FontStyle.Bold);
            public static Font Small => _small ??= new Font(FontFamily, SmallSize, FontStyle.Regular);
            public static Font Button => _button ??= new Font(FontFamily, ButtonSize, FontStyle.Bold);
        }

        #endregion

        #region Spacing

        /// <summary>
        /// Spacing ve padding değerleri
        /// </summary>
        public static class Spacing
        {
            public const int XS = 4;    // Extra Small
            public const int S = 8;     // Small
            public const int M = 12;    // Medium
            public const int L = 16;    // Large
            public const int XL = 24;   // Extra Large
            public const int XXL = 32;  // Extra Extra Large

            // Padding Helpers
            public static Padding PaddingXS => new Padding(XS);
            public static Padding PaddingS => new Padding(S);
            public static Padding PaddingM => new Padding(M);
            public static Padding PaddingL => new Padding(L);
            public static Padding PaddingXL => new Padding(XL);
            public static Padding PaddingXXL => new Padding(XXL);
        }

        #endregion

        #region Border ve Corners

        /// <summary>
        /// Border radius ve width değerleri
        /// </summary>
        public static class Borders
        {
            // Border Radius
            public const int RadiusSmall = 4;   // Button, Input
            public const int RadiusMedium = 6;  // Panel, GroupBox
            public const int RadiusLarge = 8;   // Modal, Dialog

            // Border Width
            public const int WidthDefault = 1;
            public const int WidthFocus = 2;
            public const int WidthThick = 3;
        }

        #endregion

        #region Shadow

        /// <summary>
        /// Box shadow değerleri
        /// </summary>
        public static class Shadow
        {
            // Shadow yapısı: Color ve Offset
            public static Color SmallColor = Color.FromArgb(13, 0, 0, 0);     // rgba(0,0,0,0.05)
            public static Color MediumColor = Color.FromArgb(18, 0, 0, 0);    // rgba(0,0,0,0.07)
            public static Color LargeColor = Color.FromArgb(26, 0, 0, 0);     // rgba(0,0,0,0.1)

            // Shadow boyutları
            public static Size SmallOffset = new Size(0, 1);
            public static Size MediumOffset = new Size(0, 4);
            public static Size LargeOffset = new Size(0, 10);
        }

        #endregion

        #region Control Sizes

        /// <summary>
        /// Standart kontrol boyutları
        /// </summary>
        public static class ControlSizes
        {
            // Button
            public const int ButtonHeight = 32;
            public const int ButtonMinWidth = 80;
            public const int ButtonIconSize = 16;

            // TextBox / ComboBox
            public const int InputHeight = 32;
            public const int InputMinWidth = 120;

            // Icon Sizes
            public const int IconSmall = 16;
            public const int IconMedium = 24;
            public const int IconLarge = 32;

            // DataGridView
            public const int DataGridRowHeight = 32;
            public const int DataGridHeaderHeight = 36;
        }

        #endregion

        #region Animation

        /// <summary>
        /// Animasyon süreleri (millisaniye)
        /// </summary>
        public static class Animation
        {
            public const int Fast = 150;       // Hover, focus
            public const int Normal = 250;     // Transitions
            public const int Slow = 400;       // Fade in/out
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Rengi belirtilen alpha değeriyle döndürür
        /// </summary>
        public static Color WithAlpha(Color color, int alpha)
        {
            return Color.FromArgb(Math.Max(0, Math.Min(255, alpha)), color.R, color.G, color.B);
        }

        /// <summary>
        /// Rengi koyulaştırır (darken)
        /// </summary>
        public static Color Darken(Color color, float amount)
        {
            amount = Math.Max(0f, Math.Min(1f, amount)); // 0-1 arası
            return Color.FromArgb(
                color.A,
                (int)(color.R * (1 - amount)),
                (int)(color.G * (1 - amount)),
                (int)(color.B * (1 - amount))
            );
        }

        /// <summary>
        /// Rengi açar (lighten)
        /// </summary>
        public static Color Lighten(Color color, float amount)
        {
            amount = Math.Max(0f, Math.Min(1f, amount)); // 0-1 arası
            return Color.FromArgb(
                color.A,
                color.R + (int)((255 - color.R) * amount),
                color.G + (int)((255 - color.G) * amount),
                color.B + (int)((255 - color.B) * amount)
            );
        }

        #endregion
    }
}

