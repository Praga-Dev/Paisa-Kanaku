using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup.Group;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IGroupRepository
    {
        Task<Response<Guid>> SaveGroupInfo(GroupInfoDB groupInfoDb, Guid loggedInUserId);
        Task<Response<List<GroupInfoDB>>> GetGroupInfoList(Guid loggedInUserId);
        Task<Response<GroupInfoDB>> GetGroupInfoById(Guid groupInfoId, Guid loggedInUserId);
    }
}
