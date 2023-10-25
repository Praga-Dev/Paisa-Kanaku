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

    }
}
