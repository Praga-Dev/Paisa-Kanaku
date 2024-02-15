using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyWellbeing;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyWellbeing;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using static Praga.PaisaKanaku.Core.Common.Constants.AppConstants;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseFamilyWellbeingController : BaseController
    {
        private readonly ILogger<ExpenseFamilyWellbeingController> _logger;
        private readonly IExpenseFamilyWellbeingService _expenseFamilyWellbeingService;
        private readonly IExpenseService _expenseService;


        public ExpenseFamilyWellbeingController(ILogger<ExpenseFamilyWellbeingController> logger, 
            IExpenseFamilyWellbeingService expenseFamilyWellbeingService, IExpenseService expenseService) : base()
        {
            _logger = logger; ;
            _expenseFamilyWellbeingService = expenseFamilyWellbeingService;
            _expenseService = expenseService;
        }

        [HttpGet, Route("~/expense-family-wellbeing/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);
            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/ExpenseFamilyWellbeing/Index.cshtml", response);
                }

                int currMonth = DateTime.UtcNow.Month;
                int currYear = DateTime.UtcNow.Year;

                var dbresponse = await _expenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListByMonth(currMonth, currYear, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.Index({@loggedInUserId})", LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/ExpenseFamilyWellbeing/Index.cshtml", response);
        }

        [HttpPut, Route("~/expense-family-wellbeing/")]
        public async Task<IActionResult> SaveExpenseFamilyWellbeingInfo(ExpenseFamilyWellbeingSaveRequestDTO expenseFamilyWellbeingSaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseFamilyWellbeingSaveRequestDTO == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseFamilyWellbeingService.SaveExpenseFamilyWellbeingInfo(expenseFamilyWellbeingSaveRequestDTO, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.SaveExpenseFamilyWellbeingInfo({@expenseFamilyWellbeingSaveRequestDTO}, {@loggedInUserId})", expenseFamilyWellbeingSaveRequestDTO.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense-family-wellbeing/{month:int}/{year:int}")]
        public async Task<IActionResult> GetExpenseFamilyWellbeingInfo(int month, int year)
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_ExpenseFamilyWellbeingList.cshtml", response);
                }

                var dbresponse = await _expenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListByMonth(month, year, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.GetExpenseFamilyWellbeingInfo({@month}, {@year}, {@loggedInUserId})", month, year, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_ExpenseFamilyWellbeingList.cshtml", response);
        }

        [HttpGet, Route("~/expense-family-wellbeing/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetExpenseFamilyWellbeingInfo(DateTime expenseDate)
        {
            Response<List<ExpenseFamilyWellbeingInfo>> response = new Response<List<ExpenseFamilyWellbeingInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingCartList.cshtml", response);
                }

                var dbresponse = await _expenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.GetExpenseFamilyWellbeingInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-family-wellbeing/{expenseDate:DateTime}/cart")]
        public async Task<IActionResult> GetExpenseFamilyWellbeingInfoData(DateTime expenseDate)
        {
            Response<List<ExpenseFamilyWellbeingInfo>> response = new Response<List<ExpenseFamilyWellbeingInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingCartList.cshtml", response);
                }

                var dbresponse = await _expenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.GetExpenseFamilyWellbeingInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-family-wellbeing/{expenseFamilyWellbeingInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseFamilyWellbeingInfoById(Guid expenseFamilyWellbeingInfoId)
        {
            Response<ExpenseFamilyWellbeingInfo> response = new Response<ExpenseFamilyWellbeingInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingForm.cshtml", response.Data);
                }

                var dbresponse = await _expenseFamilyWellbeingService.GetExpenseFamilyWellbeingInfoById(expenseFamilyWellbeingInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.GetExpenseFamilyWellbeingInfoById({@expenseFamilyWellbeingInfoId}, {@loggedInUserId})", expenseFamilyWellbeingInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseFamilyWellbeing/_CreateExpenseFamilyWellbeingForm.cshtml", response.Data);

        }

        [HttpDelete, Route("~/expense-family-wellbeing/{expenseFamilyWellbeingInfoId:Guid}")]
        public async Task<IActionResult> DeleteTempExpenseInfo(Guid expenseFamilyWellbeingInfoId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseService.DeleteExpenseByType(expenseFamilyWellbeingInfoId
                    , ExpenseTypeConstants.EXPENSE_TYPE_FAMILY_WELLBEING, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseFamilyWellbeingController.DeleteTempExpenseInfo({@expenseFamilyWellbeingInfoId}, {@loggedInUserId})", expenseFamilyWellbeingInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
