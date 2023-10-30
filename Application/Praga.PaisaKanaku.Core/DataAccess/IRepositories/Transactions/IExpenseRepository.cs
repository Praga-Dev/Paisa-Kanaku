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
        Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfoDb expenseSaveInfoDb, Guid loggedInUserId);
        Task<Response<List<ExpenseReferenceDetailInfoDb>>> GetExpenseInfoList(Guid loggedInUserId);
        Task<Response<ExpenseReferenceDetailInfoDb>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoDb>>> GetExpenseBaseInfoList(Guid loggedInUserId);
        Task<Response<Guid>> SaveTempProductExpenseInfo(TempProductExpenseInfoDb tempProductExpenseInfoDb, Guid loggedInUserId);
        Task<Response<List<TempProductExpenseInfoDb>>> GetTempProductExpenseInfo(DateTime expenseDate, Guid loggedInUserId);
        Task<Response<TempProductExpenseInfoDb>> GetTempProductExpenseInfoById(Guid tempExpenseInfoId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteTempExpenseInfo(Guid tempExpenseInfoId, Guid loggedInUserId);

    }
}
