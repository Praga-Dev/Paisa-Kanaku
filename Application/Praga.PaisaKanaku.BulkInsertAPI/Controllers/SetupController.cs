using Microsoft.AspNetCore.Mvc;
using Praga.PaisaKanaku.BulkInsertAPI.Helper;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System.Collections.Generic;

namespace Praga.PaisaKanaku.BulkInsertAPI.Controllers
{
    [ApiController]
    public class SetupController : BaseController
    {
        private readonly ILogger<SetupController> _logger;
        private readonly IBrandService _brandService;
        private readonly IMemberService _memberService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public SetupController(ILogger<SetupController> logger, IBrandService brandService
            , IMemberService memberService, IProductCategoryService productCategoryService
            , IProductService productService) : base()
        {
            _logger = logger;
            _brandService = brandService;
            _memberService = memberService;
            _productCategoryService = productCategoryService;
            _productService = productService;
        }


        [HttpPost, Route("~/v1/setup/brand/create")]
        public async Task<IActionResult> CreateBrandInfo(int brandCount)
        {
            Response<string> response = new Response<string>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                for (int i = 0; i < brandCount; i++)
                {
                    string brandName = RandomGeneratorHelper.GetRandomString(50);
                    BrandInfo brandInfo = new()
                    {
                        Name = brandName
                    };

                    var dbresponse = await _brandService.SaveBrandInfo(brandInfo, false, LoggedInUserId);
                    if (!dbresponse.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
                    }
                }

                response = response.GetSuccessResponse(ResponseConstants.SUCCESS);
                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SetupController.CreateBrandInfo({@brandCount}, {@loggedInUserId})", brandCount.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPost, Route("~/v1/setup/member/create")]
        public async Task<IActionResult> CreateMemberInfo(int memberCount)
        {
            Response<string> response = new Response<string>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                for (int i = 0; i < memberCount; i++)
                {
                    string memberName = RandomGeneratorHelper.GetRandomString(50);
                    MemberInfo memberInfo = new()
                    {
                        Name = memberName
                    };

                    var dbresponse = await _memberService.SaveMemberInfo(memberInfo, false, LoggedInUserId);
                    if (!dbresponse.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
                    }
                }

                response = response.GetSuccessResponse(ResponseConstants.SUCCESS);
                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SetupController.CreateMemberInfo({@memberCount}, {@loggedInUserId})", memberCount.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpPost, Route("~/v1/setup/product-category/create")]
        public async Task<IActionResult> CreateProductCategoryInfo(int productCategoryCount)
        {
            Response<string> response = new Response<string>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                for (int i = 0; i < productCategoryCount; i++)
                {
                    string productCategoryName = RandomGeneratorHelper.GetRandomString(50);
                    ProductCategoryInfo productCategoryInfo = new()
                    {
                        Name = productCategoryName
                    };

                    var dbresponse = await _productCategoryService.SaveProductCategoryInfo(productCategoryInfo, false, LoggedInUserId);
                    if (!dbresponse.IsSuccess)
                    {
                        return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
                    }
                }

                response = response.GetSuccessResponse(ResponseConstants.SUCCESS);
                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SetupController.CreateProductCategoryInfo({@productCategoryCount}, {@loggedInUserId})", productCategoryCount.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
