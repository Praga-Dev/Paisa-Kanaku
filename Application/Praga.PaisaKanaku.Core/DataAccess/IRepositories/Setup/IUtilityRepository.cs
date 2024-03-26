using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IUtilityRepository
    {
        Task<Response<Guid>> SaveUtilityInfo(UtilityInfoDB utilityInfoDb, Guid loggedInUserId);
        Task<Response<List<UtilityInfoDB>>> GetUtilityInfoList(Guid loggedInUserId);
        Task<Response<UtilityInfoDB>> GetUtilityInfoById(Guid utilityInfoId, Guid loggedInUserId);
    }
}
