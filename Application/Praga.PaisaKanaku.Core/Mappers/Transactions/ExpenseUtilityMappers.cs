using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseUtility;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseUtility;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseUtility;

namespace Praga.PaisaKanaku.Core.Mappers.Transactions
{
    public static class ExpenseUtilityMappers
    {
        public static ExpenseUtilityInfo ToExpenseUtilityInfo(this ExpenseUtilityInfoDB expenseUtilityInfoDB)
        {
            return new ExpenseUtilityInfo()
            {
                Id = expenseUtilityInfoDB.Id,
                ExpenseInfoId = expenseUtilityInfoDB.ExpenseInfoId,
                ExpenseAmount = expenseUtilityInfoDB.ExpenseAmount,
                ExpenseDate = expenseUtilityInfoDB.ExpenseDate,
                FromDate = expenseUtilityInfoDB.FromDate,
                ToDate = expenseUtilityInfoDB.ToDate,
                ServiceDuration = new()
                {
                    TimePeriodType = expenseUtilityInfoDB.ServiceDuration,
                    TimePeriodTypeValue = expenseUtilityInfoDB.ServiceDurationValue,
                },
                UtilityInfo = new()
                {
                    Id = expenseUtilityInfoDB.UtilityId,
                    Name = expenseUtilityInfoDB.UtilityName,
                },
                ExpenseByInfo = new()
                {
                    Id = expenseUtilityInfoDB.ExpenseById,
                    Name = expenseUtilityInfoDB.ExpenseByName
                },
                ConsumerInfo = new()
                {
                    Id = expenseUtilityInfoDB.ConsumerId,
                    Name = expenseUtilityInfoDB.ConsumerName
                },
                ConsumerTypeInfo = new()
                {
                    ConsumerType = expenseUtilityInfoDB.ConsumerType,
                    ConsumerTypeValue = expenseUtilityInfoDB.ConsumerTypeValue
                },
                Description = expenseUtilityInfoDB.Description,
                SequenceId = expenseUtilityInfoDB.SequenceId,
                CreatedBy = expenseUtilityInfoDB.CreatedBy,
                CreatedDate = expenseUtilityInfoDB.CreatedDate,
                ModifiedBy = expenseUtilityInfoDB.ModifiedBy,
                ModifiedDate = expenseUtilityInfoDB.ModifiedDate,
                RowStatus = expenseUtilityInfoDB.RowStatus
            };
        }

        public static List<ExpenseUtilityInfo> ToExpenseUtilityInfoList(this List<ExpenseUtilityInfoDB> expenseUtilityInfoDBList)
        {
            return expenseUtilityInfoDBList
                .Select(expenseUtilityInfoDB => expenseUtilityInfoDB.ToExpenseUtilityInfo())
                .ToList();
        }

        public static ExpenseUtilityInfoDB ToExpenseUtilityInfoDB(this ExpenseUtilitySaveRequestDTO expenseUtility)
        {
            return new()
            {
                Id = expenseUtility.Id,
                ExpenseInfoId = expenseUtility.ExpenseInfoId,
                ExpenseAmount = expenseUtility.ExpenseAmount,
                ExpenseById = expenseUtility.ExpenseByInfoId,
                ExpenseDate = expenseUtility.ExpenseDate,
                UtilityId = expenseUtility.UtilityInfoId,
                ConsumerId = expenseUtility.ConsumerInfoId,
                ConsumerType = expenseUtility.ConsumerType,
                ServiceDuration = expenseUtility.ServiceDuration,
                FromDate = expenseUtility.FromDate,
                ToDate = expenseUtility.ToDate,
                Description = expenseUtility.Description
            };
        }
    }
}
