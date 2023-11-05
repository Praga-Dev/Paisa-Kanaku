using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseGroceryService : IExpenseGroceryService
    {
        private readonly ILogger<ExpenseGroceryService> _logger;

        private readonly IExpenseGroceryRepository _expenseGroceryRepository;
        private readonly ICommonService _commonService;

        public ExpenseGroceryService(ILogger<ExpenseGroceryService> logger, IExpenseGroceryRepository expenseGroceryRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseGroceryRepository = expenseGroceryRepository;
        }


        public async Task<Response<Guid>> DeleteExpenseGroceryInfo(Guid expenseGroceryInfoId, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseGroceryInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseGroceryInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseGroceryInfoId, AppConstants.EXPENSE_GROCERY_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryService.DeleteExpenseGroceryInfo({@expenseGroceryInfoId}, {@loggedInUserId})", expenseGroceryInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseGroceryInfo>> GetExpenseGroceryInfoById(Guid expenseGroceryInfoId, Guid loggedInUserId)
        {
            Response<ExpenseGroceryInfo> response = new Response<ExpenseGroceryInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseGroceryInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseGroceryInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseGroceryRepository.GetExpenseGroceryInfoById(expenseGroceryInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    ExpenseGroceryInfoDB expenseGroceryInfoDB = dbResponse.Data;

                    response.Data = new ExpenseGroceryInfo()
                    {
                        Id = expenseGroceryInfoDB.Id,
                        ExpenseInfoId = expenseGroceryInfoDB.ExpenseInfoId,
                        ExpenseDate = expenseGroceryInfoDB.ExpenseDate,
                        ExpenseAmount = expenseGroceryInfoDB.ExpenseAmount,
                        GroceryBaseInfo = new()
                        {
                            Id = expenseGroceryInfoDB.GroceryInfoId,
                            Name = expenseGroceryInfoDB.GroceryInfoName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseGroceryInfoDB.ExpenseById,
                            Name = expenseGroceryInfoDB.ExpenseByName
                        },
                        Quantity = expenseGroceryInfoDB.Quantity,
                        Description = expenseGroceryInfoDB.Description,
                        SequenceId = expenseGroceryInfoDB.SequenceId,
                        CreatedBy = expenseGroceryInfoDB.CreatedBy,
                        CreatedDate = expenseGroceryInfoDB.CreatedDate,
                        ModifiedBy = expenseGroceryInfoDB.ModifiedBy,
                        ModifiedDate = expenseGroceryInfoDB.ModifiedDate,
                        RowStatus = expenseGroceryInfoDB.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryService.GetExpenseGroceryInfoListById({@expenseGroceryInfoId}, {@loggedInUserId})", expenseGroceryInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseGroceryInfo>>> GetExpenseGroceryInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseGroceryInfo>> response = new Response<List<ExpenseGroceryInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (expenseDate.Day > DateTime.UtcNow.Day)
                {
                    response.ValidationErrorMessages.Add("Invalid Date");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseGroceryRepository.GetExpenseGroceryInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expenseGrocery => new ExpenseGroceryInfo()
                    {
                        Id = expenseGrocery.Id,
                        ExpenseInfoId = expenseGrocery.ExpenseInfoId,
                        ExpenseDate = expenseGrocery.ExpenseDate,
                        ExpenseAmount = expenseGrocery.ExpenseAmount,
                        GroceryBaseInfo = new()
                        {
                            Id = expenseGrocery.GroceryInfoId,
                            Name = expenseGrocery.GroceryInfoName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseGrocery.ExpenseById,
                            Name = expenseGrocery.ExpenseByName
                        },
                        Quantity = expenseGrocery.Quantity,
                        Description = expenseGrocery.Description,
                        SequenceId = expenseGrocery.SequenceId,
                        CreatedBy = expenseGrocery.CreatedBy,
                        CreatedDate = expenseGrocery.CreatedDate,
                        ModifiedBy = expenseGrocery.ModifiedBy,
                        ModifiedDate = expenseGrocery.ModifiedDate,
                        RowStatus = expenseGrocery.RowStatus
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryService.GetExpenseGroceryInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseGroceryInfoListByMonth(int month, int year, Guid loggedInUserId)
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidMonth(month))
                {
                    response.ValidationErrorMessages.Add("Invalid Month");
                }

                if (!Helpers.IsValidYear(year))
                {
                    response.ValidationErrorMessages.Add("Invalid Year");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseGroceryRepository.GetExpenseGroceryInfoListByMonth(month, year, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expense => new ExpenseInfoSumAmountByDate()
                    {
                        ExpenseDate = expense.ExpenseDate,
                        TotalExpenseAmount = expense.TotalExpenseAmount,
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryService.GetExpenseGroceryInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseGroceryInfo(ExpenseGrocerySaveRequestDTO expenseGrocerySaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (expenseGrocerySaveRequestDTO == null)
                {
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseGrocerySaveRequestDTO.GroceryInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid GroceryInfo Id");
                }

                if (expenseGrocerySaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (expenseGrocerySaveRequestDTO.Quantity <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Quantity");
                }

                if (expenseGrocerySaveRequestDTO.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseGroceryInfoDB expenseGroceryInfoDB = new()
                {
                    Id = expenseGrocerySaveRequestDTO.Id,
                    ExpenseInfoId = expenseGrocerySaveRequestDTO.ExpenseInfoId,
                    Quantity = expenseGrocerySaveRequestDTO.Quantity,
                    ExpenseAmount = expenseGrocerySaveRequestDTO.ExpenseAmount,
                    ExpenseById = expenseGrocerySaveRequestDTO.ExpenseByInfoId,
                    ExpenseDate = expenseGrocerySaveRequestDTO.ExpenseDate,
                    GroceryInfoId = expenseGrocerySaveRequestDTO.GroceryInfoId,
                    Description = expenseGrocerySaveRequestDTO.Description
                };

                return await _expenseGroceryRepository.SaveExpenseGroceryInfoDB(expenseGroceryInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryService.SaveTempExpenseInfo({@expenseGrocerySaveRequestDTO}, {@loggedInUserId})", expenseGrocerySaveRequestDTO.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
