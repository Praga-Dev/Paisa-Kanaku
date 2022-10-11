using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
