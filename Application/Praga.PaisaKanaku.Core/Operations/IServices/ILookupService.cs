using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.Operations.IServices
{
    public interface ILookupService
    {
        Task<Response<List<ExpenseTypeInfo>>> GetExpenseTypeInfoList(Guid loggedInUserId);
        Task<Response<List<LiquidMeasureInfo>>> GetLiquidMeasureInfoList(Guid loggedInUserId);
        Task<Response<List<MeasureTypeInfo>>> GetMeasureTypeInfoList(Guid loggedInUserId);
        Task<Response<List<TimePeriodTypeInfo>>> GetTimePeriodInfoList(Guid loggedInUserId);
    }
}
