using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Setup;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup
{
    public interface IRepairTypeRepository
    {
        Task<Response<Guid>> SaveRepairTypeInfo(RepairTypeInfoDb repairTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<RepairTypeInfoDb>>> GetRepairTypeInfoList(Guid loggedInUserId);
        Task<Response<RepairTypeInfoDb>> GetRepairTypeInfoById(Guid repairTypeInfoId, Guid loggedInUserId);
    }
}
