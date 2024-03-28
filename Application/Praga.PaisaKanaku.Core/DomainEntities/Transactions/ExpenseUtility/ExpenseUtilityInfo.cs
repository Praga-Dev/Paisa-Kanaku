using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseUtility
{
    public class ExpenseUtilityInfo : ExpenseBaseInfo
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public ConsumerInfo ConsumerInfo { get; set; } = new();
        public ConsumerTypeInfo ConsumerTypeInfo { get; set; } = new();
        public TimePeriodTypeInfo ServiceDuration { get; set; } = new();
        public UtilityInfo UtilityInfo { get; set; } = new();
    }
}
