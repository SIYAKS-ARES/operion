using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace operion.Application.Services
{
    /// <summary>
    /// AI için senaryo bazlı prompt şablonları oluşturur
    /// </summary>
    public class PromptBuilder
    {
        /// <summary>
        /// Rapor özeti için prompt oluşturur
        /// </summary>
        public string BuildReportSummaryPrompt(ReportSummaryContext context)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<system_role>");
            sb.AppendLine("Sen uzman bir iş analistisin. Verilen ticari verileri analiz edip öngörüler sunarsın.");
            sb.AppendLine("</system_role>");
            sb.AppendLine();
            
            sb.AppendLine("<task_context>");
            sb.AppendLine($"    <report_type>{context.ReportType}</report_type>");
            sb.AppendLine($"    <date_range>{context.StartDate:dd.MM.yyyy} - {context.EndDate:dd.MM.yyyy}</date_range>");
            if (!string.IsNullOrEmpty(context.FilterInfo))
            {
                sb.AppendLine($"    <filters>{context.FilterInfo}</filters>");
            }
            sb.AppendLine("</task_context>");
            sb.AppendLine();
            
            sb.AppendLine("<input_data>");
            sb.AppendLine("<![CDATA[");
            sb.AppendLine(context.Data);
            sb.AppendLine("]]>");
            sb.AppendLine("</input_data>");
            sb.AppendLine();
            
            sb.AppendLine("<output_format>");
            sb.AppendLine("Yanıtını aşağıdaki formatta ver (Parsing için bu yapı zorunludur):");
            sb.AppendLine("1. **ÖZET (2-5 madde)**: Ana bulguları sayısal metriklerle özetle");
            sb.AppendLine("2. **AKSİYON MADDELERİ (3-7 madde)**: Yapılabilecek somut aksiyonlar");
            sb.AppendLine("</output_format>");
            sb.AppendLine();
            
            sb.AppendLine("<instructions>");
            sb.AppendLine("<instruction>Türkçe yaz, profesyonel dil kullan</instruction>");
            sb.AppendLine("<instruction>Sayısal değerleri koru (ciro, adet, oran vb.)</instruction>");
            sb.AppendLine("<instruction>Kısa ve öz ol, gereksiz detay verme</instruction>");
            sb.AppendLine("<instruction>Maddeleri işaretli liste olarak sun</instruction>");
            sb.AppendLine("</instructions>");
            
            return sb.ToString();
        }

        /// <summary>
        /// Rapor özeti için prompt oluşturur (detaylı format seçenekleriyle)
        /// </summary>
        public string BuildReportSummaryPrompt(string data, ReportFormatOptions options)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<system_role>");
            sb.AppendLine("Sen yardımcı bir asistansın. Verilen metni özetlersin.");
            sb.AppendLine("</system_role>");
            sb.AppendLine();
            
            sb.AppendLine("<input_data>");
            sb.AppendLine("<![CDATA[");
            sb.AppendLine(data);
            sb.AppendLine("]]>");
            sb.AppendLine("</input_data>");
            sb.AppendLine();
            
            sb.AppendLine("<instructions>");
            if (options.IncludeMetrics)
            {
                sb.AppendLine("<instruction>Önemli metrikleri vurgula (ciro, kar, adet, oran)</instruction>");
            }
            if (options.IncludeActions)
            {
                sb.AppendLine("<instruction>Aksiyon önerileri ekle</instruction>");
            }
            sb.AppendLine($"<instruction>Maksimum {options.MaxBulletPoints} madde kullan</instruction>");
            sb.AppendLine("<instruction>Türkçe, iş diline uygun yaz</instruction>");
            sb.AppendLine("</instructions>");
            
            return sb.ToString();
        }

        /// <summary>
        /// E-posta şablonu için prompt oluşturur
        /// </summary>
        public string BuildEmailTemplatePrompt(EmailTemplateContext context)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<system_role>");
            sb.AppendLine("Sen profesyonel bir iletişim uzmanısın. Müşteriler için etkili e-posta taslakları hazırlarsın.");
            sb.AppendLine("</system_role>");
            sb.AppendLine();
            
            sb.AppendLine("<task_context>");
            sb.AppendLine($"    <scenario>{GetScenarioDescription(context.Scenario)}</scenario>");
            sb.AppendLine($"    <customer>{context.CustomerReference}</customer>");
            
            if (!string.IsNullOrEmpty(context.ProductsInfo))
            {
                sb.AppendLine($"    <products>{context.ProductsInfo}</products>");
            }
            if (context.Amount > 0)
            {
                sb.AppendLine($"    <amount>{context.Amount.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("tr-TR"))} TL</amount>");
            }
            if (context.Discount > 0)
            {
                sb.AppendLine($"    <discount>%{context.Discount}</discount>");
            }
            if (!string.IsNullOrEmpty(context.DeliveryTerms))
            {
                sb.AppendLine($"    <delivery>{context.DeliveryTerms}</delivery>");
            }
            if (!string.IsNullOrEmpty(context.AdditionalInfo))
            {
                sb.AppendLine($"    <additional_info>{context.AdditionalInfo}</additional_info>");
            }
            sb.AppendLine("</task_context>");
            sb.AppendLine();
            
            sb.AppendLine("<output_format>");
            sb.AppendLine("### Konu Satırları (3 alternatif):");
            sb.AppendLine("1. [Konu 1]");
            sb.AppendLine("2. [Konu 2]");
            sb.AppendLine("3. [Konu 3]");
            sb.AppendLine();
            sb.AppendLine("### E-posta Gövdesi:");
            sb.AppendLine("[E-posta içeriği]");
            sb.AppendLine("</output_format>");
            sb.AppendLine();
            
            sb.AppendLine("<instructions>");
            sb.AppendLine($"<instruction>Ton: {GetToneDescription(context.Tone)}</instruction>");
            sb.AppendLine($"<instruction>Uzunluk: {GetLengthDescription(context.Length)}</instruction>");
            sb.AppendLine("<instruction>Türkçe, iş yazışmasına uygun</instruction>");
            sb.AppendLine("<instruction>Profesyonel ancak samimi</instruction>");
            sb.AppendLine("<instruction>Sayısal değerleri [TUTAR], [ISKONTO] gibi placeholder olarak bırak</instruction>");
            sb.AppendLine("</instructions>");
            
            return sb.ToString();
        }

        /// <summary>
        /// E-posta yanıt şablonu için basit prompt
        /// </summary>
        public string BuildEmailReplyPrompt(string originalEmail, EmailTone tone)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<system_role>");
            sb.AppendLine("Sen profesyonel bir asistansın. E-postalar için yanıt taslakları oluşturursun.");
            sb.AppendLine("</system_role>");
            sb.AppendLine();
            
            sb.AppendLine("<input_email>");
            sb.AppendLine("<![CDATA[");
            sb.AppendLine(originalEmail);
            sb.AppendLine("]]>");
            sb.AppendLine("</input_email>");
            sb.AppendLine();
            
            sb.AppendLine("<instructions>");
            sb.AppendLine($"<instruction>Ton: {GetToneDescription(tone)}</instruction>");
            sb.AppendLine("<instruction>Kısa ve öz, profesyonel yanıt ver</instruction>");
            sb.AppendLine("<instruction>Türkçe yaz</instruction>");
            sb.AppendLine("</instructions>");
            
            return sb.ToString();
        }

        private string GetScenarioDescription(EmailScenario scenario)
        {
            return scenario switch
            {
                EmailScenario.Teklif => "Müşteriye Teklif Sunumu",
                EmailScenario.Tesekkur => "Teşekkür ve Takip",
                EmailScenario.OdemeHatirlatma => "Ödeme Hatırlatma",
                EmailScenario.TeslimatBilgi => "Teslimat Bilgilendirme",
                EmailScenario.GenelYanit => "Genel Yanıt",
                _ => "Genel İş E-postası"
            };
        }

        private string GetToneDescription(EmailTone tone)
        {
            return tone switch
            {
                EmailTone.Resmi => "Resmi ve kurumsal",
                EmailTone.Notr => "Nötr ve profesyonel",
                EmailTone.Samimi => "Samimi ve yakın",
                EmailTone.Acil => "Acil ve net",
                _ => "Profesyonel"
            };
        }

        private string GetLengthDescription(EmailLength length)
        {
            return length switch
            {
                EmailLength.Kisa => "Kısa (2-3 paragraf)",
                EmailLength.Orta => "Orta (3-5 paragraf)",
                EmailLength.Uzun => "Uzun (5-7 paragraf)",
                _ => "Orta uzunlukta"
            };
        }

        /// <summary>
        /// RAG (Retrieval-Augmented Generation) için prompt oluşturur
        /// </summary>
        public string BuildRagPrompt(string userQuery, List<string> contextDocs)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<system_role>");
            sb.AppendLine("Sen Operion Kurumsal Asistanısın. Şirket verilerine ve dökümanlarına erişimin var.");
            sb.AppendLine("Sadece sana verilen bağlamı (context) kullanarak soruları cevapla. Bilmediğin konularda uydurma.");
            sb.AppendLine("</system_role>");
            sb.AppendLine();
            
            if (contextDocs != null && contextDocs.Any())
            {
                sb.AppendLine("<context>");
                // CDATA koruması
                sb.AppendLine("<![CDATA[");
                foreach (var doc in contextDocs)
                {
                    sb.AppendLine($"--- Document ---");
                    sb.AppendLine(doc);
                    sb.AppendLine();
                }
                sb.AppendLine("]]>");
                sb.AppendLine("</context>");
                sb.AppendLine();
            }
            
            sb.AppendLine("<user_query>");
            sb.AppendLine(userQuery);
            sb.AppendLine("</user_query>");
            sb.AppendLine();
            
            sb.AppendLine("<instructions>");
            sb.AppendLine("<instruction>Verilen bağlamdaki bilgileri kullanarak net ve doğru yanıt ver.</instruction>");
            sb.AppendLine("<instruction>Eğer bağlamda yanıt yoksa 'Bu konuda bilgim yok' de.</instruction>");
            sb.AppendLine("<instruction>Türkçe ve profesyonel bir dil kullan.</instruction>");
            sb.AppendLine("</instructions>");
            
            return sb.ToString();
        }
    }

    #region Context ve Enum Modelleri

    /// <summary>
    /// Rapor özeti için bağlam bilgisi
    /// </summary>
    public class ReportSummaryContext
    {
        public string? ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? FilterInfo { get; set; }
        public string? Data { get; set; }
    }

    /// <summary>
    /// Rapor format seçenekleri
    /// </summary>
    public class ReportFormatOptions
    {
        public bool IncludeMetrics { get; set; } = true;
        public bool IncludeActions { get; set; } = true;
        public int MaxBulletPoints { get; set; } = 5;
    }

    /// <summary>
    /// E-posta şablonu için bağlam bilgisi
    /// </summary>
    public class EmailTemplateContext
    {
        public EmailScenario Scenario { get; set; }
        public string? CustomerReference { get; set; }  // Maskelenmiş müşteri referansı
        public string? ProductsInfo { get; set; }        // Ürün kategorileri/kodları
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string? DeliveryTerms { get; set; }
        public string? AdditionalInfo { get; set; }
        public EmailTone Tone { get; set; }
        public EmailLength Length { get; set; }
    }

    public enum EmailScenario
    {
        Teklif,
        Tesekkur,
        OdemeHatirlatma,
        TeslimatBilgi,
        GenelYanit
    }

    public enum EmailTone
    {
        Resmi,
        Notr,
        Samimi,
        Acil
    }

    public enum EmailLength
    {
        Kisa,
        Orta,
        Uzun
    }

    #endregion
}

