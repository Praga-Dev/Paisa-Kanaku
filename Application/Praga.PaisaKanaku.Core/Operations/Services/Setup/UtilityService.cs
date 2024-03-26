using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.PaisaKanaku.Core.Operations.Services.Setup
{
    public class UtilityService : IUtilityService
    {
        private readonly ILogger<UtilityService> _logger;

        private readonly IUtilityRepository _utilityRepository;

        public UtilityService(ILogger<UtilityService> logger, IUtilityRepository utilityRepository)
        {
            _logger = logger;
            _utilityRepository = utilityRepository;
        }

        public async Task<Response<UtilityInfo>> GetUtilityInfoById(Guid utilityInfoId, Guid loggedInUserId)
        {
            Response<UtilityInfo> response = new Response<UtilityInfo>().GetNotFoundResponse();

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    return response.GetNotAuthorizedResponse();
                }

                if (!Helpers.IsValidGuid(utilityInfoId))
                {
                    return response.GetFailedResponse(ResponseConstants.INVALID_ID);
                }

                Response<UtilityInfoDB> dbResponse = await _utilityRepository.GetUtilityInfoById(utilityInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = GetUtilityInfo(dbResponse.Data);
                    response = response.GetSuccessResponse(response.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UtilityService.GetUtilityInfoList({@utilityInfoId}, {@loggedInUserId})", utilityInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<UtilityInfo>>> GetUtilityInfoList(Guid loggedInUserId)
        {
            Response<List<UtilityInfo>> response = new Response<List<UtilityInfo>>().GetNotFoundResponse();

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    return response.GetNotAuthorizedResponse();
                }

                var dbResponse = await _utilityRepository.GetUtilityInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(utility => GetUtilityInfo(utility)).ToList();
                    response = response.GetSuccessResponse(response.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UtilityService.GetUtilityInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveUtilityInfo(UtilityInfo utilityInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    return response.GetNotAuthorizedResponse();
                }

                if (utilityInfo == null)
                {
                    return response.GetFailedResponse(ResponseConstants.INVALID_PARAM);
                }

                if (string.IsNullOrWhiteSpace(utilityInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid Utility Name");
                }

                if (utilityInfo.Name.Length < 2 || utilityInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("Utility Name must be between 2 and 50 Characters long.");
                }

                if (utilityInfo.ConsumerType == null || string.IsNullOrWhiteSpace(utilityInfo.ConsumerType.ConsumerType))
                {
                    response.ValidationErrorMessages.Add("ConsumerType is Invalid.");
                }

                if (utilityInfo.RecurringType == null || string.IsNullOrWhiteSpace(utilityInfo.RecurringType.TimePeriodType))
                {
                    response.ValidationErrorMessages.Add("ConsumerType is Invalid.");
                }

                if (response.HasValidationErrorMessages)
                {
                    return response.GetValidationFailedResponse();
                }

                UtilityInfoDB utilityInfoDb = GetUtilityInfoDBForSave(utilityInfo);

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(utilityInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    utilityInfoDb.Id = utilityInfo.Id;
                }

                return await _utilityRepository.SaveUtilityInfo(utilityInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UtilityService.SaveUtilityInfo({@utilityInfo}, {@loggedInUserId})", utilityInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        #region Private Methods

        private static UtilityInfoDB GetUtilityInfoDBForSave(UtilityInfo utility) => utility is null ? new UtilityInfoDB()
            : new UtilityInfoDB()
            {
                Name = utility.Name,
                ConsumerType = utility.ConsumerType.ConsumerType,
                RecurringType = utility.RecurringType.TimePeriodType,
                IsEssential = utility.IsEssential
            };

        private static UtilityInfo GetUtilityInfo(UtilityInfoDB utility) => utility is null ? new UtilityInfo()
            : new UtilityInfo()
            {
                Id = utility.Id,
                Name = utility.Name,
                IsEssential = utility.IsEssential,
                ConsumerType = new()
                {
                    ConsumerType = utility.ConsumerType,
                    ConsumerTypeValue = utility.ConsumerTypeValue,
                },
                RecurringType = new()
                {
                    TimePeriodType = utility.RecurringType,
                    TimePeriodTypeValue = utility.RecurringTypeValue,
                },
                SequenceId = utility.SequenceId,
                CreatedBy = utility.CreatedBy,
                CreatedDate = utility.CreatedDate,
                ModifiedBy = utility.ModifiedBy,
                ModifiedDate = utility.ModifiedDate,
                RowStatus = utility.RowStatus
            };

        #endregion
    }
}
