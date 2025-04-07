using ExtractorLib.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Dto
{
    public class BarclaysOutputDto : BaseOutputFormatDto
    {
        [OrderMap(3)]
        public string ContractSize { get; set; }
    }
}
