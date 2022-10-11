using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Setup
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IDataBaseConnection _db;

        public ProductRepository(ILogger<ProductRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<ProductInfoDb>> GetProductInfoById(Guid productInfoId, Guid loggedInUserId)
        {
            Response<ProductInfoDb> response = new Response<ProductInfoDb>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_PRODUCT_INFO_GET_BY_ID;

                DynamicParameters parameters = new();
                parameters.Add("@ProductInfoId", productInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<ProductInfoDb>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductRepository.GetProductInfoById({@productInfoId}, {@loggedInUserId})", productInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ProductInfoDb>>> GetProductInfoList(Guid loggedInUserId)
        {
            Response<List<ProductInfoDb>> response = new Response<List<ProductInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_PRODUCT_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ProductInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductRepository.GetProductInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveProductInfo(ProductInfoDb productInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_PRODUCT_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", productInfoDb.Id, DbType.Guid);
                parameters.Add("@ProductName", productInfoDb.Name, DbType.String);
                parameters.Add("@ProductCategoryId", productInfoDb.ProductCategoryId, DbType.Guid);
                parameters.Add("@ProductCategoryName", productInfoDb.ProductCategoryName, DbType.String);
                parameters.Add("@BrandId", productInfoDb.BrandId, DbType.Guid);
                parameters.Add("@BrandName", productInfoDb.BrandName, DbType.String);
                parameters.Add("@ExpenseType", productInfoDb.ExpenseType, DbType.String);
                parameters.Add("@Price", productInfoDb.Price, DbType.Double);
                parameters.Add("@Description", productInfoDb.Description, DbType.String);
                parameters.Add("@PreferredRecurringTimePeriod", productInfoDb.PreferredRecurringTimePeriod, DbType.String);
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
                _logger.LogError(ex, "Error in ProductRepository.SaveProductInfo({@productInfoDb}, {@loggedInUserId})", productInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
