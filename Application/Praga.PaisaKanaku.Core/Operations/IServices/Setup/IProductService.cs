using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IProductService
    {
        Task<Response<Guid>> SaveProductInfo(ProductInfo productInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<ProductInfo>>> GetProductInfoList(Guid loggedInUserId);
        Task<Response<string>> ExportProductInfoData(Guid loggedInUserId);
        Task<Response<ProductInfo>> GetProductInfoById(Guid productInfoId, Guid loggedInUserId);
    }
}
