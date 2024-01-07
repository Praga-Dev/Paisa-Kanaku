using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseOutdoorFood
{
    public class ExpenseOutdoorFoodInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseById { get; set; }
        public string ExpenseByName { get; set; }
        public Guid OutdoorFoodVendorId { get; set; }
        public string OutdoorFoodVendorName { get; set; }
        public double ExpenseAmount { get; set; }
        public string BillImageURL { get; set; }
        public string Description { get; set; }
    }
}
