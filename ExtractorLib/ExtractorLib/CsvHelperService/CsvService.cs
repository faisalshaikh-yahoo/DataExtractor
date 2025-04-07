using CsvHelper;
using ExtractorLib.Dto;
using ExtractorLib.Extractor;
using System.Globalization;

namespace ExtractorLib.CsvHelperService
{
    public class CsvService : ICsvService
    {
        public List<T> PreProcessAndReadFile<T>(string inputFilePath, int skipLines) where T : BaseInputFormatDto
        {
            using var reader = new StreamReader(inputFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            //skip top extra lines
            for (int i = 0; i < skipLines; i++)
                reader.ReadLine();

            csv.Read();
            csv.ReadHeader();
            return csv.GetRecords<T>().ToList();
        } 

        public void WriteExtractedFile<T>(List<T> records, string outputPath) where T : BaseOutputFormatDto
        {
            using var writer = new StreamWriter(outputPath);
            using var csvOutput = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvOutput.Context.RegisterClassMap<OrderCsvMap<T>>();
            csvOutput.WriteRecords(records);
            Console.WriteLine("Output File name : " + outputPath);

        }
    }
}
