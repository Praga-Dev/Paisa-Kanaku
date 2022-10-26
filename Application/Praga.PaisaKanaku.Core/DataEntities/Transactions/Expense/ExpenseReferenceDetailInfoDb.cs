using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense
{
    public class ExpenseReferenceDetailInfoDb : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public Guid ExpenseBy { get; set; }
        public DateTime DateOfExpense { get; set; }
        public string? ExpenseDescription { get; set; }
        public string? ExpenseType { get; set; }
        public string? ExpenseTypeValue { get; set; }
        public Guid ReferenceId { get; set; }
        public double ExpenseAmount { get; set; }

        #region Product

        public bool IsChangeInProduct { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public Guid BrandId { get; set; }
        public string? BrandName { get; set; }
        public double ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public string? PreferredRecurringTimePeriod { get; set; }
        public string? PreferredRecurringTimePeriodValue { get; set; }

        #endregion
    }
}
