using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseTravel
{
    public class ExpenseTravelInfoDB : ExpenseBaseInfo
    {
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
        public string TransportMode { get; set; } = string.Empty;
        public string TransportModeValue { get; set; } = string.Empty;
        public string TravelService { get; set; } = string.Empty;
        public string TravelServiceValue { get; set; } = string.Empty;
    }
}
