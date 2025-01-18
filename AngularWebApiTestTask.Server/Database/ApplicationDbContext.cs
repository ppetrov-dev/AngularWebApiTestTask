using AngularWebApiTestTask.Server.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Province)
            .WithMany() 
            .HasForeignKey(u => u.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Province>()
            .HasOne(p => p.Country)
            .WithMany(c => c.Provinces)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}