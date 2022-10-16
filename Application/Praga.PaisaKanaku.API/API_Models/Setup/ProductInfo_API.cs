using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.API.API_Models.Setup
{
    public class ProductInfo_API
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ProductCategoryInfo_API ProductCategoryInfo_API { get; set; }
        public BrandInfo_API BrandInfo_API { get; set; }
        public ExpenseTypeInfo ExpenseTypeInfo { get; set; }
        public TimePeriodTypeInfo PreferredRecurringTimePeriodInfo { get; set; }
    }
}
