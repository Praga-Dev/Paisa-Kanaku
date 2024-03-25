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
    public class OutdoorFoodVendorService : IOutdoorFoodVendorService
    {
        private readonly ILogger<OutdoorFoodVendorService> _logger;

        private readonly IOutdoorFoodVendorRepository _outdoorFoodVendorRepository;

        public OutdoorFoodVendorService(ILogger<OutdoorFoodVendorService> logger, IOutdoorFoodVendorRepository outdoorFoodVendorRepository)
        {
            _logger = logger;
            _outdoorFoodVendorRepository = outdoorFoodVendorRepository;
        }

        public async Task<Response<OutdoorFoodVendorInfo>> GetOutdoorFoodVendorInfoById(Guid outdoorFoodVendorInfoId, Guid loggedInUserId)
        {
            Response<OutdoorFoodVendorInfo> response = new Response<OutdoorFoodVendorInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(outdoorFoodVendorInfoId))
                {
                    return response;
                }

                Response<OutdoorFoodVendorInfoDB> dbResponse = await _outdoorFoodVendorRepository.GetOutdoorFoodVendorInfoById(outdoorFoodVendorInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new OutdoorFoodVendorInfo()
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
                _logger.LogError(ex, "Error in OutdoorFoodVendorService.GetOutdoorFoodVendorInfoList({@outdoorFoodVendorInfoId}, {@loggedInUserId})", outdoorFoodVendorInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<OutdoorFoodVendorInfo>>> GetOutdoorFoodVendorInfoList(Guid loggedInUserId)
        {
            Response<List<OutdoorFoodVendorInfo>> response = new Response<List<OutdoorFoodVendorInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _outdoorFoodVendorRepository.GetOutdoorFoodVendorInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(outdoorFoodVendor => new OutdoorFoodVendorInfo()
                  {
                      Id = outdoorFoodVendor.Id,
                      Name = outdoorFoodVendor.Name,
                      SequenceId = outdoorFoodVendor.SequenceId,
                      CreatedBy = outdoorFoodVendor.CreatedBy,
                      CreatedDate = outdoorFoodVendor.CreatedDate,
                      ModifiedBy = outdoorFoodVendor.ModifiedBy,
                      ModifiedDate = outdoorFoodVendor.ModifiedDate,
                      RowStatus = outdoorFoodVendor.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorService.GetOutdoorFoodVendorInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveOutdoorFoodVendorInfo(OutdoorFoodVendorInfo outdoorFoodVendorInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    return response.GetNotAuthorizedResponse();
                }

                if (outdoorFoodVendorInfo == null)
                {
                    return response;
                }

                if (string.IsNullOrWhiteSpace(outdoorFoodVendorInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid OutdoorFoodVendor Name");
                }
                else if (outdoorFoodVendorInfo.Name.Length < 2 || outdoorFoodVendorInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("OutdoorFoodVendor Name must be between 2 and 50 Characters long.");
                }

                if (response.HasValidationErrorMessages)
                {
                    return response.GetValidationFailedResponse();
                }

                OutdoorFoodVendorInfoDB outdoorFoodVendorInfoDb = new OutdoorFoodVendorInfoDB()
                {
                    Name = outdoorFoodVendorInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(outdoorFoodVendorInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    outdoorFoodVendorInfoDb.Id = outdoorFoodVendorInfo.Id;
                }

                return await _outdoorFoodVendorRepository.SaveOutdoorFoodVendorInfo(outdoorFoodVendorInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorService.SaveOutdoorFoodVendorInfo({@outdoorFoodVendorInfo}, {@loggedInUserId})", outdoorFoodVendorInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
