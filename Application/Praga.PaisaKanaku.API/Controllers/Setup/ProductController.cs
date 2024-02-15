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
    public class ProductController : BaseController
    {
        private readonly ILogger<LookupsController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<LookupsController> logger, IProductService productService) : base()
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpPost, Route("~/v1/setup/product/create")]
        public async Task<IActionResult> CreateProductInfo(ProductInfo_API productInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (productInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var productInfo = CommonMapper.GetProductInfo(productInfoAPISave);
                var dbresponse = await _productService.SaveProductInfo(productInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductController.SaveProductInfo({@productInfo}, {@loggedInUserId})", productInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/v1/setup/product/update")]
        public async Task<IActionResult> UpdateProductInfo(ProductInfo_API productInfoAPISave)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (productInfoAPISave == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var productInfo = CommonMapper.GetProductInfo(productInfoAPISave);
                var dbresponse = await _productService.SaveProductInfo(productInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductController.SaveProductInfo({@productInfo}, {@loggedInUserId})", productInfoAPISave.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/v1/setup/product")]
        public async Task<IActionResult> GetProductInfoList()
        {
            Response<List<ProductInfo_API>> response = new Response<List<ProductInfo_API>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _productService.GetProductInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetProductInfoListAPI(dbresponse.Data);
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

        [HttpGet, Route("~/v1/setup/product/{productInfoId:Guid}")]
        public async Task<IActionResult> GetProductInfoById(Guid productInfoId)
        {
            Response<ProductInfo_API> response = new Response<ProductInfo_API>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _productService.GetProductInfoById(productInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    var data = CommonMapper.GetProductInfoAPI(dbresponse.Data);
                    response = response.GetSuccessResponse(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsController.GetExpenseTypeInfoList({@productInfoId}, {@loggedInUserId})", productInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
