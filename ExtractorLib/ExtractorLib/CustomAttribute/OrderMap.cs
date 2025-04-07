
namespace ExtractorLib.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OrderMap : Attribute
    {
        public int Order { get; }  // required

        public OrderMap(int order)
        {
            Order = order;
        }
    }
}
