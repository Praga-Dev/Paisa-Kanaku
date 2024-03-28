using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common
{
    public class ExpenseBaseInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
        public MemberInfo ExpenseByInfo { get; set; } = new();
        public double ExpenseAmount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
