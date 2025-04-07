using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractorLib.CustomAttribute.Services
{
    public class ExtractContractSizeFromAlgoParams : IComplexFieldExtractor
    {
        private readonly IPropertyMapService _propertyMapService;
        public ExtractContractSizeFromAlgoParams(IPropertyMapService propertyMapService)
        {
            _propertyMapService = propertyMapService;
        }

        public void ExtractComplexField<TInput, TOutput>(TInput input, TOutput output) 
            where TInput : BaseInputFormatDto
            where TOutput : BaseOutputFormatDto
        {
            var value = _propertyMapService.GetProperty<TInput>(input, "AlgoParams").ToString();
            var match = Regex.Match(value, @"PriceMultiplier:(?<value>[^|]+)");
            if (match.Success)
                _propertyMapService.SetProperty<TOutput>(output, "ContractSize", match.Groups["value"].Value);
        }
    }
}
