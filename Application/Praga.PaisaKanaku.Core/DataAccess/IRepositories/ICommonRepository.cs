using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories
{
    public interface ICommonRepository
    {
        Task<Response<Guid>> UpdateRowStatus(Guid id, string tableName, string schemaName, string rowStatus, Guid loggedInUserId);
    }
}
