using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IMemberService
    {
        Task<Response<Guid>> SaveMemberInfo(MemberInfo memberInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<MemberInfo>>> GetMemberInfoList(Guid loggedInUserId);
        Task<Response<MemberInfo>> GetMemberInfoById(Guid memberInfoId, Guid loggedInUserId);
    }
}
