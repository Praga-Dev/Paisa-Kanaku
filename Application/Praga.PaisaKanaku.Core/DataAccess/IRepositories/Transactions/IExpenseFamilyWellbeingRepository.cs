using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyWellbeing;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseFamilyWellbeingRepository
    {
        Task<Response<Guid>> SaveExpenseFamilyWellbeingInfoDB(ExpenseFamilyWellbeingInfoDB expenseFamilyWellbeingInfoDB, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseFamilyWellbeingInfoListByMonth(int month, int year, Guid loggedInUserId);
        Task<Response<List<ExpenseFamilyWellbeingInfoDB>>> GetExpenseFamilyWellbeingInfoListByDate(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<ExpenseFamilyWellbeingInfoDB>> GetExpenseFamilyWellbeingInfoById(Guid expenseFamilyWellbeingInfoId, Guid loggedInUserId);
    }
}
