using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services
{
    public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IAuthService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public async Task<IdentityResult> SignUpAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> SignInAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
