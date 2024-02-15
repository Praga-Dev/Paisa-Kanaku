namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseProduct
{
    public class ExpenseProductSaveRequestDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ProductInfoId { get; set; }
        public Guid ExpenseByInfoId { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}
