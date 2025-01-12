using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Otp> Otps { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set up the relationship between User and Country (One-to-Many)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Country)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
