using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseOutdoorFood;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions
{
    public class ExpenseOutdoorFoodRepository : IExpenseOutdoorFoodRepository
    {
        private readonly ILogger<ExpenseOutdoorFoodRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ExpenseOutdoorFoodRepository(ILogger<ExpenseOutdoorFoodRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<ExpenseOutdoorFoodInfoDB>> GetExpenseOutdoorFoodInfoById(Guid expenseOutdoorFoodInfoId, Guid loggedInUserId)
        {
            Response<ExpenseOutdoorFoodInfoDB> response = new Response<ExpenseOutdoorFoodInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_OUTDOOR_FOOD_INFO_GET_BY_ID;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseOutdoorFoodInfoId", expenseOutdoorFoodInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseOutdoorFoodInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.First()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodRepository.GetExpenseOutdoorFoodInfoById({@expenseOutdoorFoodInfoId}, {@loggedInUserId})", expenseOutdoorFoodInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseOutdoorFoodInfoDB>>> GetExpenseOutdoorFoodInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseOutdoorFoodInfoDB>> response = new Response<List<ExpenseOutdoorFoodInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_OUTDOOR_FOOD_INFO_GET_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseDate, DbType.Date);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseOutdoorFoodInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodRepository.GetExpenseOutdoorFoodInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseOutdoorFoodInfoListByMonth(int month, int year, Guid loggedInUserId)
        {
            Response<List<ExpenseInfoSumAmountByDateDB>> response = new Response<List<ExpenseInfoSumAmountByDateDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_OUTDOOR_FOOD_INFO_GET_SUM_AMOUNT_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@Month", month, DbType.Int16);
                parameters.Add("@Year", year, DbType.Int16);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseInfoSumAmountByDateDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodRepository.GetTempExpenseInfo({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveExpenseOutdoorFoodInfoDB(ExpenseOutdoorFoodInfoDB expenseOutdoorFoodInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_OUTDOOR_FOOD_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", expenseOutdoorFoodInfoDB.Id, DbType.Guid);
                parameters.Add("@ExpenseInfoId", expenseOutdoorFoodInfoDB.ExpenseInfoId, DbType.Guid);
                parameters.Add("@ExpenseDate", expenseOutdoorFoodInfoDB.ExpenseDate, DbType.Date);
                parameters.Add("@ExpenseById", expenseOutdoorFoodInfoDB.ExpenseById, DbType.Guid);
                parameters.Add("@OutdoorFoodVendorId", expenseOutdoorFoodInfoDB.OutdoorFoodVendorId, DbType.Guid);
                parameters.Add("@ExpenseAmount", expenseOutdoorFoodInfoDB.ExpenseAmount, DbType.Double);
                parameters.Add("@BillImageUrl", expenseOutdoorFoodInfoDB.BillImageURL, DbType.String);
                parameters.Add("@Description", expenseOutdoorFoodInfoDB.Description, DbType.String);
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
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodRepository.SaveExpenseOutdoorFoodInfoDB({@expenseOutdoorFoodInfoDB}, {@loggedInUserId})", expenseOutdoorFoodInfoDB.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
