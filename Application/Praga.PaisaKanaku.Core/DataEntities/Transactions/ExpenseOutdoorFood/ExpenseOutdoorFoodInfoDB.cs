using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseOutdoorFood
{
    public class ExpenseOutdoorFoodInfoDB : ExpenseBaseInfo
    {
        public Guid OutdoorFoodVendorId { get; set; }
        public string OutdoorFoodVendorName { get; set; } = string.Empty;
        public string BillImageURL { get; set; } = string.Empty;
     }
}
