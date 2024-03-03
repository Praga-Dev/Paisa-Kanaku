using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseTravel
{
    public class ExpenseTravelInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
        public MemberInfo ExpenseByInfo { get; set; } = new();
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; } = DateTime.UtcNow;
        public double ExpenseAmount { get; set; }
        public TransportModeInfo TransportModeInfo { get; set; } = new();
        public TravelServiceInfo TravelServiceInfo { get; set; } = new();
        public string Description { get; set; } = string.Empty;
    }
}
