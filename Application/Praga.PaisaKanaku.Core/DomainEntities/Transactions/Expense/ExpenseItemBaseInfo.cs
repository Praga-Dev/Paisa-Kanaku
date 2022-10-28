namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseItemBaseInfo
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; } = 1;

        /// <summary>
        /// The expense amount is the amount spent for the product. i.e expenseAmount is equalto (productAmount * Quantity)
        /// </summary>
        public double ExpenseAmount { get; set; } 
        
        public string? Description { get; set; }
    }
}
