using System;
using NUnit.Framework;
using operion.Application.Services;

namespace operion.Tests.Application.Services
{
    /// <summary>
    /// Prompt Builder için birim testleri
    /// </summary>
    [TestFixture]
    public class PromptBuilderTests
    {
        private PromptBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _builder = new PromptBuilder();
        }

        #region Rapor Özeti Prompt Testleri

        [Test]
        public void BuildReportSummaryPrompt_ShouldContainTurkishInstructions()
        {
            // Arrange
            var context = new ReportSummaryContext
            {
                ReportType = "Firmalar Raporu",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 10, 1),
                Data = "Test rapor verisi..."
            };

            // Act
            string prompt = _builder.BuildReportSummaryPrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Türkçe"));
            Assert.That(prompt, Does.Contain("özet"));
        }

        [Test]
        public void BuildReportSummaryPrompt_ShouldContainReportType()
        {
            // Arrange
            var context = new ReportSummaryContext
            {
                ReportType = "Firmalar Raporu",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 10, 1),
                Data = "Test data"
            };

            // Act
            string prompt = _builder.BuildReportSummaryPrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Firmalar Raporu"));
        }

        [Test]
        public void BuildReportSummaryPrompt_ShouldContainDateRange()
        {
            // Arrange
            var context = new ReportSummaryContext
            {
                ReportType = "Test Raporu",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 10, 1),
                Data = "Test data"
            };

            // Act
            string prompt = _builder.BuildReportSummaryPrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("01.09.2025"));
            Assert.That(prompt, Does.Contain("01.10.2025"));
        }

        [Test]
        public void BuildReportSummaryPrompt_ShouldContainReportData()
        {
            // Arrange
            string testData = "Sütunlar: FirmaAd, VergiNo\n---\nABC Ltd., 1234567890";
            var context = new ReportSummaryContext
            {
                ReportType = "Test Raporu",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 10, 1),
                Data = testData
            };

            // Act
            string prompt = _builder.BuildReportSummaryPrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain(testData));
        }

        [Test]
        public void BuildReportSummaryPrompt_ShouldContainOutputFormat()
        {
            // Arrange
            var context = new ReportSummaryContext
            {
                ReportType = "Test Raporu",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 10, 1),
                Data = "Test data"
            };

            // Act
            string prompt = _builder.BuildReportSummaryPrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Özet"));
            Assert.That(prompt, Does.Contain("Aksiyon"));
        }

        [Test]
        public void BuildReportSummaryPrompt_ShouldContainFilterInfoWhenProvided()
        {
            // Arrange
            var context = new ReportSummaryContext
            {
                ReportType = "Test Raporu",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 10, 1),
                FilterInfo = "Aktif firmalar",
                Data = "Test data"
            };

            // Act
            string prompt = _builder.BuildReportSummaryPrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Aktif firmalar"));
        }

        #endregion

        #region E-posta Şablonu Prompt Testleri

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainScenario()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Teklif"));
            Assert.That(prompt, Does.Contain("SENARYO"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainCustomerReference()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("MUSTERI_001"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainTone()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Samimi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Ton"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainLength()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Kisa,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Uzunluk"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainSubjectLineFormat()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Konu Satırları"));
            Assert.That(prompt, Does.Contain("3 alternatif"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainEmailBodyFormat()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("E-posta Gövdesi"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainProductsInfoWhenProvided()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001",
                ProductsInfo = "Laptop, Mouse, Klavye"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Laptop, Mouse, Klavye"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainAmountWhenProvided()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001",
                Amount = 15000.50m
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("15.000,50"));
        }

        [Test]
        public void BuildEmailTemplatePrompt_ShouldContainTurkishInstructions()
        {
            // Arrange
            var context = new EmailTemplateContext
            {
                Scenario = EmailScenario.Teklif,
                Tone = EmailTone.Resmi,
                Length = EmailLength.Orta,
                CustomerReference = "MUSTERI_001"
            };

            // Act
            string prompt = _builder.BuildEmailTemplatePrompt(context);

            // Assert
            Assert.That(prompt, Does.Contain("Türkçe"));
        }

        #endregion

        #region Senaryo Açıklamaları Testleri

        [Test]
        public void BuildEmailTemplatePrompt_ShouldHandleAllScenarios()
        {
            var scenarios = new[]
            {
                EmailScenario.Teklif,
                EmailScenario.Tesekkur,
                EmailScenario.OdemeHatirlatma,
                EmailScenario.TeslimatBilgi,
                EmailScenario.GenelYanit
            };

            foreach (var scenario in scenarios)
            {
                // Arrange
                var context = new EmailTemplateContext
                {
                    Scenario = scenario,
                    Tone = EmailTone.Notr,
                    Length = EmailLength.Orta,
                    CustomerReference = "MUSTERI_001"
                };

                // Act
                string prompt = _builder.BuildEmailTemplatePrompt(context);

                // Assert
                Assert.That(prompt, Is.Not.Empty, $"Scenario {scenario} should generate prompt");
            }
        }

        #endregion
    }
}

