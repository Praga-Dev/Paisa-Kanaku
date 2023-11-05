using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery
{
    public class GroceryInfo : GroceryBaseInfo
    {
        public GroceryCategoryInfo GroceryCategoryInfo { get; set; } = new();
        public BrandInfo BrandInfo { get; set; } = new();
        public MetricSystemInfo MetricSystemInfo { get; set; }
        public MeasureTypeInfo MeasureTypeInfo { get; set; }
        public TimePeriodTypeInfo PreferredTimePeriodInfo { get; set; } = new();
    }
}
