using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IOutdoorFoodVendorRepository
    {
        Task<Response<Guid>> SaveOutdoorFoodVendorInfo(OutdoorFoodVendorInfoDB outdoorFoodVendorInfoDB, Guid loggedInUserId);
        Task<Response<List<OutdoorFoodVendorInfoDB>>> GetOutdoorFoodVendorInfoList(Guid loggedInUserId);
        Task<Response<OutdoorFoodVendorInfoDB>> GetOutdoorFoodVendorInfoById(Guid outdoorFoodVendorInfoId, Guid loggedInUserId);
    }
}

