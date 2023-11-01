using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.Operations.IServices
{
    public interface ICommonService
    {
        Task<Response<Guid>> DeleteRecord(Guid id, string tableName, string schemaName, Guid loggedInUserId);
    }
}
