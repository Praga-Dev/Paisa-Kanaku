using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup
{
    public class MemberInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RelationshipTypeInfo RelationshipTypeInfo { get; set; } = new();
    }
}
