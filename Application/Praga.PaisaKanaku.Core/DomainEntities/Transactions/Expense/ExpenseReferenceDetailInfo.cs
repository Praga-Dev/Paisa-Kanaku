using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseReferenceDetailInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime DateOfExpense { get; set; } = DateTime.UtcNow;
        public double ExpenseAmount { get; set; }
        public string? Description { get; set; }
        public MemberInfo ExpenseBy { get; set; } = new();
        public ExpenseTypeInfo ExpenseTypeInfo { get; set; } = new();
        public ProductInfo ProductInfo { get; set; } = new();
        public int Quantity { get; set; }
    }
}
