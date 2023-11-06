using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System.Text;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class GroceryController : BaseController
    {
        private readonly ILogger<GroceryController> _logger;
        private readonly IGroceryService _groceryService;

        public GroceryController(ILogger<GroceryController> logger, IGroceryService groceryService) : base()
        {
            _logger = logger;
            _groceryService = groceryService;
        }

        [HttpGet, Route("~/grocery/")]
        public async Task<IActionResult> Index()
        {
            Response<List<GroceryInfo>> response = new Response<List<GroceryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/Grocery/Index.cshtml", response);
                }

                var dbresponse = await _groceryService.GetGroceryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/Grocery/Index.cshtml", response);
        }

        [HttpPost, Route("~/grocery/create")]
        public async Task<IActionResult> CreateGroceryInfo(GroceryInfo groceryInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (groceryInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _groceryService.SaveGroceryInfo(groceryInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.CreateGroceryInfo({@groceryInfo}, {@loggedInUserId})", groceryInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/grocery/update")]
        public async Task<IActionResult> UpdateGroceryInfo(GroceryInfo groceryInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (groceryInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _groceryService.SaveGroceryInfo(groceryInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.UpdateGroceryInfo({@groceryInfo}, {@loggedInUserId})", groceryInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/grocery/list")]
        public async Task<IActionResult> GetGroceryInfoList()
        {
            Response<List<GroceryInfo>> response = new Response<List<GroceryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/Grocery/_GroceryList.cshtml", response);
                }

                var dbresponse = await _groceryService.GetGroceryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.GetGroceryInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/Grocery/_GroceryList.cshtml", response);

        }

        [HttpGet, Route("~/grocery/{groceryInfoId:Guid}")]
        public async Task<IActionResult> GetGroceryInfoById(Guid groceryInfoId)
        {
            Response<GroceryInfo> response = new Response<GroceryInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/Grocery/_CreateGrocery.cshtml", null);
                }

                if (!Helpers.IsValidGuid(groceryInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/Grocery/_CreateGrocery.cshtml", null);
                }

                var dbresponse = await _groceryService.GetGroceryInfoById(groceryInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.GetGroceryInfoById({@groceryInfoId}, {@loggedInUserId})", groceryInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/Grocery/_CreateGrocery.cshtml", response.Data);
        }

        [HttpGet, Route("~/grocery/data-list")]
        public async Task<IActionResult> GetGroceryInfoDataList()
        {
            Response<List<GroceryInfo>> response = new Response<List<GroceryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_GroceryList.cshtml", response);
                }

                var dbresponse = await _groceryService.GetGroceryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.GetGroceryInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_GroceryList.cshtml", response);
        }

        [HttpGet, Route("~/grocery/{groceryInfoId:Guid}/data")]
        public async Task<IActionResult> GetGroceryInfoDataById(Guid groceryInfoId)
        {
            Response<GroceryInfo> response = new Response<GroceryInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (!Helpers.IsValidGuid(groceryInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _groceryService.GetGroceryInfoById(groceryInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryController.GetGroceryInfoDataById({@groceryInfoId}, {@loggedInUserId})", groceryInfoId, LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
