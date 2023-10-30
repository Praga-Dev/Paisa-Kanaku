using Dapper;
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
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;

        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Response<ProductInfo>> GetProductInfoById(Guid productInfoId, Guid loggedInUserId)
        {
            Response<ProductInfo> response = new Response<ProductInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(productInfoId))
                {
                    return response;
                }

                Response<ProductInfoDB> dbResponse = await _productRepository.GetProductInfoById(productInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new ProductInfo()
                    {
                        Id = dbResponse.Data.Id,
                        Name = dbResponse.Data.Name,
                        BrandInfo = new()
                        {
                            Id = dbResponse.Data.BrandId,
                            Name = dbResponse.Data.BrandName,
                        },
                        Description = dbResponse.Data.Description,
                        ExpenseTypeInfo = new()
                        {
                            ExpenseType = dbResponse.Data.ExpenseType,
                            ExpenseTypeValue = dbResponse.Data.ExpenseTypeValue
                        },
                        PreferredTimePeriodInfo = new()
                        {
                            TimePeriodType = dbResponse.Data.PreferredRecurringTimePeriod,
                            TimePeriodTypeValue = dbResponse.Data.PreferredRecurringTimePeriodValue,
                        },
                        Price = dbResponse.Data.Price,
                        ProductCategoryInfo = new()
                        {
                            ProductCategory = dbResponse.Data.ProductCategory,
                            ProductCategoryValue = dbResponse.Data.ProductCategoryValue
                        },
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
                _logger.LogError(ex, "Error in ProductService.GetProductInfoList({@productInfoId}, {@loggedInUserId})", productInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<ProductInfo>>> GetProductInfoList(Guid loggedInUserId)
        {
            Response<List<ProductInfo>> response = new Response<List<ProductInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _productRepository.GetProductInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(product => new ProductInfo()
                  {
                      Id = product.Id,
                      Name = product.Name,
                      BrandInfo = new()
                      {
                          Id = product.BrandId,
                          Name = product.BrandName,
                      },
                      Description = product.Description,
                      ExpenseTypeInfo = new()
                      {
                          ExpenseType = product.ExpenseType,
                          ExpenseTypeValue = product.ExpenseTypeValue
                      },
                      PreferredTimePeriodInfo = new()
                      {
                          TimePeriodType = product.PreferredRecurringTimePeriod,
                          TimePeriodTypeValue = product.PreferredRecurringTimePeriodValue,
                      },
                      Price = product.Price,
                      ProductCategoryInfo = new()
                      {
                          ProductCategory = product.ProductCategory,
                          ProductCategoryValue = product.ProductCategoryValue
                      },
                      SequenceId = product.SequenceId,
                      CreatedBy = product.CreatedBy,
                      CreatedDate = product.CreatedDate,
                      ModifiedBy = product.ModifiedBy,
                      ModifiedDate = product.ModifiedDate,
                      RowStatus = product.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductService.GetProductInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveProductInfo(ProductInfo productInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (productInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(productInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid Product Name");
                }

                if (productInfo.Name.Length < 2 || productInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("Product Name must be between 2 and 50 Characters long.");
                }

                if (productInfo.BrandInfo == null || (!Helpers.IsValidGuid(productInfo.BrandInfo.Id) && string.IsNullOrWhiteSpace(productInfo.BrandInfo.Name)))
                {
                    response.ValidationErrorMessages.Add("Invalid Brand");
                }

                if (productInfo.ProductCategoryInfo == null || string.IsNullOrWhiteSpace(productInfo.ProductCategoryInfo.ProductCategory))
                {
                    response.ValidationErrorMessages.Add("Invalid Product Category");
                }

                if (productInfo.ExpenseTypeInfo == null || string.IsNullOrWhiteSpace(productInfo.ExpenseTypeInfo.ExpenseType))
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Type");
                }

                if (productInfo.PreferredTimePeriodInfo == null || string.IsNullOrWhiteSpace(productInfo.PreferredTimePeriodInfo.TimePeriodType))
                {
                    response.ValidationErrorMessages.Add("Invalid Preferred Time Period");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ProductInfoDB productInfoDb = new ProductInfoDB()
                {
                    Name = productInfo.Name,
                    BrandId = productInfo.BrandInfo.Id,
                    BrandName = productInfo.BrandInfo.Name,
                    Description = productInfo.Description,
                    ExpenseType = productInfo.ExpenseTypeInfo.ExpenseType,
                    PreferredRecurringTimePeriod = productInfo.PreferredTimePeriodInfo?.TimePeriodType,
                    Price = productInfo.Price,
                    ProductCategory = productInfo.ProductCategoryInfo.ProductCategory
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(productInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    productInfoDb.Id = productInfo.Id;
                }

                return await _productRepository.SaveProductInfo(productInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductService.SaveProductInfo({@productInfo}, {@loggedInUserId})", productInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }

        public async Task<Response<string>> ExportProductInfoData(Guid loggedInUserId)
        {
            Response<string> response = new Response<string>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                string csv = String.Empty;
                List<string> COLUMN_NAMES = new() { "Product Name", "Price", "Product Category", "Brand", "Expense Type", "Preferred Recurring Time Period" };

                foreach (string column in COLUMN_NAMES)
                    csv += column + " , ";

                csv = csv[..^3];

                //Add new line.
                csv += "\r\n";

                Response<List<ProductInfoDB>> productInfoList = await _productRepository.GetProductInfoList(loggedInUserId);

                if (Helpers.IsResponseValid(productInfoList))
                {
                    foreach (var product in productInfoList.Data)
                    {
                        csv += product.Name?.Replace(",", ";") + ',';
                        csv += Convert.ToString(product.Price).Replace(",", ";") + ',';
                        csv += product.ProductCategoryValue?.Replace(",", ";") + ',';
                        csv += product.BrandName?.Replace(",", ";") + ',';
                        csv += product.ExpenseTypeValue?.Replace(",", ";") + ',';
                        csv += product.PreferredRecurringTimePeriodValue?.Replace(",", ";");

                        csv += "\r\n";
                    }
                }

                response = response.GetSuccessResponse(csv);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProductService.ExportProductInfoData({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
