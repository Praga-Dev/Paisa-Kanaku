using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseFamilyFund;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyFund;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyFund;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseFamilyFundService : IExpenseFamilyFundService
    {
        private readonly ILogger<ExpenseFamilyFundService> _logger;

        private readonly IExpenseFamilyFundRepository _expenseFamilyFundRepository;
        private readonly ICommonService _commonService;

        public ExpenseFamilyFundService(ILogger<ExpenseFamilyFundService> logger, IExpenseFamilyFundRepository expenseFamilyFundRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseFamilyFundRepository = expenseFamilyFundRepository;
        }


        public async Task<Response<Guid>> DeleteExpenseFamilyFundInfo(Guid expenseFamilyFundInfoId, Guid loggedInUserId)
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

                if (!Helpers.IsValidGuid(expenseFamilyFundInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseFamilyFundInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseFamilyFundInfoId, AppConstants.EXPENSE_FAMILY_FUND_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundService.DeleteExpenseFamilyFundInfo({@expenseFamilyFundInfoId}, {@loggedInUserId})", expenseFamilyFundInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseFamilyFundInfo>> GetExpenseFamilyFundInfoById(Guid expenseFamilyFundInfoId, Guid loggedInUserId)
        {
            Response<ExpenseFamilyFundInfo> response = new Response<ExpenseFamilyFundInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseFamilyFundInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseFamilyFundInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseFamilyFundRepository.GetExpenseFamilyFundInfoById(expenseFamilyFundInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    ExpenseFamilyFundInfoDB expenseFamilyFundInfoDB = dbResponse.Data;

                    response.Data = new ExpenseFamilyFundInfo()
                    {
                        Id = expenseFamilyFundInfoDB.Id,
                        ExpenseInfoId = expenseFamilyFundInfoDB.ExpenseInfoId,
                        ExpenseDate = expenseFamilyFundInfoDB.ExpenseDate,
                        ExpenseAmount = expenseFamilyFundInfoDB.ExpenseAmount,
                        RecipientInfo = new()
                        {
                            Id = expenseFamilyFundInfoDB.RecipientId,
                            Name = expenseFamilyFundInfoDB.RecipientName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseFamilyFundInfoDB.ExpenseById,
                            Name = expenseFamilyFundInfoDB.ExpenseByName
                        },
                        Description = expenseFamilyFundInfoDB.Description,
                        SequenceId = expenseFamilyFundInfoDB.SequenceId,
                        CreatedBy = expenseFamilyFundInfoDB.CreatedBy,
                        CreatedDate = expenseFamilyFundInfoDB.CreatedDate,
                        ModifiedBy = expenseFamilyFundInfoDB.ModifiedBy,
                        ModifiedDate = expenseFamilyFundInfoDB.ModifiedDate,
                        RowStatus = expenseFamilyFundInfoDB.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundService.GetExpenseFamilyFundInfoListById({@expenseFamilyFundInfoId}, {@loggedInUserId})", expenseFamilyFundInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseFamilyFundInfo>>> GetExpenseFamilyFundInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseFamilyFundInfo>> response = new Response<List<ExpenseFamilyFundInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

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

                var dbResponse = await _expenseFamilyFundRepository.GetExpenseFamilyFundInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expenseFamilyFund => new ExpenseFamilyFundInfo()
                    {
                        Id = expenseFamilyFund.Id,
                        ExpenseInfoId = expenseFamilyFund.ExpenseInfoId,
                        ExpenseDate = expenseFamilyFund.ExpenseDate,
                        ExpenseAmount = expenseFamilyFund.ExpenseAmount,
                        RecipientInfo = new()
                        {
                            Id = expenseFamilyFund.RecipientId,
                            Name = expenseFamilyFund.RecipientName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseFamilyFund.ExpenseById,
                            Name = expenseFamilyFund.ExpenseByName
                        },
                        Description = expenseFamilyFund.Description,
                        SequenceId = expenseFamilyFund.SequenceId,
                        CreatedBy = expenseFamilyFund.CreatedBy,
                        CreatedDate = expenseFamilyFund.CreatedDate,
                        ModifiedBy = expenseFamilyFund.ModifiedBy,
                        ModifiedDate = expenseFamilyFund.ModifiedDate,
                        RowStatus = expenseFamilyFund.RowStatus
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundService.GetExpenseFamilyFundInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseFamilyFundInfoListByMonth(int month, int year, Guid loggedInUserId)
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

                var dbResponse = await _expenseFamilyFundRepository.GetExpenseFamilyFundInfoListByMonth(month, year, loggedInUserId);
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
                _logger.LogError(ex, "Error in ExpenseFamilyFundService.GetExpenseFamilyFundInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseFamilyFundInfo(ExpenseFamilyFundSaveRequestDTO expenseFamilyFundSaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (expenseFamilyFundSaveRequestDTO == null)
                {
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseFamilyFundSaveRequestDTO.RecipientInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid RecipientInfo Id");
                }

                if (expenseFamilyFundSaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (expenseFamilyFundSaveRequestDTO.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseFamilyFundInfoDB expenseFamilyFundInfoDB = new()
                {
                    Id = expenseFamilyFundSaveRequestDTO.Id,
                    ExpenseInfoId = expenseFamilyFundSaveRequestDTO.ExpenseInfoId,
                    ExpenseById = expenseFamilyFundSaveRequestDTO.ExpenseByInfoId,
                    ExpenseAmount = expenseFamilyFundSaveRequestDTO.ExpenseAmount,
                    ExpenseDate = expenseFamilyFundSaveRequestDTO.ExpenseDate,
                    RecipientId = expenseFamilyFundSaveRequestDTO.RecipientInfoId,
                    Description = expenseFamilyFundSaveRequestDTO.Description,
                };

                return await _expenseFamilyFundRepository.SaveExpenseFamilyFundInfoDB(expenseFamilyFundInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundService.SaveExpenseFamilyFundInfo({@expenseFamilyFundSaveRequestDTO}, {@loggedInUserId})", expenseFamilyFundSaveRequestDTO.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
