using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace operion.Application.Services
{
    /// <summary>
    /// AI sağlayıcı yanıtlarını ayrıştırır ve yapısal hale getirir
    /// </summary>
    public class AiResponseParser
    {
        /// <summary>
        /// Rapor özeti yanıtını parse eder
        /// </summary>
        public ParsedReportSummary ParseSummaryAndActions(string aiResponse)
        {
            var result = new ParsedReportSummary();
            
            try
            {
                // Özet ve Aksiyon kısımlarını ayır
                var sections = SplitIntoSections(aiResponse);
                
                // Özet maddeleri
                if (sections.ContainsKey("özet") || sections.ContainsKey("summary"))
                {
                    var summarySection = sections.ContainsKey("özet") ? sections["özet"] : sections["summary"];
                    result.SummaryPoints = ExtractBulletPoints(summarySection);
                }
                else
                {
                    // Başlık yoksa ilk kısmı özet kabul et
                    var firstPart = aiResponse.Split(new[] { "aksiyon", "action" }, StringSplitOptions.None)[0];
                    result.SummaryPoints = ExtractBulletPoints(firstPart);
                }
                
                // Aksiyon maddeleri
                if (sections.ContainsKey("aksiyon") || sections.ContainsKey("action"))
                {
                    var actionSection = sections.ContainsKey("aksiyon") ? sections["aksiyon"] : sections["action"];
                    result.ActionItems = ExtractBulletPoints(actionSection);
                }
                else
                {
                    // Aksiyon başlığı yoksa ikinci kısmı aksiyon kabul et
                    var parts = aiResponse.Split(new[] { "aksiyon", "action" }, StringSplitOptions.None);
                    if (parts.Length > 1)
                    {
                        result.ActionItems = ExtractBulletPoints(parts[1]);
                    }
                }
                
                // Ham metin
                result.RawText = aiResponse;
                result.ParseSuccess = result.SummaryPoints.Count > 0;
            }
            catch (Exception ex)
            {
                result.ParseSuccess = false;
                result.ErrorMessage = $"Parse hatası: {ex.Message}";
                result.RawText = aiResponse;
            }
            
            return result;
        }

        /// <summary>
        /// E-posta yanıtını parse eder (konu + gövde)
        /// </summary>
        public ParsedEmailTemplate ParseEmailParts(string aiResponse)
        {
            var result = new ParsedEmailTemplate();
            
            try
            {
                // Konu satırlarını bul
                result.SubjectLines = ExtractSubjectLines(aiResponse);
                
                // E-posta gövdesini bul
                result.EmailBody = ExtractEmailBody(aiResponse);
                
                // Ham metin
                result.RawText = aiResponse;
                result.ParseSuccess = result.SubjectLines.Count > 0 && !string.IsNullOrEmpty(result.EmailBody);
            }
            catch (Exception ex)
            {
                result.ParseSuccess = false;
                result.ErrorMessage = $"Parse hatası: {ex.Message}";
                result.RawText = aiResponse;
            }
            
            return result;
        }

        /// <summary>
        /// Metni bölümlere ayırır (başlıklara göre)
        /// </summary>
        private Dictionary<string, string> SplitIntoSections(string text)
        {
            var sections = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            
            // Başlık pattern'leri: ##, **, veya büyük harf başlık
            // \s yerine space kullanarak newlines'ın başlığa dahil olmasını engelliyoruz
            var headerPattern = @"(?:##\s*|###\s*|\*\*)?([A-ZÇĞİÖŞÜ][A-ZÇĞİÖŞÜ ]+):?(?:\*\*)?";
            var matches = Regex.Matches(text, headerPattern);
            
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var title = match.Groups[1].Value.Trim().ToLower(new System.Globalization.CultureInfo("tr-TR"));
                
                var startIndex = match.Index + match.Length;
                var endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : text.Length;
                var content = text.Substring(startIndex, endIndex - startIndex).Trim();
                
                sections[title] = content;
            }
            
            return sections;
        }

        /// <summary>
        /// Madde işaretli listeyi çıkarır
        /// </summary>
        private List<string> ExtractBulletPoints(string text)
        {
            var points = new List<string>();
            
            // Madde işareti pattern'leri: -, *, •, 1., 2. veya 1), 2) vb.
            var bulletPattern = @"^\s*(?:[-*•]|[0-9]+[.)])\s+(.+)$";
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                var match = Regex.Match(trimmedLine, bulletPattern);
                
                if (match.Success)
                {
                    var point = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrEmpty(point) && point.Length > 5) // Çok kısa maddeleri atla
                    {
                        points.Add(point);
                    }
                }
            }
            
            // Madde işareti yoksa paragrafları al (boş satırlarla ayrılmış)
            if (points.Count == 0)
            {
                var paragraphs = text.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                points.AddRange(paragraphs.Select(p => p.Trim()).Where(p => p.Length > 10));
            }
            
            return points;
        }

        /// <summary>
        /// E-posta konu satırlarını çıkarır
        /// </summary>
        private List<string> ExtractSubjectLines(string text)
        {
            var subjects = new List<string>();
            
            // "Konu" veya "Subject" başlığını bul
            var subjectSection = "";
            var subjectMatch = Regex.Match(text, @"(?:konu|subject)[^\n]*:\s*\n(.*?)(?=\n\n|###|e-posta|email|$)", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
            
            if (subjectMatch.Success)
            {
                subjectSection = subjectMatch.Groups[1].Value;
            }
            
            // Numaralı listeyi çıkar (1., 2., 3.)
            var subjectPattern = @"^\s*\d+\.\s*(.+)$";
            var lines = subjectSection.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var match = Regex.Match(line.Trim(), subjectPattern);
                if (match.Success)
                {
                    var subject = match.Groups[1].Value.Trim();
                    // "[" karakterlerini temizle
                    subject = subject.Trim('[', ']');
                    if (!string.IsNullOrEmpty(subject))
                    {
                        subjects.Add(subject);
                    }
                }
            }
            
            return subjects;
        }

        /// <summary>
        /// E-posta gövdesini çıkarır
        /// </summary>
        private string ExtractEmailBody(string text)
        {
            // "E-posta Gövdesi" veya "Email Body" başlığını bul
            var bodyMatch = Regex.Match(text, @"(?:e-posta\s+gövdesi|email\s+body)[^\n]*:\s*\n(.*?)(?=##|$)", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
            
            if (bodyMatch.Success)
            {
                var body = bodyMatch.Groups[1].Value.Trim();
                // "[" karakterlerini temizle
                body = Regex.Replace(body, @"\[([^\]]+)\]", "$1");
                return body;
            }
            
            // Başlık yoksa "Konu" sonrası tüm metni al
            var afterSubject = Regex.Split(text, @"(?:konu|subject)[^\n]*:", RegexOptions.IgnoreCase);
            if (afterSubject.Length > 1)
            {
                // İlk 3-5 satırı (konu satırları) atla, geri kalanı gövde
                var remaining = afterSubject[1];
                var lines = remaining.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 5)
                {
                    return string.Join("\n", lines.Skip(5)).Trim();
                }
            }
            
            return string.IsNullOrEmpty(text) ? null : text; // Son çare olarak tüm metni döndür
        }
    }

    #region Parse Sonuç Modelleri

    /// <summary>
    /// Parse edilmiş rapor özeti
    /// </summary>
    public class ParsedReportSummary
    {
        public List<string> SummaryPoints { get; set; } = new List<string>();
        public List<string> ActionItems { get; set; } = new List<string>();
        public string? RawText { get; set; }
        public bool ParseSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// Parse edilmiş e-posta şablonu
    /// </summary>
    public class ParsedEmailTemplate
    {
        public List<string> SubjectLines { get; set; } = new List<string>();
        public string? EmailBody { get; set; }
        public string? RawText { get; set; }
        public bool ParseSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }

    #endregion
}

