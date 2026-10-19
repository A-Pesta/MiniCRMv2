using Microsoft.EntityFrameworkCore;
using MiniCRMv2.Entities;

namespace MiniCRMv2.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString =
            Environment.GetEnvironmentVariable("MINICRM_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Brak zmiennej MINICRM_CONNECTION_STRING.");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();
        modelBuilder.Entity<Customer>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<Customer>()
            .Property(c => c.City)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<Order>()
            .Property(o => o.ProductName)
            .IsRequired()
            .HasMaxLength(150);
    }
}
