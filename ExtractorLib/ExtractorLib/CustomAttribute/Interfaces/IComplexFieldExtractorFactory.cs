using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.CustomAttribute.Interfaces
{
    public interface IComplexFieldExtractorFactory
    {
        IComplexFieldExtractor GetFieldExtractor(string type);
    }
}
