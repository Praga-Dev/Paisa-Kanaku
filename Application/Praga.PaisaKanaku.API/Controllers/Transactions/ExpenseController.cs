using Microsoft.AspNetCore.Mvc;

namespace Praga.PaisaKanaku.API.Controllers.Transactions
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
