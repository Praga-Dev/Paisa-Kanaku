using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.Mappers.Transactions;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using System.Net.Http.Headers;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseTravelService : IExpenseTravelService
    {
        private readonly ILogger<ExpenseTravelService> _logger;

        private readonly IExpenseTravelRepository _expenseTravelRepository;
        private readonly ICommonService _commonService;

        public ExpenseTravelService(ILogger<ExpenseTravelService> logger, IExpenseTravelRepository expenseTravelRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseTravelRepository = expenseTravelRepository;
        }


        public async Task<Response<Guid>> DeleteExpenseTravelInfo(Guid expenseTravelInfoId, Guid loggedInUserId)
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

                if (!Helpers.IsValidGuid(expenseTravelInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseTravelInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseTravelInfoId, AppConstants.EXPENSE_TRAVEL_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelService.DeleteExpenseTravelInfo({@expenseTravelInfoId}, {@loggedInUserId})", expenseTravelInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseTravelInfo>> GetExpenseTravelInfoById(Guid expenseTravelInfoId, Guid loggedInUserId)
        {
            Response<ExpenseTravelInfo> response = new Response<ExpenseTravelInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseTravelInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseTravelInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseTravelRepository.GetExpenseTravelInfoById(expenseTravelInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.ToExpenseTravelInfo();
                    response = response.GetSuccessResponse(response.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelService.GetExpenseTravelInfoListById({@expenseTravelInfoId}, {@loggedInUserId})", expenseTravelInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseTravelInfo>>> GetExpenseTravelInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseTravelInfo>> response = new Response<List<ExpenseTravelInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

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

                var dbResponse = await _expenseTravelRepository.GetExpenseTravelInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.ToExpenseTravelInfoList();
                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelService.GetExpenseTravelInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseTravelInfoListByMonth(int month, int year, Guid loggedInUserId)
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

                var dbResponse = await _expenseTravelRepository.GetExpenseTravelInfoListByMonth(month, year, loggedInUserId);
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
                _logger.LogError(ex, "Error in ExpenseTravelService.GetExpenseTravelInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseTravelInfo(ExpenseTravelSaveRequestDTO expenseTravelSaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                response =  ExpenseTravelValidation(expenseTravelSaveRequestDTO);

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseTravelInfoDB expenseTravelInfoDB = expenseTravelSaveRequestDTO.ToExpenseTravelInfoDB();

                return await _expenseTravelRepository.SaveExpenseTravelInfoDB(expenseTravelInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelService.SaveExpenseTravelInfo({@expenseTravelSaveRequestDTO}, {@loggedInUserId})", Convert.ToString(expenseTravelSaveRequestDTO), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        #region PrivateMethods

        private static Response<Guid> ExpenseTravelValidation(ExpenseTravelSaveRequestDTO expenseTravelSaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (expenseTravelSaveRequestDTO == null)
                {
                    return response;
                }

                if (expenseTravelSaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (expenseTravelSaveRequestDTO.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (!Helpers.IsValidGuid(expenseTravelSaveRequestDTO.ExpenseByInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseByInfoId");
                }

                if (string.IsNullOrWhiteSpace(expenseTravelSaveRequestDTO.Source))
                {
                    response.ValidationErrorMessages.Add("Invalid Source");
                }

                if (string.IsNullOrWhiteSpace(expenseTravelSaveRequestDTO.Destination))
                {
                    response.ValidationErrorMessages.Add("Invalid Destination");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                // TODO LOG
            }

            return response;
        }

        #endregion
    }
}
