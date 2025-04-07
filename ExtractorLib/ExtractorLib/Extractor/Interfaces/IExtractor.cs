using ExtractorLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Extractor.Interfaces
{
    public interface IExtractor
    {
        void Extract(string inputFile);
    } // marker interface
    public interface IExtractor<TInput, TOutput> : IExtractor
        where TInput : BaseInputFormatDto
        where TOutput : BaseOutputFormatDto, new()
    {
    }
}
