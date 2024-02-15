using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseOutdoorFood;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseOutdoorFood;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseOutdoorFoodService
    {
        Task<Response<Guid>> SaveExpenseOutdoorFoodInfo(ExpenseOutdoorFoodSaveRequestDTO expenseOutdoorFoodSaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseOutdoorFoodInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseOutdoorFoodInfo>>> GetExpenseOutdoorFoodInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseOutdoorFoodInfo>> GetExpenseOutdoorFoodInfoById(Guid expenseOutdoorFoodInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseOutdoorFoodInfo(Guid expenseOutdoorFoodInfoId, Guid loggedInUserId);
    }
}
