using Microsoft.AspNetCore.Mvc;
using Praga.Paisakanaku.Web.Controllers.Base;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseProduct;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;

namespace Praga.Paisakanaku.Web.Controllers.Setup
{
    public class ExpenseProductController : BaseController
    {
        private readonly ILogger<ExpenseProductController> _logger;
        private readonly IExpenseProductService _expenseProductService;

        public ExpenseProductController(ILogger<ExpenseProductController> logger, IExpenseProductService expenseProductService) : base()
        {
            _logger = logger;;
            _expenseProductService = expenseProductService;
        }

        [HttpGet, Route("~/expense-product/")]
        public async Task<IActionResult> Index()
        {
            Response<List<ExpenseProductInfoSumAmountByDate>> response = new Response<List<ExpenseProductInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);
            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Transactions/ExpenseProduct/Index.cshtml", response);
                }

                int currMonth = DateTime.UtcNow.Month;
                int currYear = DateTime.UtcNow.Year;

                var dbresponse = await _expenseProductService.GetExpenseProductInfoListByMonth(currMonth, currYear, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.Index({@loggedInUserId})", LoggedInUserId);
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Transactions/ExpenseProduct/Index.cshtml", response);
        }

        [HttpPut, Route("~/expense-product/")]
        public async Task<IActionResult> SaveExpenseProductInfo(ExpenseProductSaveRequestDTO expenseProductSaveRequestDTO)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                if (expenseProductSaveRequestDTO == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseProductService.SaveExpenseProductInfo(expenseProductSaveRequestDTO, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.SaveExpenseProductInfo({@expenseProductSaveRequestDTO}, {@loggedInUserId})", expenseProductSaveRequestDTO.ToString(), LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet, Route("~/expense-product/{month:int}/{year:int}")]
        public async Task<IActionResult> GetExpenseProductInfo(int month, int year)
        {
            Response<List<ExpenseProductInfoSumAmountByDate>> response = new Response<List<ExpenseProductInfoSumAmountByDate>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseProduct/_ExpenseProductList.cshtml", response);
                }

                var dbresponse = await _expenseProductService.GetExpenseProductInfoListByMonth(month, year, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.GetExpenseProductInfo({@month}, {@year}, {@loggedInUserId})", month, year, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseProduct/_ExpenseProductList.cshtml", response);
        }

        [HttpGet, Route("~/expense-product/{expenseDate:DateTime}")]
        public async Task<IActionResult> GetExpenseProductInfo(DateTime expenseDate)
        {
            Response<List<ExpenseProductInfo>> response = new Response<List<ExpenseProductInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseCartList.cshtml", response);
                }

                var dbresponse = await _expenseProductService.GetExpenseProductInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseCartList.cshtml", dbresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.GetExpenseProductInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-product/{expenseDate:DateTime}/cart")]
        public async Task<IActionResult> GetExpenseProductInfoData(DateTime expenseDate)
        {
            Response<List<ExpenseProductInfo>> response = new Response<List<ExpenseProductInfo>>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseCartList.cshtml", response);
                }

                var dbresponse = await _expenseProductService.GetExpenseProductInfoListByDate(expenseDate, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.GetExpenseProductInfo({@expenseDate}, {@loggedInUserId})", expenseDate, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseCartList.cshtml", response);
        }

        [HttpGet, Route("~/expense-product/{expenseProductInfoId:Guid}")]
        public async Task<IActionResult> GetExpenseProductInfoById(Guid expenseProductInfoId)
        {
            Response<ExpenseProductInfo> response = new Response<ExpenseProductInfo>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseForm.cshtml", response.Data);
                }

                var dbresponse = await _expenseProductService.GetExpenseProductInfoById(expenseProductInfoId, LoggedInUserId);
                if (Helpers.IsResponseValid(dbresponse))
                {
                    response = dbresponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.GetExpenseProductInfoById({@expenseProductInfoId}, {@loggedInUserId})", expenseProductInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Transactions/ExpenseProduct/_CreateExpenseForm.cshtml", response.Data);

        }

        [HttpDelete, Route("~/expense-product/{expenseProductInfoId:Guid}")]
        public async Task<IActionResult> DeleteTempExpenseInfo(Guid expenseProductInfoId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.FAILED);

            try
            {
                if (!Helpers.IsValidGuid(LoggedInUserId))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                var dbresponse = await _expenseProductService.DeleteExpenseProductInfo(expenseProductInfoId, LoggedInUserId);

                return StatusCode(StatusCodes.Status200OK, Helpers.IsResponseValid(dbresponse) ? dbresponse : response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExpenseProductController.DeleteTempExpenseInfo({@expenseProductInfoId}, {@loggedInUserId})", expenseProductInfoId, LoggedInUserId);

                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
