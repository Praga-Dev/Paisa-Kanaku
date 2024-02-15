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
    public class GroceryRepository : IGroceryRepository
    {
        private readonly ILogger<GroceryRepository> _logger;
        private readonly IDataBaseConnection _db;

        public GroceryRepository(ILogger<GroceryRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;

        }
        public async Task<Response<GroceryInfoDB>> GetGroceryInfoById(Guid groceryInfoId, Guid loggedInUserId)
        {
            Response<GroceryInfoDB> response = new Response<GroceryInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_GROCERY_INFO_GET_BY_ID;

                DynamicParameters parameters = new();
                parameters.Add("@GroceryInfoId", groceryInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<GroceryInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryRepository.GetGroceryInfoById({@groceryInfoId}, {@loggedInUserId})", groceryInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<GroceryInfoDB>>> GetGroceryInfoList(Guid loggedInUserId)
        {
            Response<List<GroceryInfoDB>> response = new Response<List<GroceryInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_GROCERY_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<GroceryInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryRepository.GetGroceryInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveGroceryInfo(GroceryInfoDB groceryInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_GROCERY_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", groceryInfoDb.Id, DbType.Guid);
                parameters.Add("@Name", groceryInfoDb.Name, DbType.String);
                parameters.Add("@GroceryCategory", groceryInfoDb.GroceryCategory, DbType.String);
                parameters.Add("@BrandId", groceryInfoDb.BrandId, DbType.Guid);
                parameters.Add("@BrandName", groceryInfoDb.BrandName, DbType.String);
                parameters.Add("@MetricSystem", groceryInfoDb.MetricSystem, DbType.String);
                parameters.Add("@PreferredRecurringTimePeriod", groceryInfoDb.PreferredRecurringTimePeriod, DbType.String);
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
                _logger.LogError(ex, "Error in GroceryRepository.SaveGroceryInfo({@groceryInfoDb}, {@loggedInUserId})", groceryInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
