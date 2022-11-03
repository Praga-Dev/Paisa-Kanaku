using Praga.PaisaKanaku.Core.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.Operations.IServices
{
    public interface ICommonService
    {
        Task<Response<Guid>> UpdateRowStatus(Guid id, string tableName, string schemaName, string rowStatus, Guid loggedInUserId);
    }
}
