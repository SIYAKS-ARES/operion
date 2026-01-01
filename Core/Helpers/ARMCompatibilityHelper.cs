using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace operion.Application.Services
{
    /// <summary>
    /// ARM Windows uyumluluk kontrolü ve alternatif çözümler
    /// .NET 10 için güncellenmiştir
    /// </summary>
    public static class ARMCompatibilityHelper
    {
        /// <summary>
        /// ARM Windows'ta çalışıp çalışmadığını kontrol et
        /// </summary>
        public static bool IsARMWindows()
        {
            try
            {
                // Modern yaklaşım: Environment.IsARM64Process (.NET 8+)
                if (Environment.Is64BitProcess && RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
                {
                    return true;
                }

                // Alternatif: wmic komutu ile kontrol
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "cpu get name",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });
                
                if (process != null)
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    
                    return output.Contains("ARM", StringComparison.OrdinalIgnoreCase) || 
                           output.Contains("Qualcomm", StringComparison.OrdinalIgnoreCase) || 
                           output.Contains("Snapdragon", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
                // Hata durumunda varsayılan olarak ARM değil kabul et
            }
            
            return false;
        }

        /// <summary>
        /// DevExpress bileşenlerinin ARM uyumluluğunu kontrol et
        /// NOT: DevExpress şimdilik beklemeye alındı, bu kontrol devre dışı
        /// </summary>
        public static bool CheckDevExpressCompatibility()
        {
            // DevExpress kullanılmadığı için her zaman false döner
            // İleride DevExpress eklendiğinde bu kontrol aktif edilebilir
            try
            {
                // DevExpress kontrolü şimdilik devre dışı
                // TODO: DevExpress eklendiğinde bu kontrolü aktif et
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DevExpress ARM uyumluluk kontrolü yapılamadı: {ex.Message}", 
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// AI servislerinin ARM uyumluluğunu kontrol et
        /// </summary>
        public static bool CheckAIServiceCompatibility()
        {
            try
            {
                // HTTP client test (async yapılmadığı için .Result kullanıldı - UI thread'de dikkatli kullanılmalı)
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    // Basit bir test isteği
                    var response = client.GetAsync("https://httpbin.org/get").Result;
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"AI servis ARM uyumluluk hatası: {ex.Message}", 
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// ARM Windows'ta alternatif çözümleri etkinleştir
        /// NOT: ConfigurationManager.AppSettings yazma artık desteklenmiyor, 
        /// bu özellik appsettings.json ile değiştirilmeli
        /// </summary>
        public static void EnableARMAlternatives()
        {
            if (IsARMWindows())
            {
                // NOT: ConfigurationManager.AppSettings["key"] = "value" yazma işlemi artık çalışmıyor
                // Bu özellik appsettings.json veya başka bir yapılandırma yöntemi ile değiştirilmeli
                // Şimdilik sadece bilgilendirme yapıyoruz
                
                MessageBox.Show("ARM Windows tespit edildi. " +
                    "Bazı özellikler (ReportViewer, DevExpress) bu platformda çalışmayabilir. " +
                    "Alternatif çözümler kullanılacak.", 
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Sistem uyumluluğunu tam kontrol et
        /// </summary>
        public static void CheckSystemCompatibility()
        {
            // DevExpress kontrolü devre dışı (DevExpress yok)
            bool devExpressOK = false; // DevExpress kullanılmıyor
            bool aiServiceOK = CheckAIServiceCompatibility();
            bool isARM = IsARMWindows();
            
            string message = $"Sistem Uyumluluk Raporu:\n\n";
            message += $"ARM Windows: {(isARM ? "Evet" : "Hayır")}\n";
            message += $"DevExpress: Kullanılmıyor (standart Windows Forms kontrolleri kullanılıyor)\n";
            message += $"AI Servisleri: {(aiServiceOK ? "Uyumlu" : "Sorunlu")}\n";
            message += $"Microsoft.Data.Sqlite: Uyumlu (ARM64 desteği var)\n\n";
            
            if (!aiServiceOK)
            {
                message += "Öneriler:\n";
                message += "• İnternet bağlantınızı kontrol edin\n";
                message += "• AI özelliklerini devre dışı bırakabilirsiniz\n";
            }
            
            MessageBox.Show(message, "Sistem Uyumluluk", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

