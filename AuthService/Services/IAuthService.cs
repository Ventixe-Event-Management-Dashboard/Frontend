using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services
{
    public interface IAuthService
    {
        Task<SignInResult> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<IdentityResult> SignUpAsync(AppUser user, string password);
    }
}