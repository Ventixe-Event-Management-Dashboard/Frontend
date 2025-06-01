using Authentication.Data;
using Authentication.Entities;
using Authentication.Services;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;

public class UserContextService(
    ApplicationDbContext dbContext,
    IHttpContextAccessor contextAccessor,
    IHttpClientFactory httpClientFactory,
IJwtService jwtService)
{
    readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<UserViewModel?> GetCurrentUserAsync()
    {
        var context = _contextAccessor.HttpContext;
        if (context == null || context.User.Identity?.IsAuthenticated != true)
            return null;

        string? userId = context?.User.FindFirstValue(ClaimTypes.NameIdentifier);


        //var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //if (string.IsNullOrWhiteSpace(userId))
        //    return null;

        //var token = await EnsureJwtAsync(context, userId);
        //if (string.IsNullOrWhiteSpace(token))
        //    return null;

        //var client = _httpClientFactory.CreateClient("UserProfileService");
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //var response = await client.GetAsync($"users/{userId}");

       var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

        return new UserViewModel
        {
            FirstName = user.UserName,
            
        };
    }

    public async Task<bool> UpdateCurrentUserAsync(UserViewModel model)
    {
        var context = _contextAccessor.HttpContext;
        if (context == null || context.User.Identity?.IsAuthenticated != true)
            return false;

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
            return false;

        var token = await EnsureJwtAsync(context, userId);
        if (string.IsNullOrWhiteSpace(token))
            return false;

        var client = _httpClientFactory.CreateClient("UserProfileService");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync($"users/{userId}", model);
        return response.IsSuccessStatusCode;
    }

    private async Task<string?> EnsureJwtAsync(HttpContext context, string userId)
    {
        var token = _jwtService.GetToken(context);

        if (!string.IsNullOrWhiteSpace(token))
            return token;

        if (!context.User.Identity?.IsAuthenticated ?? true)
            return null;

        var userManager = context.RequestServices.GetRequiredService<UserManager<AppUser>>();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return null;

        token = _jwtService.GenerateToken(user);

        context.Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(60)
        });

        return token;
    }
}
