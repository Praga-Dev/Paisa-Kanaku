using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup
{
    public class UtilityInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ConsumerTypeInfo ConsumerType { get; set; } = new();
        public TimePeriodTypeInfo RecurringType { get; set; } = new();
        public bool IsEssential { get; set; }
    }
}
