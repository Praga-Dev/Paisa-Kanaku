using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseUtility;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions
{
    public class ExpenseUtilityRepository : IExpenseUtilityRepository
    {
        private readonly ILogger<ExpenseUtilityRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ExpenseUtilityRepository(ILogger<ExpenseUtilityRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<ExpenseUtilityInfoDB>> GetExpenseUtilityInfoById(Guid expenseUtilityInfoId, Guid loggedInUserId)
        {
            Response<ExpenseUtilityInfoDB> response = new Response<ExpenseUtilityInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_UTILITY_INFO_GET_BY_ID;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseUtilityInfoId", expenseUtilityInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseUtilityInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.First()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityRepository.GetExpenseUtilityInfoById({@expenseUtilityInfoId}, {@loggedInUserId})", expenseUtilityInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseUtilityInfoDB>>> GetExpenseUtilityInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseUtilityInfoDB>> response = new Response<List<ExpenseUtilityInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_UTILITY_INFO_GET_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseDate, DbType.Date);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseUtilityInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityRepository.GetExpenseUtilityInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseUtilityInfoListByMonth(int month, int year, Guid loggedInUserId)
        {
            Response<List<ExpenseInfoSumAmountByDateDB>> response = new Response<List<ExpenseInfoSumAmountByDateDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_UTILITY_INFO_GET_SUM_AMOUNT_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@Month", month, DbType.Int16);
                parameters.Add("@Year", year, DbType.Int16);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseInfoSumAmountByDateDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityRepository.GetTempExpenseInfo({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveExpenseUtilityInfoDB(ExpenseUtilityInfoDB expenseUtilityInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_UTILITY_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", expenseUtilityInfoDB.Id, DbType.Guid);
                parameters.Add("@ExpenseInfoId", expenseUtilityInfoDB.ExpenseInfoId, DbType.Guid);
                parameters.Add("@ExpenseDate", expenseUtilityInfoDB.ExpenseDate, DbType.Date);
                parameters.Add("@ExpenseById", expenseUtilityInfoDB.ExpenseById, DbType.Guid);
                parameters.Add("@UtilityId", expenseUtilityInfoDB.UtilityId, DbType.Guid);
                parameters.Add("@ConsumerType", expenseUtilityInfoDB.ConsumerType, DbType.String);
                parameters.Add("@ServiceDuration", expenseUtilityInfoDB.ServiceDuration, DbType.String);
                parameters.Add("@FromDate", expenseUtilityInfoDB.FromDate, DbType.Date);
                parameters.Add("@ToDate", expenseUtilityInfoDB.ToDate, DbType.Date);
                parameters.Add("@ExpenseAmount", expenseUtilityInfoDB.ExpenseAmount, DbType.Double);
                parameters.Add("@ConsumerId", expenseUtilityInfoDB.ConsumerId, DbType.Guid);
                parameters.Add("@Description", expenseUtilityInfoDB.Description, DbType.String);
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
                _logger.LogError(ex, "Error in ExpenseUtilityRepository.SaveExpenseUtilityInfoDB({@expenseUtilityInfoDB}, {@loggedInUserId})", expenseUtilityInfoDB.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
