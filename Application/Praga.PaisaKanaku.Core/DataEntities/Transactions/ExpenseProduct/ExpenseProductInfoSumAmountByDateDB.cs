using Praga.PaisaKanaku.Core.Common.Model;


namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseProduct
{
    /// <summary>
    /// Used for Calendar View Data
    /// </summary>
    public class ExpenseProductInfoSumAmountByDateDB : BaseInfo
    {
        public DateTime ExpenseDate { get; set; }
        public double TotalExpenseAmount { get; set; }
    }
}
