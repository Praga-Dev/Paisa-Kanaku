using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.Operations.IServices;

namespace Praga.PaisaKanaku.Core.Operations.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILogger<LookupService> _logger;

        private readonly ILookupsRepository _lookupsRepository;

        public LookupService(ILogger<LookupService> logger, ILookupsRepository lookupsRepository)
        {
            _logger = logger;
            _lookupsRepository = lookupsRepository;
        }

        public async Task<Response<List<ExpenseTypeInfo>>> GetExpenseTypeInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseTypeInfo>> response = new Response<List<ExpenseTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetExpenseTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(expense => new ExpenseTypeInfo() { ExpenseType = expense.ExpenseType, ExpenseTypeValue = expense.ExpenseTypeValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetExpenseTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<MeasureTypeInfo>>> GetMeasureTypeInfoList(Guid loggedInUserId)
        {
            Response<List<MeasureTypeInfo>> response = new Response<List<MeasureTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetMeasureTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(measure => new MeasureTypeInfo() { MeasureType = measure.MeasureType, MeasureTypeValue = measure.MeasureTypeValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMeasureTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<TimePeriodTypeInfo>>> GetTimePeriodInfoList(Guid loggedInUserId)
        {
            Response<List<TimePeriodTypeInfo>> response = new Response<List<TimePeriodTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetTimePeriodInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(timeperiod => new TimePeriodTypeInfo() { TimePeriodType = timeperiod.TimePeriodType, TimePeriodTypeValue = timeperiod.TimePeriodTypeValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTimePeriodInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<ProductCategoryInfo>>> GetProductCategoryInfoList(Guid loggedInUserId)
        {
            Response<List<ProductCategoryInfo>> response = new Response<List<ProductCategoryInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetProductCategoryInfoList(loggedInUserId);

                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(productCategory => new ProductCategoryInfo() { ProductCategory = productCategory.ProductCategory, ProductCategoryValue = productCategory.ProductCategoryValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetProductCategoryInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<MetricSystemInfo>>> GetMetricSystemInfoList(Guid loggedInUserId)
        {
            Response<List<MetricSystemInfo>> response = new Response<List<MetricSystemInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetMetricSystemInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(measure => new MetricSystemInfo() { MetricSystem = measure.MetricSystem, MetricSystemValue = measure.MetricSystemValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMetricSystemInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<RelationshipTypeInfo>>> GetRelationshipTypeInfoList(Guid loggedInUserId)
        {
            Response<List<RelationshipTypeInfo>> response = new Response<List<RelationshipTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetRelationshipTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(
                        relationship => new RelationshipTypeInfo()
                        {
                            RelationshipType = relationship.RelationshipType,
                            RelationshipTypeValue = relationship.RelationshipType
                        }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetRelationshipTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<MeasureTypeInfo>>> GetMeasureTypeInfoListByGroceryInfoId(Guid groceryInfoId, Guid loggedInUserId)
        {
            Response<List<MeasureTypeInfo>> response = new Response<List<MeasureTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetMeasureTypeInfoListByGroceryInfoId(groceryInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                    .Select(measure => new MeasureTypeInfo() { MeasureType = measure.MeasureType, MeasureTypeValue = measure.MeasureTypeValue })
                    .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMeasureTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<TransportModeInfo>>> GetTransportModeInfo(Guid loggedInUserId)
        {
            Response<List<TransportModeInfo>> response = new Response<List<TransportModeInfo>>()
                .GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetTransportModeInfo(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(
                        relationship => new TransportModeInfo()
                        {
                            TransportMode = relationship.TransportMode,
                            TransportModeValue = relationship.TransportModeValue
                        }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTransportModeInfo({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<TravelServiceInfo>>> GetTravelServiceInfo(Guid loggedInUserId)
        {
            Response<List<TravelServiceInfo>> response = new Response<List<TravelServiceInfo>>()
                .GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetTravelServiceInfo(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(
                        relationship => new TravelServiceInfo()
                        {
                            TravelService = relationship.TravelService,
                            TravelServiceValue = relationship.TravelServiceValue
                        }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTravelServiceInfo({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<ConsumerTypeInfo>>> GetConsumerTypeInfo(Guid loggedInUserId)
        {
            Response<List<ConsumerTypeInfo>> response = new Response<List<ConsumerTypeInfo>>()
                .GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetConsumerTypeInfo(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(
                        relationship => new ConsumerTypeInfo()
                        {
                            ConsumerType = relationship.ConsumerType,
                            ConsumerTypeValue = relationship.ConsumerTypeValue
                        }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetConsumerTypeInfo({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
