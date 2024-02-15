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
    public class ProductCategoryController : BaseController
    {
        private readonly ILogger<LookupsController> _logger;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(ILogger<LookupsController> logger, IProductCategoryService productCategoryService) : base()
        {
            _logger = logger;
            _productCategoryService = productCategoryService;
        }


        [HttpPost, Route("~/v1/setup/productCategory/create")]
        public async Task<IActionResult> CreateProductCategoryInfo(ProductCategoryInfo_API productCategoryInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (productCategoryInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var productCategoryInfo = CommonMapper.GetProductCategoryInfo(productCategoryInfoAPISave);
                var dbresponse = await _productCategoryService.SaveProductCategoryInfo(productCategoryInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.SaveProductCategoryInfo({@productCategoryInfo}, {@loggedInUserId})", productCategoryInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/v1/setup/productCategory/update")]
        public async Task<IActionResult> UpdateProductCategoryInfo(ProductCategoryInfo_API productCategoryInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (productCategoryInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var productCategoryInfo = CommonMapper.GetProductCategoryInfo(productCategoryInfoAPISave);
                var dbresponse = await _productCategoryService.SaveProductCategoryInfo(productCategoryInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.SaveProductCategoryInfo({@productCategoryInfo}, {@loggedInUserId})", productCategoryInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/v1/setup/productCategory")]
        public async Task<IActionResult> GetProductCategoryInfoList()
        {
            Response<List<ProductCategoryInfo_API>> response = new Response<List<ProductCategoryInfo_API>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _productCategoryService.GetProductCategoryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetProductCategoryInfoListAPI(dbresponse.Data);
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

        [HttpGet, Route("~/v1/setup/productCategory/{productCategoryInfoId:Guid}")]
        public async Task<IActionResult> GetProductCategoryInfoById(Guid productCategoryInfoId)
        {
            Response<ProductCategoryInfo_API> response = new Response<ProductCategoryInfo_API>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _productCategoryService.GetProductCategoryInfoById(productCategoryInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetProductCategoryInfoAPI(dbresponse.Data);
                    response = response.GetSuccessResponse(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsController.GetExpenseTypeInfoList({@productCategoryInfoId}, {@loggedInUserId})", productCategoryInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
