using ExtractorLib.Dto;
using ExtractorLib.Helper;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtraction.Tests.ExtensionHelperTest
{
    class ValidateCustomDtoHelperTest
    {
        [Test]
        public void ValidateFileData_ShouldReturnOnlyValidRecords()
        {
            // Arrange
            var records = new List<BarclaysInputDto>
                                    {
                                        new BarclaysInputDto
                                        {
                                            ISIN = "DE000C4SA5W8",
                                            CFICode = "FFICSX",
                                            Venue = "XEUR",
                                            AlgoParams = "SomeKey:ABC|PriceMultiplier:123.45|OtherKey:XYZ"

                                        },
                                        new BarclaysInputDto
                                        {
                                            ISIN = "",
                                            CFICode = "FFICSX",
                                            Venue = "WDER",
                                            AlgoParams = "SomeKey:ABC|PriceMultiplier:123.45|OtherKey:XYZ"
                                        }
                                    };

            // Act
            using var sw = new StringWriter();
            Console.SetOut(sw); // capture console output

            var result = ValidateCustomDtoHelper.ValidateFileData(records);

            // Assert
            var output = sw.ToString();
            Assert.That(output, Does.Contain("Row 2: ISIN is empty"));
        }

        [Test]
        public void ValidateFileData_ShouldReturnEmptyList_WhenExceptionOccurs()
        {
            // Arrange
            var expctedOutput = new List<BarclaysInputDto>
                                    {
                                        new BarclaysInputDto
                                        {
                                            ISIN = "DE000C4SA5W8",
                                            CFICode = "FFICSX",
                                            Venue = "XEUR",
                                            AlgoParams = "SomeKey:ABC|PriceMultiplier:123.45|OtherKey:XYZ"

                                        },
                                        new BarclaysInputDto
                                        {
                                            ISIN = "FL0GF001933L",
                                            CFICode = "FFICSX",
                                            Venue = "WDER",
                                            AlgoParams = "SomeKey:ABC|PriceMultiplier:123.45|OtherKey:XYZ"
                                        }
                                    };

            // Act
            using var sw = new StringWriter();
            Console.SetOut(sw); // capture console output

            var output = ValidateCustomDtoHelper.ValidateFileData(expctedOutput);

            // Assert
            Assert.That(output.Count, Is.EqualTo(2), "Expected output count not match");
            Assert.True(output.Count == expctedOutput.Count &&
                !output.Any(a => !expctedOutput.Any(b =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                a.Venue == b.Venue)) &&
                !expctedOutput.Any(b => !output.Any(a =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)), "Expected data not match");
        }
    }
}
