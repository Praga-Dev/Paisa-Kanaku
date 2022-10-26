using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup
{
    public class ProductInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public bool IsChangeInProduct { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public ProductCategoryInfo ProductCategoryInfo { get; set; }
        public BrandInfo BrandInfo { get; set; }
        public ExpenseTypeInfo ExpenseTypeInfo { get; set; }
        public TimePeriodTypeInfo PreferredTimePeriodInfo { get; set; }
    }
}
