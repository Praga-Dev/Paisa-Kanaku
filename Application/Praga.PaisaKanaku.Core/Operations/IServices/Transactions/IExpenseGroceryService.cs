using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseGrocery;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseGroceryService
    {
        Task<Response<Guid>> SaveExpenseGroceryInfo(ExpenseGrocerySaveRequestDTO expenseGrocerySaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseGroceryInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseGroceryInfo>>> GetExpenseGroceryInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseGroceryInfo>> GetExpenseGroceryInfoById(Guid expenseGroceryInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseGroceryInfo(Guid expenseGroceryInfoId, Guid loggedInUserId);
    }
}
