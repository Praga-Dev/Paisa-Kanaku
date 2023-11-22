using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyFund;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyFund;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public interface IExpenseFamilyFundService
    {
        Task<Response<Guid>> SaveExpenseFamilyFundInfo(ExpenseFamilyFundSaveRequestDTO expenseFamilyFundSaveRequestDTO, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseFamilyFundInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseFamilyFundInfo>>> GetExpenseFamilyFundInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseFamilyFundInfo>> GetExpenseFamilyFundInfoById(Guid expenseFamilyFundInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteExpenseFamilyFundInfo(Guid expenseFamilyFundInfoId, Guid loggedInUserId);
    }
}
