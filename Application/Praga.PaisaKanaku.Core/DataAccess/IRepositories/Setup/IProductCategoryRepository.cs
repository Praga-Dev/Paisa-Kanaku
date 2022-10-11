using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IProductCategoryRepository
    {
        Task<Response<Guid>> SaveProductCategoryInfo(ProductCategoryInfoDb productCategoryInfoDb, Guid loggedInUserId);
        Task<Response<List<ProductCategoryInfoDb>>> GetProductCategoryInfoList(Guid loggedInUserId);
        Task<Response<ProductCategoryInfoDb>> GetProductCategoryInfoById(Guid productCategoryInfoId, Guid loggedInUserId);
    }
}
