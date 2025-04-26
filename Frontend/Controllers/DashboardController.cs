using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "dashboard";

            return View();
        }
    }
}
