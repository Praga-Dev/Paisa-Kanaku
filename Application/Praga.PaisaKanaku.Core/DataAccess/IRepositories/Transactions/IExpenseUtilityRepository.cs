using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseUtility;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseUtilityRepository
    {
        Task<Response<Guid>> SaveExpenseUtilityInfoDB(ExpenseUtilityInfoDB expenseUtilityInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseUtilityInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseUtilityInfoDB>>> GetExpenseUtilityInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseUtilityInfoDB>> GetExpenseUtilityInfoById(Guid expenseUtilityInfoId, Guid loggedInUserId);
    }
}
