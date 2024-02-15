using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseGrocery;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseGroceryRepository
    {
        Task<Response<Guid>> SaveExpenseGroceryInfoDB(ExpenseGroceryInfoDB expenseGroceryInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseGroceryInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseGroceryInfoDB>>> GetExpenseGroceryInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseGroceryInfoDB>> GetExpenseGroceryInfoById(Guid expenseGroceryInfoId, Guid loggedInUserId);
    }
}
