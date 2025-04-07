using ExtractorLib.CsvHelperService;
using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.Dto;
using ExtractorLib.Extractor.Interfaces;
using ExtractorLib.Helper;
using System.Text.RegularExpressions;

namespace ExtractorLib.Extractor.Services
{
    public class HdfcExtractor<TInput, TOutput> : IExtractor<TInput, TOutput>
    where TInput : BaseInputFormatDto
    where TOutput : BaseOutputFormatDto, new()
    {
        private readonly ICsvService _csvService;
        private readonly IPropertyMapService _propertyMapService;
        public HdfcExtractor(ICsvService csvService, IPropertyMapService propertyMapService)
        {
            _csvService = csvService;
            _propertyMapService = propertyMapService;
        }
        const int skipLines = 1;
        public void Extract(string inputFile)
        {

            try
            {
                var inputList = _csvService.PreProcessAndReadFile<TInput>(inputFile, skipLines).DistinctBy(t => t.ISIN).ToList();
                var validatedInputList = ValidateCustomDtoHelper.ValidateFileData(inputList);
                var outputList = _propertyMapService.MapProperties<TInput, TOutput>(validatedInputList);

                if (outputList.Count() > 0)
                {
                    var outputPath = Path.GetFileNameWithoutExtension(inputFile) + "_output.csv";
                    _csvService.WriteExtractedFile(outputList, outputPath);
                }
                else
                    Console.WriteLine("Output file is not present");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
