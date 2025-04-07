using ExtractorLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.CustomAttribute.Interfaces
{
    public interface IComplexFieldExtractor
    {
        void ExtractComplexField<TInput, TOutput>(TInput input, TOutput output)
            where TInput : BaseInputFormatDto
            where TOutput : BaseOutputFormatDto;
    }
}
