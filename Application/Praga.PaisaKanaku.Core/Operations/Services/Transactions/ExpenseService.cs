using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public class ExpenseService : IExpenseService
    {
        private readonly ILogger<ExpenseService> _logger;

        private readonly IExpenseRepository _expenseRepository;
        private readonly IProductService _productService;

        public ExpenseService(ILogger<ExpenseService> logger, IExpenseRepository expenseRepository, IProductService productService)
        {
            _logger = logger;
            _expenseRepository = expenseRepository;
            _productService = productService;
        }

        public async Task<Response<ExpenseReferenceDetailInfo>> GetExpenseInfoById(Guid expenseInfoId, Guid loggedInUserId)
        {
            Response<ExpenseReferenceDetailInfo> response = new Response<ExpenseReferenceDetailInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(expenseInfoId))
                {
                    return response;
                }

                Response<ExpenseReferenceDetailInfoDb> dbResponse = await _expenseRepository.GetExpenseInfoById(expenseInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new ExpenseReferenceDetailInfo()
                    {
                        Id = dbResponse.Data.Id,
                        DateOfExpense = dbResponse.Data.DateOfExpense,
                        ExpenseAmount = dbResponse.Data.ExpenseAmount,
                        ExpenseInfoId = dbResponse.Data.ExpenseInfoId,
                        ReferenceId = dbResponse.Data.ReferenceId,
                        Description = dbResponse.Data.ExpenseDescription,
                        ExpenseTypeInfo = new()
                        {
                            ExpenseType = dbResponse.Data.ExpenseType ?? String.Empty,
                            ExpenseTypeValue = dbResponse.Data.ExpenseTypeValue ?? String.Empty
                        },
                        ProductInfo = new()
                        {
                            Id = dbResponse.Data.ProductId,
                            Name = dbResponse.Data.ProductName,
                            BrandInfo = new()
                            {
                                Id = dbResponse.Data.BrandId,
                                Name = dbResponse.Data.BrandName ?? String.Empty,
                            },
                            Description = dbResponse.Data.ProductDescription,
                            ExpenseTypeInfo = new()
                            {
                                ExpenseType = dbResponse.Data.ExpenseType ?? String.Empty,
                                ExpenseTypeValue = dbResponse.Data.ExpenseTypeValue ?? String.Empty
                            },
                            PreferredTimePeriodInfo = new()
                            {
                                TimePeriodType = dbResponse.Data.PreferredRecurringTimePeriod ?? String.Empty,
                                TimePeriodTypeValue = dbResponse.Data.PreferredRecurringTimePeriodValue ?? String.Empty,
                            },
                            Price = dbResponse.Data.ExpenseAmount,
                            ProductCategoryInfo = new()
                            {
                                Id = dbResponse.Data.ProductCategoryId,
                                Name = dbResponse.Data.ProductCategoryName ?? String.Empty
                            }
                        },
                        SequenceId = dbResponse.Data.SequenceId,
                        CreatedBy = dbResponse.Data.CreatedBy,
                        CreatedDate = dbResponse.Data.CreatedDate,
                        ModifiedBy = dbResponse.Data.ModifiedBy,
                        ModifiedDate = dbResponse.Data.ModifiedDate,
                        RowStatus = dbResponse.Data.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.GetExpenseInfoList({@expenseInfoId}, {@loggedInUserId})", expenseInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<ExpenseReferenceDetailInfo>>> GetExpenseInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseReferenceDetailInfo>> response = new Response<List<ExpenseReferenceDetailInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _expenseRepository.GetExpenseInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expense => new ExpenseReferenceDetailInfo()
                    {
                        Id = expense.Id,
                        DateOfExpense = expense.DateOfExpense,
                        ExpenseAmount = expense.ExpenseAmount,
                        ExpenseInfoId = expense.ExpenseInfoId,
                        ReferenceId = expense.ReferenceId,
                        Description = expense.ExpenseDescription,
                        ExpenseTypeInfo = new()
                        {
                            ExpenseType = expense.ExpenseType ?? String.Empty,
                            ExpenseTypeValue = expense.ExpenseTypeValue ?? String.Empty
                        },
                        ProductInfo = new()
                        {
                            Id = expense.ProductId,
                            Name = expense.ProductName,
                            BrandInfo = new()
                            {
                                Id = expense.BrandId,
                                Name = expense.BrandName ?? String.Empty,
                            },
                            Description = expense.ProductDescription,
                            ExpenseTypeInfo = new()
                            {
                                ExpenseType = expense.ExpenseType ?? String.Empty,
                                ExpenseTypeValue = expense.ExpenseTypeValue ?? String.Empty
                            },
                            PreferredTimePeriodInfo = new()
                            {
                                TimePeriodType = expense.PreferredRecurringTimePeriod ?? String.Empty,
                                TimePeriodTypeValue = expense.PreferredRecurringTimePeriodValue ?? String.Empty,
                            },
                            Price = expense.ExpenseAmount,
                            ProductCategoryInfo = new()
                            {
                                Id = expense.ProductCategoryId,
                                Name = expense.ProductCategoryName ?? String.Empty
                            }
                        },
                        SequenceId = expense.SequenceId,
                        CreatedBy = expense.CreatedBy,
                        CreatedDate = expense.CreatedDate,
                        ModifiedBy = expense.ModifiedBy,
                        ModifiedDate = expense.ModifiedDate,
                        RowStatus = expense.RowStatus
                    }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.GetExpenseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveExpenseInfo(ExpenseReferenceDetailInfo expenseInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (expenseInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (expenseInfo.DateOfExpense == DateTime.MinValue || expenseInfo.DateOfExpense == DateTime.MaxValue)
                {
                    response.ValidationErrorMessages.Add("Invalid Date Of Expense");
                }

                if (expenseInfo.DateOfExpense > DateTime.UtcNow)
                {
                    response.ValidationErrorMessages.Add("Future Date is not allowed for Date of Expense");
                }

                if (!string.IsNullOrWhiteSpace(expenseInfo.Description)
                    && (expenseInfo.Description.Length < 2 || expenseInfo.Description.Length > 250))
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Description");
                }

                if (expenseInfo.ExpenseAmount < 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (expenseInfo.ExpenseBy == null)
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseBy");
                }
                else
                {
                    if (!Helpers.IsValidGuid(expenseInfo.ExpenseBy.Id))
                    {
                        response.ValidationErrorMessages.Add("Invalid ExpenseBy");

                    }
                    response.ValidationErrorMessages.Add("Invalid ExpenseBy ");
                }

                if (!Helpers.IsValidGuid(expenseInfo.ReferenceId))
                {
                    if (expenseInfo.ProductInfo == null)
                    {
                        response.ValidationErrorMessages.Add("Invalid Product Data");
                        response.Message = ResponseConstants.INVALID_PARAM;
                        return response;
                    }

                    if (string.IsNullOrWhiteSpace(expenseInfo.ProductInfo.Name))
                    {
                        response.ValidationErrorMessages.Add("Invalid Expense Name");
                    }
                    else
                    {
                        if (expenseInfo.ProductInfo.Name.Length < 2 || expenseInfo.ProductInfo.Name.Length > 50)
                        {
                            response.ValidationErrorMessages.Add("Expense Name must be between 2 and 50 Characters long.");
                        }
                    }

                    if (expenseInfo.ProductInfo.BrandInfo == null || (!Helpers.IsValidGuid(expenseInfo.ProductInfo.BrandInfo.Id) && string.IsNullOrWhiteSpace(expenseInfo.ProductInfo.BrandInfo.Name)))
                    {
                        response.ValidationErrorMessages.Add("Invalid Brand");
                    }

                    if (expenseInfo.ProductInfo.ExpenseTypeInfo == null || string.IsNullOrWhiteSpace(expenseInfo.ProductInfo.ExpenseTypeInfo.ExpenseType))
                    {
                        response.ValidationErrorMessages.Add("Invalid Expense Type");
                    }

                    if (expenseInfo.ExpenseTypeInfo == null || string.IsNullOrWhiteSpace(expenseInfo.ExpenseTypeInfo.ExpenseType))
                    {
                        response.ValidationErrorMessages.Add("Invalid Expense Type");
                    }

                    if (expenseInfo.ProductInfo.PreferredTimePeriodInfo == null || string.IsNullOrWhiteSpace(expenseInfo.ProductInfo.PreferredTimePeriodInfo.TimePeriodType))
                    {
                        response.ValidationErrorMessages.Add("Invalid Preferred Time Period");
                    }
                }

                ExpenseReferenceDetailInfoDb expenseInfoDb = new()
                {
                    DateOfExpense = expenseInfo.DateOfExpense,
                    ExpenseDescription = expenseInfo.Description,
                    ExpenseAmount = expenseInfo.ExpenseAmount,
                    ExpenseType = expenseInfo.ExpenseTypeInfo?.ExpenseType,
                    ExpenseBy = expenseInfo.ExpenseBy.Id
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(expenseInfo.Id))
                    {
                        response.ValidationErrorMessages.Add("Invalid Id");
                    }

                    if (!Helpers.IsValidGuid(expenseInfo.ExpenseInfoId))
                    {
                        response.ValidationErrorMessages.Add("Invalid ExpenseInfo Id");
                    }
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                expenseInfoDb.Id = isUpdate ? expenseInfo.Id : Guid.Empty;

                return await _expenseRepository.SaveExpenseInfo(expenseInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.SaveExpenseInfo({@expenseInfo}, {@loggedInUserId})", expenseInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }

        public async Task<Response<string>> ExportExpenseInfoData(Guid loggedInUserId)
        {
            Response<string> response = new Response<string>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                string csv = String.Empty;
                List<string> COLUMN_NAMES = new() { "Expense Date", "Price" };

                foreach (string column in COLUMN_NAMES)
                    csv += column + " , ";

                csv = csv[..^3];

                //Add new line.
                csv += "\r\n";

                Response<List<ExpenseReferenceDetailInfoDb>> expenseInfoList = await _expenseRepository.GetExpenseInfoList(loggedInUserId);

                if (Helpers.IsResponseValid(expenseInfoList))
                {
                    foreach (var expense in expenseInfoList.Data)
                    {
                        csv += expense.DateOfExpense.ToString().Replace(",", ";") + ',';
                        csv += Convert.ToString(expense.ExpenseAmount).Replace(",", ";") + ',';
                        csv += "\r\n";
                    }
                }

                response = response.GetSuccessResponse(csv);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.ExportExpenseInfoData({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public Task<Response<List<ExpenseInfo>>> GetExpenseBaseInfoList(Guid loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Guid>> SaveTempExpenseInfo(TempProductExpenseInfo tempProductExpenseInfo, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (tempProductExpenseInfo == null)
                {
                    return response;
                }

                if (!Helpers.IsValidGuid(tempProductExpenseInfo.ProductId))
                {
                    response.ValidationErrorMessages.Add("Invalid Product Id");
                }


                if (tempProductExpenseInfo.ExpenseBy == null || !Helpers.IsValidGuid(tempProductExpenseInfo.ExpenseBy?.Id))
                {
                    response.ValidationErrorMessages.Add("Invalid Expense By Id");
                }

                if (tempProductExpenseInfo.ExpenseDate > DateTime.UtcNow.Date)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Date");
                }

                if (tempProductExpenseInfo.ExpenseAmount <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Amount");
                }

                if (tempProductExpenseInfo.Quantity <= 0)
                {
                    response.ValidationErrorMessages.Add("Invalid Quantity");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                TempProductExpenseInfoDb tempProductExpenseInfoDb = new()
                {
                    Amount = tempProductExpenseInfo.ExpenseAmount,
                    MemberId = tempProductExpenseInfo.ExpenseBy?.Id ?? Guid.Empty,
                    Date = tempProductExpenseInfo.ExpenseDate,
                    Id = tempProductExpenseInfo.Id,
                    ProductId = tempProductExpenseInfo.ProductId,
                    Quantity = tempProductExpenseInfo.Quantity,
                    Description = tempProductExpenseInfo.Description
                };

                return await _expenseRepository.SaveTempExpenseInfo(tempProductExpenseInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.SaveTempExpenseInfo({@tempProductExpenseInfo}, {@loggedInUserId})", tempProductExpenseInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }
        }

        public async Task<Response<List<TempProductExpenseInfo>>> GetTempExpenseInfo(DateTime expenseDate, Guid loggedInUserId)
        {
            Response<List<TempProductExpenseInfo>> response = new Response<List<TempProductExpenseInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (expenseDate > DateTime.UtcNow)
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseDate");
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    return response;
                }

                var dbResponse = await _expenseRepository.GetTempExpenseInfo(expenseDate, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expense => new TempProductExpenseInfo()
                    {
                        Id = expense.Id,
                        ExpenseDate = expense.Date,
                        ExpenseAmount = expense.Amount,
                        ProductId = expense.ProductId,
                        ExpenseBy = new MemberInfo()
                        {
                            Id = expense.MemberId,
                            Name = expense.MemberName,
                        },
                        Quantity = expense.Quantity,
                        SequenceId = expense.SequenceId,
                        CreatedBy = expense.CreatedBy,
                        CreatedDate = expense.CreatedDate,
                        ModifiedBy = expense.ModifiedBy,
                        ModifiedDate = expense.ModifiedDate,
                        RowStatus = expense.RowStatus,
                        Description = expense.Description
                    }).ToList();

                    // Todo Get from DB
                    foreach (var item in response.Data)
                    {
                        if (item != null && Helpers.IsValidGuid(item.ProductId))
                        {
                            var productInfo = await _productService.GetProductInfoById(item.ProductId, loggedInUserId);
                            if (Helpers.IsResponseValid(productInfo))
                            {
                                item.ProductInfo = productInfo.Data;
                            }
                        }
                    }


                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.GetTempExpenseInfo({@expenseDate}, {@loggedInUserId})", expenseDate, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

            return response;
        }
    }
}
