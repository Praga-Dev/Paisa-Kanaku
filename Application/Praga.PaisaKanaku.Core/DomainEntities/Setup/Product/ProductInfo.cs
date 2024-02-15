using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup.Product
{
    public class ProductInfo : ProductBaseInfo
    {
        public string? Description { get; set; }
        public ProductCategoryInfo ProductCategoryInfo { get; set; } = new();
        public BrandInfo BrandInfo { get; set; } = new();
        public TimePeriodTypeInfo PreferredTimePeriodInfo { get; set; } = new();
    }
}
