using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace operion.Application.Services
{
    /// <summary>
    /// Kişisel veri (PII) maskeleme servisi
    /// AI'a gönderilecek verilerden hassas bilgileri temizler
    /// </summary>
    public class PiiMaskingService
    {
        private readonly Dictionary<string, string> _maskingCache = new Dictionary<string, string>();
        private int _maskingCounter = 0;

        /// <summary>
        /// Metinden PII verilerini maskeler
        /// </summary>
        public string MaskText(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            string masked = text;

            // E-posta maskeleme
            masked = MaskEmails(masked);

            // Telefon numarası maskeleme (Türkiye formatları)
            masked = MaskPhoneNumbers(masked);

            // TC kimlik/vergi no maskeleme
            masked = MaskIdentificationNumbers(masked);

            // IBAN maskeleme
            masked = MaskIban(masked);

            // Kişi adı maskeleme (yaygın Türkçe isim kalıpları)
            masked = MaskPersonNames(masked);

            return masked;
        }

        /// <summary>
        /// Müşteri/firma bilgilerini kategorik referansa çevirir
        /// Örn: "Ahmet Yılmaz" -> "MUSTERI_001"
        /// </summary>
        public string MaskCustomerReference(string? customerName)
        {
            if (string.IsNullOrEmpty(customerName)) return "[MÜŞTERİ]";

            // Cache kontrolü
            if (_maskingCache.ContainsKey(customerName))
            {
                return _maskingCache[customerName];
            }

            var masked = $"MUSTERI_{++_maskingCounter:D3}";
            _maskingCache[customerName] = masked;
            return masked;
        }

        /// <summary>
        /// Firma referansı maskeleme
        /// </summary>
        public string MaskCompanyReference(string? companyName)
        {
            if (string.IsNullOrEmpty(companyName)) return "[FİRMA]";

            if (_maskingCache.ContainsKey(companyName))
            {
                return _maskingCache[companyName];
            }

            var masked = $"FIRMA_{++_maskingCounter:D3}";
            _maskingCache[companyName] = masked;
            return masked;
        }

        /// <summary>
        /// Ürün adını kategori+kod'a çevirir
        /// Örn: "Dell Inspiron 15" -> "URUN_Laptop_001"
        /// </summary>
        public string MaskProductInfo(string? productName, string category = "")
        {
            if (string.IsNullOrEmpty(productName)) return "[ÜRÜN]";

            var categoryPart = string.IsNullOrEmpty(category) ? "Urun" : category;
            return $"URUN_{categoryPart}_{++_maskingCounter:D3}";
        }

        /// <summary>
        /// E-posta adreslerini maskeler
        /// </summary>
        private string MaskEmails(string text)
        {
            return Regex.Replace(text,
                @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b",
                "[EMAIL]",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Telefon numaralarını maskeler
        /// </summary>
        private string MaskPhoneNumbers(string text)
        {
            // Türkiye telefon formatları
            // +90 xxx xxx xx xx, 0xxx xxx xx xx, (0xxx) xxx xx xx vb.
            text = Regex.Replace(text,
                @"\+?90[\s-]?(\(?\d{3}\)?[\s-]?)?\d{3}[\s-]?\d{2}[\s-]?\d{2}",
                "[TELEFON]",
                RegexOptions.IgnoreCase);

            // 0xxx formatı
            text = Regex.Replace(text,
                @"\b0\d{3}[\s-]?\d{3}[\s-]?\d{2}[\s-]?\d{2}\b",
                "[TELEFON]",
                RegexOptions.IgnoreCase);

            return text;
        }

        /// <summary>
        /// TC kimlik ve vergi numaralarını maskeler
        /// </summary>
        private string MaskIdentificationNumbers(string text)
        {
            // 11 haneli sayılar (TC kimlik/vergi no)
            return Regex.Replace(text,
                @"\b\d{10,11}\b",
                "[KIMLIK_NO]",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// IBAN numaralarını maskeler
        /// </summary>
        private string MaskIban(string text)
        {
            // TR ile başlayan IBAN
            text = Regex.Replace(text,
                @"\bTR\d{2}\s?\d{4}\s?\d{4}\s?\d{4}\s?\d{4}\s?\d{4}\s?\d{2}\b",
                "[IBAN]",
                RegexOptions.IgnoreCase);

            return text;
        }

        /// <summary>
        /// Kişi adlarını maskeler (basit yöntem - geliştirilmeli)
        /// </summary>
        private string MaskPersonNames(string text)
        {
            // Türkçe büyük harfle başlayan 2-3 kelime (isim kalıbı)
            // Bu basit bir yaklaşımdır, false positive olabilir
            // Daha gelişmiş NER (Named Entity Recognition) kullanılabilir
            
            // Örnek: "Ahmet Yılmaz" veya "Ali Veli Mehmet"
            text = Regex.Replace(text,
                @"\b([A-ZÇĞİÖŞÜ][a-zçğıöşü]+)\s+([A-ZÇĞİÖŞÜ][a-zçğıöşü]+)(?:\s+([A-ZÇĞİÖŞÜ][a-zçğıöşü]+))?\b",
                match =>
                {
                    // Bazı yaygın firma/yer isimleri için istisna
                    var fullMatch = match.Value;
                    if (IsBusinessTerm(fullMatch))
                    {
                        return fullMatch;
                    }
                    return "[KİŞİ_ADI]";
                },
                RegexOptions.None);

            return text;
        }

        /// <summary>
        /// İş terimleri kontrolü (isim olarak maskelenmemesi için)
        /// </summary>
        private bool IsBusinessTerm(string text)
        {
            var businessTerms = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Limited Şirket", "Anonim Şirket", "Kamu Kurumu",
                "Sanayi Ticaret", "Pazarlama Satış", "İnsan Kaynakları",
                "Bilgi Teknolojileri", "Müşteri Hizmetleri"
            };

            return businessTerms.Contains(text);
        }

        /// <summary>
        /// Rapor verilerini özetlerken PII-safe format oluşturur
        /// </summary>
        public string PrepareReportDataForAi(string? reportData, bool includeCustomerNames = false)
        {
            if (string.IsNullOrEmpty(reportData)) return reportData ?? "";

            var masked = reportData;

            // Tüm PII'ları maskele
            masked = MaskText(masked);

            // Müşteri adlarını kategori referanslarına çevir
            if (!includeCustomerNames)
            {
                // Bu kısım rapor formatına göre özelleştirilebilir
                // Örneğin: CSV satırlarındaki isim sütunlarını maskele
            }

            return masked;
        }

        /// <summary>
        /// E-posta içeriği için PII-safe format
        /// </summary>
        public string PrepareEmailContextForAi(string? emailContent)
        {
            if (string.IsNullOrEmpty(emailContent)) return emailContent ?? "";

            var masked = emailContent;

            // Temel PII maskeleme
            masked = MaskText(masked);

            // İmza bloklarını kaldır
            masked = RemoveEmailSignature(masked);

            return masked;
        }

        /// <summary>
        /// E-posta imzasını kaldırır
        /// </summary>
        private string RemoveEmailSignature(string emailContent)
        {
            // Yaygın imza kalıpları
            var signaturePatterns = new[]
            {
                @"--[\r\n].*",
                @"Saygılarımla,?[\r\n].*",
                @"İyi günler,?[\r\n].*",
                @"Best regards,?[\r\n].*"
            };

            foreach (var pattern in signaturePatterns)
            {
                emailContent = Regex.Replace(emailContent, pattern, "[İMZA]", 
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }

            return emailContent;
        }

        /// <summary>
        /// Maskeleme cache'ini temizler
        /// </summary>
        public void ClearCache()
        {
            _maskingCache.Clear();
            _maskingCounter = 0;
        }

        /// <summary>
        /// Maskelenmiş referans için orijinal değeri döndürür (varsa)
        /// </summary>
        public string GetOriginalValue(string? maskedValue)
        {
            if (string.IsNullOrEmpty(maskedValue)) return maskedValue ?? "";
            
            foreach (var kvp in _maskingCache)
            {
                if (kvp.Value == maskedValue)
                {
                    return kvp.Key;
                }
            }
            return maskedValue;
        }
    }
}

