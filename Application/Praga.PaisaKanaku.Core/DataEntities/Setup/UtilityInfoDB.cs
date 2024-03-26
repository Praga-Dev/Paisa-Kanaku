using Praga.PaisaKanaku.Core.Common.Model;
using System.Diagnostics.CodeAnalysis;

namespace Praga.PaisaKanaku.Core.DataEntities.Setup
{
    public class UtilityInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ConsumerType { get; set; } = string.Empty;
        public string ConsumerTypeValue { get; set; } = string.Empty;
        public string RecurringType { get; set; } = string.Empty;
        public string RecurringTypeValue { get; set; } = string.Empty;
        public bool IsEssential { get; set; }
    }
}
