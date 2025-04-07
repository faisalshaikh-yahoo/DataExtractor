using ExtractorLib.Dto;
using ExtractorLib.Extractor.Interfaces;
using ExtractorLib.Extractor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExtractorLib.Extractor
{
    public class ExtractorServiceFactory : IExtractorServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ExtractorServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IExtractor GetExtractor(string type)
        {
            HashSet<string> validTypes = new HashSet<string> { "barclays", "hdfc" };
            if (type != null && !validTypes.Contains(type.ToLower()))
            {
                Console.WriteLine("Error : Bank Name not present");
                return null;
            }
            return type.ToLower() switch
            {
                "barclays" => _serviceProvider.GetRequiredService<BarclaysExtractor<BarclaysInputDto, BarclaysOutputDto>>(),
                "hdfc" => _serviceProvider.GetRequiredService<HdfcExtractor<HdfcInputDto, HdfcOutputDto>>(),
                _ => throw new ArgumentException($"Invalid Bank Name: {type}")
            };
        }
    }
}
