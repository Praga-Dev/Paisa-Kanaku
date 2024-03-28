namespace Praga.PaisaKanaku.Core.DTO.Transactions.Base
{
    public class ExpenseBaseInfoDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseByInfoId { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
