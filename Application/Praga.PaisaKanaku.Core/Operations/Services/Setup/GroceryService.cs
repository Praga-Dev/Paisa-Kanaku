using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Grocery;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;

namespace Praga.PaisaKanaku.Core.Operations.Services.Setup
{
    public class GroceryService : IGroceryService
    {
        public Task<Response<string>> ExportGroceryInfoData(Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<GroceryInfo>> GetGroceryInfoById(Guid groceryInfoId, Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<GroceryInfo>>> GetGroceryInfoList(Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Guid>> SaveGroceryInfo(GroceryInfo groceryInfoDb, bool isUpdate, Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }
    }
}
