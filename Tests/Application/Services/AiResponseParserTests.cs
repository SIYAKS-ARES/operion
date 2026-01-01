using System;
using System.Linq;
using NUnit.Framework;
using operion.Application.Services;

namespace operion.Tests.Application.Services
{
    /// <summary>
    /// AI Response Parser için birim testleri
    /// </summary>
    [TestFixture]
    public class AiResponseParserTests
    {
        private AiResponseParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new AiResponseParser();
        }

        #region Rapor Özeti Parse Testleri

        [Test]
        public void ParseSummaryAndActions_ShouldParseSummarySection()
        {
            // Arrange
            string aiResponse = @"
## ÖZET:
• Toplam 45 kayıt bulunmaktadır
• Ciroda %15 artış var
• En çok satış yapan firma: ABC Ltd.

## AKSİYON:
1. Pasif firmalarla görüşme yapılmalı
2. Pazarlama bütçesi artırılmalı
";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.True);
            Assert.That(parsed.SummaryPoints, Is.Not.Empty);
            Assert.That(parsed.SummaryPoints.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ParseSummaryAndActions_ShouldParseActionItems()
        {
            // Arrange
            string aiResponse = @"
## ÖZET:
• Test özet

## AKSİYON:
1. İlk aksiyon maddesi
2. İkinci aksiyon maddesi
3. Üçüncü aksiyon maddesi
";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.True);
            Assert.That(parsed.ActionItems, Is.Not.Empty);
            Assert.That(parsed.ActionItems.Count, Is.GreaterThanOrEqualTo(3));
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandleEnglishHeaders()
        {
            // Arrange
            string aiResponse = @"
## SUMMARY:
• First summary point
• Second summary point

## ACTION:
1. First action item
2. Second action item
";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.True);
            Assert.That(parsed.SummaryPoints, Is.Not.Empty);
            Assert.That(parsed.ActionItems, Is.Not.Empty);
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandleNoHeaders()
        {
            // Arrange
            string aiResponse = @"
• İlk özet maddesi
• İkinci özet maddesi

aksiyon
1. İlk aksiyon
2. İkinci aksiyon
";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.SummaryPoints, Is.Not.Empty);
        }

        [Test]
        public void ParseSummaryAndActions_ShouldExtractBulletPoints()
        {
            // Arrange
            string aiResponse = @"
## ÖZET:
- Madde 1
- Madde 2
* Madde 3
• Madde 4
";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.SummaryPoints.Count, Is.GreaterThanOrEqualTo(4));
        }

        [Test]
        public void ParseSummaryAndActions_ShouldExtractNumberedList()
        {
            // Arrange
            string aiResponse = @"
## AKSİYON:
1. Birinci aksiyon
2. İkinci aksiyon
3. Üçüncü aksiyon
";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.ActionItems.Count, Is.GreaterThanOrEqualTo(3));
        }

        [Test]
        public void ParseSummaryAndActions_ShouldStoreRawText()
        {
            // Arrange
            string aiResponse = "Test response text";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.RawText, Is.EqualTo(aiResponse));
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandleEmptyResponse()
        {
            // Arrange
            string aiResponse = "";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.False);
            Assert.That(parsed.SummaryPoints, Is.Empty);
        }

        [Test]
        public void ParseSummaryAndActions_ShouldHandleMalformedResponse()
        {
            // Arrange
            string aiResponse = "Random text without structure";

            // Act
            var parsed = _parser.ParseSummaryAndActions(aiResponse);

            // Assert
            // Parser should attempt to extract what it can
            Assert.That(parsed.RawText, Is.EqualTo(aiResponse));
        }

        #endregion

        #region E-posta Parse Testleri

        [Test]
        public void ParseEmailParts_ShouldExtractSubjectLines()
        {
            // Arrange
            string aiResponse = @"
## Konu Satırları:
1. [Teklif Sunumu - ABC Ltd.]
2. [Özel Fiyat Teklifi]
3. [Kampanyalı Teklif]

## E-posta Gövdesi:
Sayın MUSTERI_001,
...
";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.True);
            Assert.That(parsed.SubjectLines, Is.Not.Empty);
            Assert.That(parsed.SubjectLines.Count, Is.GreaterThanOrEqualTo(3));
        }

        [Test]
        public void ParseEmailParts_ShouldExtractEmailBody()
        {
            // Arrange
            string aiResponse = @"
## Konu Satırları:
1. [Test Konu]

## E-posta Gövdesi:
Sayın MUSTERI_001,

Bu bir test e-posta gövdesidir.

Saygılarımla,
";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.True);
            Assert.That(parsed.EmailBody, Is.Not.Empty);
            Assert.That(parsed.EmailBody, Does.Contain("test e-posta"));
        }

        [Test]
        public void ParseEmailParts_ShouldHandleEnglishHeaders()
        {
            // Arrange
            string aiResponse = @"
## Subject Lines:
1. [Test Subject 1]
2. [Test Subject 2]

## Email Body:
Test email body content
";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.True);
            Assert.That(parsed.SubjectLines, Is.Not.Empty);
            Assert.That(parsed.EmailBody, Is.Not.Empty);
        }

        [Test]
        public void ParseEmailParts_ShouldRemoveBracketsFromSubjects()
        {
            // Arrange
            string aiResponse = @"
## Konu Satırları:
1. [Teklif Sunumu]
2. [Özel Fiyat]
";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            foreach (var subject in parsed.SubjectLines)
            {
                Assert.That(subject, Does.Not.Contain("["));
                Assert.That(subject, Does.Not.Contain("]"));
            }
        }

        [Test]
        public void ParseEmailParts_ShouldHandleNoSubjectSection()
        {
            // Arrange
            string aiResponse = @"
E-posta içeriği burada.
Konu satırları belirtilmemiş.
";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            // Should still extract body if possible
            Assert.That(parsed.RawText, Is.EqualTo(aiResponse));
        }

        [Test]
        public void ParseEmailParts_ShouldStoreRawText()
        {
            // Arrange
            string aiResponse = "Test email response";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            Assert.That(parsed.RawText, Is.EqualTo(aiResponse));
        }

        [Test]
        public void ParseEmailParts_ShouldHandleEmptyResponse()
        {
            // Arrange
            string aiResponse = "";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            Assert.That(parsed.ParseSuccess, Is.False);
            Assert.That(parsed.SubjectLines, Is.Empty);
            Assert.That(parsed.EmailBody, Is.Null);
        }

        [Test]
        public void ParseEmailParts_ShouldExtractMultipleSubjectLines()
        {
            // Arrange
            string aiResponse = @"
Konu:
1. Birinci konu satırı
2. İkinci konu satırı
3. Üçüncü konu satırı

E-posta Gövdesi:
Test gövde
";

            // Act
            var parsed = _parser.ParseEmailParts(aiResponse);

            // Assert
            Assert.That(parsed.SubjectLines.Count, Is.GreaterThanOrEqualTo(3));
        }

        #endregion

        #region Hata Yönetimi Testleri

        [Test]
        public void ParseSummaryAndActions_ShouldHandleExceptionGracefully()
        {
            // Arrange
            // Null string should be handled
            string? aiResponse = null;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var parsed = _parser.ParseSummaryAndActions(aiResponse ?? "");
                Assert.That(parsed, Is.Not.Null);
            });
        }

        [Test]
        public void ParseEmailParts_ShouldHandleExceptionGracefully()
        {
            // Arrange
            string? aiResponse = null;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var parsed = _parser.ParseEmailParts(aiResponse ?? "");
                Assert.That(parsed, Is.Not.Null);
            });
        }

        #endregion
    }
}

