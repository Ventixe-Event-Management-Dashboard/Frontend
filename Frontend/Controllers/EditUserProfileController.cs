using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class EditUserProfileController(UserContextService userContextService) : Controller
    {
        private readonly UserContextService _userContextService = userContextService;

        [HttpGet("updateuser")]
        public async Task<IActionResult> Index()
        {
            var userProfile = await _userContextService.GetCurrentUserAsync();

            if (userProfile == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(userProfile);
        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> Index(UserViewModel model)
        {
            var success = await _userContextService.UpdateCurrentUserAsync(model);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Could not update user profile.");
                return View(model);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
