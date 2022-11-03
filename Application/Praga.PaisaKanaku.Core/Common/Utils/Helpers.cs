using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.Common.Utils
{
    public static class Helpers
    {
        public static bool IsValidGuid(Guid? id) => id != null && id != Guid.Empty;

        public static bool IsResponseValid<T>(Response<T> response) => response != null && response.Data != null && response.IsSuccess ;
        
        public static bool IsValidRowStatus(string? rowStatus) => string.IsNullOrWhiteSpace(rowStatus) && RowStatusInfo.RowStatusDictionary.ContainsKey(rowStatus);
    }
}
