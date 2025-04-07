using ExtractorLib.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExtractorLib.Dto
{
    public class BarclaysInputDto : BaseInputFormatDto
    {
        [CsvMap("ContractSize", ExtractColumnType = "PriceMultiplier")]
        public string AlgoParams { get; set; }

        public override bool IsValid(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(AlgoParams) || !AlgoParams.Contains("PriceMultiplier"))
                errors.Add("AlgoParams not contain PriceMultiplier item");

            if(string.IsNullOrEmpty(ISIN))
                errors.Add("ISIN is empty");

            if (string.IsNullOrWhiteSpace(Venue))
                errors.Add("Venue is empty");

            if (string.IsNullOrWhiteSpace(CFICode))
                errors.Add("CFICode is empty");

            return errors.Count == 0;
        }
    }
}
