using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseGrocery
{
    public class ExpenseGroceryInfo : ExpenseBaseInfo
    {
        public GroceryBaseInfo GroceryBaseInfo { get; set; } = new();
        public MeasureTypeInfo MeasureTypeInfo { get; set; } = new();
        public float Quantity { get; set; } = 1;
    }
}
