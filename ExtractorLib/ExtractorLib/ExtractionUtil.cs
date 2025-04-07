
using ExtractorLib.Extractor.Interfaces;

namespace ExtractorLib
{
    public class ExtractionUtil : IExtractionUtil
    {
        private readonly IExtractorServiceFactory _factory;

        public ExtractionUtil(IExtractorServiceFactory factory)
        {
            _factory = factory;
        }

        public void ExtractData(string bankName, string filePath)
        {
            var extractor = _factory.GetExtractor(bankName);
            if (extractor == null)
                return;
            extractor.Extract(filePath);
        }
    }
}
