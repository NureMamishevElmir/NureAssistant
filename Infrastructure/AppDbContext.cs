using DomainEntity.AIEntities;
using DomainEntity.ChatEntities;
using DomainEntity.CustomerEntities;
using DomainEntity.FileEntities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
