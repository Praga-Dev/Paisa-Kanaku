using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseProduct;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseProductService
    {
        Task<Response<Guid>> SaveExpenseProductInfo(ExpenseProductSaveRequestDTO expenseProductSaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseProductInfoSumAmountByDate>>> GetExpenseProductInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseProductInfo>>> GetExpenseProductInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseProductInfo>> GetExpenseProductInfoById(Guid expenseProductInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseProductInfo(Guid expenseProductInfoId, Guid loggedInUserId);
    }
}
