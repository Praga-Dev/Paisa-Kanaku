using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyWellbeing;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyWellbeing;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyWellbeing;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseFamilyWellbeingService : IExpenseFamilyWellbeingService
    {
        private readonly ILogger<ExpenseFamilyWellbeingService> _logger;

        private readonly IExpenseFamilyWellbeingRepository _expenseFamilyWellbeingRepository;
        private readonly ICommonService _commonService;

        public ExpenseFamilyWellbeingService(ILogger<ExpenseFamilyWellbeingService> logger, IExpenseFamilyWellbeingRepository expenseFamilyWellbeingRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseFamilyWellbeingRepository = expenseFamilyWellbeingRepository;
        }


        public async Task<Response<Guid>> DeleteExpenseFamilyWellbeingInfo(Guid expenseFamilyWellbeingInfoId, Guid loggedInUserId)
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

                if (!Helpers.IsValidGuid(expenseFamilyWellbeingInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseFamilyWellbeingInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseFamilyWellbeingInfoId, AppConstants.EXPENSE_FAMILY_FUND_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingService.DeleteExpenseFamilyWellbeingInfo({@expenseFamilyWellbeingInfoId}, {@loggedInUserId})", expenseFamilyWellbeingInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseFamilyWellbeingInfo>> GetExpenseFamilyWellbeingInfoById(Guid expenseFamilyWellbeingInfoId, Guid loggedInUserId)
        {
            Response<ExpenseFamilyWellbeingInfo> response = new Response<ExpenseFamilyWellbeingInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseFamilyWellbeingInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseFamilyWellbeingInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseFamilyWellbeingRepository.GetExpenseFamilyWellbeingInfoById(expenseFamilyWellbeingInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    ExpenseFamilyWellbeingInfoDB expenseFamilyWellbeingInfoDB = dbResponse.Data;

                    response.Data = new ExpenseFamilyWellbeingInfo()
                    {
                        Id = expenseFamilyWellbeingInfoDB.Id,
                        ExpenseInfoId = expenseFamilyWellbeingInfoDB.ExpenseInfoId,
                        ExpenseDate = expenseFamilyWellbeingInfoDB.ExpenseDate,
                        ExpenseAmount = expenseFamilyWellbeingInfoDB.ExpenseAmount,
                        RecipientInfo = new()
                        {
                            Id = expenseFamilyWellbeingInfoDB.RecipientId,
                            Name = expenseFamilyWellbeingInfoDB.RecipientName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseFamilyWellbeingInfoDB.ExpenseById,
                            Name = expenseFamilyWellbeingInfoDB.ExpenseByName
                        },
                        Description = expenseFamilyWellbeingInfoDB.Description,
                        SequenceId = expenseFamilyWellbeingInfoDB.SequenceId,
                        CreatedBy = expenseFamilyWellbeingInfoDB.CreatedBy,
                        CreatedDate = expenseFamilyWellbeingInfoDB.CreatedDate,
                        ModifiedBy = expenseFamilyWellbeingInfoDB.ModifiedBy,
                        ModifiedDate = expenseFamilyWellbeingInfoDB.ModifiedDate,
                        RowStatus = expenseFamilyWellbeingInfoDB.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListById({@expenseFamilyWellbeingInfoId}, {@loggedInUserId})", expenseFamilyWellbeingInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseFamilyWellbeingInfo>>> GetExpenseFamilyWellbeingInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseFamilyWellbeingInfo>> response = new Response<List<ExpenseFamilyWellbeingInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

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

                var dbResponse = await _expenseFamilyWellbeingRepository.GetExpenseFamilyWellbeingInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expenseFamilyWellbeing => new ExpenseFamilyWellbeingInfo()
                    {
                        Id = expenseFamilyWellbeing.Id,
                        ExpenseInfoId = expenseFamilyWellbeing.ExpenseInfoId,
                        ExpenseDate = expenseFamilyWellbeing.ExpenseDate,
                        ExpenseAmount = expenseFamilyWellbeing.ExpenseAmount,
                        RecipientInfo = new()
                        {
                            Id = expenseFamilyWellbeing.RecipientId,
                            Name = expenseFamilyWellbeing.RecipientName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseFamilyWellbeing.ExpenseById,
                            Name = expenseFamilyWellbeing.ExpenseByName
                        },
                        Description = expenseFamilyWellbeing.Description,
                        SequenceId = expenseFamilyWellbeing.SequenceId,
                        CreatedBy = expenseFamilyWellbeing.CreatedBy,
                        CreatedDate = expenseFamilyWellbeing.CreatedDate,
                        ModifiedBy = expenseFamilyWellbeing.ModifiedBy,
                        ModifiedDate = expenseFamilyWellbeing.ModifiedDate,
                        RowStatus = expenseFamilyWellbeing.RowStatus
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseFamilyWellbeingInfoListByMonth(int month, int year, Guid loggedInUserId)
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

                var dbResponse = await _expenseFamilyWellbeingRepository.GetExpenseFamilyWellbeingInfoListByMonth(month, year, loggedInUserId);
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
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseFamilyWellbeingInfo(ExpenseFamilyWellbeingSaveRequestDTO expenseFamilyWellbeingSaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (expenseFamilyWellbeingSaveRequestDTO == null)
                {
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseFamilyWellbeingSaveRequestDTO.RecipientInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid RecipientInfo Id");
                }

                if (expenseFamilyWellbeingSaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (expenseFamilyWellbeingSaveRequestDTO.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseFamilyWellbeingInfoDB expenseFamilyWellbeingInfoDB = new()
                {
                    Id = expenseFamilyWellbeingSaveRequestDTO.Id,
                    ExpenseInfoId = expenseFamilyWellbeingSaveRequestDTO.ExpenseInfoId,
                    ExpenseById = expenseFamilyWellbeingSaveRequestDTO.ExpenseByInfoId,
                    ExpenseAmount = expenseFamilyWellbeingSaveRequestDTO.ExpenseAmount,
                    ExpenseDate = expenseFamilyWellbeingSaveRequestDTO.ExpenseDate,
                    RecipientId = expenseFamilyWellbeingSaveRequestDTO.RecipientInfoId,
                    Description = expenseFamilyWellbeingSaveRequestDTO.Description,
                };

                return await _expenseFamilyWellbeingRepository.SaveExpenseFamilyWellbeingInfoDB(expenseFamilyWellbeingInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingService.SaveExpenseFamilyWellbeingInfo({@expenseFamilyWellbeingSaveRequestDTO}, {@loggedInUserId})", expenseFamilyWellbeingSaveRequestDTO.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
