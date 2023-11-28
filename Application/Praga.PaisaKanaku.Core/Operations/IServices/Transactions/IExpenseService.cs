using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseService
    {
        Task<Response<List<ExpenseInfo>>> GetExpenseBaseInfoList(Guid loggedInUserId);
        Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfo expenseSaveInfo, Guid loggedInUserId);
        Task<Response<string>> ExportExpenseInfoData(Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseByType(Guid Id, string ExpenseCategory, Guid loggedInUserId);
        

        Task<Response<List<ExpenseReferenceDetailInfo>>> GetExpenseInfoList(Guid loggedInUserId);
        Task<Response<ExpenseReferenceDetailInfo>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId);
    }
}
