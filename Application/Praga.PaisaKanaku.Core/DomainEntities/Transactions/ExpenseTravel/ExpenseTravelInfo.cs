using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseTravel
{
    public class ExpenseTravelInfo : ExpenseBaseInfo
    {
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; } = DateTime.UtcNow;
        public TransportModeInfo TransportModeInfo { get; set; } = new();
        public TravelServiceInfo TravelServiceInfo { get; set; } = new();
    }
}
