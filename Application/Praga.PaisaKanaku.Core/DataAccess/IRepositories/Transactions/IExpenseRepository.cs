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
        Task<Response<Guid>> SaveExpenseInfo(ExpenseReferenceDetailInfoDb expenseInfoDb, Guid loggedInUserId);
        Task<Response<List<ExpenseInfoDb>>> GetExpenseInfoList(Guid loggedInUserId);
        Task<Response<ExpenseInfoDb>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId);
    }
}
