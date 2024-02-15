using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IProductRepository
    {
        Task<Response<Guid>> SaveProductInfo(ProductInfoDB productInfoDb, Guid loggedInUserId);
        Task<Response<List<ProductInfoDB>>> GetProductInfoList(Guid loggedInUserId);
        Task<Response<ProductInfoDB>> GetProductInfoById(Guid productInfoId, Guid loggedInUserId);
    }
}
