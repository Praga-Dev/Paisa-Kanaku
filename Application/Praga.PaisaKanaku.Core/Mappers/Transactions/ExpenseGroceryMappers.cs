using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseGrocery;

namespace Praga.PaisaKanaku.Core.Mappers.Transactions
{
    public static class ExpenseGroceryMappers
    {
        public static ExpenseGroceryInfoDB ToExpenseGroceryInfo(this ExpenseGrocerySaveRequestDTO expenseGrocerySaveRequestDTO)
        {
            try
            {
                return new()
                {
                    Id = expenseGrocerySaveRequestDTO.Id,
                    ExpenseInfoId = expenseGrocerySaveRequestDTO.ExpenseInfoId,
                    MeasureType = expenseGrocerySaveRequestDTO.MeasureType,
                    Quantity = expenseGrocerySaveRequestDTO.Quantity,
                    ExpenseAmount = expenseGrocerySaveRequestDTO.ExpenseAmount,
                    ExpenseById = expenseGrocerySaveRequestDTO.ExpenseByInfoId,
                    ExpenseDate = expenseGrocerySaveRequestDTO.ExpenseDate,
                    GroceryInfoId = expenseGrocerySaveRequestDTO.GroceryInfoId,
                    Description = expenseGrocerySaveRequestDTO.Description
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
