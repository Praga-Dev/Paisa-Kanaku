using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseGrocery;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseGroceryController : BaseController
    {
        private readonly ILogger<ExpenseGroceryController> _logger;
        private readonly IExpenseGroceryService _expenseGroceryService;

        public ExpenseGroceryController(ILogger<ExpenseGroceryController> logger, IExpenseGroceryService expenseGroceryService) : base()
        {
            _logger = logger; ;
            _expenseGroceryService = expenseGroceryService;
        }

        [HttpGet, Route("~/expense-grocery/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);
            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/ExpenseGrocery/Index.cshtml", response);
                }

                int currMonth = DateTime.UtcNow.Month;
                int currYear = DateTime.UtcNow.Year;

                var dbresponse = await _expenseGroceryService.GetExpenseGroceryInfoListByMonth(currMonth, currYear, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.Index({@loggedInUserId})", LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/ExpenseGrocery/Index.cshtml", response);
        }

        [HttpPut, Route("~/expense-grocery/")]
        public async Task<IActionResult> SaveExpenseGroceryInfo(ExpenseGrocerySaveRequestDTO expenseGrocerySaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseGrocerySaveRequestDTO == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseGroceryService.SaveExpenseGroceryInfo(expenseGrocerySaveRequestDTO, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.SaveExpenseGroceryInfo({@expenseGrocerySaveRequestDTO}, {@loggedInUserId})", expenseGrocerySaveRequestDTO.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense-grocery/{month:int}/{year:int}")]
        public async Task<IActionResult> GetExpenseGroceryInfo(int month, int year)
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseGrocery/_ExpenseGroceryList.cshtml", response);
                }

                var dbresponse = await _expenseGroceryService.GetExpenseGroceryInfoListByMonth(month, year, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.GetExpenseGroceryInfo({@month}, {@year}, {@loggedInUserId})", month, year, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseGrocery/_ExpenseGroceryList.cshtml", response);
        }

        [HttpGet, Route("~/expense-grocery/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetExpenseGroceryInfo(DateTime expenseDate)
        {
            Response<List<ExpenseGroceryInfo>> response = new Response<List<ExpenseGroceryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryCartList.cshtml", response);
                }

                var dbresponse = await _expenseGroceryService.GetExpenseGroceryInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.GetExpenseGroceryInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-grocery/{expenseDate:DateTime}/cart")]
        public async Task<IActionResult> GetExpenseGroceryInfoData(DateTime expenseDate)
        {
            Response<List<ExpenseGroceryInfo>> response = new Response<List<ExpenseGroceryInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryCartList.cshtml", response);
                }

                var dbresponse = await _expenseGroceryService.GetExpenseGroceryInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.GetExpenseGroceryInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-grocery/{expenseGroceryInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseGroceryInfoById(Guid expenseGroceryInfoId)
        {
            Response<ExpenseGroceryInfo> response = new Response<ExpenseGroceryInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryForm.cshtml", response.Data);
                }

                var dbresponse = await _expenseGroceryService.GetExpenseGroceryInfoById(expenseGroceryInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.GetExpenseGroceryInfoById({@expenseGroceryInfoId}, {@loggedInUserId})", expenseGroceryInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseGrocery/_CreateExpenseGroceryForm.cshtml", response.Data);

        }

        [HttpDelete, Route("~/expense-grocery/{expenseGroceryInfoId:Guid}")]
        public async Task<IActionResult> DeleteTempExpenseInfo(Guid expenseGroceryInfoId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseGroceryService.DeleteExpenseGroceryInfo(expenseGroceryInfoId, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseGroceryController.DeleteTempExpenseInfo({@expenseGroceryInfoId}, {@loggedInUserId})", expenseGroceryInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
