using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "feedback";

            return View();
        }
    }
}
