using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "bookings";

            return View();
        }
    }
}
