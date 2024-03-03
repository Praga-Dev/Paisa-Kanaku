using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseTravel;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseTravelService
    {
        Task<Response<Guid>> SaveExpenseTravelInfo(ExpenseTravelSaveRequestDTO expenseTravelSaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseTravelInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseTravelInfo>>> GetExpenseTravelInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseTravelInfo>> GetExpenseTravelInfoById(Guid expenseTravelInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseTravelInfo(Guid expenseTravelInfoId, Guid loggedInUserId);
    }
}
