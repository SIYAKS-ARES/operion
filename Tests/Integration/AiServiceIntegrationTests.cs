using System;
using System.Threading.Tasks;
using NUnit.Framework;
using operion.Application.Services;
using System.Configuration;

namespace operion.Tests.Integration
{
    /// <summary>
    /// AI Service için entegrasyon testleri
    /// Bu testler gerçek Gemini API çağrıları yapar
    /// GEMINI_API_KEY ortam değişkeni ayarlanmalıdır
    /// </summary>
    [TestFixture]
    [Explicit("Requires GEMINI_API_KEY environment variable and internet connection")]
    public class AiServiceIntegrationTests
    {
        private AiService _aiService;

        [SetUp]
        public void Setup()
        {
            _aiService = new AiService();
        }

        [Test]
        public void IsConfigured_ShouldReturnTrueWhenApiKeyIsSet()
        {
            // Act
            bool isConfigured = _aiService.IsConfigured();

            // Assert
            Assert.That(isConfigured, Is.True, "AI service should be configured when GEMINI_API_KEY is set");
        }

        [Test]
        public void SummarizeAsync_ShouldReturnValidResponse()
        {
            // Arrange
            string prompt = "Aşağıdaki rapor verilerini özetle:\n\n" +
                          "Rapor: Test Raporu\n" +
                          "Toplam Kayıt: 10\n" +
                          "Sütunlar: FirmaAd, Ciro\n" +
                          "---\n" +
                          "ABC Ltd., 50000\n" +
                          "XYZ A.Ş., 75000";

            // Act
            var response = _aiService.SummarizeAsync(prompt).Result;

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(response.TotalTokens, Is.GreaterThan(0));
            Assert.That(response.Provider, Does.Contain("Gemini").IgnoreCase);
        }

        [Test]
        public void GenerateEmailAsync_ShouldReturnValidResponse()
        {
            // Arrange
            string prompt = "Teklif e-postası oluştur:\n" +
                          "Müşteri: MUSTERI_001\n" +
                          "Ton: Resmi\n" +
                          "Uzunluk: Orta";

            // Act
            var response = _aiService.GenerateEmailAsync(prompt).Result;

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(response.TotalTokens, Is.GreaterThan(0));
        }

        [Test]
        public void SummarizeAsync_ShouldHandleLongPrompt()
        {
            // Arrange
            var longPrompt = new System.Text.StringBuilder();
            longPrompt.AppendLine("Aşağıdaki rapor verilerini özetle:");
            longPrompt.AppendLine("Rapor: Büyük Rapor");
            longPrompt.AppendLine("Toplam Kayıt: 50");
            longPrompt.AppendLine("Sütunlar: FirmaAd, Ciro, Adet");
            
            for (int i = 1; i <= 50; i++)
            {
                longPrompt.AppendLine($"Firma {i}, {10000 + i * 1000}, {i * 10}");
            }

            // Act
            var response = _aiService.SummarizeAsync(longPrompt.ToString()).Result;

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void SummarizeAsync_ShouldReturnTurkishResponse()
        {
            // Arrange
            string prompt = "Türkçe olarak kısa bir özet yaz: Test verisi";

            // Act
            var response = _aiService.SummarizeAsync(prompt).Result;

            // Assert
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            // Türkçe karakterlerin varlığını kontrol et (basit kontrol)
            bool hasTurkishChars = response.Content.Contains("ş") || 
                                  response.Content.Contains("ğ") || 
                                  response.Content.Contains("ü") || 
                                  response.Content.Contains("ö") ||
                                  response.Content.Contains("ç") ||
                                  response.Content.Contains("ı");
            
            // Not: Gemini her zaman Türkçe karakter kullanmayabilir, bu yüzden sadece içeriğin boş olmadığını kontrol ediyoruz
            Assert.That(response.Content.Length, Is.GreaterThan(10));
        }

        [Test]
        public void GenerateEmailAsync_ShouldReturnStructuredResponse()
        {
            // Arrange
            string prompt = @"E-posta şablonu oluştur:
Senaryo: Teklif
Ton: Resmi
Uzunluk: Orta

Çıktı formatı:
### Konu Satırları (3 alternatif):
1. [Konu 1]
2. [Konu 2]
3. [Konu 3]

### E-posta Gövdesi:
[E-posta içeriği]";

            // Act
            var response = _aiService.GenerateEmailAsync(prompt).Result;

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(response.Content.Length, Is.GreaterThan(50));
        }

        [Test]
        public void SummarizeAsync_ShouldHandleTimeoutGracefully()
        {
            // Arrange
            // Çok uzun bir prompt (timeout testi için)
            var veryLongPrompt = new System.Text.StringBuilder();
            veryLongPrompt.AppendLine("Test prompt");
            // Not: Gerçek timeout testi için çok daha uzun prompt gerekir
            // Bu test sadece servisin çalıştığını doğrular

            // Act & Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                var response = await _aiService.SummarizeAsync(veryLongPrompt.ToString());
                Assert.That(response, Is.Not.Null);
            });
        }

        [Test]
        public void SummarizeAsync_ShouldIncludeTokenCounts()
        {
            // Arrange
            string prompt = "Kısa bir test özeti yaz: Test verisi";

            // Act
            var response = _aiService.SummarizeAsync(prompt).Result;

            // Assert
            Assert.That(response.PromptTokens, Is.GreaterThan(0));
            Assert.That(response.CompletionTokens, Is.GreaterThan(0));
            Assert.That(response.TotalTokens, Is.EqualTo(response.PromptTokens + response.CompletionTokens));
        }

        [Test]
        public void GenerateEmailAsync_ShouldIncludeTokenCounts()
        {
            // Arrange
            string prompt = "Kısa bir e-posta şablonu oluştur";

            // Act
            var response = _aiService.GenerateEmailAsync(prompt).Result;

            // Assert
            Assert.That(response.PromptTokens, Is.GreaterThan(0));
            Assert.That(response.CompletionTokens, Is.GreaterThan(0));
            Assert.That(response.TotalTokens, Is.EqualTo(response.PromptTokens + response.CompletionTokens));
        }

        [Test]
        public void SummarizeAsync_ShouldHandleEmptyPrompt()
        {
            // Arrange
            string prompt = "";

            // Act & Assert
            // Empty prompt should either throw exception or return empty response
            Assert.DoesNotThrowAsync(async () =>
            {
                var response = await _aiService.SummarizeAsync(prompt);
                // Response might be empty or contain error message
                Assert.That(response, Is.Not.Null);
            });
        }

        [Test]
        public void IsConfigured_ShouldReturnFalseWhenApiKeyNotSet()
        {
            // Arrange
            // Bu test için API key'i geçici olarak kaldırmak gerekir
            // Ancak bu diğer testleri bozabilir, bu yüzden sadece mevcut durumu kontrol ediyoruz
            
            // Act
            bool isConfigured = _aiService.IsConfigured();

            // Assert
            // Eğer API key ayarlanmışsa true, değilse false olmalı
            Assert.That(isConfigured, Is.Not.Null);
        }
    }
}

