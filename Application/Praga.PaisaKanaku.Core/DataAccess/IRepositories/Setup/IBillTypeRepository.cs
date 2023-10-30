using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IBillTypeRepository
    {
        Task<Response<Guid>> SaveBillTypeInfo(BillTypeInfoDB BillTypeInfoDB, Guid loggedInUserId);
        Task<Response<List<BillTypeInfoDB>>> GetBillTypeInfoList(Guid loggedInUserId);
        Task<Response<BillTypeInfoDB>> GetBillTypeInfoById(Guid billTypeInfoId, Guid loggedInUserId);
    }
}
