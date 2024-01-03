using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IOutdoorFoodVendorService
    {
        Task<Response<Guid>> SaveOutdoorFoodVendorInfo(OutdoorFoodVendorInfo outdoorFoodVendorInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<OutdoorFoodVendorInfo>>> GetOutdoorFoodVendorInfoList(Guid loggedInUserId);
        Task<Response<OutdoorFoodVendorInfo>> GetOutdoorFoodVendorInfoById(Guid outdoorFoodVendorInfoId, Guid loggedInUserId);
    }
}
