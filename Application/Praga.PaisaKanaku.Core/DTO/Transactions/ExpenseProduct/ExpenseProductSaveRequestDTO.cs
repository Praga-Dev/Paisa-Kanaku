using Praga.PaisaKanaku.Core.DTO.Transactions.Base;

namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseProduct
{
    public class ExpenseProductSaveRequestDTO : ExpenseBaseInfoDTO
    {
        public Guid ProductInfoId { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
