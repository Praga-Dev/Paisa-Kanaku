using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.PaisaKanaku.Core.Operations.Services.Transactions
{
    public class ExpenseProductService : IExpenseProductService
    {
        private readonly ILogger<ExpenseProductService> _logger;

        private readonly IExpenseProductRepository _expenseProductRepository;
        private readonly ICommonService _commonService;

        public ExpenseProductService(ILogger<ExpenseProductService> logger, IExpenseProductRepository expenseProductRepository
            , ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
            _expenseProductRepository = expenseProductRepository;
        }

        public async Task<Response<Guid>> SaveExpenseProductInfo(ExpenseProductSaveRequestDTO expenseProductSaveRequestDTO, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (expenseProductSaveRequestDTO == null)
                {
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseProductSaveRequestDTO.ProductInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ProductInfo Id");
                }

                if (expenseProductSaveRequestDTO.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (expenseProductSaveRequestDTO.ProductPrice <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Product Price");
                }

                if (expenseProductSaveRequestDTO.Quantity <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Quantity");
                }

                if (expenseProductSaveRequestDTO.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                ExpenseProductInfoDB expenseProductInfoDB = new()
                {
                    Id = expenseProductSaveRequestDTO.Id,
                    ExpenseInfoId = expenseProductSaveRequestDTO.ExpenseInfoId,
                    ProductPrice = expenseProductSaveRequestDTO.ProductPrice,
                    Quantity = expenseProductSaveRequestDTO.Quantity,
                    ExpenseAmount = expenseProductSaveRequestDTO.ExpenseAmount,
                    ExpenseById = expenseProductSaveRequestDTO.ExpenseByInfoId,
                    ExpenseDate = expenseProductSaveRequestDTO.ExpenseDate,
                    ProductInfoId = expenseProductSaveRequestDTO.ProductInfoId,
                    Description = expenseProductSaveRequestDTO.Description
                };

                return await _expenseProductRepository.SaveExpenseProductInfoDB(expenseProductInfoDB, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductService.SaveExpenseProductInfo({@expenseProductSaveRequestDTO}, {@loggedInUserId})", expenseProductSaveRequestDTO.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<ExpenseInfoSumAmountByDate>>> GetExpenseProductInfoListByMonth(int month, int year, Guid loggedInUserId)
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

                var dbResponse = await _expenseProductRepository.GetExpenseProductInfoListByMonth(month, year, loggedInUserId);
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
                _logger.LogError(ex, "Error in ExpenseProductService.GetExpenseProductInfoListByMonth({@month}, {@year}, {@loggedInUserId})", month, year, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<List<ExpenseProductInfo>>> GetExpenseProductInfoListByDate(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<ExpenseProductInfo>> response = new Response<List<ExpenseProductInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

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

                var dbResponse = await _expenseProductRepository.GetExpenseProductInfoListByDate(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expenseProduct => new ExpenseProductInfo()
                    {
                        Id = expenseProduct.Id,
                        ExpenseInfoId = expenseProduct.ExpenseInfoId,
                        ExpenseDate = expenseProduct.ExpenseDate,
                        ExpenseAmount = expenseProduct.ExpenseAmount,
                        ProductBaseInfo = new()
                        {
                            Id = expenseProduct.ProductInfoId,
                            Name = expenseProduct.ProductInfoName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseProduct.ExpenseById,
                            Name = expenseProduct.ExpenseByName
                        },
                        ProductPrice = expenseProduct.ProductPrice,
                        Quantity = expenseProduct.Quantity,
                        Description = expenseProduct.Description,
                        SequenceId = expenseProduct.SequenceId,
                        CreatedBy = expenseProduct.CreatedBy,
                        CreatedDate = expenseProduct.CreatedDate,
                        ModifiedBy = expenseProduct.ModifiedBy,
                        ModifiedDate = expenseProduct.ModifiedDate,
                        RowStatus = expenseProduct.RowStatus
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductService.GetExpenseProductInfoListByDate({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<ExpenseProductInfo>> GetExpenseProductInfoById(Guid expenseProductInfoId, Guid loggedInUserId)
        {
            Response<ExpenseProductInfo> response = new Response<ExpenseProductInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                #region Validation Block

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseProductInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseProductInfo Id");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                #endregion

                var dbResponse = await _expenseProductRepository.GetExpenseProductInfoById(expenseProductInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    ExpenseProductInfoDB expenseProductInfoDB = dbResponse.Data;

                    response.Data = new ExpenseProductInfo()
                    {
                        Id = expenseProductInfoDB .Id,
                        ExpenseInfoId = expenseProductInfoDB .ExpenseInfoId,
                        ExpenseDate = expenseProductInfoDB .ExpenseDate,
                        ProductPrice = expenseProductInfoDB.ProductPrice,
                        ExpenseAmount = expenseProductInfoDB .ExpenseAmount,
                        ProductBaseInfo = new()
                        {
                            Id = expenseProductInfoDB .ProductInfoId,
                            Name = expenseProductInfoDB .ProductInfoName
                        },
                        ExpenseByInfo = new()
                        {
                            Id = expenseProductInfoDB .ExpenseById,
                            Name = expenseProductInfoDB .ExpenseByName
                        },
                        Quantity = expenseProductInfoDB .Quantity,
                        Description = expenseProductInfoDB .Description,
                        SequenceId = expenseProductInfoDB .SequenceId,
                        CreatedBy = expenseProductInfoDB .CreatedBy,
                        CreatedDate = expenseProductInfoDB .CreatedDate,
                        ModifiedBy = expenseProductInfoDB .ModifiedBy,
                        ModifiedDate = expenseProductInfoDB .ModifiedDate,
                        RowStatus = expenseProductInfoDB .RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductService.GetExpenseProductInfoListById({@expenseProductInfoId}, {@loggedInUserId})", expenseProductInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }

        public async Task<Response<Guid>> DeleteExpenseProductInfo(Guid expenseProductInfoId, Guid loggedInUserId)
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

                if (!Helpers.IsValidGuid(expenseProductInfoId))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseProductInfo Id");
                }

                #endregion

                return await _commonService.DeleteRecord(expenseProductInfoId, AppConstants.EXPENSE_PRODUCT_INFO_TABLE, AppConstants.TRANSACTIONS_SCHEMA, loggedInUserId);

            } catch (Exception ex) 
            {
                _logger.LogError(ex, "Error in ExpenseProductService.DeleteExpenseProductInfo({@expenseProductInfoId}, {@loggedInUserId})", expenseProductInfoId, loggedInUserId);

                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }
    }
}
