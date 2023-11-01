using Dapper;
using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DataAccess.Utils;
using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories
{
    internal class CommonRepository : ICommonRepository
    {
        private readonly ILogger<CommonRepository> _logger;
        private readonly IDataBaseConnection _db;

        public CommonRepository(ILogger<CommonRepository> logger, IDataBaseConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Response<Guid>> UpdateRowStatus(Guid id, string tableName, string schemaName, char rowStatus, Guid loggedInUserId)
        {
            Response<Guid> Response = new();
            try
            {
                var spName = DatabaseConstants.USP_ROW_STATUS_UPDATE;
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@Id", id, DbType.Guid);
                _params.Add("@TableSchema", schemaName, DbType.String);
                _params.Add("@TableName", tableName, DbType.String);
                _params.Add("@RowStatus", rowStatus, DbType.String);
                _params.Add("@LoggedInUser", loggedInUserId, DbType.Guid);
                _params.Add("@Result", null, DbType.Guid, direction: ParameterDirection.Output);
                var returnValue = await _db.Connection.QueryAsync<Guid>(spName, _params, commandType: CommandType.StoredProcedure);
                var result = _params.Get<Guid>("@Result");
                if (returnValue != null && Helpers.IsValidGuid(result))
                {
                    Response.Data = result;
                    Response.IsSuccess = true;
                    Response.Message = ResponseConstants.SUCCESS;
                    return Response;
                }
                Response.Message = ResponseConstants.FAILED;
                return Response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CommonRepository.UpdateRowStatus({@id}, {@tableName}, {@schemaName}, {@rowStatus}, {@loggedInUserId})", id, tableName, schemaName, rowStatus, loggedInUserId);

                Response.Message = ex.Message switch
                {
                    "INVALID_PARAM_LOGGED_IN_USER" => ResponseConstants.INVALID_LOGGED_IN_USER,
                    "INVALID_PARAM_ID" => ResponseConstants.INVALID_ID,
                    "INVALID_PARAM_TABLE_NAME" => ResponseConstants.INVALID_PARAM_TABLE_NAME,
                    "INVALID_PARAM_ROW_STATUS" => ResponseConstants.INVALID_PARAM_ROW_STATUS,
                    _ => ResponseConstants.SOMETHING_WENT_WRONG
                };
                return Response;
            }
        }
    }
}
