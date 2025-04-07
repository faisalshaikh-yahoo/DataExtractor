using ExtractorLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Extractor.Interfaces
{
    public interface IExtractorServiceFactory
    {
        IExtractor GetExtractor(string type);
    }
}
