using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.CustomAttribute.Services;
using ExtractorLib.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtraction.Tests.CustomAttributeTest
{
    class ComplexFieldExtractor
    {
        private Mock<IPropertyMapService> _mockPropertyMapService;
        private ExtractContractSizeFromAlgoParams _extractor;

        [SetUp]
        public void Setup()
        {
            _mockPropertyMapService = new Mock<IPropertyMapService>();
            _extractor = new ExtractContractSizeFromAlgoParams(_mockPropertyMapService.Object);
        }

        [Test]
        public void ExtractComplexField_ShouldExtractPriceMultiplier_AndSetContractSize()
        {
            // Arrange
            var input = new BarclaysInputDto();
            var output = new BarclaysOutputDto();

            // Simulated input: AlgoParams contains "PriceMultiplier"
            _mockPropertyMapService
                .Setup(m => m.GetProperty<BarclaysInputDto>(input, "AlgoParams"))
                .Returns("SomeKey:ABC|PriceMultiplier:123.45|OtherKey:XYZ");

            // Act
            _extractor.ExtractComplexField(input, output);

            // Assert
            _mockPropertyMapService.Verify(m =>
                m.SetProperty<BarclaysOutputDto>(output, "ContractSize", "123.45"),
                Times.Once);
        }
    }
}
