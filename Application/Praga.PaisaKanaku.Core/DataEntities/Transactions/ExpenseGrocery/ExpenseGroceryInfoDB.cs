using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseGrocery
{
    public class ExpenseGroceryInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid GroceryInfoId { get; set; }
        public string GroceryInfoName { get; set; }
        public Guid ExpenseById { get; set; }
        public string ExpenseByName { get; set; }
        public string MeasureType { get; set; }
        public string MeasureTypeValue { get; set; }
        public float Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}
