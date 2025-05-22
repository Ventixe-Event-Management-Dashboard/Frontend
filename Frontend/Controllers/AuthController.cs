using Authentication.Entities;
using Authentication.Services;
using Frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    [AllowAnonymous]
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        #region Signup
        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var user = new AppUser
            {
                UserName = form.Email,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email
            };

            var result = await _authService.SignUpAsync(user, form.Password);

            if (!result.Succeeded)
                return View(form);

            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region Signin
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInForm form, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(form);

            var result = await _authService.SignInAsync(form.Email, form.Password);

            if (!result.Succeeded)
            {
                ViewBag.ErrorMessage = "Invalid credentials";
                return View(form);
            }

            return LocalRedirect(ViewBag.ReturnUrl);
        }
        #endregion

        #region SignOut
        [HttpGet("signout")]
        public new async Task<IActionResult> SignOut()
        {
            await _authService.SignOutAsync();

            return RedirectToAction("SignIn", "Auth");
        }
        #endregion
    }
}
