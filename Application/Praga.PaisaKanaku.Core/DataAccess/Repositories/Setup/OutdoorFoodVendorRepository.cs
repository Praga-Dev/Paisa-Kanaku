﻿using Dapper;
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
    public class OutdoorFoodVendorRepository : IOutdoorFoodVendorRepository
    {
        private readonly ILogger<OutdoorFoodVendorRepository> _logger;
        private readonly IDataBaseConnection _db;

        public OutdoorFoodVendorRepository(ILogger<OutdoorFoodVendorRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<OutdoorFoodVendorInfoDB>> GetOutdoorFoodVendorInfoById(Guid outdoorFoodVendorInfoId, Guid loggedInUserId)
        {
            Response<OutdoorFoodVendorInfoDB> response = new Response<OutdoorFoodVendorInfoDB>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_OUTDOOR_FOOD_VENDOR_INFO_GET_BY_ID;

                DynamicParameters parameters = new();
                parameters.Add("@OutdoorFoodVendorInfoId", outdoorFoodVendorInfoId, DbType.Guid);
                parameters.Add("@LoggedInUserId", loggedInUserId, DbType.Guid);

                var result = await _db.Connection.QueryAsync<OutdoorFoodVendorInfoDB>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result != null ? response.GetSuccessResponse(result.FirstOrDefault()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorRepository.GetOutdoorFoodVendorInfoById({@outdoorFoodVendorInfoId}, {@loggedInUserId})", outdoorFoodVendorInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<OutdoorFoodVendorInfoDB>>> GetOutdoorFoodVendorInfoList(Guid loggedInUserId)
        {
            Response<List<OutdoorFoodVendorInfoDB>> response = new Response<List<OutdoorFoodVendorInfoDB>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_OUTDOOR_FOOD_VENDOR_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<OutdoorFoodVendorInfoDB>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OutdoorFoodVendorRepository.GetOutdoorFoodVendorInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<Guid>> SaveOutdoorFoodVendorInfo(OutdoorFoodVendorInfoDB outdoorFoodVendorInfoDb, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                string spName = DatabaseConstants.USP_OUTDOOR_FOOD_VENDOR_INFO_SAVE;

                DynamicParameters parameters = new();
                parameters.Add("@Id", outdoorFoodVendorInfoDb.Id, DbType.Guid);
                parameters.Add("@Name", outdoorFoodVendorInfoDb.Name, DbType.String);
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
                _logger.LogError(ex, "Error in OutdoorFoodVendorRepository.SaveOutdoorFoodVendorInfo({@outdoorFoodVendorInfoDb}, {@loggedInUserId})", outdoorFoodVendorInfoDb.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
