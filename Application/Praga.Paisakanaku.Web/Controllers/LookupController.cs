using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.Operations.IServices;
namespace Praga.Paisakanaku.Web.Controllers
{
    public class LookupController : BaseController
    {
        private readonly ILogger<LookupController> _logger;
        private readonly ILookupService _lookupService;

        public LookupController(ILogger<LookupController> logger, ILookupService lookupService) : base()
        {
            _logger = logger;
            _lookupService = lookupService;
        }
        
        [HttpGet, Route("~/lookup/expense-type")]
        public async Task<IActionResult> GetExpenseTypeInfoList()
        {
            Response<List<ExpenseTypeInfo>> response = new Response<List<ExpenseTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_ExpenseTypeList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetExpenseTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_ExpenseTypeList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/time-period")]
        public async Task<IActionResult> GetTimePeriodInfoList()
        {
            Response<List<TimePeriodTypeInfo>> response = new Response<List<TimePeriodTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_TimePeriodList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetTimePeriodInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetTimePeriodInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_TimePeriodList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/product-category")]
        public async Task<IActionResult> GetProductCategoryInfoList()
        {
            Response<List<ProductCategoryInfo>> response = new Response<List<ProductCategoryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_ProductCategoryList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetProductCategoryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetProductCategoryInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_ProductCategoryList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/metric-system")]
        public async Task<IActionResult> GetMetricSystemInfoList()
        {
            Response<List<MetricSystemInfo>> response = new Response<List<MetricSystemInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_MetricSystemList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetMetricSystemInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetMetricSystemInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_MetricSystemList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/relationship-type")]
        public async Task<IActionResult> GetRelationshipTypeInfoList()
        {
            Response<List<RelationshipTypeInfo>> response = new Response<List<RelationshipTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_RelationshipTypeList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetRelationshipTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetMeasureTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_RelationshipTypeList.cshtml", response);
        }
        
        [HttpGet, Route("~/lookup/measure-type")]
        public async Task<IActionResult> GetMeasureTypeInfoList()
        {
            Response<List<MeasureTypeInfo>> response = new Response<List<MeasureTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_MeasureTypeList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetMeasureTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetMeasureTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_MeasureTypeList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/measure-type/grocery/{groceryInfoId:Guid}")]
        public async Task<IActionResult> GetMeasureTypeInfoListByGroceryInfoId(Guid groceryInfoId)
        {
            Response<List<MeasureTypeInfo>> response = new Response<List<MeasureTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_MeasureTypeList.cshtml", response);
                }

                if (!Helpers.IsValidGuid(groceryInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_GROCERY_INFO_ID;
                    return PartialView("~/Views/Common/_MeasureTypeList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetMeasureTypeInfoListByGroceryInfoId(groceryInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetMeasureTypeInfoList({@groceryInfoId}, {@loggedInUserId})", groceryInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_MeasureTypeList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/transport-mode")]
        public async Task<IActionResult> GetTransportModeInfo()
        {
            Response<List<TransportModeInfo>> response = new Response<List<TransportModeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_TransportModeList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetTransportModeInfo(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetTransportModeInfo({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_TransportModeList.cshtml", response);
        }

        [HttpGet, Route("~/lookup/travel-service")]
        public async Task<IActionResult> GetTravelServiceInfo()
        {
            Response<List<TravelServiceInfo>> response = new Response<List<TravelServiceInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_TravelServiceList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetTravelServiceInfo(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetTravelServiceInfo({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_TravelServiceList.cshtml", response);
        }


        [HttpGet, Route("~/lookup/consumer-type")]
        public async Task<IActionResult> GetConsumerTypeInfo()
        {
            Response<List<ConsumerTypeInfo>> response = new Response<List<ConsumerTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_ConsumerTypeList.cshtml", response);
                }

                var dbresponse = await _lookupService.GetConsumerTypeInfo(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupController.GetConsumerTypeInfo({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_ConsumerTypeList.cshtml", response);
        }

    }
}
