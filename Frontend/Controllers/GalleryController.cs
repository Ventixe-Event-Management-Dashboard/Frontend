using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "gallery";

            return View();
        }
    }
}
