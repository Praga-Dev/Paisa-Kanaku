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
    public class BillTypeController : BaseController
    {
        private readonly ILogger<BillTypeController> _logger;
        private readonly IBillTypeService _billTypeService;

        public BillTypeController(ILogger<BillTypeController> logger, IBillTypeService billTypeService) : base()
        {
            _logger = logger;
            _billTypeService = billTypeService;
        }

        [HttpGet, Route("~/bill-type/")]
        public async Task<IActionResult> Index()
        {
            Response<List<BillTypeInfo>> response = new Response<List<BillTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/BillType/Index.cshtml", response);
                }

                var dbresponse = await _billTypeService.GetBillTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return View("~/Views/Setup/BillType/Index.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/BillType/Index.cshtml", response);
        }

        [HttpPost, Route("~/bill-type/create")]
        public async Task<IActionResult> CreateBillTypeInfo(BillTypeInfo billTypeInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (billTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _billTypeService.SaveBillTypeInfo(billTypeInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeController.CreateBillTypeInfo({@billTypeInfo}, {@loggedInUserId})", billTypeInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/bill-type/update")]
        public async Task<IActionResult> UpdateBillTypeInfo(BillTypeInfo billTypeInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (billTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _billTypeService.SaveBillTypeInfo(billTypeInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeController.UpdateBillTypeInfo({@billTypeInfo}, {@loggedInUserId})", billTypeInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/bill-type/list")]
        public async Task<IActionResult> GetBillTypeInfoList()
        {
            Response<List<BillTypeInfo>> response = new Response<List<BillTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/BillType/_BillTypeList.cshtml", response);
                }

                var dbresponse = await _billTypeService.GetBillTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeController.GetBillTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }
            
            return PartialView("~/Views/Setup/BillType/_BillTypeList.cshtml", response);

        }

        [HttpGet, Route("~/bill-type/{billTypeInfoId:Guid}")]
        public async Task<IActionResult> GetBillTypeInfoById(Guid billTypeInfoId)
        {
            Response<BillTypeInfo> response = new Response<BillTypeInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/BillType/_CreateBillType.cshtml", null);
                }

                if (!Helpers.IsValidGuid(billTypeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/BillType/_CreateBillType.cshtml", null);
                }

                var dbresponse = await _billTypeService.GetBillTypeInfoById(billTypeInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeController.GetBillTypeInfoById({@billTypeInfoId}, {@loggedInUserId})", billTypeInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/BillType/_CreateBillType.cshtml", response.Data);
        }

        [HttpGet, Route("~/bill-type/data-list")]
        public async Task<IActionResult> GetBillTypeInfoDataList()
        {
            Response<List<BillTypeInfo>> response = new Response<List<BillTypeInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_BillTypeList.cshtml", response);
                }

                var dbresponse = await _billTypeService.GetBillTypeInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeController.GetBillTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_BillTypeList.cshtml", response);
        }
    }
}
