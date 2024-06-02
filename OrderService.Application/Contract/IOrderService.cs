using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Domain.Aggregates.OrderAggregate.DTO.Request;
using OrderService.Domain.DTOs;
using OrderService.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Contract
{
    public interface IOrderService
    {
        Task<ResponseWrapper<IEnumerable<Order>>> GetAllOrders();
        Task<ResponseWrapper<IEnumerable<ProductDTO>>> GetAllProducts();
        Task<ResponseWrapper<Order>> GetOrderById(string id);
        Task<ResponseWrapper<string>> PostOrder(PostOrderRequest request);
    }
}
