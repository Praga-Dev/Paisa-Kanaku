using Praga.PaisaKanaku.Core.DTO.Transactions.Base;

namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseUtility
{
    public class ExpenseUtilitySaveRequestDTO : ExpenseBaseInfoDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid UtilityInfoId { get; set; }
        public Guid ConsumerInfoId { get; set; }
        public string ConsumerType { get; set; } = string.Empty;
        public string ServiceDuration { get; set; } = string.Empty;
    }
}
