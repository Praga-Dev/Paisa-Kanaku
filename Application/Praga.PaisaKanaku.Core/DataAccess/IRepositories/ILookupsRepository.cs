using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DataEntities.Lookups;

namespace Praga.PaisaKanaku.Core.DataAccess.IRepositories
{
    public interface ILookupsRepository
    {
        Task<Response<List<ExpenseTypeInfoDb>>> GetExpenseTypeInfoList(Guid loggedInUserId);
        Task<Response<List<LiquidMeasureInfoDb>>> GetLiquidMeasureInfoList(Guid loggedInUserId);
        Task<Response<List<MeasureTypeInfoDb>>> GetMeasureTypeInfoList(Guid loggedInUserId);
        Task<Response<List<TimePeriodInfoDb>>> GetTimePeriodInfoList(Guid loggedInUserId);
        Task<Response<List<ProductCategoryInfoDB>>> GetProductCategoryInfoList(Guid loggedInUserId);

    }
}
