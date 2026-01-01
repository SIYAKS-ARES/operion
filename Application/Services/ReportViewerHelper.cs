using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;
using operion.Application.Services;

namespace operion.Application.Services
{
    /// <summary>
    /// ReportViewer ARM uyumluluk sorunu için alternatif rapor çözümü
    /// Microsoft.Data.Sqlite kullanarak HTML rapor oluşturur
    /// .NET 10 için güncellenmiştir
    /// </summary>
    public static class ReportViewerHelper
    {
        /// <summary>
        /// ReportViewer yerine HTML rapor oluştur
        /// </summary>
        public static string GenerateHtmlReport(string reportType, string title)
        {
            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    string sql = GetReportSql(reportType);
                    using (var command = new SqliteCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        return GenerateHtmlFromDataReader(reader, title);
                    }
                }
            }
            catch (Exception ex)
            {
                return $"<html><body><h1>Rapor Hatası</h1><p>{System.Web.HttpUtility.HtmlEncode(ex.Message)}</p></body></html>";
            }
        }

        private static string GetReportSql(string reportType)
        {
            return reportType.ToLowerInvariant() switch
            {
                "firmalar" => "SELECT * FROM TBL_FIRMALAR ORDER BY FirmaAd",
                "musteriler" => "SELECT * FROM TBL_MUSTERILER ORDER BY MusteriAd",
                "giderler" => "SELECT * FROM TBL_GIDERLER ORDER BY GiderID",
                "personeller" => "SELECT * FROM TBL_PERSONELLER ORDER BY PersonelAd",
                _ => "SELECT 'Bilinmeyen rapor tipi' as Hata"
            };
        }

        private static string GenerateHtmlFromDataReader(SqliteDataReader reader, string title)
        {
            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='utf-8'>");
            html.AppendLine($"<title>{HtmlEncode(title)}</title>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
            html.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 20px; }");
            html.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
            html.AppendLine("th { background-color: #f2f2f2; font-weight: bold; }");
            html.AppendLine("tr:nth-child(even) { background-color: #f9f9f9; }");
            html.AppendLine("h1 { color: #333; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine($"<h1>{HtmlEncode(title)}</h1>");
            html.AppendLine($"<p>Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}</p>");
            html.AppendLine("<table>");

            // Header
            html.AppendLine("<tr>");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                html.AppendLine($"<th>{HtmlEncode(reader.GetName(i))}</th>");
            }
            html.AppendLine("</tr>");

            // Data
            while (reader.Read())
            {
                html.AppendLine("<tr>");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string value = reader.IsDBNull(i) ? "" : reader.GetValue(i)?.ToString() ?? "";
                    html.AppendLine($"<td>{HtmlEncode(value)}</td>");
                }
                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }

        /// <summary>
        /// HTML encoding için basit helper (System.Web.HttpUtility yok, kendi implementasyonumuz)
        /// </summary>
        private static string HtmlEncode(string? text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            
            return text
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;");
        }

        /// <summary>
        /// HTML raporu dosyaya kaydet
        /// </summary>
        public static void SaveHtmlReport(string htmlContent, string fileName)
        {
            try
            {
                // AppContext.BaseDirectory kullan (.NET 10 önerisi)
                string reportsDir = Path.Combine(AppContext.BaseDirectory, "Reports");
                if (!Directory.Exists(reportsDir))
                    Directory.CreateDirectory(reportsDir);

                string filePath = Path.Combine(reportsDir, fileName);
                File.WriteAllText(filePath, htmlContent, Encoding.UTF8);
                
                MessageBox.Show($"Rapor kaydedildi: {filePath}", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rapor kaydetme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// HTML raporu varsayılan tarayıcıda aç
        /// </summary>
        public static void OpenHtmlReport(string htmlContent)
        {
            try
            {
                string tempFile = Path.GetTempFileName() + ".html";
                File.WriteAllText(tempFile, htmlContent, Encoding.UTF8);
                
                // .NET 10'da Process.Start yeni API kullanır
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = tempFile,
                    UseShellExecute = true
                };
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rapor açma hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

