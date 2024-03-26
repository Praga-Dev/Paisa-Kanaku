using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseGrocery
{
    public class ExpenseGroceryInfoDB : ExpenseBaseInfo
    {
        public Guid GroceryInfoId { get; set; }
        public string GroceryInfoName { get; set; } = string.Empty;
        public string MeasureType { get; set; } = string.Empty;
        public string MeasureTypeValue { get; set; } = string.Empty;
        public float Quantity { get; set; }
    }
}
