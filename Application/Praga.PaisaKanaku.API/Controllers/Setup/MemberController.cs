using Microsoft.AspNetCore.Mvc;
using Praga.PaisaKanaku.API.API_Models.Setup;
using Praga.PaisaKanaku.API.Utils;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.PaisaKanaku.API.Controllers.Setup
{
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly ILogger<LookupsController> _logger;
        private readonly IMemberService _memberService;

        public MemberController(ILogger<LookupsController> logger, IMemberService memberService) : base()
        {
            _logger = logger;
            _memberService = memberService;
        }


        [HttpPost, Route("~/v1/setup/member/create")]
        public async Task<IActionResult> CreateMemberInfo(MemberInfo_API memberInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (memberInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var memberInfo = CommonMapper.GetMemberInfo(memberInfoAPISave);
                var dbresponse = await _memberService.SaveMemberInfo(memberInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberController.SaveMemberInfo({@memberInfo}, {@loggedInUserId})", memberInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/v1/setup/member/update")]
        public async Task<IActionResult> UpdateMemberInfo(MemberInfo_API memberInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (memberInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var memberInfo = CommonMapper.GetMemberInfo(memberInfoAPISave);
                var dbresponse = await _memberService.SaveMemberInfo(memberInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberController.SaveMemberInfo({@memberInfo}, {@loggedInUserId})", memberInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/v1/setup/member")]
        public async Task<IActionResult> GetMemberInfoList()
        {
            Response<List<MemberInfo_API>> response = new Response<List<MemberInfo_API>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _memberService.GetMemberInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetMemberInfoListAPI(dbresponse.Data);
                    response = response.GetSuccessResponse(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet, Route("~/v1/setup/member/{memberInfoId:Guid}")]
        public async Task<IActionResult> GetMemberInfoById(Guid memberInfoId)
        {
            Response<MemberInfo_API> response = new Response<MemberInfo_API>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _memberService.GetMemberInfoById(memberInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetMemberInfoAPI(dbresponse.Data);
                    response = response.GetSuccessResponse(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsController.GetExpenseTypeInfoList({@memberInfoId}, {@loggedInUserId})", memberInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
