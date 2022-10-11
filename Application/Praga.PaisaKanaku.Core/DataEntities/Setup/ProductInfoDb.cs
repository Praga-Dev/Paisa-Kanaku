using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Setup
{
    public class ProductInfoDb : BaseInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public Guid BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? ExpenseType { get; set; }
        public string? ExpenseTypeValue { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? PreferredRecurringTimePeriod { get; set; }
        public string? PreferredRecurringTimePeriodValue { get; set; }
    }
}
