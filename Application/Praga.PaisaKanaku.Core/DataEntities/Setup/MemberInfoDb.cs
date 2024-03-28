using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Setup
{
    public class MemberInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RelationshipType { get; set; } = string.Empty;
        public string RelationshipTypeValue { get; set; } = string.Empty;
    }
}
