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
            
            sb.AppendLine("# GÖREV: Ticari Rapor Özetleme");
            sb.AppendLine();
            sb.AppendLine("Aşağıdaki rapor verilerini analiz edip Türkçe özet çıkar.");
            sb.AppendLine();
            
            sb.AppendLine("## RAPOR BİLGİLERİ:");
            sb.AppendLine($"- Rapor Türü: {context.ReportType}");
            sb.AppendLine($"- Tarih Aralığı: {context.StartDate:dd.MM.yyyy} - {context.EndDate:dd.MM.yyyy}");
            
            if (!string.IsNullOrEmpty(context.FilterInfo))
            {
                sb.AppendLine($"- Filtreler: {context.FilterInfo}");
            }
            
            sb.AppendLine();
            sb.AppendLine("## RAPOR VERİLERİ:");
            sb.AppendLine(context.Data);
            sb.AppendLine();
            
            sb.AppendLine("## ÇIKTI FORMATI:");
            sb.AppendLine("1. **Özet (2-5 madde)**: Ana bulguları sayısal metriklerle özetle");
            sb.AppendLine("2. **Aksiyon Maddeleri (3-7 madde)**: Yapılabilecek somut aksiyonlar");
            sb.AppendLine();
            
            sb.AppendLine("## KURALLAR:");
            sb.AppendLine("- Türkçe yaz, profesyonel dil kullan");
            sb.AppendLine("- Sayısal değerleri koru (ciro, adet, oran vb.)");
            sb.AppendLine("- Kısa ve öz ol, gereksiz detay verme");
            sb.AppendLine("- Maddeleri işaretli liste olarak sun");
            
            return sb.ToString();
        }

        /// <summary>
        /// Rapor özeti için prompt oluşturur (detaylı format seçenekleriyle)
        /// </summary>
        public string BuildReportSummaryPrompt(string data, ReportFormatOptions options)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# GÖREV: Rapor Özetleme");
            sb.AppendLine();
            sb.AppendLine("Aşağıdaki rapor verilerini özetle:");
            sb.AppendLine();
            sb.AppendLine(data);
            sb.AppendLine();
            
            if (options.IncludeMetrics)
            {
                sb.AppendLine("- Önemli metrikleri vurgula (ciro, kar, adet, oran)");
            }
            
            if (options.IncludeActions)
            {
                sb.AppendLine("- Aksiyon önerileri ekle");
            }
            
            sb.AppendLine($"- Maksimum {options.MaxBulletPoints} madde kullan");
            sb.AppendLine("- Türkçe, iş diline uygun yaz");
            
            return sb.ToString();
        }

        /// <summary>
        /// E-posta şablonu için prompt oluşturur
        /// </summary>
        public string BuildEmailTemplatePrompt(EmailTemplateContext context)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# GÖREV: Ticari E-posta Şablonu Üretimi");
            sb.AppendLine();
            sb.AppendLine($"## SENARYO: {GetScenarioDescription(context.Scenario)}");
            sb.AppendLine();
            
            sb.AppendLine("## BAĞLAM:");
            sb.AppendLine($"- Müşteri: {context.CustomerReference}");
            
            if (!string.IsNullOrEmpty(context.ProductsInfo))
            {
                sb.AppendLine($"- Ürünler: {context.ProductsInfo}");
            }
            
            if (context.Amount > 0)
            {
                sb.AppendLine($"- Tutar: {context.Amount.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("tr-TR"))} TL");
            }
            
            if (context.Discount > 0)
            {
                sb.AppendLine($"- İskonto: %{context.Discount}");
            }
            
            if (!string.IsNullOrEmpty(context.DeliveryTerms))
            {
                sb.AppendLine($"- Teslimat: {context.DeliveryTerms}");
            }
            
            if (!string.IsNullOrEmpty(context.AdditionalInfo))
            {
                sb.AppendLine($"- Ek Bilgi: {context.AdditionalInfo}");
            }
            
            sb.AppendLine();
            sb.AppendLine("## ÇIKTI FORMATI:");
            sb.AppendLine("### Konu Satırları (3 alternatif):");
            sb.AppendLine("1. [Konu 1]");
            sb.AppendLine("2. [Konu 2]");
            sb.AppendLine("3. [Konu 3]");
            sb.AppendLine();
            sb.AppendLine("### E-posta Gövdesi:");
            sb.AppendLine("[E-posta içeriği]");
            sb.AppendLine();
            
            sb.AppendLine("## KURALLAR:");
            sb.AppendLine($"- Ton: {GetToneDescription(context.Tone)}");
            sb.AppendLine($"- Uzunluk: {GetLengthDescription(context.Length)}");
            sb.AppendLine("- Türkçe, iş yazışmasına uygun");
            sb.AppendLine("- Profesyonel ancak samimi");
            sb.AppendLine("- Sayısal değerleri [TUTAR], [ISKONTO] gibi placeholder olarak bırak");
            
            return sb.ToString();
        }

        /// <summary>
        /// E-posta yanıt şablonu için basit prompt
        /// </summary>
        public string BuildEmailReplyPrompt(string originalEmail, EmailTone tone)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# GÖREV: E-posta Yanıt Şablonu");
            sb.AppendLine();
            sb.AppendLine("Aşağıdaki e-postaya Türkçe yanıt şablonu oluştur:");
            sb.AppendLine();
            sb.AppendLine("## GELEN E-POSTA:");
            sb.AppendLine(originalEmail);
            sb.AppendLine();
            sb.AppendLine($"Ton: {GetToneDescription(tone)}");
            sb.AppendLine("Kısa ve öz, profesyonel yanıt ver.");
            
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

