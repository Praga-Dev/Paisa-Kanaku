using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseProduct
{
    public class ExpenseProductInfoDB : ExpenseBaseInfo
    {
        public Guid ProductInfoId { get; set; }
        public string ProductInfoName { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
