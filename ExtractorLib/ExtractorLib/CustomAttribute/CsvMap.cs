using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvMap : Attribute
    {
        public string Column { get; }  // required
        public string ExtractColumnType { get; set; }    // optional in case of complex column

        public CsvMap(string column)
        {
            Column = column;
        }
    }
}
