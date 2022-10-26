using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using Praga.PaisaKanaku.Core.Operations.Services.Setup;
using System.Net;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ProductCategoryController : BaseController
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(ILogger<ProductCategoryController> logger, IProductCategoryService productCategoryService) : base()
        {
            _logger = logger;
            _productCategoryService = productCategoryService;
        }

        [HttpGet, Route("~/product-category/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ProductCategoryInfo>> response = new Response<List<ProductCategoryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Setup/ProductCategory/Index.cshtml", response);
                }

                var dbresponse = await _productCategoryService.GetProductCategoryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.Index()", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Setup/ProductCategory/Index.cshtml", response);
        }


        [HttpPost, Route("~/product-category/create")]
        public async Task<IActionResult> CreateProductCategoryInfo(ProductCategoryInfo productCategoryInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (productCategoryInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _productCategoryService.SaveProductCategoryInfo(productCategoryInfo, false, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.CreateProductCategoryInfo({@productCategoryInfo}, {@loggedInUserId})", productCategoryInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPut, Route("~/product-category/update")]
        public async Task<IActionResult> UpdateProductCategoryInfo(ProductCategoryInfo productCategoryInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (productCategoryInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _productCategoryService.SaveProductCategoryInfo(productCategoryInfo, true, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.UpdateProductCategoryInfo({@productCategoryInfo}, {@loggedInUserId})", productCategoryInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/product-category/list")]
        public async Task<IActionResult> GetProductCategoryInfoList()
        {
            Response<List<ProductCategoryInfo>> response = new Response<List<ProductCategoryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/ProductCategory/_ProductCategoryList.cshtml", response);
                }

                var dbresponse = await _productCategoryService.GetProductCategoryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.GetProductCategoryInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/ProductCategory/_ProductCategoryList.cshtml", response);

        }

        [HttpGet, Route("~/product-category/{productCategoryInfoId:Guid}")]
        public async Task<IActionResult> GetProductCategoryInfoById(Guid productCategoryInfoId)
        {
            Response<ProductCategoryInfo> response = new Response<ProductCategoryInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Setup/ProductCategory/_CreateProductCategory.cshtml", null);
                }

                if (!Helpers.IsValidGuid(productCategoryInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Setup/ProductCategory/_CreateProductCategory.cshtml", null);
                }

                var dbresponse = await _productCategoryService.GetProductCategoryInfoById(productCategoryInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryController.GetProductCategoryInfoById({@productCategoryInfoId}, {@loggedInUserId})", productCategoryInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Setup/ProductCategory/_CreateProductCategory.cshtml", response.Data);
        }

        [HttpGet, Route("~/product-category/data-list")]
        public async Task<IActionResult> GetProductCategoryInfoDataList()
        {
            Response<List<ProductCategoryInfo>> response = new Response<List<ProductCategoryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_ProductCategoryList.cshtml", response);
                }

                var dbresponse = await _productCategoryService.GetProductCategoryInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandController.GetProductCategoryInfoDataList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_ProductCategoryList.cshtml", response);
        }

    }
}
