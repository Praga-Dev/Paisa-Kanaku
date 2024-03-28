using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Setup.Group;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Setup
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ILogger<GroupRepository> _logger;
        private readonly IDataBaseConnection _db;

        public GroupRepository(ILogger<GroupRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<GroupInfoDB>> GetGroupInfoById(Guid groupInfoId, Guid loggedInUserId)
        {
            Response<GroupInfoDB> response = new Response<GroupInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_GROUP_INFO_GET_BY_ID;

                DynamicParameters parameters = new();
                parameters.Add("@GroupInfoId", groupInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<GroupInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupRepository.GetGroupInfoById({@groupInfoId}, {@loggedInUserId})", groupInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<GroupInfoDB>>> GetGroupInfoList(Guid loggedInUserId)
        {
            Response<List<GroupInfoDB>> response = new Response<List<GroupInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_GROUP_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<GroupInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GroupRepository.GetGroupInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveGroupInfo(GroupInfoDB groupInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_GROUP_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", groupInfoDb.Id, DbType.Guid);
                parameters.Add("@Name", groupInfoDb.Name, DbType.String);
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
                _logger.LogError(ex, "Error in GroupRepository.SaveGroupInfo({@groupInfoDb}, {@loggedInUserId})", groupInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }

}
