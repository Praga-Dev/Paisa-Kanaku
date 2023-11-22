using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
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

        public async Task<Response<List<ExpenseReferenceDetailInfoDB>>> GetExpenseInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseReferenceDetailInfoDB>> response = new Response<List<ExpenseReferenceDetailInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_BRAND_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ExpenseReferenceDetailInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }

        public async Task<Response<ExpenseReferenceDetailInfoDB>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId)
        {
            Response<ExpenseReferenceDetailInfoDB> response = new Response<ExpenseReferenceDetailInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_BRAND_INFO_GET;

                DynamicParameters parameters = new();
                parameters.Add("@ExpenseInfoId", expenseInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseReferenceDetailInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoById({@expenseInfoId}, {@loggedInUserId})", expenseInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }
    }
}
