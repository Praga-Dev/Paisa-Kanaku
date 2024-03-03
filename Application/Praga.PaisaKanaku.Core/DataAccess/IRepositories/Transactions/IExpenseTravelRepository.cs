using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseTravel;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseTravelRepository
    {
        Task<Response<Guid>> SaveExpenseTravelInfoDB(ExpenseTravelInfoDB expenseTravelInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseTravelInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseTravelInfoDB>>> GetExpenseTravelInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseTravelInfoDB>> GetExpenseTravelInfoById(Guid expenseTravelInfoId, Guid loggedInUserId);
    }
}
