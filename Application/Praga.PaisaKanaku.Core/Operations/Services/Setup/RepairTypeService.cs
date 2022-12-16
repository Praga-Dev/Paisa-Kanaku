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
    public class RepairTypeService : IRepairTypeService
    {
        private readonly ILogger<RepairTypeService> _logger;

        private readonly IRepairTypeRepository _repairTypeRepository;

        public RepairTypeService(ILogger<RepairTypeService> logger, IRepairTypeRepository repairTypeRepository)
        {
            _logger = logger;
            _repairTypeRepository = repairTypeRepository;
        }

        public async Task<Response<RepairTypeInfo>> GetRepairTypeInfoById(Guid repairTypeInfoId, Guid loggedInUserId)
        {
            Response<RepairTypeInfo> response = new Response<RepairTypeInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(repairTypeInfoId))
                {
                    return response;
                }

                Response<RepairTypeInfoDb> dbResponse = await _repairTypeRepository.GetRepairTypeInfoById(repairTypeInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new RepairTypeInfo()
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
                _logger.LogError(ex, "Error in RepairTypeService.GetRepairTypeInfoList({@repairTypeInfoId}, {@loggedInUserId})", repairTypeInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<RepairTypeInfo>>> GetRepairTypeInfoList(Guid loggedInUserId)
        {
            Response<List<RepairTypeInfo>> response = new Response<List<RepairTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _repairTypeRepository.GetRepairTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(repairType => new RepairTypeInfo()
                  {
                      Id = repairType.Id,
                      Name = repairType.Name,
                      SequenceId = repairType.SequenceId,
                      CreatedBy = repairType.CreatedBy,
                      CreatedDate = repairType.CreatedDate,
                      ModifiedBy = repairType.ModifiedBy,
                      ModifiedDate = repairType.ModifiedDate,
                      RowStatus = repairType.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeService.GetRepairTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveRepairTypeInfo(RepairTypeInfo repairTypeInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (repairTypeInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(repairTypeInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid RepairType Name");
                }

                if (repairTypeInfo.Name.Length < 2 || repairTypeInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("RepairType Name must be between 2 and 50 Characters long.");
                }

                RepairTypeInfoDb repairTypeInfoDb = new RepairTypeInfoDb()
                {
                    Name = repairTypeInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(repairTypeInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    repairTypeInfoDb.Id = repairTypeInfo.Id;
                }

                return await _repairTypeRepository.SaveRepairTypeInfo(repairTypeInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeService.SaveRepairTypeInfo({@repairTypeInfo}, {@loggedInUserId})", repairTypeInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
