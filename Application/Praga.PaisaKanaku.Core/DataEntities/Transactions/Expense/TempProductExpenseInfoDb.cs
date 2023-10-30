using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense
{
    public class TempProductExpenseInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
    }
}
