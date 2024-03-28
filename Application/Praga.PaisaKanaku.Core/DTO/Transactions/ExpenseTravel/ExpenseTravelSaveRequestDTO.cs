using Praga.PaisaKanaku.Core.DTO.Transactions.Base;

namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseTravel
{
    public class ExpenseTravelSaveRequestDTO : ExpenseBaseInfoDTO
    {
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
        public string TransportMode { get; set; } = string.Empty;
        public string TravelService { get; set; } = string.Empty;
    }
}
