using OrderService.Domain.Aggregates.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.RepositoryContracts
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order newOrder);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
