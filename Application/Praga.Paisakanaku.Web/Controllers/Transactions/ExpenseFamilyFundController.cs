using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyFund;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyFund;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseFamilyFundController : BaseController
    {
        private readonly ILogger<ExpenseFamilyFundController> _logger;
        private readonly IExpenseFamilyFundService _expenseFamilyFundService;

        public ExpenseFamilyFundController(ILogger<ExpenseFamilyFundController> logger, IExpenseFamilyFundService expenseFamilyFundService) : base()
        {
            _logger = logger; ;
            _expenseFamilyFundService = expenseFamilyFundService;
        }

        [HttpGet, Route("~/expense-family-fund/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);
            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/ExpenseFamilyFund/Index.cshtml", response);
                }

                int currMonth = DateTime.UtcNow.Month;
                int currYear = DateTime.UtcNow.Year;

                var dbresponse = await _expenseFamilyFundService.GetExpenseFamilyFundInfoListByMonth(currMonth, currYear, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.Index({@loggedInUserId})", LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/ExpenseFamilyFund/Index.cshtml", response);
        }

        [HttpPut, Route("~/expense-family-fund/")]
        public async Task<IActionResult> SaveExpenseFamilyFundInfo(ExpenseFamilyFundSaveRequestDTO expenseFamilyFundSaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseFamilyFundSaveRequestDTO == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseFamilyFundService.SaveExpenseFamilyFundInfo(expenseFamilyFundSaveRequestDTO, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.SaveExpenseFamilyFundInfo({@expenseFamilyFundSaveRequestDTO}, {@loggedInUserId})", expenseFamilyFundSaveRequestDTO.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense-family-fund/{month:int}/{year:int}")]
        public async Task<IActionResult> GetExpenseFamilyFundInfo(int month, int year)
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyFund/_ExpenseFamilyFundList.cshtml", response);
                }

                var dbresponse = await _expenseFamilyFundService.GetExpenseFamilyFundInfoListByMonth(month, year, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.GetExpenseFamilyFundInfo({@month}, {@year}, {@loggedInUserId})", month, year, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyFund/_ExpenseFamilyFundList.cshtml", response);
        }

        [HttpGet, Route("~/expense-family-fund/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetExpenseFamilyFundInfo(DateTime expenseDate)
        {
            Response<List<ExpenseFamilyFundInfo>> response = new Response<List<ExpenseFamilyFundInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundCartList.cshtml", response);
                }

                var dbresponse = await _expenseFamilyFundService.GetExpenseFamilyFundInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.GetExpenseFamilyFundInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-family-fund/{expenseDate:DateTime}/cart")]
        public async Task<IActionResult> GetExpenseFamilyFundInfoData(DateTime expenseDate)
        {
            Response<List<ExpenseFamilyFundInfo>> response = new Response<List<ExpenseFamilyFundInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundCartList.cshtml", response);
                }

                var dbresponse = await _expenseFamilyFundService.GetExpenseFamilyFundInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.GetExpenseFamilyFundInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-family-fund/{expenseFamilyFundInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseFamilyFundInfoById(Guid expenseFamilyFundInfoId)
        {
            Response<ExpenseFamilyFundInfo> response = new Response<ExpenseFamilyFundInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundForm.cshtml", response.Data);
                }

                var dbresponse = await _expenseFamilyFundService.GetExpenseFamilyFundInfoById(expenseFamilyFundInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.GetExpenseFamilyFundInfoById({@expenseFamilyFundInfoId}, {@loggedInUserId})", expenseFamilyFundInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyFund/_CreateExpenseFamilyFundForm.cshtml", response.Data);

        }

        [HttpDelete, Route("~/expense-family-fund/{expenseFamilyFundInfoId:Guid}")]
        public async Task<IActionResult> DeleteTempExpenseInfo(Guid expenseFamilyFundInfoId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseFamilyFundService.DeleteExpenseFamilyFundInfo(expenseFamilyFundInfoId, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyFundController.DeleteTempExpenseInfo({@expenseFamilyFundInfoId}, {@loggedInUserId})", expenseFamilyFundInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
