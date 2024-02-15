using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.Common.Utils
{
    public static class Helpers
    {
        public static bool IsValidGuid(Guid? id) => id != null && id != Guid.Empty;

        public static bool IsResponseValid<T>(Response<T> response) => response != null && response.Data != null && response.IsSuccess ;

        // TODO add Minyear & Maxyear for the system in Appsettings
        public static bool IsValidMonth(int month) => month > 0 && month <= 12;
        public static bool IsValidYear(int year) => year > 2022 && year <= 2050;
    }
}
