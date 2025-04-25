using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "";

        return View();
    }
}
