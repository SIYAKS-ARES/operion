using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using System.Xml;
using operion.Core.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using operion.Application.Services;
using operion.Presentation.Controls;
using System.Xml;
using operion.Core.Models;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

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

        private async void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
             await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            // Veritabani islemleri
            await azalanstoklarAsync();
            await ajandaAsync();
            await sonhareketlerAsync();
            await fihristAsync();
            
            // Web islemleri (Parallel ve Exception safe)
            var t1 = dovizkurlariAsync();
            var t2 = haberlerAsync();
            
            await Task.WhenAll(t1, t2);
        }

        async Task azalanstoklarAsync()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("UrunAd", typeof(string));
                dt.Columns.Add("Kalan Stok Sayıları", typeof(int));

                using (var connection = DatabaseService.GetConnection())
                {
                    if (connection.State != ConnectionState.Open) await connection.OpenAsync();

                    using (var cmd = new SqliteCommand(
                        "SELECT UrunAd,SUM(UrunAdet) AS 'Kalan Stok Sayıları' FROM TBL_URUNLER GROUP BY UrunAd HAVING SUM(UrunAdet) <=20 ORDER BY SUM(UrunAdet)",
                        connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dt.Rows.Add(
                                    await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                                    await reader.IsDBNullAsync(1) ? 0 : reader.GetInt32(1)
                                );
                            }
                        }
                    }
                }
                
                grd_azalanstoklar.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_azalanstoklar);
                ModernDataGridViewHelper.EnableHoverEffect(grd_azalanstoklar);
                
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
                 System.Diagnostics.Debug.WriteLine($"Azalan stoklar yüklenirken hata: {ex.Message}");
            }
        }

        async Task ajandaAsync()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("NotTarih", typeof(string));
                dt.Columns.Add("NotSaat", typeof(string));
                dt.Columns.Add("NotBaslik", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    if (connection.State != ConnectionState.Open) await connection.OpenAsync();
                    
                    using (var cmd = new SqliteCommand(
                        "SELECT NotTarih,NotSaat,NotBaslik FROM TBL_NOTLAR ORDER BY NotID DESC LIMIT 8",
                        connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dt.Rows.Add(
                                    await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                                    await reader.IsDBNullAsync(1) ? "" : reader.GetString(1),
                                    await reader.IsDBNullAsync(2) ? "" : reader.GetString(2)
                                );
                            }
                        }
                    }
                }
                grd_ajanda.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_ajanda);
                ModernDataGridViewHelper.EnableHoverEffect(grd_ajanda);
                
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
                 System.Diagnostics.Debug.WriteLine($"Ajanda yüklenirken hata: {ex.Message}");
            }
        }

        async Task sonhareketlerAsync()
        {
            try
            {
                DataTable dt = new DataTable();
                using (var connection = DatabaseService.GetConnection())
                {
                    if (connection.State != ConnectionState.Open) await connection.OpenAsync();
                    
                    using (var cmd = new SqliteCommand("SELECT * FROM SonFirmaHareketler", connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader); 
                        }
                    }
                }
                grd_firmahareketler.DataSource = dt;
                ModernDataGridViewHelper.ApplyModernStyle(grd_firmahareketler);
                ModernDataGridViewHelper.EnableHoverEffect(grd_firmahareketler);
                
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
                 System.Diagnostics.Debug.WriteLine($"Son hareketler yüklenirken hata: {ex.Message}");
            }
        }

        async Task fihristAsync()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FirmaAd", typeof(string));
                dt.Columns.Add("FirmaTelefon1", typeof(string));

                using (var connection = DatabaseService.GetConnection())
                {
                    if (connection.State != ConnectionState.Open) await connection.OpenAsync();
                    
                    using (var cmd = new SqliteCommand(
                        "SELECT DISTINCT FirmaAd,FirmaTelefon1 FROM TBL_FIRMALAR",
                        connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dt.Rows.Add(
                                    await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                                    await reader.IsDBNullAsync(1) ? "" : reader.GetString(1)
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
                 System.Diagnostics.Debug.WriteLine($"İletişim rehberi yüklenirken hata: {ex.Message}");
            }
        }

        async Task haberlerAsync()
        {
            await Task.Run(() => 
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate {
                        lstbx_haberler.Items.Clear();
                        lstbx_haberler.Items.Add("Haberler yükleniyor...");
                    });
                    
                    // Legacy WebRequest - System Proxy ayarlarini daha iyi kullanir
                    var xmlStr = FetchXmlString("https://www.hurriyet.com.tr/rss/anasayfa/");
                    
                    var newsItems = new List<string>();

                    using (var reader = new StringReader(xmlStr))
                    using (var xmloku = new XmlTextReader(reader))
                    {
                        while (xmloku.Read())
                        {
                            if (xmloku.Name == "title")
                            {
                                string title = xmloku.ReadString();
                                if (!string.IsNullOrEmpty(title) && !title.Contains("Hürriyet Anasayfa"))
                                {
                                    newsItems.Add(title);
                                }
                            }
                        }
                    }

                    this.Invoke((MethodInvoker)delegate {
                        lstbx_haberler.Items.Clear();
                        if (newsItems.Count > 0)
                        {
                            foreach (var item in newsItems)
                            {
                                lstbx_haberler.Items.Add(item);
                            }
                        }
                        else
                        {
                            lstbx_haberler.Items.Add("Haber listesi boş.");
                        }
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        lstbx_haberler.Items.Clear();
                        lstbx_haberler.Items.Add("Haberler yüklenirken hata oluştu.");
                        System.Diagnostics.Debug.WriteLine($"Haber hatası: {ex.Message}");
                    });
                }
            });
        }

        async Task dovizkurlariAsync()
        {
            await Task.Run(() => 
            {
                try
                {
                    string loadingHtml = "<html><body style='font-family:Segoe UI, sans-serif; padding:10px; color:#333;'><h3>Yükleniyor...</h3></body></html>";
                    this.Invoke((MethodInvoker)delegate {
                        if (wb_doviz.Document == null) { wb_doviz.DocumentText = loadingHtml; }
                    });

                    // Legacy WebRequest kullanımı
                    string xmlStr = FetchXmlString("https://www.tcmb.gov.tr/kurlar/today.xml");
                    
                    var kurlar = new List<DovizKuru>();
                    
                    using (var reader = new StringReader(xmlStr))
                    using (var xmloku = new XmlTextReader(reader))
                    {
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
                                        int.TryParse(value, out int unit);
                                        kur.Birim = unit;
                                        break;
                                    case "Isim":
                                        kur.Isim = value;
                                        break;
                                    case "ForexBuying":
                                        decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal alis);
                                        kur.Alis = alis;
                                        break;
                                    case "ForexSelling":
                                        decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal satis);
                                        kur.Satis = satis;
                                        break;
                                    case "BanknoteBuying":
                                        decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal efAlis);
                                        kur.EfektifAlis = efAlis;
                                        break;
                                    case "BanknoteSelling":
                                        decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal efSatis);
                                        kur.EfektifSatis = efSatis;
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
                    }

                    string html = GenerateDovizHtml(kurlar);
                    
                    this.Invoke((MethodInvoker)delegate {
                        wb_doviz.DocumentText = html;
                    });
                }
                catch (Exception ex)
                {
                    string errorHtml = $"<html><body style='font-family:Segoe UI, sans-serif; padding:10px; color:red;'><h3>Veri Alinamadi</h3><p>{ex.Message}</p></body></html>";
                    this.Invoke((MethodInvoker)delegate {
                        wb_doviz.DocumentText = errorHtml;
                    });
                     System.Diagnostics.Debug.WriteLine($"Döviz hatası: {ex.Message}");
                }
            });
        }

        private string FetchXmlString(string url)
        {
            // WebRequest kullanımı - HttpClient yerine System.Net stack'ini kullanır
            // Bu yöntem eski Windows Forms uygulamalarında daha stabildir ve sistem proxy/DNS ayarlarını daha agresif kullanır.
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
            request.Timeout = 30000;
            request.ReadWriteTimeout = 30000;
            // Configured in Program.cs
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13; 
            
            // Proxy - Default sistem proxy'sini kullan ama credentials da gönder
            request.Proxy = WebRequest.DefaultWebProxy;
            if (request.Proxy != null)
            {
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private string GenerateDovizHtml(List<DovizKuru> kurlar)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><style>");
            sb.Append("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 14px; margin: 0; padding: 10px; background-color: #F3F3F3; }");
            sb.Append("table { width: 100%; border-collapse: collapse; background-color: white; box-shadow: 0 1px 3px rgba(0,0,0,0.1); border-radius: 8px; overflow: hidden; }");
            sb.Append("th { background-color: #0078D4; color: white; text-align: left; padding: 12px; font-weight: 500; }");
            sb.Append("td { padding: 10px 12px; border-bottom: 1px solid #eee; color: #333; }");
            sb.Append("tr:last-child td { border-bottom: none; }");
            sb.Append("tr:hover { background-color: #f8f8f8; }");
            sb.Append(".currency-code { font-weight: bold; color: #201F1E; }");
            sb.Append(".rate { text-align: right; font-family: 'Consolas', monospace; color: #005A9E; }");
            sb.Append("</style></head><body>");
            
            sb.Append("<table>");
            sb.Append("<thead><tr><th>Döviz</th><th>Birim</th><th style='text-align:right'>Alış</th><th style='text-align:right'>Satış</th></tr></thead>");
            sb.Append("<tbody>");

            var priorityCodes = new List<string> { "USD", "EUR", "GBP", "CHF", "CAD", "KWD", "JPY", "SAR" };
            
            foreach (var code in priorityCodes)
            {
                var kur = kurlar.Find(k => k.Kod == code);
                if (kur != null)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td class='currency-code'>{kur.Kod} / TRY</td>");
                    sb.Append($"<td>{kur.Birim}</td>");
                    sb.Append($"<td class='rate'>{kur.Alis:N4}</td>");
                    sb.Append($"<td class='rate'>{kur.Satis:N4}</td>");
                    sb.Append("</tr>");
                }
            }
            
            sb.Append("</tbody></table>");
            sb.Append($"<p style='color:#666; font-size:11px; text-align:right; margin-top:5px;'>TCMB Son Güncelleme: {DateTime.Now:dd.MM.yyyy}</p>");
            sb.Append("</body></html>");

            return sb.ToString();
        }
    }
}


