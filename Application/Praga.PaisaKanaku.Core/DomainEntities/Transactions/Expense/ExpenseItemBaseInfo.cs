namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseItemBaseInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? BrandName { get; set; }
        public int Quantity { get; set; }
    }
}
