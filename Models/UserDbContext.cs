using Microsoft.EntityFrameworkCore;
using csharp_core_web_api.Abstracts;

namespace csharp_core_web_api.Models;

public class UserDbContext : CustomDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }
    public DbSet<Users> Users { get; set; }

}