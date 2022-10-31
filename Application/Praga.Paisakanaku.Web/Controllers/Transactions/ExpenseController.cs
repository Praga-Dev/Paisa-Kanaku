using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System.IO;
using System;
using System.Net;
using System.Text;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseController : BaseController
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IExpenseService _expenseService;

        public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService) : base()
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        [HttpGet, Route("~/expense/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseReferenceDetailInfo>> response = new Response<List<ExpenseReferenceDetailInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/Expense/Index.cshtml", response);
                }

                var dbresponse = await _expenseService.GetExpenseInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.GetExpenseTypeInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/Expense/Index.cshtml", response);
        }

        [HttpPost, Route("~/expense/")]
        public async Task<IActionResult> CreateExpenseInfo(ExpenseSaveInfo expenseSaveInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseSaveInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseService.CreateExpenseInfo(expenseSaveInfo, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.CreateExpenseInfo({@expenseSaveInfo}, {@loggedInUserId})", expenseSaveInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense/list")]
        public async Task<IActionResult> GetExpenseInfoList()
        {
            Response<List<ExpenseReferenceDetailInfo>> response = new Response<List<ExpenseReferenceDetailInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/Expense/_ExpenseList.cshtml", response);
                }

                var dbresponse = await _expenseService.GetExpenseInfoList(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.GetExpenseInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/Expense/_ExpenseList.cshtml", response);

        }

        [HttpGet, Route("~/expense/{expenseInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseInfoById(Guid expenseInfoId)
        {
            Response<ExpenseReferenceDetailInfo> response = new Response<ExpenseReferenceDetailInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/Expense/_CreateExpense.cshtml", null);
                }

                if (!Helpers.IsValidGuid(expenseInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return PartialView("~/Views/Transactions/Expense/_CreateExpense.cshtml", null);
                }

                var dbresponse = await _expenseService.GetExpenseInfoById(expenseInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.GetExpenseInfoById({@expenseInfoId}, {@loggedInUserId})", expenseInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/Expense/_CreateExpense.cshtml", response.Data);
        }

        [HttpPut, Route("~/expense/temp")]
        public async Task<IActionResult> SaveTempExpenseInfo(TempProductExpenseInfo tempProductExpenseInfo)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (tempProductExpenseInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseService.SaveTempExpenseInfo(tempProductExpenseInfo, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.SaveTempExpenseInfo({@tempProductExpenseInfo}, {@loggedInUserId})", tempProductExpenseInfo.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense/temp/date/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetTempExpenseInfoList(DateTime expenseDate)
        {
            Response<List<TempProductExpenseInfo>> response = new Response<List<TempProductExpenseInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/Expense/_CreateExpenseCartList.cshtml", response);
                }

                var dbresponse = await _expenseService.GetTempExpenseInfo(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/Expense/_CreateExpenseCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.GetExpenseInfoList({@loggedInUserId})", LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/Expense/_CreateExpenseCartList.cshtml", response);

        }

        [HttpGet, Route("~/expense/export")]
        public async Task<IActionResult> ExportExpenseInfoData()
        {
            Response<string> response = new Response<string>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(this.LoggedInUserId))
                {
                    byte[] failuremsgByte = Encoding.ASCII.GetBytes("SOMETHING WENT WRONG !!!");
                    return File(failuremsgByte, "application/text", "ExpenseList.csv");
                }

                var dbresponse = await _expenseService.ExportExpenseInfoData(LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseController.ExportExpenseInfoData({@loggedInUserId})", LoggedInUserId);
            }

            byte[] bytes = Encoding.ASCII.GetBytes(response.Data);
            var file = File(bytes, "application/text", "ExpenseList.csv");
            return file;
        }
    }
}
