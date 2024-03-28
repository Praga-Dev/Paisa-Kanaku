using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Group;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IGroupService
    {
        Task<Response<Guid>> SaveGroupInfo(GroupInfo groupInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<GroupInfo>>> GetGroupInfoList(Guid loggedInUserId);
        Task<Response<GroupInfo>> GetGroupInfoById(Guid groupInfoId, Guid loggedInUserId);
    }
}
