using Authentication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Contracts;
using System.Net.Http.Json;

namespace Authentication.Services
{
    public class AuthService(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService, IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("UserProfileService");
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<IdentityResult> SignUpAsync(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return result;

            var userDto = new CreateUserProfileDto
            {
                Id = user.Id,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };

            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt"];
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.PostAsJsonAsync("users", userDto);

                if (!response.IsSuccessStatusCode)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "ProfileCreationFailed",
                        Description = "User account was created, but the profile could not be created."
                    });
                }
            }
            catch
            {
            }

            return result;
        }

        public async Task<string?> SignInAsync(SignInDto signInDto)
        {
            var user = await _userManager.FindByEmailAsync(signInDto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password, false, false);

            if (!result.Succeeded)
                return null;

            return _jwtService.GenerateToken(user);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> DeleteByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return result;

            var response = await _httpClient.DeleteAsync($"users/{user.Id}");

            if (!response.IsSuccessStatusCode)
                return IdentityResult.Failed(new IdentityError { Description = "Could not delete user profile data" });

            return result;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
