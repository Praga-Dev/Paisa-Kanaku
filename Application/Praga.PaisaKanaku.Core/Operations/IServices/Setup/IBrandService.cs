using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IBrandService
    {
        Task<Response<Guid>> SaveBrandInfo(BrandInfo brandInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<BrandInfo>>> GetBrandInfoList(Guid loggedInUserId);
        Task<Response<BrandInfo>> GetBrandInfoById(Guid brandInfoId, Guid loggedInUserId);
    }
}
