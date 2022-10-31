using Dapper;
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

        public async Task<Response<List<ExpenseInfoDb>>> GetExpenseBaseInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseInfoDb>> response = new Response<List<ExpenseInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_TEMP_EXPENSE_INFO_PRODUCT_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ExpenseInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }

        public async Task<Response<ExpenseReferenceDetailInfoDb>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId)
        {
            Response<ExpenseReferenceDetailInfoDb> response = new Response<ExpenseReferenceDetailInfoDb>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_BRAND_INFO_GET;

                DynamicParameters parameters = new();
                parameters.Add("@ExpenseInfoId", expenseInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseReferenceDetailInfoDb>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoById({@expenseInfoId}, {@loggedInUserId})", expenseInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }

        public async Task<Response<List<ExpenseReferenceDetailInfoDb>>> GetExpenseInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseReferenceDetailInfoDb>> response = new Response<List<ExpenseReferenceDetailInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_BRAND_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ExpenseReferenceDetailInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }
            return response;
        }

        public async Task<Response<List<TempProductExpenseInfoDb>>> GetTempExpenseInfo(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<TempProductExpenseInfoDb>> response = new Response<List<TempProductExpenseInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_TEMP_EXPENSE_INFO_PRODUCT_GET;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseDate, DbType.DateTime);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<TempProductExpenseInfoDb>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.GetTempExpenseInfo({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfoDb expenseSaveInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_INFO_PRODUCT_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseSaveInfoDb.ExpenseDate, DbType.Date);
                parameters.Add("@ExpenseData", expenseSaveInfoDb.ExpenseData.ToString(), DbType.String);                
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);
                parameters.Add("@Result", null, DbType.Guid, direction: ParameterDirection.Output);

                var returnValue = await _db.Connection.QueryAsync<Guid>(spName, parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<Guid>("@Result");

                if (!returnValue.Any() && result != Guid.Empty)
                {
                    response = response.GetSuccessResponse(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.SaveExpenseInfo({@expenseSaveInfoDb}, {@loggedInUserId})", expenseSaveInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveTempExpenseInfo(TempProductExpenseInfoDb tempProductExpenseInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_TEMP_EXPENSE_INFO_PRODUCT_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", tempProductExpenseInfoDb.Id, DbType.Guid);
                parameters.Add("@ExpenseBy", tempProductExpenseInfoDb.MemberId, DbType.Guid);
                parameters.Add("@ExpenseDate", tempProductExpenseInfoDb.Date, DbType.Date);
                parameters.Add("@ProductId", tempProductExpenseInfoDb.ProductId, DbType.Guid);
                parameters.Add("@Quantity", tempProductExpenseInfoDb.Quantity, DbType.Double);
                parameters.Add("@ExpenseAmount", tempProductExpenseInfoDb.Amount, DbType.Double);
                parameters.Add("@Description", tempProductExpenseInfoDb.Description, DbType.String);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);
                parameters.Add("@Result", null, DbType.Guid, direction: ParameterDirection.Output);

                var returnValue = await _db.Connection.QueryAsync<Guid>(spName, parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<Guid>("@Result");

                if (!returnValue.Any() && result != Guid.Empty)
                {
                    response = response.GetSuccessResponse(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseRepository.SaveTempExpenseInfo({@expenseInfoDb}, {@loggedInUserId})", tempProductExpenseInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
