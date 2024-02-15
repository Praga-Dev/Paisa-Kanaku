using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyWellbeing;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions
{
    public class ExpenseFamilyWellbeingRepository : IExpenseFamilyWellbeingRepository
    {
        private readonly ILogger<ExpenseFamilyWellbeingRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ExpenseFamilyWellbeingRepository(ILogger<ExpenseFamilyWellbeingRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<ExpenseFamilyWellbeingInfoDB>> GetExpenseFamilyWellbeingInfoById(Guid expenseFamilyWellbeingInfoId, Guid loggedInUserId)
        {
            Response<ExpenseFamilyWellbeingInfoDB> response = new Response<ExpenseFamilyWellbeingInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_FAMILY_FUND_INFO_GET_BY_ID;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseFamilyWellbeingInfoId", expenseFamilyWellbeingInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseFamilyWellbeingInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.First()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingRepository.GetExpenseFamilyWellbeingInfoById({@expenseFamilyWellbeingInfoId}, {@loggedInUserId})", expenseFamilyWellbeingInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseFamilyWellbeingInfoDB>>> GetExpenseFamilyWellbeingInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseFamilyWellbeingInfoDB>> response = new Response<List<ExpenseFamilyWellbeingInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_FAMILY_FUND_INFO_GET_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@ExpenseDate", expenseDate, DbType.Date);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseFamilyWellbeingInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingRepository.GetExpenseFamilyWellbeingInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDateDB>>> GetExpenseFamilyWellbeingInfoListByMonth(int month, int year, Guid loggedInUserId)
        {
            Response<List<ExpenseInfoSumAmountByDateDB>> response = new Response<List<ExpenseInfoSumAmountByDateDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_FAMILY_FUND_INFO_GET_SUM_AMOUNT_BY_DATE;
                DynamicParameters parameters = new();
                parameters.Add("@Month", month, DbType.Int16);
                parameters.Add("@Year", year, DbType.Int16);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ExpenseInfoSumAmountByDateDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingRepository.GetTempExpenseInfo({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveExpenseFamilyWellbeingInfoDB(ExpenseFamilyWellbeingInfoDB expenseFamilyWellbeingInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_FAMILY_FUND_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", expenseFamilyWellbeingInfoDB.Id, DbType.Guid);
                parameters.Add("@ExpenseInfoId", expenseFamilyWellbeingInfoDB.ExpenseInfoId, DbType.Guid);
                parameters.Add("@ExpenseDate", expenseFamilyWellbeingInfoDB.ExpenseDate, DbType.Date);
                parameters.Add("@ExpenseById", expenseFamilyWellbeingInfoDB.ExpenseById, DbType.Guid);
                parameters.Add("@RecipientId", expenseFamilyWellbeingInfoDB.RecipientId, DbType.Guid);
                parameters.Add("@ExpenseAmount", expenseFamilyWellbeingInfoDB.ExpenseAmount, DbType.Double);
                parameters.Add("@Description", expenseFamilyWellbeingInfoDB.Description, DbType.String);
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
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingRepository.SaveExpenseFamilyWellbeingInfoDB({@expenseFamilyWellbeingInfoDB}, {@loggedInUserId})", expenseFamilyWellbeingInfoDB.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
