using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IBillTypeService
    {
        Task<Response<Guid>> SaveBillTypeInfo(BillTypeInfo billTypeInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<BillTypeInfo>>> GetBillTypeInfoList(Guid loggedInUserId);
        Task<Response<BillTypeInfo>> GetBillTypeInfoById(Guid billTypeInfoId, Guid loggedInUserId);
    }
}
