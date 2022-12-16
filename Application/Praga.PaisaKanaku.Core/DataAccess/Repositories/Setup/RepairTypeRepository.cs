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
    public class RepairTypeRepository : IRepairTypeRepository
    {
        private readonly ILogger<RepairTypeRepository> _logger;
        private readonly IDataBaseConnection _db;

        public RepairTypeRepository(ILogger<RepairTypeRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<RepairTypeInfoDb>> GetRepairTypeInfoById(Guid repairTypeInfoId, Guid loggedInUserId)
        {
            Response<RepairTypeInfoDb> response = new Response<RepairTypeInfoDb>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_REPAIR_TYPE_INFO_GET_BY_ID;

                DynamicParameters parameters = new();
                parameters.Add("@RepairTypeInfoId", repairTypeInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<RepairTypeInfoDb>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeRepository.GetRepairTypeInfoById({@repairTypeInfoId}, {@loggedInUserId})", repairTypeInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<RepairTypeInfoDb>>> GetRepairTypeInfoList(Guid loggedInUserId)
        {
            Response<List<RepairTypeInfoDb>> response = new Response<List<RepairTypeInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_REPAIR_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<RepairTypeInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RepairTypeRepository.GetRepairTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveRepairTypeInfo(RepairTypeInfoDb repairTypeInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_REPAIR_TYPE_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", repairTypeInfoDb.Id, DbType.Guid);
                parameters.Add("@Name", repairTypeInfoDb.Name, DbType.String);
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
                _logger.LogError(ex, "Error in RepairTypeRepository.SaveRepairTypeInfo({@repairTypeInfoDb}, {@loggedInUserId})", repairTypeInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
