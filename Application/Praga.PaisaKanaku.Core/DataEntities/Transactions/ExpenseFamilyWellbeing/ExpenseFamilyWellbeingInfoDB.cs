using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyWellbeing
{
    public class ExpenseFamilyWellbeingInfoDB : ExpenseBaseInfo
    {
        public Guid RecipientId { get; set; }
        public string RecipientName { get; set; } = string.Empty;
    }
}
