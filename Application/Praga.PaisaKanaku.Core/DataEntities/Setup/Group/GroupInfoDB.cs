using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Setup.Group
{
    public class GroupInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<MemberInfoDB> MemberInfoDB { get; set; } = new();
    }
}