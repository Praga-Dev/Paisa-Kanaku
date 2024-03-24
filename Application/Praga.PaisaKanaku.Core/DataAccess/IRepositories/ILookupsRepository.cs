using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories
{
    public interface ILookupsRepository
    {
        Task<Response<List<ExpenseTypeInfoDB>>> GetExpenseTypeInfoList(Guid loggedInUserId);
        Task<Response<List<MetricSystemInfoDB>>> GetMetricSystemInfoList(Guid loggedInUserId);
        Task<Response<List<MeasureTypeInfoDB>>> GetMeasureTypeInfoList(Guid loggedInUserId);
        Task<Response<List<TimePeriodInfoDB>>> GetTimePeriodInfoList(Guid loggedInUserId);
        Task<Response<List<ProductCategoryInfoDB>>> GetProductCategoryInfoList(Guid loggedInUserId);
        Task<Response<List<RelationshipTypeInfoDB>>> GetRelationshipTypeInfoList(Guid loggedInUserId);
        Task<Response<List<MeasureTypeInfoDB>>> GetMeasureTypeInfoListByGroceryInfoId(Guid groceryInfoId, Guid loggedInUserId);
        Task<Response<List<TransportModeInfoDB>>> GetTransportModeInfo(Guid loggedInUserId);
        Task<Response<List<TravelServiceInfoDB>>> GetTravelServiceInfo(Guid loggedInUserId);
        Task<Response<List<ConsumerTypeInfoDB>>> GetConsumerTypeInfo(Guid loggedInUserId);

    }
}
