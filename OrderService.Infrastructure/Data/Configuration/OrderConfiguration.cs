using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
    }
}
