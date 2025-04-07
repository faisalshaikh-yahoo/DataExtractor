using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.CustomAttribute.Services;
using ExtractorLib.Dto;
using ExtractorLib.Extractor.Interfaces;
using ExtractorLib.Extractor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExtractorLib.CustomAttribute
{
    public class ComplexFieldExtractorFactory : IComplexFieldExtractorFactory
    {

        private readonly IServiceProvider _serviceProvider;
        public ComplexFieldExtractorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IComplexFieldExtractor GetFieldExtractor(string type)
        {
            return type.ToLower() switch
            {
                "pricemultiplier" => _serviceProvider.GetRequiredService<ExtractContractSizeFromAlgoParams>(),
                "hdfcbankname" => _serviceProvider.GetRequiredService<ExtractHdfcBankName>(),
                _ => throw new ArgumentException($"Invalid Field type: {type}")
            };
        }
    }
}
