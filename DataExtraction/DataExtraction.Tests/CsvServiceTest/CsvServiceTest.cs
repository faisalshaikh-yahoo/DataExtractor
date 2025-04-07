using CsvHelper;
using ExtractorLib.CsvHelperService;
using ExtractorLib.Dto;
using System.Globalization;
using System.Reflection;

namespace DataExtraction.Tests.CsvServiceTest
{
    public class CsvServiceTest
    {
        private static string _currentDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _inputFilePath = Path.Combine(_currentDirectoryPath, "CsvServiceTest", "Data", "Input", "TestInputCsvReader.csv");
        private CsvService _csvService;
        private int _skipLines = 1;
        private string _outputFilePath;
        [SetUp]
        public void Setup()
        {
            //Setup
            _csvService = new CsvService();
            _outputFilePath = Path.GetTempFileName();
        }

        [Test]
        public void PreProcessAndReadFile_ShouldReadCsvAndSkipLines()
        {
            //Arrange
            var expctedOutput =  new List<BarclaysInputDto>
                                    {
                                        new BarclaysInputDto
                                        {
                                            ISIN = "DE000C4SA5W8",
                                            CFICode = "FFICSX",
                                            Venue = "XEUR"
                                        },
                                        new BarclaysInputDto
                                        {
                                            ISIN = "PL0GF0019331",
                                            CFICode = "FFICSX",
                                            Venue = "WDER"
                                        }
                                    };

            //Act
            var output = _csvService.PreProcessAndReadFile<BarclaysInputDto>(_inputFilePath, _skipLines);

            //Assert
            Assert.That(output.Count, Is.EqualTo(2), "Expected output count not match"); 
            Assert.True(output.Count == expctedOutput.Count &&
                !output.Any(a => !expctedOutput.Any(b =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)) &&
                !expctedOutput.Any(b => !output.Any(a =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)), "Expected output not match");
        }
        [Test]
        public void WriteExtractedFile_ShouldWriteCorrectCsvFile()
        {
            // Arrange
            var records = new List<BarclaysOutputDto>
                                    {
                                        new BarclaysOutputDto
                                        {
                                            ISIN = "AE000C4SA5WO",
                                            CFICode = "AFICSX",
                                            Venue = "MEUR"
                                        },
                                        new BarclaysOutputDto
                                        {
                                            ISIN = "FL0GF001933L",
                                            CFICode = "HFICSX",
                                            Venue = "KDER"
                                        }
                                    };

            // Act
            _csvService.WriteExtractedFile(records, _outputFilePath);

            // Assert – Read the written file back
            using var reader = new StreamReader(_outputFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var output = csv.GetRecords<BarclaysOutputDto>().ToList();

            //Assert
            Assert.That(output.Count, Is.EqualTo(2), "Expected output count not match");
            Assert.True(output.Count == records.Count &&
                !output.Any(a => !records.Any(b =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)) &&
                !records.Any(b => !output.Any(a =>
                    a.ISIN == b.ISIN &&
                    a.CFICode == b.CFICode &&
                    a.Venue == b.Venue)), "Expected output not match");
        }
        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_outputFilePath))
                File.Delete(_outputFilePath);
            if (File.Exists(_inputFilePath))
                File.Delete(_inputFilePath);
        }
    }
}
