using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class OutdoorFoodVendorController : BaseController
    {
        private readonly ILogger<OutdoorFoodVendorController> _logger;
        private readonly IOutdoorFoodVendorService _outdoorFoodVendorService;

        public OutdoorFoodVendorController(ILogger<OutdoorFoodVendorController> logger, IOutdoorFoodVendorService outdoorFoodVendorService) : base()
        {
            _logger = logger;
            _outdoorFoodVendorService = outdoorFoodVendorService;
        }

        [HttpGet, Route("~/outdoor-food-vendor/")]
        public async Task<IActionResult> Index()
        {
            Response<List<OutdoorFoodVendorInfo>> response = new Response<List<OutdoorFoodVendorInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/OutdoorFoodVendor/Index.cshtml", response);
                }

                var dbresponse = await _outdoorFoodVendorService.GetOutdoorFoodVendorInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return View("~/Views/Setup/OutdoorFoodVendor/Index.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/OutdoorFoodVendor/Index.cshtml", response);
        }

        [HttpPost, Route("~/outdoor-food-vendor/create")]
        public async Task<IActionResult> CreateOutdoorFoodVendorInfo(OutdoorFoodVendorInfo outdoorFoodVendorInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (outdoorFoodVendorInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _outdoorFoodVendorService.SaveOutdoorFoodVendorInfo(outdoorFoodVendorInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorController.CreateOutdoorFoodVendorInfo({@outdoorFoodVendorInfo}, {@loggedInUserId})", outdoorFoodVendorInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/outdoor-food-vendor/update")]
        public async Task<IActionResult> UpdateOutdoorFoodVendorInfo(OutdoorFoodVendorInfo outdoorFoodVendorInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (outdoorFoodVendorInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _outdoorFoodVendorService.SaveOutdoorFoodVendorInfo(outdoorFoodVendorInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorController.UpdateOutdoorFoodVendorInfo({@outdoorFoodVendorInfo}, {@loggedInUserId})", outdoorFoodVendorInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/outdoor-food-vendor/list")]
        public async Task<IActionResult> GetOutdoorFoodVendorInfoList()
        {
            Response<List<OutdoorFoodVendorInfo>> response = new Response<List<OutdoorFoodVendorInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/OutdoorFoodVendor/_OutdoorFoodVendorList.cshtml", response);
                }

                var dbresponse = await _outdoorFoodVendorService.GetOutdoorFoodVendorInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorController.GetOutdoorFoodVendorInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/OutdoorFoodVendor/_OutdoorFoodVendorList.cshtml", response);

        }

        [HttpGet, Route("~/outdoor-food-vendor/{outdoorFoodVendorInfoId:Guid}")]
        public async Task<IActionResult> GetOutdoorFoodVendorInfoById(Guid outdoorFoodVendorInfoId)
        {
            Response<OutdoorFoodVendorInfo> response = new Response<OutdoorFoodVendorInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/OutdoorFoodVendor/_CreateOutdoorFoodVendor.cshtml", null);
                }

                if (!Helpers.IsValidGuid(outdoorFoodVendorInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/OutdoorFoodVendor/_CreateOutdoorFoodVendor.cshtml", null);
                }

                var dbresponse = await _outdoorFoodVendorService.GetOutdoorFoodVendorInfoById(outdoorFoodVendorInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorController.GetOutdoorFoodVendorInfoById({@outdoorFoodVendorInfoId}, {@loggedInUserId})", outdoorFoodVendorInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/OutdoorFoodVendor/_CreateOutdoorFoodVendor.cshtml", response.Data);
        }

        [HttpGet, Route("~/outdoor-food-vendor/data-list")]
        public async Task<IActionResult> GetOutdoorFoodVendorInfoDataList()
        {
            Response<List<OutdoorFoodVendorInfo>> response = new Response<List<OutdoorFoodVendorInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_OutdoorFoodVendorList.cshtml", response);
                }

                var dbresponse = await _outdoorFoodVendorService.GetOutdoorFoodVendorInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorController.GetOutdoorFoodVendorInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_OutdoorFoodVendorList.cshtml", response);
        }
    }
}
