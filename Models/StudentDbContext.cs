using Microsoft.EntityFrameworkCore;
using csharp_core_web_api.Abstracts;

namespace csharp_core_web_api.Models;

public class StudentDbContext : CustomDbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }
    public DbSet<Student> Students { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=host.docker.internal;User ID=sa;Password=Platinum01;Database=webapi_db;Encrypt=True;TrustServerCertificate=True;");
    // }
}