using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseTravel;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using static Praga.PaisaKanaku.Core.Common.Constants.AppConstants;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseTravelController : BaseController
    {
        private readonly ILogger<ExpenseTravelController> _logger;
        private readonly IExpenseTravelService _expenseTravelService;
        private readonly IExpenseService _expenseService;

        public ExpenseTravelController(ILogger<ExpenseTravelController> logger, IExpenseTravelService expenseTravelService, IExpenseService expenseService) : base()
        {
            _logger = logger; ;
            _expenseTravelService = expenseTravelService;
            _expenseService = expenseService;
        }

        [HttpGet, Route("~/expense-travel/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);
            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/ExpenseTravel/Index.cshtml", response);
                }

                int currMonth = DateTime.UtcNow.Month;
                int currYear = DateTime.UtcNow.Year;

                var dbresponse = await _expenseTravelService.GetExpenseTravelInfoListByMonth(currMonth, currYear, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.Index({@loggedInUserId})", LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/ExpenseTravel/Index.cshtml", response);
        }

        [HttpPut, Route("~/expense-travel/")]
        public async Task<IActionResult> SaveExpenseTravelInfo(ExpenseTravelSaveRequestDTO expenseTravelSaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseTravelSaveRequestDTO == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseTravelService.SaveExpenseTravelInfo(expenseTravelSaveRequestDTO, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.SaveExpenseTravelInfo({@expenseTravelSaveRequestDTO}, {@loggedInUserId})", expenseTravelSaveRequestDTO.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense-travel/{month:int}/{year:int}")]
        public async Task<IActionResult> GetExpenseTravelInfo(int month, int year)
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseTravel/_ExpenseTravelList.cshtml", response);
                }

                var dbresponse = await _expenseTravelService.GetExpenseTravelInfoListByMonth(month, year, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.GetExpenseTravelInfo({@month}, {@year}, {@loggedInUserId})", month, year, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseTravel/_ExpenseTravelList.cshtml", response);
        }

        [HttpGet, Route("~/expense-travel/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetExpenseTravelInfo(DateTime expenseDate)
        {
            Response<List<ExpenseTravelInfo>> response = new Response<List<ExpenseTravelInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelCartList.cshtml", response);
                }

                var dbresponse = await _expenseTravelService.GetExpenseTravelInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.GetExpenseTravelInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-travel/{expenseDate:DateTime}/cart")]
        public async Task<IActionResult> GetExpenseTravelInfoData(DateTime expenseDate)
        {
            Response<List<ExpenseTravelInfo>> response = new Response<List<ExpenseTravelInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelCartList.cshtml", response);
                }

                var dbresponse = await _expenseTravelService.GetExpenseTravelInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.GetExpenseTravelInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-travel/{expenseTravelInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseTravelInfoById(Guid expenseTravelInfoId)
        {
            Response<ExpenseTravelInfo> response = new Response<ExpenseTravelInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelForm.cshtml", response.Data);
                }

                var dbresponse = await _expenseTravelService.GetExpenseTravelInfoById(expenseTravelInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.GetExpenseTravelInfoById({@expenseTravelInfoId}, {@loggedInUserId})", expenseTravelInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseTravel/_CreateExpenseTravelForm.cshtml", response.Data);

        }

        [HttpDelete, Route("~/expense-travel/{expenseTravelInfoId:Guid}")]
        public async Task<IActionResult> DeleteTravelExpenseInfo(Guid expenseTravelInfoId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseService.DeleteExpenseByType(expenseTravelInfoId
                    , ExpenseTypeConstants.EXPENSE_TYPE_TRAVEL, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseTravelController.DeleteTempExpenseInfo({@expenseTravelInfoId}, {@loggedInUserId})", expenseTravelInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
