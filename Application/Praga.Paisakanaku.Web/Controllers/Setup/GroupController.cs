using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Group;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class GroupController : BaseController
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;

        public GroupController(ILogger<GroupController> logger, IGroupService groupService) : base()
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet, Route("~/group/")]
        public async Task<IActionResult> Index()
        {
            Response<List<GroupInfo>> response = new Response<List<GroupInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/Group/Index.cshtml", response);
                }

                var dbresponse = await _groupService.GetGroupInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return View("~/Views/Setup/Group/Index.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/Group/Index.cshtml", response);
        }

        [HttpPost, Route("~/group/create")]
        public async Task<IActionResult> CreateGroupInfo(GroupInfo groupInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (groupInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _groupService.SaveGroupInfo(groupInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupController.CreateGroupInfo({@groupInfo}, {@loggedInUserId})", groupInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/group/update")]
        public async Task<IActionResult> UpdateGroupInfo(GroupInfo groupInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (groupInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _groupService.SaveGroupInfo(groupInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupController.UpdateGroupInfo({@groupInfo}, {@loggedInUserId})", groupInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/group/list")]
        public async Task<IActionResult> GetGroupInfoList()
        {
            Response<List<GroupInfo>> response = new Response<List<GroupInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/Group/_GroupList.cshtml", response);
                }

                var dbresponse = await _groupService.GetGroupInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupController.GetGroupInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/Group/_GroupList.cshtml", response);

        }

        [HttpGet, Route("~/group/{groupInfoId:Guid}")]
        public async Task<IActionResult> GetGroupInfoById(Guid groupInfoId)
        {
            Response<GroupInfo> response = new Response<GroupInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/Group/_CreateGroup.cshtml", null);
                }

                if (!Helpers.IsValidGuid(groupInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/Group/_CreateGroup.cshtml", null);
                }

                var dbresponse = await _groupService.GetGroupInfoById(groupInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupController.GetGroupInfoById({@groupInfoId}, {@loggedInUserId})", groupInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/Group/_CreateGroup.cshtml", response.Data);
        }

        [HttpGet, Route("~/group/data-list")]
        public async Task<IActionResult> GetGroupInfoDataList()
        {
            Response<List<GroupInfo>> response = new Response<List<GroupInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_GroupList.cshtml", response);
                }

                var dbresponse = await _groupService.GetGroupInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupController.GetGroupInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_GroupList.cshtml", response);
        }
    }
}
