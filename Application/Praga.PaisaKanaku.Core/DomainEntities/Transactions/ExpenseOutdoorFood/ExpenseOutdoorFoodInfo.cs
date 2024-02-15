using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseOutdoorFood
{
    public class ExpenseOutdoorFoodInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
        public MemberInfo ExpenseByInfo { get; set; }
        public OutdoorFoodVendorInfo OutdoorFoodVendorInfo { get; set; }
        public double ExpenseAmount { get; set; }
        public string BillImageURL { get; set; }
        public string Description { get; set; }
    }
}
