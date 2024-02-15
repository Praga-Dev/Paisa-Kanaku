namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseItemBaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseById { get; set; }

        public int Quantity { get; set; } = 1;

        /// <summary>
        /// The expense amount is the amount spent for the product. i.e expenseAmount is equal to (productAmount * Quantity)
        /// </summary>
        public double ExpenseAmount { get; set; } 
        
        public string? Description { get; set; }
    }
}
