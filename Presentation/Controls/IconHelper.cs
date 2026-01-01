using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace operion.Presentation.Controls
{
    /// <summary>
    /// İkon yönetimi için helper sınıfı
    /// Fluent Icons kullanımını kolaylaştırır
    /// </summary>
    public static class IconHelper
    {
        private static Dictionary<string, Image> _iconCache = new Dictionary<string, Image>();

        /// <summary>
        /// İkon dizininin yolu
        /// </summary>
        private static string IconDirectory
        {
            get
            {
                string baseDir = AppContext.BaseDirectory;
                return Path.Combine(baseDir, "Assets", "Icons");
            }
        }

        #region Icon Loading

        /// <summary>
        /// İkonu yükler (cache kullanır)
        /// </summary>
        public static Image? GetIcon(string iconName, int size = 16)
        {
            string key = $"{iconName}_{size}";

            if (_iconCache.ContainsKey(key))
            {
                return _iconCache[key];
            }

            try
            {
                // Icon dosya yolunu oluştur
                string iconPath = Path.Combine(IconDirectory, $"{iconName}_{size}.png");

                if (File.Exists(iconPath))
                {
                    Image icon = Image.FromFile(iconPath);
                    _iconCache[key] = icon;
                    return icon;
                }
                else
                {
                    // Dosya yoksa placeholder icon döndür
                    Image placeholder = CreatePlaceholderIcon(size);
                    _iconCache[key] = placeholder;
                    return placeholder;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Birden fazla ikonu tek seferde yükler
        /// </summary>
        public static void PreloadIcons(string[] iconNames, int size = 16)
        {
            foreach (string iconName in iconNames)
            {
                GetIcon(iconName, size);
            }
        }

        #endregion

        #region Common Icons

        /// <summary>
        /// Yaygın kullanılan ikonlar için kısayol metodlar
        /// </summary>
        public static class CommonIcons
        {
            public static Image? Save => GetIcon("save");
            public static Image? Delete => GetIcon("delete");
            public static Image? Edit => GetIcon("edit");
            public static Image? Add => GetIcon("add");
            public static Image? Refresh => GetIcon("refresh");
            public static Image? Search => GetIcon("search");
            public static Image? Settings => GetIcon("settings");
            public static Image? User => GetIcon("person");
            public static Image? Home => GetIcon("home");
            public static Image? Document => GetIcon("document");
            public static Image? Box => GetIcon("box");
            public static Image? People => GetIcon("people");
            public static Image? Building => GetIcon("building");
            public static Image? Money => GetIcon("money");
            public static Image? Chart => GetIcon("chart");
            public static Image? Note => GetIcon("note");
            public static Image? Book => GetIcon("book");
            public static Image? Mail => GetIcon("mail");
            public static Image? Close => GetIcon("dismiss");
            public static Image? Check => GetIcon("checkmark");
            public static Image? Warning => GetIcon("warning");
            public static Image? Error => GetIcon("error_circle");
            public static Image? Info => GetIcon("info");
            public static Image? Filter => GetIcon("filter");
            public static Image? Export => GetIcon("arrow_export");
            public static Image? Import => GetIcon("arrow_import");
            public static Image? Print => GetIcon("print");
            public static Image? Calendar => GetIcon("calendar");
            public static Image? Clock => GetIcon("clock");
            public static Image? Phone => GetIcon("phone");
            public static Image? Location => GetIcon("location");
        }

        #endregion

        #region Icon Manipulation

        /// <summary>
        /// İkonu belirtilen boyuta ölçeklendirir
        /// </summary>
        public static Image? ResizeIcon(Image icon, int newSize)
        {
            if (icon == null) return null;

            try
            {
                Bitmap resized = new Bitmap(newSize, newSize);
                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(icon, 0, 0, newSize, newSize);
                }
                return resized;
            }
            catch
            {
                return icon;
            }
        }

        /// <summary>
        /// İkonu belirtilen renge boyar
        /// </summary>
        public static Image? TintIcon(Image icon, Color color)
        {
            if (icon == null) return null;

            try
            {
                Bitmap tinted = new Bitmap(icon.Width, icon.Height);
                using (Graphics g = Graphics.FromImage(tinted))
                {
                    // Color matrix ile renklendirme
                    System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][]
                        {
                            new float[] {color.R / 255f, 0, 0, 0, 0},
                            new float[] {0, color.G / 255f, 0, 0, 0},
                            new float[] {0, 0, color.B / 255f, 0, 0},
                            new float[] {0, 0, 0, color.A / 255f, 0},
                            new float[] {0, 0, 0, 0, 1}
                        });

                    System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
                    attributes.SetColorMatrix(colorMatrix);

                    g.DrawImage(icon,
                        new Rectangle(0, 0, icon.Width, icon.Height),
                        0, 0, icon.Width, icon.Height,
                        GraphicsUnit.Pixel,
                        attributes);
                }
                return tinted;
            }
            catch
            {
                return icon;
            }
        }

        #endregion

        #region Placeholder Icon

        /// <summary>
        /// Placeholder icon oluşturur (icon dosyası bulunamazsa)
        /// </summary>
        private static Image CreatePlaceholderIcon(int size)
        {
            Bitmap placeholder = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Arka plan
                using (SolidBrush brush = new SolidBrush(DesignSystem.Colors.SurfaceHover))
                {
                    g.FillRectangle(brush, 0, 0, size, size);
                }

                // Çerçeve
                using (Pen pen = new Pen(DesignSystem.Colors.Border, 1))
                {
                    g.DrawRectangle(pen, 0, 0, size - 1, size - 1);
                }

                // "?" işareti
                using (Font font = new Font("Segoe UI", size / 2, FontStyle.Bold))
                {
                    using (SolidBrush brush = new SolidBrush(DesignSystem.Colors.TextLight))
                    {
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString("?", font, brush, new RectangleF(0, 0, size, size), format);
                    }
                }
            }
            return placeholder;
        }

        #endregion

        #region Cache Management

        /// <summary>
        /// Icon cache'ini temizler
        /// </summary>
        public static void ClearCache()
        {
            foreach (var icon in _iconCache.Values)
            {
                icon?.Dispose();
            }
            _iconCache.Clear();
        }

        /// <summary>
        /// Belirli bir ikonu cache'den kaldırır
        /// </summary>
        public static void RemoveFromCache(string iconName, int size = 16)
        {
            string key = $"{iconName}_{size}";
            if (_iconCache.ContainsKey(key))
            {
                _iconCache[key]?.Dispose();
                _iconCache.Remove(key);
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Icon sistemi ni başlatır ve yaygın ikonları yükler
        /// </summary>
        public static void Initialize()
        {
            // Assets/Icons dizinini oluştur (yoksa)
            if (!Directory.Exists(IconDirectory))
            {
                Directory.CreateDirectory(IconDirectory);
            }

            // Yaygın kullanılan ikonları ön yükle
            string[] commonIconNames = new[]
            {
                "save", "delete", "edit", "add", "refresh", "search",
                "settings", "person", "home", "document", "box", "people",
                "building", "money", "chart", "note", "book", "mail"
            };

            PreloadIcons(commonIconNames, 16);
            PreloadIcons(commonIconNames, 24);
        }

        #endregion
    }
}

