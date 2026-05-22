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

    public DbSet<AI> AIs { get; set; }

    public DbSet<AIFile> AIFiles { get; set; }

    public DbSet<AIFileChat> AIFileChats { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<NureFile> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        modelBuilder.Entity<AI>().HasKey(_ => _.Id);
        modelBuilder.Entity<AIFile>().HasKey(_ => _.Id);
        modelBuilder.Entity<AIFileChat>().HasKey(_ => _.Id);
        modelBuilder.Entity<Chat>().HasKey(_ => _.Id);
        modelBuilder.Entity<Message>().HasKey(_ => _.Id);
        modelBuilder.Entity<NureFile>().HasKey(_ => _.Id);

        base.OnModelCreating(modelBuilder);
    }
}
