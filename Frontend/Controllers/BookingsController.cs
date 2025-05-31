using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "booking";

            return View();
        }
    }
}
