using Authentication.Entities;
using Microsoft.AspNetCore.Http;

namespace Authentication.Services
{
    public interface IJwtService
    {
        string? GetToken(HttpContext context);
        string GenerateToken(AppUser user);
    }
}