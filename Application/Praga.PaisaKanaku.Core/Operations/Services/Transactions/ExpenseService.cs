using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense;
using Praga.PaisaKanaku.Core.DomainEntities.Lookups;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System.Xml.Linq;

namespace Praga.PaisaKanaku.Core.Operations.IServices.Transactions
{
    public class ExpenseService : IExpenseService
    {
        private readonly ILogger<ExpenseService> _logger;

        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(ILogger<ExpenseService> logger, IExpenseRepository expenseRepository)
        {
            _logger = logger;
            _expenseRepository = expenseRepository;
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

                Response<ExpenseReferenceDetailInfoDB> dbResponse = await _expenseRepository.GetExpenseInfoById(expenseInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new ExpenseReferenceDetailInfo()
                    {
                        Id = dbResponse.Data.Id,
                        ExpenseDate = dbResponse.Data.ExpenseDate,
                        ExpenseAmount = dbResponse.Data.ExpenseAmount,
                        ExpenseInfoId = dbResponse.Data.ExpenseInfoId,
                        ReferenceId = dbResponse.Data.ReferenceId,
                        Description = dbResponse.Data.ExpenseDescription,
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
                            PreferredTimePeriodInfo = new()
                            {
                                TimePeriodType = dbResponse.Data.PreferredRecurringTimePeriod ?? String.Empty,
                                TimePeriodTypeValue = dbResponse.Data.PreferredRecurringTimePeriodValue ?? String.Empty,
                            },
                            Price = dbResponse.Data.ExpenseAmount,
                            ProductCategoryInfo = new ProductCategoryInfo()
                            {
                                ProductCategory = dbResponse.Data.ProductCategory ?? String.Empty,
                                ProductCategoryValue = dbResponse.Data.ProductCategoryValue ?? String.Empty
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
                        ExpenseDate = expense.ExpenseDate,
                        ExpenseAmount = expense.ExpenseAmount,
                        ExpenseInfoId = expense.ExpenseInfoId,
                        ReferenceId = expense.ReferenceId,
                        Description = expense.ExpenseDescription,
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
                            PreferredTimePeriodInfo = new()
                            {
                                TimePeriodType = expense.PreferredRecurringTimePeriod ?? String.Empty,
                                TimePeriodTypeValue = expense.PreferredRecurringTimePeriodValue ?? String.Empty,
                            },
                            Price = expense.ExpenseAmount,
                            ProductCategoryInfo = new()
                            {
                                ProductCategory = expense.ProductCategory ?? String.Empty,
                                ProductCategoryValue = expense.ProductCategoryValue ?? String.Empty
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
                if (expenseSaveInfo.ExpenseDate == DateTime.MinValue)
                {
                    response.ValidationErrorMessages.Add("Invalid Date Of Expense");
                }

                if (expenseSaveInfo.ExpenseDate > DateTime.UtcNow)
                {
                    response.ValidationErrorMessages.Add("Future Date is not allowed for Date of Expense");
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

                    if (!Helpers.IsValidGuid(expenseItem.ExpenseById))
                    {
                        response.ValidationErrorMessages.Add("Invalid ExpenseById in Expense Product Items");
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
                            new XElement("ExpenseById", expenseItem.ExpenseById),
                            new XElement("Quantity", expenseItem.Quantity),
                            new XElement("ExpenseAmount", expenseItem.ExpenseAmount),
                            new XElement("Description", expenseItem.Description)
                        )
                    ));

                ExpenseSaveInfoDB expenseInfoDb = new()
                {
                    ExpenseDate = expenseSaveInfo.ExpenseDate,
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

                Response<List<ExpenseReferenceDetailInfoDB>> expenseInfoList = await _expenseRepository.GetExpenseInfoList(loggedInUserId);

                if (Helpers.IsResponseValid(expenseInfoList))
                {
                    foreach (var expense in expenseInfoList.Data)
                    {
                        csv += expense.ExpenseDate.ToString().Replace(",", ";") + ',';
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

        public async Task<Response<List<ExpenseInfo>>> GetExpenseBaseInfoList(Guid loggedInUserId)
        {
            Response<List<ExpenseInfo>> response = new Response<List<ExpenseInfo>>().GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _expenseRepository.GetExpenseBaseInfoList(loggedInUserId);
                
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data.Select(expense => new ExpenseInfo()
                    {
                        Id = expense.Id,
                        Amount = expense.Amount,
                        Date = expense.Date,
                        CreatedBy = expense.CreatedBy,  
                        CreatedDate =expense.CreatedDate,
                        ModifiedBy = expense.ModifiedBy,
                        ModifiedDate = expense.ModifiedDate,
                        RowStatus = expense.RowStatus
                    }).ToList();

                    response.IsSuccess = dbResponse.IsSuccess;
                    response.StatusCode = dbResponse.StatusCode;
                    response.Message = dbResponse.Message;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.GetExpenseBaseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> DeleteExpenseByType(Guid id, string expenseCategory, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(id))
                {
                    response.ValidationErrorMessages.Add("Invalid Id");
                }

                if (string.IsNullOrWhiteSpace(expenseCategory))
                {
                    response.ValidationErrorMessages.Add("Invalid Expense Category");
                }

                //if (!AppConstants.EXPENSE_TYPE_INFO_LIST.Contains(expenseCategory))
                //{
                //    response.ValidationErrorMessages.Add("Invalid Expense Category Not Exist");
                //}

                if (response.ValidationErrorMessages.Count > 0)
                {
                    response = new Response<Guid>().GetValidationFailedResponse(response.ValidationErrorMessages);
                    return response;
                }

                return await _expenseRepository.DeleteExpenseByType(id, expenseCategory, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseService.GetExpenseBaseInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }
    }
}
