using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseOutdoorFood
{
    public class ExpenseOutdoorFoodInfo : ExpenseBaseInfo
    {
        public OutdoorFoodVendorInfo OutdoorFoodVendorInfo { get; set; } = new OutdoorFoodVendorInfo();
        public string BillImageURL { get; set; } = string.Empty;
    }
}
