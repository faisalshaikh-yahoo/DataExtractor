using ExtractorLib.Dto;

namespace ExtractorLib.CsvHelperService
{
    public interface ICsvService
    {
        List<T> PreProcessAndReadFile<T>(string inputFilePath, int skipLines) where T : BaseInputFormatDto;
        void WriteExtractedFile<T>(List<T> records, string outputPath) where T : BaseOutputFormatDto;
    }
}