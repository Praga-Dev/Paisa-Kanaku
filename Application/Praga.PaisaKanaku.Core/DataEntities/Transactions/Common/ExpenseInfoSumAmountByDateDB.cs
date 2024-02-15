using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.Common
{
    public class ExpenseInfoSumAmountByDateDB : BaseInfo
    {
        public DateTime ExpenseDate { get; set; }
        public double TotalExpenseAmount { get; set; }
    }
}