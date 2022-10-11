using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IMemberRepository
    {
        Task<Response<Guid>> SaveMemberInfo(MemberInfoDb memberInfoDb, Guid loggedInUserId);
        Task<Response<List<MemberInfoDb>>> GetMemberInfoList(Guid loggedInUserId);
        Task<Response<MemberInfoDb>> GetMemberInfoById(Guid memberInfoId, Guid loggedInUserId);
    }
}
