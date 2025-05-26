using Authentication.Entities;

namespace Authentication.Services
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}