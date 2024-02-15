using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseOutdoorFood;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseOutdoorFood;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseOutdoorFood;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseOutdoorFoodService : IExpenseOutdoorFoodService
    {
        private readonly ILogger<ExpenseOutdoorFoodService> _logger;

        private readonly IExpenseOutdoorFoodRepository _expenseOutdoorFoodRepository;
        private readonly ICommonService _commonService;

        public ExpenseOutdoorFoodService(ILogger<ExpenseOutdoorFoodService> logger, IExpenseOutdoorFoodRepository expenseOutdoorFoodRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseOutdoorFoodRepository = expenseOutdoorFoodRepository;
        }


        public async Task<Response<Guid>> DeleteExpenseOutdoorFoodInfo(Guid expenseOutdoorFoodInfoId, Guid loggedInUserId)
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

                if (!Helpers.IsValidGuid(expenseOutdoorFoodInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseOutdoorFoodInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseOutdoorFoodInfoId, AppConstants.EXPENSE_FAMILY_FUND_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodService.DeleteExpenseOutdoorFoodInfo({@expenseOutdoorFoodInfoId}, {@loggedInUserId})", expenseOutdoorFoodInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<ExpenseOutdoorFoodInfo>> GetExpenseOutdoorFoodInfoById(Guid expenseOutdoorFoodInfoId, Guid loggedInUserId)
        {
            Response<ExpenseOutdoorFoodInfo> response = new Response<ExpenseOutdoorFoodInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseOutdoorFoodInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseOutdoorFoodInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseOutdoorFoodRepository.GetExpenseOutdoorFoodInfoById(expenseOutdoorFoodInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    ExpenseOutdoorFoodInfoDB expenseOutdoorFoodInfoDB = dbResponse.Data;

                    response.Data = new ExpenseOutdoorFoodInfo()
                    {
                        Id = expenseOutdoorFoodInfoDB.Id,
                        ExpenseInfoId = expenseOutdoorFoodInfoDB.ExpenseInfoId,
                        ExpenseDate = expenseOutdoorFoodInfoDB.ExpenseDate,
                        ExpenseAmount = expenseOutdoorFoodInfoDB.ExpenseAmount,
                        OutdoorFoodVendorInfo = new()
                        {
                            Id = expenseOutdoorFoodInfoDB.OutdoorFoodVendorId,
                            Name = expenseOutdoorFoodInfoDB.OutdoorFoodVendorName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseOutdoorFoodInfoDB.ExpenseById,
                            Name = expenseOutdoorFoodInfoDB.ExpenseByName
                        },
                        BillImageURL = expenseOutdoorFoodInfoDB.BillImageURL,
                        Description = expenseOutdoorFoodInfoDB.Description,
                        SequenceId = expenseOutdoorFoodInfoDB.SequenceId,
                        CreatedBy = expenseOutdoorFoodInfoDB.CreatedBy,
                        CreatedDate = expenseOutdoorFoodInfoDB.CreatedDate,
                        ModifiedBy = expenseOutdoorFoodInfoDB.ModifiedBy,
                        ModifiedDate = expenseOutdoorFoodInfoDB.ModifiedDate,
                        RowStatus = expenseOutdoorFoodInfoDB.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListById({@expenseOutdoorFoodInfoId}, {@loggedInUserId})", expenseOutdoorFoodInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseOutdoorFoodInfo>>> GetExpenseOutdoorFoodInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseOutdoorFoodInfo>> response = new Response<List<ExpenseOutdoorFoodInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

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

                var dbResponse = await _expenseOutdoorFoodRepository.GetExpenseOutdoorFoodInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expenseOutdoorFood => new ExpenseOutdoorFoodInfo()
                    {
                        Id = expenseOutdoorFood.Id,
                        ExpenseInfoId = expenseOutdoorFood.ExpenseInfoId,
                        ExpenseDate = expenseOutdoorFood.ExpenseDate,
                        ExpenseAmount = expenseOutdoorFood.ExpenseAmount,
                        OutdoorFoodVendorInfo = new()
                        {
                            Id = expenseOutdoorFood.Id,
                            Name = expenseOutdoorFood.OutdoorFoodVendorName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseOutdoorFood.ExpenseById,
                            Name = expenseOutdoorFood.ExpenseByName
                        },
                        BillImageURL = expenseOutdoorFood.BillImageURL,
                        Description = expenseOutdoorFood.Description,
                        SequenceId = expenseOutdoorFood.SequenceId,
                        CreatedBy = expenseOutdoorFood.CreatedBy,
                        CreatedDate = expenseOutdoorFood.CreatedDate,
                        ModifiedBy = expenseOutdoorFood.ModifiedBy,
                        ModifiedDate = expenseOutdoorFood.ModifiedDate,
                        RowStatus = expenseOutdoorFood.RowStatus
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseOutdoorFoodInfoListByMonth(int month, int year, Guid loggedInUserId)
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

                var dbResponse = await _expenseOutdoorFoodRepository.GetExpenseOutdoorFoodInfoListByMonth(month, year, loggedInUserId);
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
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseOutdoorFoodInfo(ExpenseOutdoorFoodSaveRequestDTO expenseOutdoorFoodSaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (expenseOutdoorFoodSaveRequestDTO == null)
                {
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseOutdoorFoodSaveRequestDTO.OutdoorFoodVendorInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid Outdoor Food Vendor Info Id");
                }

                if (expenseOutdoorFoodSaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (expenseOutdoorFoodSaveRequestDTO.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseOutdoorFoodInfoDB expenseOutdoorFoodInfoDB = new()
                {
                    Id = expenseOutdoorFoodSaveRequestDTO.Id,
                    ExpenseInfoId = expenseOutdoorFoodSaveRequestDTO.ExpenseInfoId,
                    ExpenseById = expenseOutdoorFoodSaveRequestDTO.ExpenseByInfoId,
                    ExpenseAmount = expenseOutdoorFoodSaveRequestDTO.ExpenseAmount,
                    ExpenseDate = expenseOutdoorFoodSaveRequestDTO.ExpenseDate,
                    OutdoorFoodVendorId = expenseOutdoorFoodSaveRequestDTO.OutdoorFoodVendorInfoId,
                    BillImageURL = expenseOutdoorFoodSaveRequestDTO.BillImageURL,
                    Description = expenseOutdoorFoodSaveRequestDTO.Description,
                };

                return await _expenseOutdoorFoodRepository.SaveExpenseOutdoorFoodInfoDB(expenseOutdoorFoodInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodService.SaveExpenseOutdoorFoodInfo({@expenseOutdoorFoodSaveRequestDTO}, {@loggedInUserId})", expenseOutdoorFoodSaveRequestDTO.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
