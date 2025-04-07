using ExtractorLib.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Dto
{
    public class BaseOutputFormatDto
    {
        [OrderMap(0)]
        public string ISIN { get; set; }
        [OrderMap(1)]
        public string CFICode { get; set; }
        [OrderMap(2)]
        public string Venue { get; set; }
    }
}
