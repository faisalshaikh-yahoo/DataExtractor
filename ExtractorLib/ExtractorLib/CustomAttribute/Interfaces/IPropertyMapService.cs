using ExtractorLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.CustomAttribute.Interfaces
{
    public interface IPropertyMapService
    {
        public List<TOutput> MapProperties<TInput, TOutput>(List<TInput> inputRecords)
            where TInput : BaseInputFormatDto
            where TOutput : BaseOutputFormatDto, new();
        void SetProperty<T>(T dto, string propertyName, object value) where T : BaseOutputFormatDto;
        object? GetProperty<T>(T dto, string propertyName) where T : BaseInputFormatDto;
    }
}
