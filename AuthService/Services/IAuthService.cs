using Microsoft.AspNetCore.Identity;
using Shared.Contracts;

namespace Authentication.Services
{
    public interface IAuthService
    {
        Task<string?> SignInAsync(SignInDto signInDto);
        Task SignOutAsync();
        Task<IdentityResult> SignUpAsync(RegisterDto registerDto);
        Task<IdentityResult> DeleteByIdAsync(string id);
    }
}