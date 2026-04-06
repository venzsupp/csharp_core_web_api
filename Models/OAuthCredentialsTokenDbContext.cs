using Microsoft.EntityFrameworkCore;
using csharp_core_web_api.Abstracts;

namespace csharp_core_web_api.Models;

public class OAuthCredentialsTokenDbContext(DbContextOptions<OAuthCredentialsTokenDbContext> options) : CustomDbContext(options)
{
    public DbSet<OAuthCredentialsToken> OAuthCredentialsToken { get; set; }

}