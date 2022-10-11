using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.Operations.Services.Setup
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ILogger<ProductCategoryService> _logger;

        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(ILogger<ProductCategoryService> logger, IProductCategoryRepository productCategoryRepository)
        {
            _logger = logger;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<Response<ProductCategoryInfo>> GetProductCategoryInfoById(Guid productCategoryInfoId, Guid loggedInUserId)
        {
            Response<ProductCategoryInfo> response = new Response<ProductCategoryInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(productCategoryInfoId))
                {
                    return response;
                }

                Response<ProductCategoryInfoDb> dbResponse = await _productCategoryRepository.GetProductCategoryInfoById(productCategoryInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new ProductCategoryInfo()
                    {
                        Id = dbResponse.Data.Id,
                        Name = dbResponse.Data.Name,
                        SequenceId = dbResponse.Data.SequenceId,
                        CreatedBy = dbResponse.Data.CreatedBy,
                        CreatedDate = dbResponse.Data.CreatedDate,
                        ModifiedBy = dbResponse.Data.ModifiedBy,
                        ModifiedDate = dbResponse.Data.ModifiedDate,
                        RowStatus = dbResponse.Data.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryService.GetProductCategoryInfoList({@productCategoryInfoId}, {@loggedInUserId})", productCategoryInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<ProductCategoryInfo>>> GetProductCategoryInfoList(Guid loggedInUserId)
        {
            Response<List<ProductCategoryInfo>> response = new Response<List<ProductCategoryInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _productCategoryRepository.GetProductCategoryInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(productCategory => new ProductCategoryInfo()
                  {
                      Id = productCategory.Id,
                      Name = productCategory.Name,
                      SequenceId = productCategory.SequenceId,
                      CreatedBy = productCategory.CreatedBy,
                      CreatedDate = productCategory.CreatedDate,
                      ModifiedBy = productCategory.ModifiedBy,
                      ModifiedDate = productCategory.ModifiedDate,
                      RowStatus = productCategory.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryService.GetProductCategoryInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveProductCategoryInfo(ProductCategoryInfo productCategoryInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (productCategoryInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(productCategoryInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid ProductCategory Name");
                }

                if (productCategoryInfo.Name.Length < 2 || productCategoryInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("ProductCategory Name must be between 2 and 50 Characters long.");
                }

                ProductCategoryInfoDb productCategoryInfoDb = new ProductCategoryInfoDb()
                {
                    Name = productCategoryInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(productCategoryInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    productCategoryInfoDb.Id = productCategoryInfo.Id;
                }

                return await _productCategoryRepository.SaveProductCategoryInfo(productCategoryInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductCategoryService.SaveProductCategoryInfo({@productCategoryInfo}, {@loggedInUserId})", productCategoryInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
