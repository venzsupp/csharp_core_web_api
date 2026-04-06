using Microsoft.EntityFrameworkCore;

namespace csharp_core_web_api.Abstracts;

public class CustomDbContext : DbContext
{
     public CustomDbContext(DbContextOptions options)
        : base(options)
    {
    }
}

// error CS0101: The namespace 'csharp_core_web_api.Abstracts' 
// already contains a definition for 'CustomDbContext' [/var/www/html/csharp_core_web_api.csproj]