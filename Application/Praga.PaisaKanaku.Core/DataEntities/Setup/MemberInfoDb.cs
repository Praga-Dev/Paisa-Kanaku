using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Setup
{
    public class MemberInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RelationshipType { get; set; }
        public string RelationshipTypeValue { get; set; }
    }
}
