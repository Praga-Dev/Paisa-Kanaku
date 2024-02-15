using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseOutdoorFood;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseOutdoorFoodRepository
    {
        Task<Response<Guid>> SaveExpenseOutdoorFoodInfoDB(ExpenseOutdoorFoodInfoDB expenseOutdoorFoodInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseOutdoorFoodInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseOutdoorFoodInfoDB>>> GetExpenseOutdoorFoodInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseOutdoorFoodInfoDB>> GetExpenseOutdoorFoodInfoById(Guid expenseOutdoorFoodInfoId, Guid loggedInUserId);
    }
}
