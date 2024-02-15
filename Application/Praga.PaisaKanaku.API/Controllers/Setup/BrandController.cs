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
    public class BrandController : BaseController
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(ILogger<BrandController> logger, IBrandService brandService) : base()
        {
            _logger = logger;
            _brandService = brandService;
        }


        [HttpPost, Route("~/v1/setup/brand/create")]
        public async Task<IActionResult> CreateBrandInfo(BrandInfo_API brandInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (brandInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var brandInfo = CommonMapper.GetBrandInfo(brandInfoAPISave);
                var dbresponse = await _brandService.SaveBrandInfo(brandInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.SaveBrandInfo({@brandInfo}, {@loggedInUserId})", brandInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/v1/setup/brand/update")]
        public async Task<IActionResult> UpdateBrandInfo(BrandInfo_API brandInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (brandInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var brandInfo = CommonMapper.GetBrandInfo(brandInfoAPISave);
                var dbresponse = await _brandService.SaveBrandInfo(brandInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.SaveBrandInfo({@brandInfo}, {@loggedInUserId})", brandInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/v1/setup/brand")]
        public async Task<IActionResult> GetBrandInfoList()
        {
            Response<List<BrandInfo_API>> response = new Response<List<BrandInfo_API>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _brandService.GetBrandInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetBrandInfoListAPI(dbresponse.Data);
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

        [HttpGet, Route("~/v1/setup/brand/{brandInfoId:Guid}")]
        public async Task<IActionResult> GetBrandInfoById(Guid brandInfoId)
        {
            Response<BrandInfo_API> response = new Response<BrandInfo_API>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _brandService.GetBrandInfoById(brandInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetBrandInfoAPI(dbresponse.Data);
                    response = response.GetSuccessResponse(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsController.GetExpenseTypeInfoList({@brandInfoId}, {@loggedInUserId})", brandInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }
            
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
