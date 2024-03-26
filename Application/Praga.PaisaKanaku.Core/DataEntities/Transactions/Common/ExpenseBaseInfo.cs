using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.Common
{
    public class ExpenseBaseInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double ExpenseAmount { get; set; }
        public Guid ExpenseById { get; set; }
        public string ExpenseByName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
