using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class InvoicesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "invoices";

            return View();
        }
    }
}
