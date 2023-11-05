using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Setup
{
    public interface IGroceryService
    {
        Task<Response<Guid>> SaveGroceryInfo(GroceryInfo groceryInfoDb, bool isUpdate, Guid loggedInUserId);
        Task<Response<List<GroceryInfo>>> GetGroceryInfoList(Guid loggedInUserId);
        Task<Response<string>> ExportGroceryInfoData(Guid loggedInUserId);
        Task<Response<GroceryInfo>> GetGroceryInfoById(Guid groceryInfoId, Guid loggedInUserId);
    }
}
