using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseProduct;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseProductRepository
    {
        Task<Response<Guid>> SaveExpenseProductInfoDB(ExpenseProductInfoDB expenseProductInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseProductInfoSumAmountByDateDB>>> GetExpenseProductInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseProductInfoDB>>> GetExpenseProductInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseProductInfoDB>> GetExpenseProductInfoById(Guid expenseProductInfoId, Guid loggedInUserId);
    }
}
