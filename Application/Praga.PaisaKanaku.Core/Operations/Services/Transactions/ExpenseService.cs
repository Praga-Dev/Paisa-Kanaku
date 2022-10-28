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
using System.Xml.Linq;
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

        public async Task<Response<Guid>> CreateExpenseInfo(ExpenseSaveInfo expenseSaveInfo, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (expenseSaveInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                // Todo Add a valid start date, like 2000
                if (expenseSaveInfo.DateOfExpense == DateTime.MinValue)
                {
                    response.ValidationErrorMessages.Add("Invalid Date Of Expense");
                }

                if (expenseSaveInfo.DateOfExpense > DateTime.UtcNow)
                {
                    response.ValidationErrorMessages.Add("Future Date is not allowed for Date of Expense");
                }

                if (!Helpers.IsValidGuid(expenseSaveInfo.ExpenseBy))
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseBy");
                }

                if (expenseSaveInfo.ExpenseItemBaseInfoList == null || !expenseSaveInfo.ExpenseItemBaseInfoList.Any())
                {
                    response.ValidationErrorMessages.Add("Invalid ExpenseItemBaseInfoList");
                    return response;
                }

                if (response.ValidationErrorMessages.Count > 0)
                {
                    response = new Response<Guid>().GetValidationFailedResponse(response.ValidationErrorMessages);
                    return response;
                }

                List<ExpenseItemBaseInfo> expenseItemBaseInfoList = expenseSaveInfo.ExpenseItemBaseInfoList;

                foreach (var expenseItem in expenseSaveInfo.ExpenseItemBaseInfoList)
                {
                    if (!Helpers.IsValidGuid(expenseItem.Id))
                    {
                        response.ValidationErrorMessages.Add("Invalid ProductId in Expense Product Items");
                    }
                    
                    if (expenseItem.ExpenseAmount <= 0)
                    {
                        response.ValidationErrorMessages.Add("Invalid ExpenseAmount in Expense Product Items");
                    }

                    // Todo Add min and max as constant and refer here.
                    if (!string.IsNullOrWhiteSpace(expenseItem.Description)
                        && (expenseItem.Description.Length < 2 || expenseItem.Description.Length > 250))
                    {
                        response.ValidationErrorMessages.Add("Invalid Expense Description");
                    }

                    if (response.ValidationErrorMessages.Count > 0)
                    {
                        response = new Response<Guid>().GetValidationFailedResponse(response.ValidationErrorMessages);
                        return response;
                    }
                }

                XElement expenseData = new("Expense",
                    expenseSaveInfo.ExpenseItemBaseInfoList.Select(expenseItem =>
                        new XElement("Product",
                            new XElement("ProductId", expenseItem.Id),
                            new XElement("Quantity", expenseItem.Quantity),
                            new XElement("ExpenseAmount", expenseItem.ExpenseAmount),
                            new XElement("Description", expenseItem.Description)
                        )
                    ));

                ExpenseSaveInfoDb expenseInfoDb = new()
                {
                    ExpenseDate = expenseSaveInfo.DateOfExpense,
                    ExpenseBy = expenseSaveInfo.ExpenseBy,
                    ExpenseData = expenseData
                };

                return await _expenseRepository.CreateExpenseInfo(expenseInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.SaveExpenseInfo({@expenseSaveInfo}, {@loggedInUserId})", expenseSaveInfo.ToString(), loggedInUserId);
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
