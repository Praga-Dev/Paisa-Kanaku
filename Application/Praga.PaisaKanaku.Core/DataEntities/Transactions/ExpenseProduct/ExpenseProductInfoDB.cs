using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseProduct
{
    public class ExpenseProductInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ProductInfoId { get; set; }
        public string ProductInfoName { get; set; }
        public Guid ExpenseById { get; set; }
        public string ExpenseByName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}
