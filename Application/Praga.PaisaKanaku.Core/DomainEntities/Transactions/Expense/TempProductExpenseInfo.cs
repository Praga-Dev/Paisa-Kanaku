using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class TempProductExpenseInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public MemberInfo ExpenseBy { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string? Description { get; set; }
        public ProductInfo? ProductInfo { get; set; }
    }
}
