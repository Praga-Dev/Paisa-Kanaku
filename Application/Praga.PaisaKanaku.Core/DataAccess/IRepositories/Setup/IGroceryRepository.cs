using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IGroceryRepository
    {
        Task<Response<Guid>> SaveGroceryInfo(GroceryInfoDB groceryInfoDb, Guid loggedInUserId);
        Task<Response<List<GroceryInfoDB>>> GetGroceryInfoList(Guid loggedInUserId);
        Task<Response<GroceryInfoDB>> GetGroceryInfoById(Guid groceryInfoId, Guid loggedInUserId);
    }
}
