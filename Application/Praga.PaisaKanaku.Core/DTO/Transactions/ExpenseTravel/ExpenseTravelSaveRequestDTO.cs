namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseTravel
{
    public class ExpenseTravelSaveRequestDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseByInfoId { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
        public string TransportMode { get; set; } = string.Empty;
        public string TravelService { get; set; } = string.Empty;
        public double ExpenseAmount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
