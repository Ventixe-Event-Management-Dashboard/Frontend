using Authentication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
    }
}
