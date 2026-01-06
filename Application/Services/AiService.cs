using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace operion.Application.Services
{
    /// <summary>
    /// AI sağlayıcısı ile iletişim için temel servis sınıfı
    /// OpenAI/Azure OpenAI ve Google Gemini API destekler
    /// .NET 10 için güncellenmiştir
    /// </summary>
    public class AiService
    {
        private HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly string _apiKey;
        private readonly string _provider;
        private readonly int _timeoutMs;
        private readonly int _retryCount;
        private readonly AiLogger _logger;
        private readonly TokenUsageService _tokenUsageService;

        public AiService()
        {
            // TLS 1.2 ve 1.3'ü zorla
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

            _endpoint = ConfigurationManager.AppSettings["AI_ENDPOINT"] ?? "";
            _apiKey = GetApiKey();
            _provider = ConfigurationManager.AppSettings["AI_PROVIDER"] ?? "OpenAI";
            _timeoutMs = int.Parse(ConfigurationManager.AppSettings["AI_TIMEOUT_MS"] ?? "30000");
            _retryCount = int.Parse(ConfigurationManager.AppSettings["AI_RETRY_COUNT"] ?? "3");
            _logger = new AiLogger();
            _tokenUsageService = new TokenUsageService();

            // Proxy sorunlarını aşmak için handler yapılandırması
            // "No such host is known" hatası bazen yanlış proxy ayarlarından kaynaklanabilir
            var handler = new HttpClientHandler
            {
                UseProxy = false, // Proxy'yi devre dışı bırak
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true // Sertifika hatalarını geçici olarak yut (opsiyonel, debug için)
            };
            
            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromMilliseconds(_timeoutMs);
        }

        /// <summary>
        /// API anahtarını güvenli şekilde alır (ENV: prefix ve .env dosyası desteği ile)
        /// </summary>
        private string GetApiKey()
        {
            string apiKeyConfig = ConfigurationManager.AppSettings["AI_API_KEY"] ?? "";
            
            if (apiKeyConfig.StartsWith("ENV:", StringComparison.Ordinal))
            {
                string envVarName = apiKeyConfig.Substring(4);
                
                // Önce ortam değişkeninden dene
                string envValue = Environment.GetEnvironmentVariable(envVarName);
                if (!string.IsNullOrEmpty(envValue))
                {
                    return envValue;
                }
                
                // Ortam değişkeni yoksa .env dosyasından oku
                string envFileValue = ReadFromEnvFile(envVarName);
                if (!string.IsNullOrEmpty(envFileValue))
                {
                    return envFileValue;
                }
            }
            
            return apiKeyConfig;
        }

        /// <summary>
        /// .env dosyasından değer okur
        /// </summary>
        private string ReadFromEnvFile(string keyName)
        {
            try
            {
                // Uygulama dizininde .env dosyasını ara
                string[] searchPaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"),
                    Path.Combine(Directory.GetCurrentDirectory(), ".env"),
                    ".env"
                };

                foreach (string envPath in searchPaths)
                {
                    if (File.Exists(envPath))
                    {
                        string[] lines = File.ReadAllLines(envPath);
                        foreach (string line in lines)
                        {
                            // Boş satırları ve yorumları atla
                            string trimmedLine = line.Trim();
                            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
                                continue;

                            // KEY=VALUE formatını parse et
                            int equalsIndex = trimmedLine.IndexOf('=');
                            if (equalsIndex > 0)
                            {
                                string key = trimmedLine.Substring(0, equalsIndex).Trim();
                                string value = trimmedLine.Substring(equalsIndex + 1).Trim();

                                // Tırnak işaretlerini kaldır
                                if (value.StartsWith("\"") && value.EndsWith("\""))
                                {
                                    value = value.Substring(1, value.Length - 2);
                                }
                                else if (value.StartsWith("'") && value.EndsWith("'"))
                                {
                                    value = value.Substring(1, value.Length - 2);
                                }

                                if (key.Equals(keyName, StringComparison.OrdinalIgnoreCase))
                                {
                                    return value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // .env dosyası okuma hatası - sessizce devam et
                System.Diagnostics.Debug.WriteLine($".env dosyası okuma hatası: {ex.Message}");
            }

            return "";
        }

        /// <summary>
        /// Rapor özetleme için AI çağrısı yapar
        /// </summary>
        public async Task<AiResponse> SummarizeAsync(string prompt)
        {
            return await CallAiWithRetryAsync("summarize", prompt);
        }

        /// <summary>
        /// E-posta şablonu üretimi için AI çağrısı yapar
        /// </summary>
        public async Task<AiResponse> GenerateEmailAsync(string prompt)
        {
            return await CallAiWithRetryAsync("email", prompt);
        }

        /// <summary>
        /// Genel AI çağrısı - retry mekanizması ile
        /// </summary>
        private async Task<AiResponse> CallAiWithRetryAsync(string requestType, string prompt)
        {
            Exception? lastException = null;
            
            for (int attempt = 0; attempt < _retryCount; attempt++)
            {
                try
                {
                    // Check Limit
                    if (_tokenUsageService.IsLimitExceeded())
                    {
                        var stats = _tokenUsageService.GetUsageStats();
                        throw new AiServiceException($"Günlük token limiti aşıldı! ({stats.DailyUsed}/{stats.DailyLimit})");
                    }

                    var startTime = DateTime.Now;
                    
                    var response = await CallAiApiAsync(prompt);
                    
                    // Track Usage
                    _tokenUsageService.TrackUsage(response.PromptTokens, response.CompletionTokens);

                    var duration = (DateTime.Now - startTime).TotalMilliseconds;
                    _logger.LogRequest(requestType, prompt.Length, duration, true, "");
                    
                    return response;
                }
                catch (HttpRequestException ex) when (ex.Data.Contains("StatusCode") && ex.Data["StatusCode"] is System.Net.HttpStatusCode status && status == System.Net.HttpStatusCode.TooManyRequests)
                {
                    lastException = ex;
                    _logger.LogRequest(requestType, prompt.Length, 0, false, "429 - Rate limit");
                    
                    // Exponential backoff
                    await Task.Delay((int)Math.Pow(2, attempt) * 1000);
                }
                catch (TaskCanceledException ex)
                {
                    lastException = ex;
                    _logger.LogRequest(requestType, prompt.Length, _timeoutMs, false, "Timeout");
                    
                    if (attempt == _retryCount - 1)
                        break;
                        
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    _logger.LogRequest(requestType, prompt.Length, 0, false, ex.Message);
                    
                    if (attempt == _retryCount - 1)
                        break;
                        
                    await Task.Delay(1000);
                }
            }
            
            throw new AiServiceException($"AI çağrısı {_retryCount} denemeden sonra başarısız oldu. Detay: {lastException?.Message}", lastException);
        }

        /// <summary>
        /// Gerçek API çağrısını yapar
        /// </summary>
        private async Task<AiResponse> CallAiApiAsync(string prompt)
        {
            if (string.IsNullOrEmpty(_endpoint) || string.IsNullOrEmpty(_apiKey))
            {
                throw new AiServiceException("AI yapılandırması eksik. Lütfen ayarları kontrol edin.");
            }

            var requestBody = BuildRequestBody(prompt);
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            
            HttpRequestMessage request;
            
            // Provider'a göre header ve endpoint ekleme
            if (_provider.ToLowerInvariant().Contains("gemini"))
            {
                // Gemini API key is passed as query parameter
                var endpointWithKey = $"{_endpoint}?key={Uri.EscapeDataString(_apiKey)}";
                request = new HttpRequestMessage(HttpMethod.Post, endpointWithKey)
                {
                    Content = content
                };
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, _endpoint)
                {
                    Content = content
                };
                
                if (_provider.ToLowerInvariant().Contains("openai"))
                {
                    request.Headers.Add("Authorization", $"Bearer {_apiKey}");
                }
                else if (_provider.ToLowerInvariant().Contains("azure"))
                {
                    request.Headers.Add("api-key", _apiKey);
                }
            }
            
            var response = await _httpClient.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                string errorMessage = GetGeminiErrorMessage(response.StatusCode, errorContent);
                throw new HttpRequestException(errorMessage);
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            return ParseResponse(responseContent);
        }

        /// <summary>
        /// Provider'a göre request body oluşturur
        /// </summary>
        private object BuildRequestBody(string prompt)
        {
            if (_provider.ToLowerInvariant().Contains("gemini"))
            {
                return new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        maxOutputTokens = int.Parse(ConfigurationManager.AppSettings["AI_MAX_TOKENS"] ?? "2000"),
                        topP = 0.8,
                        topK = 40
                    },
                    systemInstruction = new
                    {
                        parts = new[]
                        {
                            new { text = "Sen Türkçe konuşan bir iş asistanısın. Her zaman Türkçe yanıt verirsin." }
                        }
                    }
                };
            }
            else if (_provider.ToLowerInvariant().Contains("openai") || _provider.ToLowerInvariant().Contains("azure"))
            {
                return new
                {
                    model = ConfigurationManager.AppSettings["AI_MODEL"] ?? "gpt-4o-mini",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen Türkçe konuşan bir iş asistanısın. Her zaman Türkçe yanıt verirsin." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.7,
                    max_tokens = int.Parse(ConfigurationManager.AppSettings["AI_MAX_TOKENS"] ?? "2000")
                };
            }
            
            // Diğer provider'lar için genişletilebilir
            throw new NotSupportedException($"Provider '{_provider}' henüz desteklenmiyor");
        }

        /// <summary>
        /// API yanıtını parse eder
        /// </summary>
        private AiResponse ParseResponse(string responseContent)
        {
            var json = JObject.Parse(responseContent);
            
            if (_provider.ToLowerInvariant().Contains("gemini"))
            {
                var content = json["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString() ?? "";
                var usageMetadata = json["usageMetadata"];
                
                return new AiResponse
                {
                    Content = content,
                    PromptTokens = usageMetadata?["promptTokenCount"]?.Value<int>() ?? 0,
                    CompletionTokens = usageMetadata?["candidatesTokenCount"]?.Value<int>() ?? 0,
                    TotalTokens = usageMetadata?["totalTokenCount"]?.Value<int>() ?? 0,
                    Provider = _provider
                };
            }
            else if (_provider.ToLowerInvariant().Contains("openai") || _provider.ToLowerInvariant().Contains("azure"))
            {
                var content = json["choices"]?[0]?["message"]?["content"]?.ToString() ?? "";
                var usage = json["usage"];
                
                return new AiResponse
                {
                    Content = content,
                    PromptTokens = usage?["prompt_tokens"]?.Value<int>() ?? 0,
                    CompletionTokens = usage?["completion_tokens"]?.Value<int>() ?? 0,
                    TotalTokens = usage?["total_tokens"]?.Value<int>() ?? 0,
                    Provider = _provider
                };
            }
            
            throw new NotSupportedException($"Provider '{_provider}' yanıt formatı desteklenmiyor");
        }

        /// <summary>
        /// Gemini API hata mesajlarını kullanıcı dostu hale getirir
        /// </summary>
        private string GetGeminiErrorMessage(System.Net.HttpStatusCode statusCode, string errorContent)
        {
            if (_provider.ToLowerInvariant().Contains("gemini"))
            {
                try
                {
                    var errorJson = JObject.Parse(errorContent);
                    var error = errorJson["error"];
                    var message = error?["message"]?.ToString() ?? errorContent;
                    var code = error?["code"]?.ToString() ?? "";

                    return statusCode switch
                    {
                        System.Net.HttpStatusCode.BadRequest => $"Geçersiz istek: {message}",
                        System.Net.HttpStatusCode.Unauthorized => "Geçersiz API anahtarı. Lütfen GEMINI_API_KEY ortam değişkenini kontrol edin.",
                        System.Net.HttpStatusCode.Forbidden => $"Erişim reddedildi: {message}. Quota aşılmış olabilir.",
                        System.Net.HttpStatusCode.TooManyRequests => "Rate limit aşıldı. Lütfen bir süre bekleyip tekrar deneyin.",
                        System.Net.HttpStatusCode.InternalServerError => $"Gemini API sunucu hatası: {message}",
                        _ => $"Gemini API hatası ({statusCode}): {message}"
                    };
                }
                catch
                {
                    // JSON parse başarısız, ham mesajı döndür
                    return $"Gemini API hatası ({statusCode}): {errorContent}";
                }
            }
            
            return $"AI API hatası: {statusCode} - {errorContent}";
        }

        /// <summary>
        /// Servisin hazır olup olmadığını kontrol eder
        /// </summary>
        public bool IsConfigured()
        {
            return !string.IsNullOrEmpty(_endpoint) && !string.IsNullOrEmpty(_apiKey);
        }
    }

    /// <summary>
    /// AI servis yanıt modeli
    /// </summary>
    public class AiResponse
    {
        public string? Content { get; set; }
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
        public string? Provider { get; set; }
    }

    /// <summary>
    /// AI servis özel exception
    /// </summary>
    public class AiServiceException : Exception
    {
        public AiServiceException(string message) : base(message) { }
        public AiServiceException(string message, Exception? innerException) : base(message, innerException) { }
    }
}

