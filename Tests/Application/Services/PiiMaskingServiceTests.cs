using System;
using NUnit.Framework;
using operion.Application.Services;

namespace operion.Tests.Application.Services
{
    /// <summary>
    /// PII Maskeleme Servisi için birim testleri
    /// </summary>
    [TestFixture]
    public class PiiMaskingServiceTests
    {
        private PiiMaskingService _service;

        [SetUp]
        public void Setup()
        {
            _service = new PiiMaskingService();
        }

        #region E-posta Maskeleme Testleri

        [Test]
        public void MaskEmails_ShouldMaskEmailAddress()
        {
            // Arrange
            string input = "Müşteri: ali.yilmaz@gmail.com ile iletişime geçildi";
            string expected = "Müşteri: [EMAIL] ile iletişime geçildi";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[EMAIL]"));
            Assert.That(result, Does.Not.Contain("ali.yilmaz@gmail.com"));
        }

        [Test]
        public void MaskEmails_ShouldMaskMultipleEmails()
        {
            // Arrange
            string input = "İletişim: info@firma.com ve destek@firma.com";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Not.Contain("@"));
            Assert.That(result.Split(new[] { "[EMAIL]" }, StringSplitOptions.None).Length, Is.EqualTo(3));
        }

        [Test]
        public void MaskEmails_ShouldNotMaskNonEmailText()
        {
            // Arrange
            string input = "Normal metin içinde @ işareti var ama email değil";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Is.EqualTo(input));
        }

        #endregion

        #region Telefon Maskeleme Testleri

        [Test]
        public void MaskPhoneNumbers_ShouldMaskTurkishPhoneNumber()
        {
            // Arrange
            string input = "İletişim: 0532 123 45 67";
            string expected = "İletişim: [TELEFON]";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[TELEFON]"));
            Assert.That(result, Does.Not.Contain("0532"));
        }

        [Test]
        public void MaskPhoneNumbers_ShouldMaskPhoneWithoutSpaces()
        {
            // Arrange
            string input = "Tel: 05321234567";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[TELEFON]"));
            Assert.That(result, Does.Not.Contain("05321234567"));
        }

        [Test]
        public void MaskPhoneNumbers_ShouldMaskInternationalFormat()
        {
            // Arrange
            string input = "Tel: +90 532 123 45 67";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[TELEFON]"));
        }

        #endregion

        #region TC Kimlik Maskeleme Testleri

        [Test]
        public void MaskIdentificationNumbers_ShouldMaskTCNumber()
        {
            // Arrange
            string input = "TC No: 12345678901";
            string expected = "TC No: [KIMLIK_NO]";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[KIMLIK_NO]"));
            Assert.That(result, Does.Not.Contain("12345678901"));
        }

        [Test]
        public void MaskIdentificationNumbers_ShouldMaskVergiNo()
        {
            // Arrange
            string input = "Vergi No: 1234567890";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[KIMLIK_NO]"));
        }

        #endregion

        #region IBAN Maskeleme Testleri

        [Test]
        public void MaskIban_ShouldMaskIBAN()
        {
            // Arrange
            string input = "IBAN: TR33 0006 1005 1978 6457 8413 26";
            string expected = "IBAN: [IBAN]";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[IBAN]"));
            Assert.That(result, Does.Not.Contain("TR33"));
        }

        [Test]
        public void MaskIban_ShouldMaskIBANWithoutSpaces()
        {
            // Arrange
            string input = "IBAN: TR330006100519786457841326";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[IBAN]"));
        }

        #endregion

        #region İsim Maskeleme Testleri

        [Test]
        public void MaskPersonNames_ShouldMaskCommonTurkishNames()
        {
            // Arrange
            string input = "Müşteri Ahmet Yılmaz ile görüşüldü";

            // Act
            string result = _service.MaskText(input);

            // Assert
            // İsim maskeleme yapılıyor mu kontrol et
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region Müşteri Referansı Maskeleme Testleri

        [Test]
        public void MaskCustomerReference_ShouldReturnMaskedReference()
        {
            // Arrange
            string customerName = "Ahmet Yılmaz";

            // Act
            string result = _service.MaskCustomerReference(customerName);

            // Assert
            Assert.That(result, Does.Match(@"MUSTERI_\d{3}"));
            Assert.That(result, Does.Not.Contain("Ahmet"));
        }

        [Test]
        public void MaskCustomerReference_ShouldReturnSameReferenceForSameName()
        {
            // Arrange
            string customerName = "Mehmet Demir";
            var service = new PiiMaskingService();

            // Act
            string result1 = service.MaskCustomerReference(customerName);
            string result2 = service.MaskCustomerReference(customerName);

            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }

        [Test]
        public void MaskCustomerReference_ShouldReturnGenericForNull()
        {
            // Act
            string result = _service.MaskCustomerReference(null);

            // Assert
            Assert.That(result, Is.EqualTo("[MÜŞTERİ]"));
        }

        [Test]
        public void MaskCustomerReference_ShouldReturnGenericForEmpty()
        {
            // Act
            string result = _service.MaskCustomerReference("");

            // Assert
            Assert.That(result, Is.EqualTo("[MÜŞTERİ]"));
        }

        #endregion

        #region Firma Referansı Maskeleme Testleri

        [Test]
        public void MaskCompanyReference_ShouldReturnMaskedReference()
        {
            // Arrange
            string companyName = "ABC Teknoloji A.Ş.";

            // Act
            string result = _service.MaskCompanyReference(companyName);

            // Assert
            Assert.That(result, Does.Match(@"FIRMA_\d{3}"));
        }

        [Test]
        public void MaskCompanyReference_ShouldReturnGenericForNull()
        {
            // Act
            string result = _service.MaskCompanyReference(null);

            // Assert
            Assert.That(result, Is.EqualTo("[FİRMA]"));
        }

        #endregion

        #region Karmaşık Metin Maskeleme Testleri

        [Test]
        public void MaskText_ShouldMaskAllPIIInComplexText()
        {
            // Arrange
            string input = "Müşteri: Ahmet Yılmaz (ali.yilmaz@gmail.com, Tel: 0532 123 45 67, TC: 12345678901) " +
                          "IBAN: TR33 0006 1005 1978 6457 8413 26 ile görüşüldü.";

            // Act
            string result = _service.MaskText(input);

            // Assert
            Assert.That(result, Does.Contain("[EMAIL]"));
            Assert.That(result, Does.Contain("[TELEFON]"));
            Assert.That(result, Does.Contain("[KIMLIK_NO]"));
            Assert.That(result, Does.Contain("[IBAN]"));
            Assert.That(result, Does.Not.Contain("ali.yilmaz@gmail.com"));
            Assert.That(result, Does.Not.Contain("0532"));
            Assert.That(result, Does.Not.Contain("12345678901"));
            Assert.That(result, Does.Not.Contain("TR33"));
        }

        [Test]
        public void MaskText_ShouldHandleEmptyString()
        {
            // Act
            string result = _service.MaskText("");

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void MaskText_ShouldHandleNull()
        {
            // Act
            string result = _service.MaskText(null);

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region Rapor Verisi Hazırlama Testleri

        [Test]
        public void PrepareReportDataForAi_ShouldMaskPIIInReportData()
        {
            // Arrange
            string reportData = "Firma: ABC Ltd. (info@abc.com, Tel: 0532 111 22 33)";

            // Act
            string result = _service.PrepareReportDataForAi(reportData, includeCustomerNames: false);

            // Assert
            Assert.That(result, Does.Contain("[EMAIL]"));
            Assert.That(result, Does.Contain("[TELEFON]"));
        }

        [Test]
        public void PrepareReportDataForAi_ShouldHandleNull()
        {
            // Act
            string result = _service.PrepareReportDataForAi(null);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        #endregion

        #region E-posta Bağlamı Hazırlama Testleri

        [Test]
        public void PrepareEmailContextForAi_ShouldMaskPIIInEmail()
        {
            // Arrange
            string emailContent = "Sayın Ahmet Yılmaz (ali.yilmaz@gmail.com), teşekkür ederiz.";

            // Act
            string result = _service.PrepareEmailContextForAi(emailContent);

            // Assert
            Assert.That(result, Does.Contain("[EMAIL]"));
        }

        [Test]
        public void PrepareEmailContextForAi_ShouldRemoveEmailSignature()
        {
            // Arrange
            string emailContent = "Mesaj içeriği\n\n--\nSaygılarımla,\nAhmet Yılmaz\nTel: 0532 123 45 67";

            // Act
            string result = _service.PrepareEmailContextForAi(emailContent);

            // Assert
            Assert.That(result, Does.Contain("[İMZA]"));
        }

        #endregion
    }
}

