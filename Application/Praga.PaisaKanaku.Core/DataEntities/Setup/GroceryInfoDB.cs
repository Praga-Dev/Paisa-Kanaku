using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Setup
{
    public class GroceryInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? GroceryCategory { get; set; }
        public string? GroceryCategoryValue { get; set; }
        public Guid BrandId { get; set; }
        public string? BrandName { get; set; }
        public string MetricSystem { get; set; }
        public string MetricSystemValue { get; set; }
        public string MeasureType { get; set; }
        public string MeasureTypeValue { get; set; }
        public string? PreferredRecurringTimePeriod { get; set; }
        public string? PreferredRecurringTimePeriodValue { get; set; }
    }
}
