using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyFund
{
    public class ExpenseFamilyFundInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseById { get; set; }
        public string ExpenseByName { get; set; }
        public Guid RecipientId { get; set; }
        public string RecipientName { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}
