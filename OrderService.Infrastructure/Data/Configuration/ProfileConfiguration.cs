using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.ProfileAggregate;

namespace OrderService.Infrastructure.Data.Configuration;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.Property(e => e.Username).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(1000);
    }
}
