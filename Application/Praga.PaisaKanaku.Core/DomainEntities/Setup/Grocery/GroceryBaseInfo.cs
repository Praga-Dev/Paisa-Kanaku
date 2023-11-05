using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery
{
    public class GroceryBaseInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
