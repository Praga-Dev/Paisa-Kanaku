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
    public class MemberService : IMemberService
    {
        private readonly ILogger<MemberService> _logger;

        private readonly IMemberRepository _memberRepository;

        public MemberService(ILogger<MemberService> logger, IMemberRepository memberRepository)
        {
            _logger = logger;
            _memberRepository = memberRepository;
        }

        public async Task<Response<MemberInfo>> GetMemberInfoById(Guid memberInfoId, Guid loggedInUserId)
        {
            Response<MemberInfo> response = new Response<MemberInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(memberInfoId))
                {
                    return response;
                }

                Response<MemberInfoDb> dbResponse = await _memberRepository.GetMemberInfoById(memberInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new MemberInfo()
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
                _logger.LogError(ex, "Error in MemberService.GetMemberInfoList({@memberInfoId}, {@loggedInUserId})", memberInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<MemberInfo>>> GetMemberInfoList(Guid loggedInUserId)
        {
            Response<List<MemberInfo>> response = new Response<List<MemberInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _memberRepository.GetMemberInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(member => new MemberInfo()
                  {
                      Id = member.Id,
                      Name = member.Name,
                      SequenceId = member.SequenceId,
                      CreatedBy = member.CreatedBy,
                      CreatedDate = member.CreatedDate,
                      ModifiedBy = member.ModifiedBy,
                      ModifiedDate = member.ModifiedDate,
                      RowStatus = member.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberService.GetMemberInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveMemberInfo(MemberInfo memberInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (memberInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(memberInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid Member Name");
                }

                if (memberInfo.Name.Length < 2 || memberInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("Member Name must be between 2 and 50 Characters long.");
                }

                MemberInfoDb memberInfoDb = new MemberInfoDb()
                {
                    Name = memberInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(memberInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    memberInfoDb.Id = memberInfo.Id;
                }

                return await _memberRepository.SaveMemberInfo(memberInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberService.SaveMemberInfo({@memberInfo}, {@loggedInUserId})", memberInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
