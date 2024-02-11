using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Lookups;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories
{
    public class LookupsRepository : ILookupsRepository
    {
        private readonly ILogger<LookupsRepository> _logger;
        private readonly IDataBaseConnection _db;

        public LookupsRepository(ILogger<LookupsRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<List<ExpenseTypeInfoDB>>> GetExpenseTypeInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseTypeInfoDB>> response = new Response<List<ExpenseTypeInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ExpenseTypeInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetExpenseTypeInfoList({@loggedInUserId})", loggedInUserId );
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<MeasureTypeInfoDB>>> GetMeasureTypeInfoList(Guid loggedInUserId)
        {
            Response<List<MeasureTypeInfoDB>> response = new Response<List<MeasureTypeInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_MEASURE_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<MeasureTypeInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMeasureTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<TimePeriodInfoDB>>> GetTimePeriodInfoList(Guid loggedInUserId)
        {
            Response<List<TimePeriodInfoDB>> response = new Response<List<TimePeriodInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_TIME_PERIOD_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<TimePeriodInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTimePeriodInfoDbList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ProductCategoryInfoDB>>> GetProductCategoryInfoList(Guid loggedInUserId)
        {
            Response<List<ProductCategoryInfoDB>> response = new Response<List<ProductCategoryInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_PRODUCT_CATEGORY_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ProductCategoryInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetExpenseTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<MetricSystemInfoDB>>> GetMetricSystemInfoList(Guid loggedInUserId)
        {
            Response<List<MetricSystemInfoDB>> response = new Response<List<MetricSystemInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_METRIC_SYSTEM_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<MetricSystemInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMetricSystemInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<RelationshipTypeInfoDB>>> GetRelationshipTypeInfoList(Guid loggedInUserId)
        {
            Response<List<RelationshipTypeInfoDB>> response = new Response<List<RelationshipTypeInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_RELATIONSHIP_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<RelationshipTypeInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetRelationshipTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<MeasureTypeInfoDB>>> GetMeasureTypeInfoListByGroceryInfoId(Guid groceryInfoId, Guid loggedInUserId)
        {
            Response<List<MeasureTypeInfoDB>> response = new Response<List<MeasureTypeInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_MEASURE_TYPE_INFO_BY_GROCERY_INFO_ID_GET;
                DynamicParameters param = new();
                param.Add("@GroceryInfoId", groceryInfoId, DbType.Guid);
                param.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<MeasureTypeInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMeasureTypeInfoListByGroceryInfoId({@groceryInfoId}, {@loggedInUserId})", groceryInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<TransportModeInfoDB>>> GetTransportModeInfo(Guid loggedInUserId)
        {
            Response<List<TransportModeInfoDB>> response = new Response<List<TransportModeInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_TRANSPORT_MODE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<TransportModeInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTransportModeInfo({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<TravelServiceInfoDB>>> GetTravelServiceInfo(Guid loggedInUserId)
        {
            Response<List<TravelServiceInfoDB>> response = new Response<List<TravelServiceInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_TRAVEL_SERVICE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<TravelServiceInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTravelServiceInfo({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
