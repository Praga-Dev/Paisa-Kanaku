using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IUtilityService
    {
        Task<Response<Guid>> SaveUtilityInfo(UtilityInfo utilityInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<UtilityInfo>>> GetUtilityInfoList(Guid loggedInUserId);
        Task<Response<UtilityInfo>> GetUtilityInfoById(Guid utilityInfoId, Guid loggedInUserId);
    }
}
