using Praga.PaisaKanaku.Core.DomainEntities.Setup.Product;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct
{
    public class ExpenseProductInfo : ExpenseBaseInfo
    {
        public double ProductPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public ProductBaseInfo ProductBaseInfo { get; set; } = new();
    }
}
