namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseOutdoorFood
{
    public class ExpenseOutdoorFoodSaveRequestDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseByInfoId { get; set; }
        public Guid OutdoorFoodVendorInfoId { get; set; }
        public double ExpenseAmount { get; set; }
        public string BillImageURL { get; set; }
        public string Description { get; set; }
    }
}
