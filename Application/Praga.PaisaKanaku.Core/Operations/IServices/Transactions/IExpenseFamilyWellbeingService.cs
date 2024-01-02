using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyWellbeing;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyWellbeing;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseFamilyWellbeingService
    {
        Task<Response<Guid>> SaveExpenseFamilyWellbeingInfo(ExpenseFamilyWellbeingSaveRequestDTO expenseFamilyWellbeingSaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseFamilyWellbeingInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseFamilyWellbeingInfo>>> GetExpenseFamilyWellbeingInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseFamilyWellbeingInfo>> GetExpenseFamilyWellbeingInfoById(Guid expenseFamilyWellbeingInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseFamilyWellbeingInfo(Guid expenseFamilyWellbeingInfoId, Guid loggedInUserId);
    }
}
