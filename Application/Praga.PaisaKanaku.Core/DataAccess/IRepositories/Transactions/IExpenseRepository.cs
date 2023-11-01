using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseRepository
    {
        Task<Response<List<ExpenseInfoDB>>> GetExpenseBaseInfoList(Guid loggedInUserId);
        Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfoDB expenseSaveInfoDb, Guid loggedInUserId);
        Task<Response<List<ExpenseReferenceDetailInfoDB>>> GetExpenseInfoList(Guid loggedInUserId);
        Task<Response<ExpenseReferenceDetailInfoDB>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteTempExpenseInfo(Guid tempExpenseInfoId, Guid loggedInUserId);
    }
}
