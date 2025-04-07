using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.CustomAttribute.Services;
using ExtractorLib.Dto;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;

namespace DataExtraction.Tests.CustomAttributeTest
{
    class PropertyMapServiceTest
    {
        private Mock<IComplexFieldExtractorFactory> _mockFactory;
        private Mock<IComplexFieldExtractor> _mockExtractor;
        private PropertyMapService _service;

        [SetUp]
        public void Setup()
        {
            _mockFactory = new Mock<IComplexFieldExtractorFactory>();
            _mockExtractor = new Mock<IComplexFieldExtractor>();

            // Return the mocked extractor when asked
            _mockFactory
                .Setup(f => f.GetFieldExtractor("PriceMultiplier"))
                .Returns(_mockExtractor.Object);

            _service = new PropertyMapService(_mockFactory.Object);
        }

        [Test]
        public void MapProperties_ShouldMapSimpleProperties_AndUseExtractorForComplex()
        {
            // Arrange
            var input = new BarclaysInputDto
            {
                ISIN = "DE000C4SA5W8",
                CFICode = "FFICSX",
                Venue = "XEUR"
            };

            var expctedOutput = new List<BarclaysInputDto> { input };

            // Act
            var output = _service.MapProperties<BarclaysInputDto, BarclaysOutputDto>(expctedOutput);

            //Assert
            Assert.That(output.Count, Is.EqualTo(1), "Expected output count not match");
            Assert.True(output.Count == expctedOutput.Count &&
                !output.Any(a => !expctedOutput.Any(b =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)) &&
                !expctedOutput.Any(b => !output.Any(a =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)), "Expected output not match");

            // Assert - complex field was passed to extractor
            _mockFactory.Verify(f => f.GetFieldExtractor("PriceMultiplier"), Times.Once);
            _mockExtractor.Verify(e => e.ExtractComplexField<BarclaysInputDto, BarclaysOutputDto>(
                It.IsAny<BarclaysInputDto>(), It.IsAny<BarclaysOutputDto>()), Times.Once);
        }
    }
}
