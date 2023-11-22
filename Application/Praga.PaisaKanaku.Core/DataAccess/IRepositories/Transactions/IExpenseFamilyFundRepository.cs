using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyFund;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseFamilyFundRepository
    {
        Task<Response<Guid>> SaveExpenseFamilyFundInfoDB(ExpenseFamilyFundInfoDB expenseFamilyFundInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseFamilyFundInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseFamilyFundInfoDB>>> GetExpenseFamilyFundInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseFamilyFundInfoDB>> GetExpenseFamilyFundInfoById(Guid expenseFamilyFundInfoId, Guid loggedInUserId);
    }
}
