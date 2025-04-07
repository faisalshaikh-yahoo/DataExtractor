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
    public class ExtractHdfcBankName : IComplexFieldExtractor
    {
        private readonly IPropertyMapService _propertyMapService;
        public ExtractHdfcBankName(IPropertyMapService propertyMapService)
        {
            _propertyMapService = propertyMapService;
        }

        public void ExtractComplexField<TInput, TOutput>(TInput input, TOutput output) 
            where TInput : BaseInputFormatDto
            where TOutput : BaseOutputFormatDto
        {
            //Complex field logic 
            _propertyMapService.SetProperty<TOutput>(output, "BankName", "HDFC");
        }
    }
}
