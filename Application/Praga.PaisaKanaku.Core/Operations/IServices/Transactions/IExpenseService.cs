using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseService
    {
        Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfo expenseSaveInfo, Guid loggedInUserId);
        Task<Response<List<ExpenseReferenceDetailInfo>>> GetExpenseInfoList(Guid loggedInUserId);
        Task<Response<string>> ExportExpenseInfoData(Guid loggedInUserId);
        Task<Response<ExpenseReferenceDetailInfo>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId);
        Task<Response<List<ExpenseInfo>>> GetExpenseBaseInfoList(Guid loggedInUserId);
        Task<Response<Guid>> SaveTempExpenseInfo(TempProductExpenseInfo tempProductExpenseInfo, Guid loggedInUserId);
        Task<Response<List<TempProductExpenseInfo>>> GetTempExpenseInfo(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseReferenceDetailInfo>> GetTempExpenseInfoById(Guid tempExpenseInfoId, Guid loggedInUserId);
    }
}
