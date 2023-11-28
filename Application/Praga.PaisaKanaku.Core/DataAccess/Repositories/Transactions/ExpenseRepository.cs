using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ILogger<ExpenseRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ExpenseRepository(ILogger<ExpenseRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<List<ExpenseInfoDB>>> GetExpenseBaseInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseInfoDB>> response = new Response<List<ExpenseInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ExpenseInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }

        public async Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfoDB expenseSaveInfoDb, Guid loggedInUserId)
        {
            throw new NotImplementedException();           
        }

        public Task<Response<List<ExpenseReferenceDetailInfoDB>>> GetExpenseInfoList(Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ExpenseReferenceDetailInfoDB>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId)
        {
            throw new NotImplementedException();

        }

        public async Task<Response<Guid>> DeleteExpenseByType(Guid id, string expenseCategory, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_DELETE_BY_TYPE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", id, DbType.Guid);
                parameters.Add("@ExpenseType", expenseCategory, DbType.String);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);
                parameters.Add("@Result", null, DbType.Guid, direction: ParameterDirection.Output);

                var returnValue = await _db.Connection.QueryAsync<Guid>(spName, parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<Guid>("@Result");

                return Helpers.IsValidGuid(result) ? response.GetSuccessResponse(result) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.DeleteExpenseByType({@id}, {@expenseCategory})", id, expenseCategory, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }
    }
}
