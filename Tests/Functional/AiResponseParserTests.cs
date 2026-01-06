using System;
using System.Collections.Generic;
using NUnit.Framework;
using operion.Application.Services;

namespace operion.Tests.Functional
{
    [TestFixture]
    public class AiResponseParserTests
    {
        private AiResponseParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new AiResponseParser();
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandle_AllCapsHeaders()
        {
            // Arrange
            string response = @"
## ÖZET
• Toplam 5 müşteri
• Ciro artışı %20

## AKSİYON MADDELERİ
1. Müşterileri ara
2. Kampanya düzenle
";

            // Act
            var result = _parser.ParseSummaryAndActions(response);

            // Assert
            Assert.IsTrue(result.ParseSuccess, $"Parse failed: {result.ErrorMessage}\nRaw: {result.RawText}");
            Assert.AreEqual(2, result.SummaryPoints.Count, "Summary points mismatch");
            Assert.AreEqual(2, result.ActionItems.Count, "Action items mismatch");
            Assert.AreEqual("Toplam 5 müşteri", result.SummaryPoints[0]);
            Assert.AreEqual("Müşterileri ara", result.ActionItems[0]);
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandle_TitleCaseHeaders()
        {
            // Arrange
            string response = @"
## Özet
• Toplam 5 müşteri
• Ciro artışı %20

## Aksiyon Maddeleri
1. Müşterileri ara
2. Kampanya düzenle
";

            // Act
            var result = _parser.ParseSummaryAndActions(response);

            // Assert
            Assert.IsTrue(result.ParseSuccess);
            Assert.AreEqual(2, result.SummaryPoints.Count);
            Assert.AreEqual(2, result.ActionItems.Count);
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandle_MixedHeaders()
        {
            // Arrange
            string response = @"
**ÖZET**
- Madde 1

**Aksiyon Maddeleri**
- Aksiyon 1
";

            // Act
            var result = _parser.ParseSummaryAndActions(response);

            // Assert
            Assert.IsTrue(result.ParseSuccess, $"Parse failed: {result.ErrorMessage}");
            Assert.AreEqual(1, result.SummaryPoints.Count);
            Assert.AreEqual(1, result.ActionItems.Count);
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandle_NoHeaders_Fallback()
        {
            // Arrange
            string response = @"
• Özet maddesi 1
• Özet maddesi 2

AKSİYON
1. Aksiyon 1
2. Aksiyon 2
";

            // Act
            var result = _parser.ParseSummaryAndActions(response);

            // Assert
            Assert.IsTrue(result.ParseSuccess, $"Parse failed: {result.ErrorMessage}");
            Assert.AreEqual(2, result.SummaryPoints.Count);
            Assert.AreEqual(2, result.ActionItems.Count);
        }
    }
}
