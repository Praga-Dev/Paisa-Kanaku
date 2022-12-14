using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DataEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.Operations.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.Operations.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILogger<LookupService> _logger;

        private readonly ILookupsRepository _lookupsRepository;

        public LookupService(ILogger<LookupService> logger, ILookupsRepository lookupsRepository)
        {
            _logger = logger;
            _lookupsRepository = lookupsRepository;
        }

        public async Task<Response<List<ExpenseTypeInfo>>> GetExpenseTypeInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseTypeInfo>> response = new Response<List<ExpenseTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetExpenseTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(expense => new ExpenseTypeInfo() { ExpenseType = expense.ExpenseType, ExpenseTypeValue = expense.ExpenseTypeValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetExpenseTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<LiquidMeasureInfo>>> GetLiquidMeasureInfoList(Guid loggedInUserId)
        {
            Response<List<LiquidMeasureInfo>> response = new Response<List<LiquidMeasureInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetLiquidMeasureInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                { 
                    response.Data = dbResponse.Data
                  .Select(liquidMeasure => new LiquidMeasureInfo() { LiquidMeasure = liquidMeasure.LiquidMeasure, LiquidMeasureValue = liquidMeasure.LiquidMeasureValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetLiquidMeasureInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<MeasureTypeInfo>>> GetMeasureTypeInfoList(Guid loggedInUserId)
        {
            Response<List<MeasureTypeInfo>> response = new Response<List<MeasureTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetMeasureTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(measure => new MeasureTypeInfo() { MeasureType = measure.MeasureType, MeasureTypeValue= measure.MeasureTypeValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetMeasureTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<TimePeriodTypeInfo>>> GetTimePeriodInfoList(Guid loggedInUserId)
        {
            Response<List<TimePeriodTypeInfo>> response = new Response<List<TimePeriodTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _lookupsRepository.GetTimePeriodInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(timeperiod => new TimePeriodTypeInfo() { TimePeriodType = timeperiod.TimePeriodType, TimePeriodTypeValue = timeperiod.TimePeriodTypeValue })
                  .ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LookupsRepository.GetTimePeriodInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
