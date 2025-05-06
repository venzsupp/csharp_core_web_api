using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

public class StudentDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=host.docker.internal;User ID=sa;Password=Platinum01;Database=webapi_db;Encrypt=True;TrustServerCertificate=True;");
    }
}
