using CsvHelper.Configuration;
using ExtractorLib.CustomAttribute;
using System.Reflection;

namespace ExtractorLib.Extractor
{
    public class OrderCsvMap<T> : ClassMap<T>
    {
        public OrderCsvMap()
        {
            var props = typeof(T).GetProperties()
                .Select(p => new
                {
                    Property = p,
                    Attr = p.GetCustomAttribute<OrderMap>()
                })
                .Where(x => x.Attr != null)
                .OrderBy(x => x.Attr.Order);

            foreach (var prop in props)
            {
                var map = Map(typeof(T), prop.Property);
                map.Index(prop.Attr.Order); // Optional: usually ordering by Index works automatically when using .WriteRecords
            }
        }
    }
}
