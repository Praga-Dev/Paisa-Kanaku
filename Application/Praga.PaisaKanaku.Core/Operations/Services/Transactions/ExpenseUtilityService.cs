using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseUtility;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseUtility;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseUtility;
using Praga.PaisaKanaku.Core.Mappers.Transactions;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using System.Net.Http.Headers;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseUtilityService : IExpenseUtilityService
    {
        private readonly ILogger<ExpenseUtilityService> _logger;

        private readonly IExpenseUtilityRepository _expenseUtilityRepository;
        private readonly ICommonService _commonService;

        public ExpenseUtilityService(ILogger<ExpenseUtilityService> logger, IExpenseUtilityRepository expenseUtilityRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseUtilityRepository = expenseUtilityRepository;
        }


        public async Task<Response<Guid>> DeleteExpenseUtilityInfo(Guid expenseUtilityInfoId, Guid loggedInUserId)
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

                if (!Helpers.IsValidGuid(expenseUtilityInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseUtilityInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseUtilityInfoId, AppConstants.EXPENSE_UTILITY_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityService.DeleteExpenseUtilityInfo({@expenseUtilityInfoId}, {@loggedInUserId})", expenseUtilityInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseUtilityInfo>> GetExpenseUtilityInfoById(Guid expenseUtilityInfoId, Guid loggedInUserId)
        {
            Response<ExpenseUtilityInfo> response = new Response<ExpenseUtilityInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseUtilityInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseUtilityInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseUtilityRepository.GetExpenseUtilityInfoById(expenseUtilityInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.ToExpenseUtilityInfo();
                    response = response.GetSuccessResponse(response.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityService.GetExpenseUtilityInfoListById({@expenseUtilityInfoId}, {@loggedInUserId})", expenseUtilityInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseUtilityInfo>>> GetExpenseUtilityInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseUtilityInfo>> response = new Response<List<ExpenseUtilityInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

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

                var dbResponse = await _expenseUtilityRepository.GetExpenseUtilityInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.ToExpenseUtilityInfoList();
                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityService.GetExpenseUtilityInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseUtilityInfoListByMonth(int month, int year, Guid loggedInUserId)
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

                var dbResponse = await _expenseUtilityRepository.GetExpenseUtilityInfoListByMonth(month, year, loggedInUserId);
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
                _logger.LogError(ex, "Error in ExpenseUtilityService.GetExpenseUtilityInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseUtilityInfo(ExpenseUtilitySaveRequestDTO expenseUtilitySaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                response = ExpenseUtilityValidation(expenseUtilitySaveRequestDTO);

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseUtilityInfoDB expenseUtilityInfoDB = expenseUtilitySaveRequestDTO.ToExpenseUtilityInfoDB();

                return await _expenseUtilityRepository.SaveExpenseUtilityInfoDB(expenseUtilityInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseUtilityService.SaveExpenseUtilityInfo({@expenseUtilitySaveRequestDTO}, {@loggedInUserId})", Convert.ToString(expenseUtilitySaveRequestDTO), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        #region PrivateMethods

        private static Response<Guid> ExpenseUtilityValidation(ExpenseUtilitySaveRequestDTO expenseUtilitySaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            if (expenseUtilitySaveRequestDTO == null)
            {
                return response;
            }

            if (expenseUtilitySaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
            {
                response.ValidationErrorMessages.Add("Invalid Expense Date");
            }

            if (expenseUtilitySaveRequestDTO.ExpenseAmount <= 0)
            {
                response.ValidationErrorMessages.Add("Invalid Expense Amount");
            }

            if (!Helpers.IsValidGuid(expenseUtilitySaveRequestDTO.ExpenseByInfoId))
            {
                response.ValidationErrorMessages.Add("Invalid ExpenseByInfoId");
            }

            if (string.IsNullOrWhiteSpace(expenseUtilitySaveRequestDTO.ConsumerType))
            {
                response.ValidationErrorMessages.Add("Invalid Consumer Type");
            }

            if (string.IsNullOrWhiteSpace(expenseUtilitySaveRequestDTO.ServiceDuration))
            {
                response.ValidationErrorMessages.Add("Invalid Service Duration");
            }

            // TODO add validation Logic 
            //if (expenseUtilitySaveRequestDTO.FromDate > DateTime.UtcNow.Date
            //    || expenseUtilitySaveRequestDTO.FromDate >= expenseUtilitySaveRequestDTO.ToDate)
            //{
            //    response.ValidationErrorMessages.Add("Invalid From Date");
            //}

            //if (expenseUtilitySaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
            //{
            //    response.ValidationErrorMessages.Add("Invalid Expense Date");
            //}
            
            return response;
        }

        #endregion
    }
}
