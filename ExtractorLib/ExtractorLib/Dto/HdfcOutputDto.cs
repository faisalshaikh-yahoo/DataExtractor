using ExtractorLib.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Dto
{
    class HdfcOutputDto : BaseOutputFormatDto
    {
        [OrderMap(3)]
        public string BankName { get; set; }
    }
}
