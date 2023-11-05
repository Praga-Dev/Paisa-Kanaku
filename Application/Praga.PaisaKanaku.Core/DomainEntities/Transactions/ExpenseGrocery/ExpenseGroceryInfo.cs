using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseGrocery
{
    public class ExpenseGroceryInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
        public GroceryBaseInfo GroceryBaseInfo { get; set; }
        public MemberInfo ExpenseByInfo { get; set; }
        public int Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}
