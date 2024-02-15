using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common
{
    public class ExpenseInfoSumAmountByDate : BaseInfo
    {
        public DateTime ExpenseDate { get; set; }
        public double TotalExpenseAmount { get; set; }
    }
}
