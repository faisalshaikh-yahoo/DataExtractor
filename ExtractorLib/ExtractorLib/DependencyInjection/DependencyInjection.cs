using ExtractorLib.CsvHelperService;
using ExtractorLib.CustomAttribute;
using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.CustomAttribute.Services;
using ExtractorLib.Dto;
using ExtractorLib.Extractor;
using ExtractorLib.Extractor.Interfaces;
using ExtractorLib.Extractor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExtractorLib.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExtractionServices(this IServiceCollection services)
        {
            //register bank extractor
            services.AddScoped<BarclaysExtractor<BarclaysInputDto, BarclaysOutputDto>>();
            services.AddScoped<HdfcExtractor<HdfcInputDto, HdfcOutputDto>>();

            //register complex field extractor
            services.AddScoped<ExtractContractSizeFromAlgoParams>();
            services.AddScoped<ExtractHdfcBankName>();

            //register factory
            services.AddScoped<IExtractorServiceFactory, ExtractorServiceFactory>();
            services.AddScoped<IComplexFieldExtractorFactory, ComplexFieldExtractorFactory>();

            services.AddScoped<IExtractionUtil, ExtractionUtil>();
            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<IPropertyMapService, PropertyMapService>();

            return services;
        }
    }
}
