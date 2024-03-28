using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Setup.Group;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Group;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.PaisaKanaku.Core.Operations.Services.Setup
{
    public class GroupService : IGroupService
    {
        private readonly ILogger<GroupService> _logger;

        private readonly IGroupRepository _groupRepository;

        public GroupService(ILogger<GroupService> logger, IGroupRepository groupRepository)
        {
            _logger = logger;
            _groupRepository = groupRepository;
        }

        public async Task<Response<GroupInfo>> GetGroupInfoById(Guid groupInfoId, Guid loggedInUserId)
        {
            Response<GroupInfo> response = new Response<GroupInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(groupInfoId))
                {
                    return response;
                }

                Response<GroupInfoDB> dbResponse = await _groupRepository.GetGroupInfoById(groupInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new GroupInfo()
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
                _logger.LogError(ex, "Error in GroupService.GetGroupInfoList({@groupInfoId}, {@loggedInUserId})", groupInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<GroupInfo>>> GetGroupInfoList(Guid loggedInUserId)
        {
            Response<List<GroupInfo>> response = new Response<List<GroupInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _groupRepository.GetGroupInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(group => new GroupInfo()
                  {
                      Id = group.Id,
                      Name = group.Name,
                      SequenceId = group.SequenceId,
                      CreatedBy = group.CreatedBy,
                      CreatedDate = group.CreatedDate,
                      ModifiedBy = group.ModifiedBy,
                      ModifiedDate = group.ModifiedDate,
                      RowStatus = group.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupService.GetGroupInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveGroupInfo(GroupInfo groupInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (groupInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(groupInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid Group Name");
                }

                if (groupInfo.Name.Length < 2 || groupInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("Group Name must be between 2 and 50 Characters long.");
                }

                GroupInfoDB groupInfoDb = new GroupInfoDB()
                {
                    Name = groupInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(groupInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    groupInfoDb.Id = groupInfo.Id;
                }

                return await _groupRepository.SaveGroupInfo(groupInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupService.SaveGroupInfo({@groupInfo}, {@loggedInUserId})", groupInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
