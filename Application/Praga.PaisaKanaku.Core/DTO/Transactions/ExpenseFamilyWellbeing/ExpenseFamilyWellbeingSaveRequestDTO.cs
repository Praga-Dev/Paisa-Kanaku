using Praga.PaisaKanaku.Core.DTO.Transactions.Base;

namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyWellbeing
{
    public class ExpenseFamilyWellbeingSaveRequestDTO : ExpenseBaseInfoDTO
    {
        public Guid RecipientInfoId { get; set; }
    }
}
