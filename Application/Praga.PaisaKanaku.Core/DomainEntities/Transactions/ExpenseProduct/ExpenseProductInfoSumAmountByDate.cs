using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct
{
    /// <summary>
    /// Used for Calendar View Data
    /// </summary>
    public class ExpenseProductInfoSumAmountByDate : BaseInfo
    {
        public DateTime ExpenseDate { get; set; }
        public double TotalExpenseAmount { get; set; }
    }
}
