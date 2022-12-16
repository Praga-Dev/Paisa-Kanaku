using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System.Net;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class RepairTypeController : BaseController
    {
        private readonly ILogger<RepairTypeController> _logger;
        private readonly IRepairTypeService _repairTypeService;

        public RepairTypeController(ILogger<RepairTypeController> logger, IRepairTypeService repairTypeService) : base()
        {
            _logger = logger;
            _repairTypeService = repairTypeService;
        }

        [HttpGet, Route("~/repair-type/")]
        public async Task<IActionResult> Index()
        {
            Response<List<RepairTypeInfo>> response = new Response<List<RepairTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/RepairType/Index.cshtml", response);
                }

                var dbresponse = await _repairTypeService.GetRepairTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return View("~/Views/Setup/RepairType/Index.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/RepairType/Index.cshtml", response);
        }

        [HttpPost, Route("~/repair-type/create")]
        public async Task<IActionResult> CreateRepairTypeInfo(RepairTypeInfo repairTypeInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (repairTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _repairTypeService.SaveRepairTypeInfo(repairTypeInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeController.CreateRepairTypeInfo({@repairTypeInfo}, {@loggedInUserId})", repairTypeInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/repair-type/update")]
        public async Task<IActionResult> UpdateRepairTypeInfo(RepairTypeInfo repairTypeInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (repairTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _repairTypeService.SaveRepairTypeInfo(repairTypeInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeController.UpdateRepairTypeInfo({@repairTypeInfo}, {@loggedInUserId})", repairTypeInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/repair-type/list")]
        public async Task<IActionResult> GetRepairTypeInfoList()
        {
            Response<List<RepairTypeInfo>> response = new Response<List<RepairTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/RepairType/_repairTypeList.cshtml", response);
                }

                var dbresponse = await _repairTypeService.GetRepairTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeController.GetRepairTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/RepairType/_RepairTypeList.cshtml", response);

        }

        [HttpGet, Route("~/repair-type/{repairTypeInfoId:Guid}")]
        public async Task<IActionResult> GetRepairTypeInfoById(Guid repairTypeInfoId)
        {
            Response<RepairTypeInfo> response = new Response<RepairTypeInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/RepairType/_CreateRepairType.cshtml", null);
                }

                if (!Helpers.IsValidGuid(repairTypeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/RepairType/_CreateRepairType.cshtml", null);
                }

                var dbresponse = await _repairTypeService.GetRepairTypeInfoById(repairTypeInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeController.GetRepairTypeInfoById({@repairTypeInfoId}, {@loggedInUserId})", repairTypeInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/RepairType/_CreateRepairType.cshtml", response.Data);
        }

        [HttpGet, Route("~/repair-type/data-list")]
        public async Task<IActionResult> GetRepairTypeInfoDataList()
        {
            Response<List<RepairTypeInfo>> response = new Response<List<RepairTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_RepairTypeList.cshtml", response);
                }

                var dbresponse = await _repairTypeService.GetRepairTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeController.GetRepairTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_RepairTypeList.cshtml", response);
        }
    }
}
