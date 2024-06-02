using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Domain.RepositoryContracts;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repository;

public class OrderRepository : RepositoryAbstract, IOrderRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _applicationDbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _applicationDbContext.Orders.AsNoTracking().ToListAsync();
    }

    public async Task<Order> GetOrderAsync(Guid id)
    {
        return await _applicationDbContext.Orders.FindAsync(id);
    }

    public async Task<Order> AddOrderAsync(Order newOrder)
    {
        var newRecord = await _applicationDbContext.Orders.AddAsync(newOrder);
        return newRecord.Entity;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _applicationDbContext.SaveChangesAsync(new CancellationToken());
    }
}
