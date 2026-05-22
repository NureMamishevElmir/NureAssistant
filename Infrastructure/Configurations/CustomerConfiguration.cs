using DomainEntity.CustomerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);

        // Email використовується як логін, тому має бути унікальним.
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.Email).HasMaxLength(256);
        builder.Property(c => c.Name).HasMaxLength(256);
        builder.Property(c => c.Phone).HasMaxLength(32);
    }
}
