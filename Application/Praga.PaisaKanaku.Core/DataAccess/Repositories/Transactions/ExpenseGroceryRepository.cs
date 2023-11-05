using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseGrocery;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions
{
    public class ExpenseGroceryRepository : IExpenseGroceryRepository
    {
        private readonly ILogger<ExpenseGroceryRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ExpenseGroceryRepository(ILogger<ExpenseGroceryRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<ExpenseGroceryInfoDB>> GetExpenseGroceryInfoById(Guid expenseGroceryInfoId, Guid loggedInUserId)
        {
            Response<ExpenseGroceryInfoDB> response = new Response<ExpenseGroceryInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_GROCERY_INFO_GET_BY_ID;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseGroceryInfoId", expenseGroceryInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseGroceryInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.First()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryRepository.GetExpenseGroceryInfoById({@expenseGroceryInfoId}, {@loggedInUserId})", expenseGroceryInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseGroceryInfoDB>>> GetExpenseGroceryInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseGroceryInfoDB>> response = new Response<List<ExpenseGroceryInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_INFO_GROCERY_GET_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseDate, DbType.Date);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseGroceryInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryRepository.GetExpenseGroceryInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseGroceryInfoListByMonth(int month, int year, Guid loggedInUserId)
        {
            Response<List<ExpenseInfoSumAmountByDateDB>> response = new Response<List<ExpenseInfoSumAmountByDateDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_GROCERY_INFO_GET_SUM_AMOUNT_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@Month", month, DbType.Int16);
                parameters.Add("@Year", year, DbType.Int16);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseInfoSumAmountByDateDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryRepository.GetTempExpenseInfo({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveExpenseGroceryInfoDB(ExpenseGroceryInfoDB expenseGroceryInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_GROCERY_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", expenseGroceryInfoDB.Id, DbType.Guid);
                parameters.Add("@ExpenseDate", expenseGroceryInfoDB.ExpenseDate, DbType.Date);
                parameters.Add("@ExpenseInfoId", expenseGroceryInfoDB.ExpenseInfoId, DbType.Guid);
                parameters.Add("@GroceryInfoId", expenseGroceryInfoDB.GroceryInfoId, DbType.Guid);
                parameters.Add("@ExpenseById", expenseGroceryInfoDB.ExpenseById, DbType.Guid);
                parameters.Add("@Quantity", expenseGroceryInfoDB.Quantity, DbType.Int64);
                parameters.Add("@ExpenseAmount", expenseGroceryInfoDB.ExpenseAmount, DbType.Double);
                parameters.Add("@Description", expenseGroceryInfoDB.Description, DbType.String);
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
                _logger.LogError(ex, "Error in ExpenseGroceryRepository.SaveExpenseGroceryInfoDB({@expenseGroceryInfoDB}, {@loggedInUserId})", expenseGroceryInfoDB.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
