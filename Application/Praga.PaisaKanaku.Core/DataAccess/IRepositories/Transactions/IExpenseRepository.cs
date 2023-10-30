using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions
{
    public interface IExpenseRepository
    {
        Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfoDB expenseSaveInfoDb, Guid loggedInUserId);
        Task<Response<List<ExpenseReferenceDetailInfoDB>>> GetExpenseInfoList(Guid loggedInUserId);
        Task<Response<ExpenseReferenceDetailInfoDB>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoDB>>> GetExpenseBaseInfoList(Guid loggedInUserId);
        Task<Response<Guid>> SaveTempProductExpenseInfo(TempProductExpenseInfoDB tempProductExpenseInfoDb, Guid loggedInUserId);
        Task<Response<List<TempProductExpenseInfoDB>>> GetTempProductExpenseInfo(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<TempProductExpenseInfoDB>> GetTempProductExpenseInfoById(Guid tempExpenseInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteTempExpenseInfo(Guid tempExpenseInfoId, Guid loggedInUserId);

    }
}
