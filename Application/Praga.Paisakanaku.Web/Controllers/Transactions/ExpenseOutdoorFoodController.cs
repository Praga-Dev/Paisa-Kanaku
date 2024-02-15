using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseOutdoorFood;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseOutdoorFood;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using static Praga.PaisaKanaku.Core.Common.Constants.AppConstants;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseOutdoorFoodController : BaseController
    {
        private readonly ILogger<ExpenseOutdoorFoodController> _logger;
        private readonly IExpenseOutdoorFoodService _expenseOutdoorFoodService;
        private readonly IExpenseService _expenseService;

        public ExpenseOutdoorFoodController(ILogger<ExpenseOutdoorFoodController> logger, IExpenseOutdoorFoodService expenseOutdoorFoodService, IExpenseService expenseService) : base()
        {
            _logger = logger; ;
            _expenseOutdoorFoodService = expenseOutdoorFoodService;
            _expenseService = expenseService;
        }

        [HttpGet, Route("~/expense-outdoor-food/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);
            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/ExpenseOutdoorFood/Index.cshtml", response);
                }

                int currMonth = DateTime.UtcNow.Month;
                int currYear = DateTime.UtcNow.Year;

                var dbresponse = await _expenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListByMonth(currMonth, currYear, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.Index({@loggedInUserId})", LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/ExpenseOutdoorFood/Index.cshtml", response);
        }

        [HttpPut, Route("~/expense-outdoor-food/")]
        public async Task<IActionResult> SaveExpenseOutdoorFoodInfo(ExpenseOutdoorFoodSaveRequestDTO expenseOutdoorFoodSaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseOutdoorFoodSaveRequestDTO == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseOutdoorFoodService.SaveExpenseOutdoorFoodInfo(expenseOutdoorFoodSaveRequestDTO, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.SaveExpenseOutdoorFoodInfo({@expenseOutdoorFoodSaveRequestDTO}, {@loggedInUserId})", expenseOutdoorFoodSaveRequestDTO.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense-outdoor-food/{month:int}/{year:int}")]
        public async Task<IActionResult> GetExpenseOutdoorFoodInfo(int month, int year)
        {
            Response<List<ExpenseInfoSumAmountByDate>> response = new Response<List<ExpenseInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_ExpenseOutdoorFoodList.cshtml", response);
                }

                var dbresponse = await _expenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListByMonth(month, year, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.GetExpenseOutdoorFoodInfo({@month}, {@year}, {@loggedInUserId})", month, year, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_ExpenseOutdoorFoodList.cshtml", response);
        }

        [HttpGet, Route("~/expense-outdoor-food/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetExpenseOutdoorFoodInfo(DateTime expenseDate)
        {
            Response<List<ExpenseOutdoorFoodInfo>> response = new Response<List<ExpenseOutdoorFoodInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodCartList.cshtml", response);
                }

                var dbresponse = await _expenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.GetExpenseOutdoorFoodInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-outdoor-food/{expenseDate:DateTime}/cart")]
        public async Task<IActionResult> GetExpenseOutdoorFoodInfoData(DateTime expenseDate)
        {
            Response<List<ExpenseOutdoorFoodInfo>> response = new Response<List<ExpenseOutdoorFoodInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodCartList.cshtml", response);
                }

                var dbresponse = await _expenseOutdoorFoodService.GetExpenseOutdoorFoodInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.GetExpenseOutdoorFoodInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-outdoor-food/{expenseOutdoorFoodInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseOutdoorFoodInfoById(Guid expenseOutdoorFoodInfoId)
        {
            Response<ExpenseOutdoorFoodInfo> response = new Response<ExpenseOutdoorFoodInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodForm.cshtml", response.Data);
                }

                var dbresponse = await _expenseOutdoorFoodService.GetExpenseOutdoorFoodInfoById(expenseOutdoorFoodInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.GetExpenseOutdoorFoodInfoById({@expenseOutdoorFoodInfoId}, {@loggedInUserId})", expenseOutdoorFoodInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseOutdoorFood/_CreateExpenseOutdoorFoodForm.cshtml", response.Data);

        }

        [HttpDelete, Route("~/expense-outdoor-food/{expenseOutdoorFoodInfoId:Guid}")]
        public async Task<IActionResult> DeleteOutdoorFoodExpenseInfo(Guid expenseOutdoorFoodInfoId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseService.DeleteExpenseByType(expenseOutdoorFoodInfoId
                    , ExpenseTypeConstants.EXPENSE_TYPE_OUTDOOR_FOOD, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseOutdoorFoodController.DeleteTempExpenseInfo({@expenseOutdoorFoodInfoId}, {@loggedInUserId})", expenseOutdoorFoodInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
