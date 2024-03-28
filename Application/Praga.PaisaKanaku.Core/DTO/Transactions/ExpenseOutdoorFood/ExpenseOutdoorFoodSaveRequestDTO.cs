using Praga.PaisaKanaku.Core.DTO.Transactions.Base;

namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseOutdoorFood
{
    public class ExpenseOutdoorFoodSaveRequestDTO : ExpenseBaseInfoDTO
    {
        public Guid OutdoorFoodVendorInfoId { get; set; }
        public string BillImageURL { get; set; } = string.Empty;
    }
}
