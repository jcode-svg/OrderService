using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Domain.Aggregates.ProfileAggregate;
using OrderService.Infrastructure.Data.Configuration;

namespace OrderService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
    }
}
