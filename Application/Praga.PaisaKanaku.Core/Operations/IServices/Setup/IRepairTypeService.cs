using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IRepairTypeService
    {
        Task<Response<Guid>> SaveRepairTypeInfo(RepairTypeInfo repairTypeInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<RepairTypeInfo>>> GetRepairTypeInfoList(Guid loggedInUserId);
        Task<Response<RepairTypeInfo>> GetRepairTypeInfoById(Guid repairTypeInfoId, Guid loggedInUserId);
    }
}
