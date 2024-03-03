using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseTravel;

namespace Praga.PaisaKanaku.Core.Mappers.Transactions
{
    public static class ExpenseTravelMappers
    {
        public static ExpenseTravelInfo ToExpenseTravelInfo(this ExpenseTravelInfoDB expenseTravelInfoDB)
        {
            try
            {
                ExpenseTravelInfo data = new ExpenseTravelInfo()
                {
                    Id = expenseTravelInfoDB.Id,
                    ExpenseInfoId = expenseTravelInfoDB.ExpenseInfoId,
                    ExpenseDate = expenseTravelInfoDB.ExpenseDate,
                    ExpenseByInfo = new()
                    {
                        Id = expenseTravelInfoDB.ExpenseById,
                        Name = expenseTravelInfoDB.ExpenseByName
                    },
                    Source = expenseTravelInfoDB.Source,
                    Destination = expenseTravelInfoDB.Destination,
                    TravelDate = expenseTravelInfoDB.TravelDate,
                    ExpenseAmount = expenseTravelInfoDB.ExpenseAmount,
                    TransportModeInfo = new()
                    {
                        TransportMode = expenseTravelInfoDB.TransportMode,
                        TransportModeValue = expenseTravelInfoDB.TransportModeValue
                    },
                    TravelServiceInfo = new()
                    {
                        TravelService = expenseTravelInfoDB.TravelService,
                        TravelServiceValue = expenseTravelInfoDB.TravelServiceValue
                    },
                    Description = expenseTravelInfoDB.Description,
                    SequenceId = expenseTravelInfoDB.SequenceId,
                    CreatedBy = expenseTravelInfoDB.CreatedBy,
                    CreatedDate = expenseTravelInfoDB.CreatedDate,
                    ModifiedBy = expenseTravelInfoDB.ModifiedBy,
                    ModifiedDate = expenseTravelInfoDB.ModifiedDate,
                    RowStatus = expenseTravelInfoDB.RowStatus
                };

                return data;
            }
            catch (Exception ex)
            { 
                // TODO Log
            }

            return new();
        }

        public static List<ExpenseTravelInfo> ToExpenseTravelInfoList(this List<ExpenseTravelInfoDB> expenseTravelInfoDBList)
        {
            try
            {
                List<ExpenseTravelInfo> expenseTravelInfoList = expenseTravelInfoDBList
                    .Select(expenseTravelInfoDB => expenseTravelInfoDB.ToExpenseTravelInfo()).ToList();

                return expenseTravelInfoList;
            }
            catch (Exception ex)
            {
                // TODO Log
            }

            return new();
        }

        public static ExpenseTravelInfoDB ToExpenseTravelInfoDB(this ExpenseTravelSaveRequestDTO expenseTravel)
        {
            try
            {
                return new()
                {
                    Id = expenseTravel.Id,
                    ExpenseInfoId = expenseTravel.ExpenseInfoId,
                    ExpenseAmount = expenseTravel.ExpenseAmount,
                    ExpenseById = expenseTravel.ExpenseByInfoId,
                    Source = expenseTravel.Source,
                    Destination = expenseTravel.Destination,
                    TravelDate = expenseTravel.TravelDate,
                    TransportMode = expenseTravel.TransportMode,
                    TravelService = expenseTravel.TravelService,
                    ExpenseDate = expenseTravel.ExpenseDate,
                    Description = expenseTravel.Description
                };
            }
            catch (Exception ex)
            {
                // TODO Log
            }

            return new();
        }
    }
}
