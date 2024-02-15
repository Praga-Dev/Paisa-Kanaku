using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;

namespace Praga.PaisaKanaku.Core.Operations.IServices
{
    public interface ILookupService
    {
        Task<Response<List<ExpenseTypeInfo>>> GetExpenseTypeInfoList(Guid loggedInUserId);
        Task<Response<List<MetricSystemInfo>>> GetMetricSystemInfoList(Guid loggedInUserId);
        Task<Response<List<MeasureTypeInfo>>> GetMeasureTypeInfoList(Guid loggedInUserId);
        Task<Response<List<TimePeriodTypeInfo>>> GetTimePeriodInfoList(Guid loggedInUserId);
        Task<Response<List<ProductCategoryInfo>>> GetProductCategoryInfoList(Guid loggedInUserId);
        Task<Response<List<RelationshipTypeInfo>>> GetRelationshipTypeInfoList(Guid loggedInUserId);
        Task<Response<List<MeasureTypeInfo>>> GetMeasureTypeInfoListByGroceryInfoId(Guid groceryInfoId, Guid loggedInUserId);
        Task<Response<List<TransportModeInfo>>> GetTransportModeInfo(Guid loggedInUserId);
        Task<Response<List<TravelServiceInfo>>> GetTravelServiceInfo(Guid loggedInUserId);
    }
}
