using Authentication.Services;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using System.Security.Claims;

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
        public async Task<IActionResult> SignUp(SignUpViewModel form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var registerDto = new RegisterDto
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Password = form.Password,
            };

            var result = await _authService.SignUpAsync(registerDto);

            if (!result.Succeeded)
                return View(form);

            var token = await _authService.SignInAsync(new SignInDto
            {
                Email = form.Email,
                Password = form.Password
            });

            if (token != null)
            {
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
            }

            return RedirectToAction("Index", "Dashboard");
        }
        #endregion

        #region Signin
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInViewModel signInForm, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(signInForm);

            var signInDto = new SignInDto
            {
                Email = signInForm.Email,
                Password = signInForm.Password,
            };

            var token = await _authService.SignInAsync(signInDto);

            if (token == null)
            {
                ViewBag.ErrorMessage = "Invalid credentials";
                return View(signInForm);
            }

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            if (string.IsNullOrWhiteSpace(returnUrl) || returnUrl == "/signin")
                return RedirectToAction("Index", "Dashboard");

            return LocalRedirect(ViewBag.ReturnUrl);
        }
        #endregion

        #region SignOut
        [HttpGet("signout")]
        public new async Task<IActionResult> SignOut()
        {
            await _authService.SignOutAsync();

            Response.Cookies.Delete("jwt");

            return RedirectToAction("SignIn", "Auth");
        }


        #endregion

        [HttpPost("delete")]
        public async Task<IActionResult> Delete()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(user))
                return Unauthorized();

            var result = await _authService.DeleteByIdAsync(user);

            if (!result.Succeeded)
                return BadRequest("Failed to delete account.");

            await _authService.SignOutAsync();

            return RedirectToAction("Signup", "Auth");
        }
    }
}
