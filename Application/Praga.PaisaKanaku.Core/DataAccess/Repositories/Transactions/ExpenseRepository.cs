using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;
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

        public async Task<Response<ExpenseInfoDb>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId)
        {
            throw new NotImplementedException();

            //Response<ExpenseInfoDb> response = new Response<ExpenseInfoDb>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            //try
            //{
            //    string spName = DatabaseConstants.USP_BRAND_INFO_GET_BY_ID;

            //    DynamicParameters parameters = new();
            //    parameters.Add("@ExpenseInfoId", expenseInfoId, DbType.Guid);
            //    parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

            //    var result = await _db.Connection.QueryAsync<ExpenseInfoDb>(spName, parameters, commandType: CommandType.StoredProcedure);
            //    return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoById({@expenseInfoId}, {@loggedInUserId})", expenseInfoId, loggedInUserId);
            //    response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            //    return response;
            //}
        }

        public async Task<Response<List<ExpenseInfoDb>>> GetExpenseInfoList(Guid loggedInUserId)
        {
            throw new NotImplementedException();

            //Response<List<ExpenseInfoDb>> response = new Response<List<ExpenseInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            //try
            //{
            //    string spName = DatabaseConstants.USP_BRAND_INFO_GET;
            //    var param = new { LoggedInUserId = loggedInUserId };

            //    var result = await _db.Connection.QueryAsync<ExpenseInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
            //    return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error in ExpenseRepository.GetExpenseInfoList({@loggedInUserId})", loggedInUserId);
            //    response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            //    return response;
            //}
        }

        public async Task<Response<Guid>> SaveExpenseInfo(ExpenseReferenceDetailInfoDb expenseInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_INFO_PRODUCT_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", expenseInfoDb.Id, DbType.Guid);
                parameters.Add("@ExpenseInfoId", expenseInfoDb.ExpenseInfoId, DbType.Guid);
                parameters.Add("@ExpenseBy", expenseInfoDb.ExpenseBy, DbType.Guid);
                parameters.Add("@DateOfExpense", expenseInfoDb.DateOfExpense, DbType.Date);
                parameters.Add("@ExpenseDescription", expenseInfoDb.ExpenseDescription, DbType.String);
                parameters.Add("@IsChangeInProduct", expenseInfoDb.IsChangeInProduct, DbType.Boolean);
                parameters.Add("@BrandId", expenseInfoDb.BrandId, DbType.Guid);
                parameters.Add("@BrandName", expenseInfoDb.BrandName, DbType.String);
                parameters.Add("@ProductCategoryId", expenseInfoDb.ProductCategoryId, DbType.Guid);
                parameters.Add("@ProductCategoryName", expenseInfoDb.ProductCategoryName, DbType.String);
                parameters.Add("@ProductId", expenseInfoDb.ProductId, DbType.Guid);
                parameters.Add("@ProductName", expenseInfoDb.ProductName, DbType.String);
                parameters.Add("@ExpenseType", expenseInfoDb.ExpenseType, DbType.String);
                parameters.Add("@Price", expenseInfoDb.ProductPrice, DbType.Double);
                parameters.Add("@ProductDescription", expenseInfoDb.ProductDescription, DbType.String);
                parameters.Add("@PreferredRecurringTimePeriod", expenseInfoDb.PreferredRecurringTimePeriod, DbType.String);
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
                _logger.LogError(ex, "Error in ExpenseRepository.SaveExpenseInfo({@expenseInfoDb}, {@loggedInUserId})", expenseInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }

}
