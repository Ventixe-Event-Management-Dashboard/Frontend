using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AuthController : Controller
    {
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(SignUpForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            // Här ska _authService.SignUp(form) callas och account ska skapas

            //redirect till signin så länge, om alla fält är ifyllda
            return RedirectToAction("SignIn");
        }

        [Route("signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult SignIn(SignInForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            //här ska _authService.SignIn(form) callas kolla validera inloggning

            //redirect till dashboard så länge
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
