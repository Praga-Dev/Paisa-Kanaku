using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    internal class ExpenseReferenceDetailInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public ExpenseTypeInfo ExpenseTypeInfo { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime DateOfExpense { get; set; }
        public double ExpenseAmount { get; set; }
        public string? Description { get; set; }
    }
}
