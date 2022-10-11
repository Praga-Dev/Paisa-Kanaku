using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Setup
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ILogger<MemberRepository> _logger;
        private readonly IDataBaseConnection _db;

        public MemberRepository(ILogger<MemberRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<MemberInfoDb>> GetMemberInfoById(Guid memberInfoId, Guid loggedInUserId)
        {
            Response<MemberInfoDb> response = new Response<MemberInfoDb>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_MEMBER_INFO_GET_BY_ID;

                DynamicParameters parameters = new();
                parameters.Add("@MemberInfoId", memberInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<MemberInfoDb>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberRepository.GetMemberInfoById({@memberInfoId}, {@loggedInUserId})", memberInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<MemberInfoDb>>> GetMemberInfoList(Guid loggedInUserId)
        {
            Response<List<MemberInfoDb>> response = new Response<List<MemberInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_MEMBER_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<MemberInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberRepository.GetMemberInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveMemberInfo(MemberInfoDb memberInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_MEMBER_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", memberInfoDb.Id, DbType.Guid);
                parameters.Add("@Name", memberInfoDb.Name, DbType.String);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);
                parameters.Add("@Result", null, DbType.Guid, direction: ParameterDirection.Output);

                var returnValue = await _db.Connection.QueryAsync<Guid>(spName, parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<Guid>("@Result");

                if (!returnValue.Any() && result != Guid.Empty)
                {
                    response = response.GetSuccessResponse(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MemberRepository.SaveMemberInfo({@memberInfoDb}, {@loggedInUserId})", memberInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }

}
