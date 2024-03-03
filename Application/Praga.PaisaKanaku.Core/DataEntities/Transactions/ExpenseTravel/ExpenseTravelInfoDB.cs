using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseTravel
{
    public class ExpenseTravelInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseById { get; set; }
        public string ExpenseByName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
        public double ExpenseAmount { get; set; }
        public string TransportMode { get; set; } = string.Empty;
        public string TransportModeValue { get; set; } = string.Empty;
        public string TravelService { get; set; } = string.Empty;
        public string TravelServiceValue { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
