using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Product;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseReferenceDetailInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
        public Guid ReferenceId { get; set; }
        public double ExpenseAmount { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public MemberInfo ExpenseBy { get; set; } = new();
        public ProductInfo ProductInfo { get; set; } = new();
    }
}
