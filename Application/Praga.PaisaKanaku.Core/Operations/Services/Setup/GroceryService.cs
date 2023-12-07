using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.PaisaKanaku.Core.Operations.Services.Setup
{
    public class GroceryService : IGroceryService
    {
        private readonly ILogger<GroceryService> _logger;

        private readonly IGroceryRepository _groceryRepository;

        public GroceryService(ILogger<GroceryService> logger, IGroceryRepository groceryRepository)
        {
            _logger = logger;
            _groceryRepository = groceryRepository;
        }

        public async Task<Response<GroceryInfo>> GetGroceryInfoById(Guid groceryInfoId, Guid loggedInUserId)
        {
            Response<GroceryInfo> response = new Response<GroceryInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(groceryInfoId))
                {
                    return response;
                }

                Response<GroceryInfoDB> dbResponse = await _groceryRepository.GetGroceryInfoById(groceryInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new GroceryInfo()
                    {
                        Id = dbResponse.Data.Id,
                        Name = dbResponse.Data.Name,
                        BrandInfo = new()
                        {
                            Id = dbResponse.Data.BrandId,
                            Name = dbResponse.Data.BrandName,
                        },
                        MetricSystemInfo = new()
                        {
                            MetricSystem = dbResponse.Data.MetricSystem,
                            MetricSystemValue = dbResponse.Data.MetricSystemValue,
                        },
                        PreferredTimePeriodInfo = new()
                        {
                            TimePeriodType = dbResponse.Data.PreferredRecurringTimePeriod,
                            TimePeriodTypeValue = dbResponse.Data.PreferredRecurringTimePeriodValue,
                        },
                        GroceryCategoryInfo = new()
                        {
                            GroceryCategory = dbResponse.Data.GroceryCategory,
                            GroceryCategoryValue = dbResponse.Data.GroceryCategoryValue
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
                _logger.LogError(ex, "Error in GroceryService.GetGroceryInfoList({@groceryInfoId}, {@loggedInUserId})", groceryInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<GroceryInfo>>> GetGroceryInfoList(Guid loggedInUserId)
        {
            Response<List<GroceryInfo>> response = new Response<List<GroceryInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _groceryRepository.GetGroceryInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(grocery => new GroceryInfo()
                  {
                      Id = grocery.Id,
                      Name = grocery.Name,
                      BrandInfo = new()
                      {
                          Id = grocery.BrandId,
                          Name = grocery.BrandName,
                      },
                      MetricSystemInfo = new()
                      {
                          MetricSystem = grocery.MetricSystem,
                          MetricSystemValue = grocery.MetricSystemValue,
                      },
                      PreferredTimePeriodInfo = new()
                      {
                          TimePeriodType = grocery.PreferredRecurringTimePeriod,
                          TimePeriodTypeValue = grocery.PreferredRecurringTimePeriodValue,
                      },
                      GroceryCategoryInfo = new()
                      {
                          GroceryCategory = grocery.GroceryCategory,
                          GroceryCategoryValue = grocery.GroceryCategoryValue
                      },
                      SequenceId = grocery.SequenceId,
                      CreatedBy = grocery.CreatedBy,
                      CreatedDate = grocery.CreatedDate,
                      ModifiedBy = grocery.ModifiedBy,
                      ModifiedDate = grocery.ModifiedDate,
                      RowStatus = grocery.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryService.GetGroceryInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveGroceryInfo(GroceryInfo groceryInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (groceryInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(groceryInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid Grocery Name");
                }

                if (groceryInfo.Name.Length < 2 || groceryInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("Grocery Name must be between 2 and 50 Characters long.");
                }

                if (groceryInfo.BrandInfo == null || (!Helpers.IsValidGuid(groceryInfo.BrandInfo.Id) && string.IsNullOrWhiteSpace(groceryInfo.BrandInfo.Name)))
                {
                    response.ValidationErrorMessages.Add("Invalid Brand");
                }

                if (groceryInfo.GroceryCategoryInfo == null || string.IsNullOrWhiteSpace(groceryInfo.GroceryCategoryInfo.GroceryCategory))
                {
                    response.ValidationErrorMessages.Add("Invalid Grocery Category");
                }

                if (groceryInfo.MetricSystemInfo == null || string.IsNullOrWhiteSpace(groceryInfo.MetricSystemInfo.MetricSystem))
                {
                    response.ValidationErrorMessages.Add("Invalid Metric System");
                }

                if (groceryInfo.PreferredTimePeriodInfo == null || string.IsNullOrWhiteSpace(groceryInfo.PreferredTimePeriodInfo.TimePeriodType))
                {
                    response.ValidationErrorMessages.Add("Invalid Preferred Time Period");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                GroceryInfoDB groceryInfoDb = new GroceryInfoDB()
                {
                    Name = groceryInfo.Name,
                    BrandId = groceryInfo.BrandInfo.Id,
                    BrandName = groceryInfo.BrandInfo.Name,
                    GroceryCategory = groceryInfo.GroceryCategoryInfo.GroceryCategory,
                    MetricSystem = groceryInfo.MetricSystemInfo?.MetricSystem,
                    PreferredRecurringTimePeriod = groceryInfo.PreferredTimePeriodInfo?.TimePeriodType
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(groceryInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    groceryInfoDb.Id = groceryInfo.Id;
                }

                return await _groceryRepository.SaveGroceryInfo(groceryInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroceryService.SaveGroceryInfo({@groceryInfo}, {@loggedInUserId})", groceryInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
