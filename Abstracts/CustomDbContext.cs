using Microsoft.EntityFrameworkCore;

namespace csharp_core_web_api.Abstracts;

public class CustomDbContext : DbContext
{
     public CustomDbContext(DbContextOptions options)
        : base(options)
    {
    }
}