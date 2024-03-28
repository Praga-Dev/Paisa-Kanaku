using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseUtility;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseUtility;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseUtilityService
    {
        Task<Response<Guid>> SaveExpenseUtilityInfo(ExpenseUtilitySaveRequestDTO expenseUtilitySaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseUtilityInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseUtilityInfo>>> GetExpenseUtilityInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseUtilityInfo>> GetExpenseUtilityInfoById(Guid expenseUtilityInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseUtilityInfo(Guid expenseUtilityInfoId, Guid loggedInUserId);
    }
}
