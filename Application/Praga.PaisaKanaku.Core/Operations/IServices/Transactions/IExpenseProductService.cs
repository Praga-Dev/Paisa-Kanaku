using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseProductService
    {
        Task<Response<Guid>> SaveExpenseProductInfo(ExpenseProductInfo expenseProductInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseProductInfoSumAmountByDate>>> GetExpenseProductInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseProductInfo>>> GetExpenseProductInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseProductInfo>> GetExpenseProductInfoById(Guid expenseProductInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseProductInfo(Guid expenseProductInfoId, Guid loggedInUserId);
    }
}
