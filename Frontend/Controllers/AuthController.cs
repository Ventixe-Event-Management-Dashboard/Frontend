using Authentication.Services;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using System.Security.Claims;

namespace Frontend.Controllers
{
    [AllowAnonymous]
    public class AuthController(IAuthService authService, SignUpService signUpService) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly SignUpService _signUpService = signUpService;

        #region Signup
        [HttpGet("signup")]
        public IActionResult RegisterEmail()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterEmail(RegisterEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _authService.UserExistsAsync(model.Email))
            {
                ViewBag.ErrorMessage = "An account with that email already exists.";
                return View(model);
            }

            bool success = await _signUpService.SendVerificationCodeAsync(model.Email);
            if (!success)
                return View(model);

            return RedirectToAction("VerifyCode", new { email = model.Email });
        }

        [HttpGet("verifycode")]
        public IActionResult VerifyCode(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction("RegisterEmail");

            return View(new VerifyCodeViewModel { Email = email });

        }

        [HttpPost("verifycode")]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool isValid = await _signUpService.VerifyCodeAsync(model.Email, model.Code);
            if (!isValid)
                return View(model);

            return RedirectToAction("CreateAccount", new { email = model.Email });
        }

        [HttpGet("createaccount")]
        public IActionResult CreateAccount(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction("RegisterEmail");

            return View(new SignUpViewModel { Email = email });
        }

        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var registerDto = new RegisterDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
            };

            var result = await _authService.SignUpAsync(registerDto);

            if (!result.Succeeded)
                return View(model);

            var token = await _authService.SignInAsync(new SignInDto
            {
                Email = model.Email,
                Password = model.Password
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

            return RedirectToAction("Signin", "Auth");
        }
    }
}
