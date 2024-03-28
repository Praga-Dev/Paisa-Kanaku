using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;


namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseUtility
{
    public class ExpenseUtilityInfoDB : ExpenseBaseInfo
    {
        public Guid UtilityId { get; set; }
        public string UtilityName { get; set; } = string.Empty;
        public Guid ConsumerId { get; set; }
        public string ConsumerName { get; set; } = string.Empty;
        public string ConsumerType { get; set; } = string.Empty;
        public string ConsumerTypeValue { get; set; } = string.Empty;
        public string ServiceDuration { get; set; } = string.Empty;
        public string ServiceDurationValue { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
