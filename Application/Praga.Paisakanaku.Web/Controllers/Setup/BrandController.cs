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
    public class BrandController : BaseController
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(ILogger<BrandController> logger, IBrandService brandService) : base()
        {
            _logger = logger;
            _brandService = brandService;
        }

        [HttpGet, Route("~/brand/")]
        public async Task<IActionResult> Index()
        {
            Response<List<BrandInfo>> response = new Response<List<BrandInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/Brand/Index.cshtml", response);
                }

                var dbresponse = await _brandService.GetBrandInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return View("~/Views/Setup/Brand/Index.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/Brand/Index.cshtml", response);
        }

        [HttpPost, Route("~/brand/create")]
        public async Task<IActionResult> CreateBrandInfo(BrandInfo brandInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (brandInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _brandService.SaveBrandInfo(brandInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.CreateBrandInfo({@brandInfo}, {@loggedInUserId})", brandInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/brand/update")]
        public async Task<IActionResult> UpdateBrandInfo(BrandInfo brandInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (brandInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _brandService.SaveBrandInfo(brandInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.UpdateBrandInfo({@brandInfo}, {@loggedInUserId})", brandInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/brand/list")]
        public async Task<IActionResult> GetBrandInfoList()
        {
            Response<List<BrandInfo>> response = new Response<List<BrandInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/Brand/_BrandList.cshtml", response);
                }

                var dbresponse = await _brandService.GetBrandInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.GetBrandInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }
            
            return PartialView("~/Views/Setup/Brand/_BrandList.cshtml", response);

        }

        [HttpGet, Route("~/brand/{brandInfoId:Guid}")]
        public async Task<IActionResult> GetBrandInfoById(Guid brandInfoId)
        {
            Response<BrandInfo> response = new Response<BrandInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/Brand/_CreateBrand.cshtml", null);
                }

                if (!Helpers.IsValidGuid(brandInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/Brand/_CreateBrand.cshtml", null);
                }

                var dbresponse = await _brandService.GetBrandInfoById(brandInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.GetBrandInfoById({@brandInfoId}, {@loggedInUserId})", brandInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/Brand/_CreateBrand.cshtml", response.Data);
        }

        [HttpGet, Route("~/brand/data-list")]
        public async Task<IActionResult> GetBrandInfoDataList()
        {
            Response<List<BrandInfo>> response = new Response<List<BrandInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_BrandList.cshtml", response);
                }

                var dbresponse = await _brandService.GetBrandInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.GetBrandInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_BrandList.cshtml", response);
        }
    }
}
