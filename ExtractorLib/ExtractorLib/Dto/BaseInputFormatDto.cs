using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Dto
{
    public abstract class BaseInputFormatDto
    {
        public string ISIN { get; set; }
        public string CFICode { get; set; }
        public string Venue { get; set; }

        public abstract bool IsValid(out List<string> errors);

    }
}
