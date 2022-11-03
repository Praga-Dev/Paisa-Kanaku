using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.Operations.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.Operations.Services
{
    public class CommonService : ICommonService
    {
        private readonly ILogger<CommonService> _logger;

        private readonly ICommonRepository _commonRepository;

        public CommonService(ILogger<CommonService> logger, ICommonRepository commonRepository)
        {
            _logger = logger;
            _commonRepository = commonRepository;
        }

        public async Task<Response<Guid>> UpdateRowStatus(Guid id, string tableName, string schemaName, string rowStatus, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);
            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(id))
                {
                    response.ValidationErrorMessages.Add(ResponseConstants.INVALID_PARAM);
                    return response;
                }

                if (string.IsNullOrWhiteSpace(tableName))
                {
                    response.ValidationErrorMessages.Add(ResponseConstants.INVALID_PARAM_TABLE_NAME);
                }
                if (string.IsNullOrWhiteSpace(schemaName))
                {
                    response.ValidationErrorMessages.Add(ResponseConstants.INVALID_PARAM_SCHEMA_NAME);
                    return response;
                }

                if (!Helpers.IsValidRowStatus(rowStatus))
                {
                    response.ValidationErrorMessages.Add(ResponseConstants.INVALID_PARAM_ROW_STATUS);
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                return await _commonRepository.UpdateRowStatus(id, tableName, schemaName, rowStatus, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CommonService.UpdateRowStatus({@id}, {@tableName}, {@schemaName}, {@rowStatus}, {@loggedInUserId})", id, tableName, schemaName, rowStatus, loggedInUserId);

                response.Message = ex.Message;
                return response;
            }
        }
    }
}
