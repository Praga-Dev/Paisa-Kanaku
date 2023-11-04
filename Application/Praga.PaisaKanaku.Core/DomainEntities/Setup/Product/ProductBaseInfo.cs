using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup.Product
{
    public class ProductBaseInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}
