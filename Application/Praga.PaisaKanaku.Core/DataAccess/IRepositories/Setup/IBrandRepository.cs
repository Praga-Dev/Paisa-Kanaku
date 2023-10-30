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
        Task<Response<Guid>> SaveBrandInfo(BrandInfoDB brandInfoDb, Guid loggedInUserId);
        Task<Response<List<BrandInfoDB>>> GetBrandInfoList(Guid loggedInUserId);
        Task<Response<BrandInfoDB>> GetBrandInfoById(Guid brandInfoId, Guid loggedInUserId);
    }
}
