namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseGrocery
{
    public class ExpenseGrocerySaveRequestDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid GroceryInfoId { get; set; }
        public Guid ExpenseByInfoId { get; set; }
        public int Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}
