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
        Task<Response<Guid>> SaveMemberInfo(MemberInfoDB memberInfoDb, Guid loggedInUserId);
        Task<Response<List<MemberInfoDB>>> GetMemberInfoList(Guid loggedInUserId);
        Task<Response<MemberInfoDB>> GetMemberInfoById(Guid memberInfoId, Guid loggedInUserId);
    }
}
