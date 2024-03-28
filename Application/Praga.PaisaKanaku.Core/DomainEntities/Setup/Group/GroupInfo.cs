using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup.Group
{
    public class GroupInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<MemberInfoDB> MemberInfoDB { get; set; } = new();
    }
}
