using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions
{
    internal class ExpenseProductRepository : IExpenseProductRepository
    {
        private readonly ILogger<ExpenseProductRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ExpenseProductRepository(ILogger<ExpenseProductRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<Guid>> SaveExpenseProductInfoDB(ExpenseProductInfoDB expenseProductInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_PRODUCT_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", expenseProductInfoDB.Id, DbType.Guid);
                parameters.Add("@ExpenseDate", expenseProductInfoDB.ExpenseDate, DbType.Date);
                parameters.Add("@ExpenseInfoId", expenseProductInfoDB.ExpenseInfoId, DbType.Guid);
                parameters.Add("@ProductInfoId", expenseProductInfoDB.ProductInfoId, DbType.Guid);
                parameters.Add("@ExpenseById", expenseProductInfoDB.ExpenseById, DbType.Guid);
                parameters.Add("@Quantity", expenseProductInfoDB.Quantity, DbType.Int64);
                parameters.Add("@ExpenseAmount", expenseProductInfoDB.ExpenseAmount, DbType.Double);
                parameters.Add("@Description", expenseProductInfoDB.Description, DbType.String);
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
                _logger.LogError(ex, "Error in ExpenseProductRepository.SaveExpenseProductInfoDB({@expenseProductInfoDB}, {@loggedInUserId})", expenseProductInfoDB.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<ExpenseProductInfoSumAmountByDateDB>>> GetExpenseProductInfoListByMonth(int month, int year, Guid loggedInUserId)
        {
            Response<List<ExpenseProductInfoSumAmountByDateDB>> response = new Response<List<ExpenseProductInfoSumAmountByDateDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_PRODUCT_INFO_GET_SUM_AMOUNT_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@Month", month, DbType.Int16);
                parameters.Add("@Year", year, DbType.Int16);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseProductInfoSumAmountByDateDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductRepository.GetTempExpenseInfo({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseProductInfoDB>>> GetExpenseProductInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseProductInfoDB>> response = new Response<List<ExpenseProductInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_INFO_PRODUCT_GET_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseDate, DbType.Date);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseProductInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductRepository.GetExpenseProductInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseProductInfoDB>> GetExpenseProductInfoById(Guid expenseProductInfoId, Guid loggedInUserId)
        {
            Response<ExpenseProductInfoDB> response = new Response<ExpenseProductInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_INFO_PRODUCT_GET_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseProductInfoId", expenseProductInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseProductInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.First()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductRepository.GetExpenseProductInfoById({@expenseProductInfoId}, {@loggedInUserId})", expenseProductInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
