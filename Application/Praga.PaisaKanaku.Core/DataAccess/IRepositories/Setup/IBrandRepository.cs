using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IBrandRepository
    {
        Task<Response<Guid>> SaveBrandInfo(BrandInfoDb brandInfoDb, Guid loggedInUserId);
        Task<Response<List<BrandInfoDb>>> GetBrandInfoList(Guid loggedInUserId);
        Task<Response<BrandInfoDb>> GetBrandInfoById(Guid brandInfoId, Guid loggedInUserId);
    }
}
