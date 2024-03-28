using Praga.PaisaKanaku.Core.DTO.Transactions.Base;

namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseGrocery
{
    public class ExpenseGrocerySaveRequestDTO : ExpenseBaseInfoDTO
    {
        public Guid GroceryInfoId { get; set; }
        public string MeasureType { get; set; } = string.Empty;
        public float Quantity { get; set; }
    }
}
