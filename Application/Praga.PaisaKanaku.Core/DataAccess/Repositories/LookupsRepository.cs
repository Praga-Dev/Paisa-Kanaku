using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using Praga.PaisaKanaku.Core.DataEntities.Lookups;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories
{
    public class LookupsRepository : ILookupsRepository
    {
        private readonly ILogger<LookupsRepository> _logger;
        private readonly IDataBaseConnection _db;

        public LookupsRepository(ILogger<LookupsRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<List<ExpenseTypeInfoDb>>> GetExpenseTypeInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseTypeInfoDb>> response = new Response<List<ExpenseTypeInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_EXPENSE_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<ExpenseTypeInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetExpenseTypeInfoList({@loggedInUserId})", loggedInUserId );
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<LiquidMeasureInfoDb>>> GetLiquidMeasureInfoList(Guid loggedInUserId)
        {
            Response<List<LiquidMeasureInfoDb>> response = new Response<List<LiquidMeasureInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_LIQUID_MEASURE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<LiquidMeasureInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetLiquidMeasureInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<MeasureTypeInfoDb>>> GetMeasureTypeInfoList(Guid loggedInUserId)
        {
            Response<List<MeasureTypeInfoDb>> response = new Response<List<MeasureTypeInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_MEASURE_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<MeasureTypeInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMeasureTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<TimePeriodInfoDb>>> GetTimePeriodInfoList(Guid loggedInUserId)
        {
            Response<List<TimePeriodInfoDb>> response = new Response<List<TimePeriodInfoDb>>().GetFailedResponse(ResponseConstants.NO_RECORDS_FOUND);

            try
            {
                string spName = DatabaseConstants.USP_TIME_PERIOD_TYPE_INFO_GET;
                var param = new { LoggedInUserId = loggedInUserId };

                var result = await _db.Connection.QueryAsync<TimePeriodInfoDb>(spName, param, commandType: CommandType.StoredProcedure);
                return result != null && result.Any() ? response.GetSuccessResponse(result.ToList()) : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTimePeriodInfoDbList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
