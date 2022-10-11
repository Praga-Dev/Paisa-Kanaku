using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IProductCategoryService
    {
        Task<Response<Guid>> SaveProductCategoryInfo(ProductCategoryInfo productCategoryInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<ProductCategoryInfo>>> GetProductCategoryInfoList(Guid loggedInUserId);
        Task<Response<ProductCategoryInfo>> GetProductCategoryInfoById(Guid productCategoryInfoId, Guid loggedInUserId);
    }
}
