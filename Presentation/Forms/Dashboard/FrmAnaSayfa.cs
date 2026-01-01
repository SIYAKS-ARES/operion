using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using operion.Presentation.Controls;
using System.Xml;

namespace operion.Presentation.Forms.Dashboard
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
            
            // Tema sistemi
            ThemeManager.RegisterForm(this);
        }

        DatabaseService bgl = new DatabaseService();

        void azalanstoklar()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("Kalan Stok Sayıları", typeof(int));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT UrunAd,SUM(UrunAdet) AS 'Kalan Stok Sayıları' FROM TBL_URUNLER GROUP BY UrunAd HAVING SUM(UrunAdet) <=20 ORDER BY SUM(UrunAdet)",
                        connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.IsDBNull(0) ? "" : reader.GetString(0),
                                    reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                                );
                            }
                        }
                    }
                }
                grd_azalanstoklar.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_azalanstoklar);
                ModernDataGridViewHelper.EnableHoverEffect(grd_azalanstoklar);
                
                // Boş durum kontrolü
                if (dt.Rows.Count == 0)
                {
                    grd_azalanstoklar.DataSource = null;
                    grd_azalanstoklar.Rows.Clear();
                    grd_azalanstoklar.Columns.Clear();
                    grd_azalanstoklar.Columns.Add("Mesaj", "Mesaj");
                    grd_azalanstoklar.Rows.Add("Azalan stok bulunamadı");
                    grd_azalanstoklar.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Azalan stoklar yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ajanda()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("NotTarih", typeof(string));
                dt.Columns.Add("NotSaat", typeof(string));
                dt.Columns.Add("NotBaslik", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT NotTarih,NotSaat,NotBaslik FROM TBL_NOTLAR ORDER BY NotID DESC LIMIT 8",
                        connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.IsDBNull(0) ? "" : reader.GetString(0),
                                    reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    reader.IsDBNull(2) ? "" : reader.GetString(2)
                                );
                            }
                        }
                    }
                }
                grd_ajanda.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_ajanda);
                ModernDataGridViewHelper.EnableHoverEffect(grd_ajanda);
                
                // Boş durum kontrolü
                if (dt.Rows.Count == 0)
                {
                    grd_ajanda.DataSource = null;
                    grd_ajanda.Rows.Clear();
                    grd_ajanda.Columns.Clear();
                    grd_ajanda.Columns.Add("Mesaj", "Mesaj");
                    grd_ajanda.Rows.Add("Ajanda kaydı bulunamadı");
                    grd_ajanda.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ajanda yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void sonhareketler()
        {
            try
            {
                DataTable dt = new DataTable();
                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand("SELECT * FROM SonFirmaHareketler", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                grd_firmahareketler.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_firmahareketler);
                ModernDataGridViewHelper.EnableHoverEffect(grd_firmahareketler);
                
                // Boş durum kontrolü
                if (dt.Rows.Count == 0)
                {
                    grd_firmahareketler.DataSource = null;
                    grd_firmahareketler.Rows.Clear();
                    grd_firmahareketler.Columns.Clear();
                    grd_firmahareketler.Columns.Add("Mesaj", "Mesaj");
                    grd_firmahareketler.Rows.Add("Son hareket bulunamadı");
                    grd_firmahareketler.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Son hareketler yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void fihrist()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FirmaAd", typeof(string));
                dt.Columns.Add("FirmaTelefon1", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    using (var cmd = new SqliteCommand(
                        "SELECT DISTINCT FirmaAd,FirmaTelefon1 FROM TBL_FIRMALAR",
                        connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader.IsDBNull(0) ? "" : reader.GetString(0),
                                    reader.IsDBNull(1) ? "" : reader.GetString(1)
                                );
                            }
                        }
                    }
                }
                grd_fihrist.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_fihrist);
                ModernDataGridViewHelper.EnableHoverEffect(grd_fihrist);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İletişim rehberi yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void haberler()
        {
            try
            {
                lstbx_haberler.Items.Clear();
                XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa/");
                while (xmloku.Read())
                {
                    if (xmloku.Name == "title")
                    {
                        string title = xmloku.ReadString();
                        if (!string.IsNullOrEmpty(title))
                        {
                            lstbx_haberler.Items.Add(title);
                        }
                    }
                }
                xmloku.Close();
                
                // Boş durum kontrolü
                if (lstbx_haberler.Items.Count == 0)
                {
                    lstbx_haberler.Items.Add("Haber yüklenemedi. İnternet bağlantınızı kontrol edin.");
                }
            }
            catch (Exception ex)
            {
                lstbx_haberler.Items.Clear();
                lstbx_haberler.Items.Add($"Haberler yüklenirken hata: {ex.Message}");
                lstbx_haberler.Items.Add("İnternet bağlantınızı kontrol edin.");
                // MessageBox gösterme, ListBox'a mesaj ekle
            }
        }

        /// <summary>
        /// TCMB XML'den döviz kurlarını parse edip HTML formatında gösterir
        /// </summary>
        void dovizkurlari()
        {
            try
            {
                XmlTextReader xmloku = new XmlTextReader("https://www.tcmb.gov.tr/kurlar/today.xml");
                System.Collections.Generic.List<DovizKuru> kurlar = new System.Collections.Generic.List<DovizKuru>();
                DovizKuru kur = null;
                string currentElement = "";

                while (xmloku.Read())
                {
                    if (xmloku.NodeType == XmlNodeType.Element)
                    {
                        currentElement = xmloku.Name;
                        if (currentElement == "Currency")
                        {
                            kur = new DovizKuru();
                            if (xmloku.HasAttributes)
                            {
                                kur.Kod = xmloku.GetAttribute("CurrencyCode") ?? "";
                            }
                        }
                    }
                    else if (xmloku.NodeType == XmlNodeType.Text && kur != null)
                    {
                        string value = xmloku.Value.Trim();
                        switch (currentElement)
                        {
                            case "Unit":
                                if (int.TryParse(value, out int unit))
                                    kur.Birim = unit;
                                break;
                            case "Isim":
                                kur.Isim = value;
                                break;
                            case "ForexBuying":
                                if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal alis))
                                    kur.Alis = alis;
                                break;
                            case "ForexSelling":
                                if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal satis))
                                    kur.Satis = satis;
                                break;
                            case "BanknoteBuying":
                                if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal efektifAlis))
                                    kur.EfektifAlis = efektifAlis;
                                break;
                            case "BanknoteSelling":
                                if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal efektifSatis))
                                    kur.EfektifSatis = efektifSatis;
                                break;
                        }
                    }
                    else if (xmloku.NodeType == XmlNodeType.EndElement && xmloku.Name == "Currency" && kur != null)
                    {
                        if (!string.IsNullOrEmpty(kur.Kod) && kur.Kod != "XDR")
                        {
                            kurlar.Add(kur);
                        }
                        kur = null;
                    }
                }
                xmloku.Close();

                // HTML oluştur
                string html = GenerateDovizHtml(kurlar);
                wb_doviz.DocumentText = html;
            }
            catch (Exception ex)
            {
                string errorHtml = $@"
                    <html>
                    <head><meta charset='utf-8'></head>
                    <body style='font-family: Tahoma; padding: 20px; background-color: #f5f5f5;'>
                        <h3 style='color: #d32f2f;'>Döviz Kurları Yüklenemedi</h3>
                        <p>Hata: {ex.Message}</p>
                        <p>İnternet bağlantınızı kontrol edin.</p>
                    </body>
                    </html>";
                wb_doviz.DocumentText = errorHtml;
            }
        }

        /// <summary>
        /// Döviz kurları listesinden HTML tablosu oluşturur
        /// </summary>
        string GenerateDovizHtml(System.Collections.Generic.List<DovizKuru> kurlar)
        {
            System.Text.StringBuilder html = new System.Text.StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='utf-8'>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: Tahoma; font-size: 8.25pt; padding: 10px; background-color: #f5f5f5; }");
            html.AppendLine("table { width: 100%; border-collapse: collapse; background-color: white; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }");
            html.AppendLine("th { background-color: #2196F3; color: white; padding: 8px; text-align: left; font-weight: bold; }");
            html.AppendLine("td { padding: 6px; border-bottom: 1px solid #e0e0e0; }");
            html.AppendLine("tr:hover { background-color: #f0f0f0; }");
            html.AppendLine("h3 { color: #1976D2; margin-top: 0; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<h3>📊 TCMB Döviz Kurları</h3>");
            html.AppendLine("<table>");
            html.AppendLine("<tr>");
            html.AppendLine("<th>Kod</th>");
            html.AppendLine("<th>Birim</th>");
            html.AppendLine("<th>Döviz</th>");
            html.AppendLine("<th>Alış</th>");
            html.AppendLine("<th>Satış</th>");
            html.AppendLine("<th>Efektif Alış</th>");
            html.AppendLine("<th>Efektif Satış</th>");
            html.AppendLine("</tr>");

            foreach (var kur in kurlar)
            {
                html.AppendLine("<tr>");
                html.AppendLine($"<td><strong>{kur.Kod}</strong></td>");
                html.AppendLine($"<td>{kur.Birim}</td>");
                html.AppendLine($"<td>{kur.Isim}</td>");
                html.AppendLine($"<td>{kur.Alis:F4}</td>");
                html.AppendLine($"<td>{kur.Satis:F4}</td>");
                html.AppendLine($"<td>{kur.EfektifAlis:F4}</td>");
                html.AppendLine($"<td>{kur.EfektifSatis:F4}</td>");
                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            return html.ToString();
        }

        /// <summary>
        /// Döviz kuru bilgilerini tutan sınıf
        /// </summary>
        class DovizKuru
        {
            public string Kod { get; set; } = "";
            public int Birim { get; set; } = 1;
            public string Isim { get; set; } = "";
            public decimal Alis { get; set; } = 0;
            public decimal Satis { get; set; } = 0;
            public decimal EfektifAlis { get; set; } = 0;
            public decimal EfektifSatis { get; set; } = 0;
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            azalanstoklar();
            ajanda();
            sonhareketler();
            fihrist();
            dovizkurlari();
            haberler();
        }
    }
}

