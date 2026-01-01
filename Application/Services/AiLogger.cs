using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace operion.Application.Services
{
    /// <summary>
    /// AI servis çağrıları için özel logger
    /// PII maskeleme ve telemetri desteği ile
    /// .NET 10 için güncellenmiştir (AppContext.BaseDirectory kullanımı)
    /// </summary>
    public class AiLogger
    {
        private readonly string _logDirectory;
        private readonly bool _loggingEnabled;
        private static readonly object _lock = new object();

        public AiLogger()
        {
            _loggingEnabled = bool.Parse(ConfigurationManager.AppSettings["AI_LOGGING_ENABLED"] ?? "true");
            _logDirectory = ConfigurationManager.AppSettings["AI_LOG_DIRECTORY"] ?? 
                Path.Combine(AppContext.BaseDirectory, "Logs", "AI");
            
            if (_loggingEnabled && !Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        /// <summary>
        /// AI istek/yanıt bilgilerini loglar
        /// </summary>
        public void LogRequest(string requestType, int promptLength, double durationMs, bool success, string errorMessage = "")
        {
            if (!_loggingEnabled) return;

            try
            {
                lock (_lock)
                {
                    var logEntry = new StringBuilder();
                    logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] AI İstek Logu");
                    logEntry.AppendLine($"Tip: {requestType}");
                    logEntry.AppendLine($"Prompt Uzunluğu: {promptLength} karakter");
                    logEntry.AppendLine($"Süre: {durationMs:F2} ms");
                    logEntry.AppendLine($"Başarılı: {(success ? "Evet" : "Hayır")}");
                    
                    if (!success && !string.IsNullOrEmpty(errorMessage))
                    {
                        logEntry.AppendLine($"Hata: {MaskSensitiveData(errorMessage)}");
                    }
                    
                    logEntry.AppendLine(new string('-', 50));
                    
                    var logFile = GetLogFileName();
                    File.AppendAllText(logFile, logEntry.ToString());
                }
            }
            catch (Exception ex)
            {
                // Log hatası sessizce yutulur, ana akışı bozmasın
                System.Diagnostics.Debug.WriteLine($"Log yazma hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Detaylı AI yanıt bilgilerini loglar (token kullanımı ile)
        /// </summary>
        public void LogResponse(string requestType, AiResponse response, double durationMs)
        {
            if (!_loggingEnabled) return;

            try
            {
                lock (_lock)
                {
                    var logEntry = new StringBuilder();
                    logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] AI Yanıt Logu");
                    logEntry.AppendLine($"Tip: {requestType}");
                    logEntry.AppendLine($"Provider: {response.Provider}");
                    logEntry.AppendLine($"Prompt Token: {response.PromptTokens}");
                    logEntry.AppendLine($"Completion Token: {response.CompletionTokens}");
                    logEntry.AppendLine($"Toplam Token: {response.TotalTokens}");
                    logEntry.AppendLine($"Süre: {durationMs:F2} ms");
                    logEntry.AppendLine($"Yanıt Uzunluğu: {response.Content?.Length ?? 0} karakter");
                    logEntry.AppendLine(new string('-', 50));
                    
                    var logFile = GetLogFileName();
                    File.AppendAllText(logFile, logEntry.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Log yazma hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Telemetri verileri için özet log
        /// </summary>
        public void LogTelemetry(TelemetryData telemetry)
        {
            if (!_loggingEnabled) return;

            try
            {
                lock (_lock)
                {
                    var logEntry = new StringBuilder();
                    logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] AI Telemetri");
                    logEntry.AppendLine($"Özellik: {telemetry.Feature}");
                    logEntry.AppendLine($"Kullanıcı Eylemi: {telemetry.UserAction}");
                    logEntry.AppendLine($"Süre: {telemetry.DurationMs:F2} ms");
                    logEntry.AppendLine($"Başarılı: {(telemetry.Success ? "Evet" : "Hayır")}");
                    
                    if (!string.IsNullOrEmpty(telemetry.Metadata))
                    {
                        logEntry.AppendLine($"Metadata: {telemetry.Metadata}");
                    }
                    
                    logEntry.AppendLine(new string('-', 50));
                    
                    var telemetryFile = Path.Combine(_logDirectory, $"telemetry_{DateTime.Now:yyyyMMdd}.log");
                    File.AppendAllText(telemetryFile, logEntry.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Telemetri log hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Hassas verileri maskeler (PII koruması)
        /// </summary>
        private string MaskSensitiveData(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            // E-posta maskeleme
            text = Regex.Replace(text, 
                @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b", 
                "[EMAIL]", 
                RegexOptions.IgnoreCase);
            
            // Telefon maskeleme (Türkiye formatları)
            text = Regex.Replace(text,
                @"\b(\+?90[\s-]?)?(\(?\d{3}\)?[\s-]?)?\d{3}[\s-]?\d{2}[\s-]?\d{2}\b",
                "[TELEFON]",
                RegexOptions.IgnoreCase);
            
            // TC kimlik/vergi no maskeleme (11 haneli sayılar)
            text = Regex.Replace(text,
                @"\b\d{11}\b",
                "[KIMLIK_NO]");

            return text;
        }

        /// <summary>
        /// Günlük log dosya adını döndürür
        /// </summary>
        private string GetLogFileName()
        {
            return Path.Combine(_logDirectory, $"ai_log_{DateTime.Now:yyyyMMdd}.log");
        }

        /// <summary>
        /// Eski log dosyalarını temizler (retention policy)
        /// </summary>
        public void CleanOldLogs(int retentionDays = 30)
        {
            try
            {
                if (!Directory.Exists(_logDirectory)) return;

                var cutoffDate = DateTime.Now.AddDays(-retentionDays);
                var logFiles = Directory.GetFiles(_logDirectory, "*.log");

                foreach (var file in logFiles)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Eski log temizleme hatası: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Telemetri veri modeli
    /// </summary>
    public class TelemetryData
    {
        public string? Feature { get; set; }           // "RaporOzet" veya "EmailAsistan"
        public string? UserAction { get; set; }         // "OzetUret", "SablonOner" vb.
        public double DurationMs { get; set; }
        public bool Success { get; set; }
        public string? Metadata { get; set; }           // Ek bilgiler (JSON string)
    }
}

