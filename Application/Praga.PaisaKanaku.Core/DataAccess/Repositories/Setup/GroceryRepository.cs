using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.Repositories.Setup
{
    public class GroceryRepository : IGroceryRepository
    {
        public Task<Response<GroceryInfoDB>> GetGroceryInfoById(Guid groceryInfoId, Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<GroceryInfoDB>>> GetGroceryInfoList(Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Guid>> SaveGroceryInfo(GroceryInfoDB groceryInfoDb, Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }
    }
}
