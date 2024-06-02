using OrderService.Application.Contract;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Domain.Aggregates.OrderAggregate.DTO.Request;
using OrderService.Domain.DTOs;
using OrderService.Domain.RepositoryContracts;
using OrderService.Infrastructure.HttpAdapter.Contract;
using OrderService.SharedKernel;

namespace OrderService.Application.Implementation;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductPlatform _productPlatform;

    public OrderService(IOrderRepository orderRepository, IProductPlatform productPlatform)
    {
        _orderRepository = orderRepository;
        _productPlatform = productPlatform;
    }

    public async Task<ResponseWrapper<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();

        if (orders == null || !orders.Any())
        {
            return ResponseWrapper<IEnumerable<Order>>.Error("There are no orders");
        }

        return ResponseWrapper<IEnumerable<Order>>.Success(orders);
    }

    public async Task<ResponseWrapper<Order>> GetOrderById(string id)
    {
        var isParsed = Guid.TryParse(id, out Guid parsedId);

        if (!isParsed)
        {
            return ResponseWrapper<Order>.Error("Invalid Order Id");
        }

        var order = await _orderRepository.GetOrderAsync(parsedId);

        if (order == null)
        {
            return ResponseWrapper<Order>.Error("Invalid Order Id");
        }

        return ResponseWrapper<Order>.Success(order);
    }

    public async Task<ResponseWrapper<IEnumerable<ProductDTO>>> GetAllProducts()
    {
        var productsResponse = await _productPlatform.GetAllProduct();

        if (!productsResponse.IsSuccessful)
        {
            return ResponseWrapper<IEnumerable<ProductDTO>>.Error(productsResponse.Message);
        }

        return ResponseWrapper<IEnumerable<ProductDTO>>.Success(productsResponse.ResponseObject);
    }

    public async Task<ResponseWrapper<string>> PostOrder(PostOrderRequest request)
    {
        var productResponse = await _productPlatform.GetProduct(request.ProductId);

        if (!productResponse.IsSuccessful)
        {
            return ResponseWrapper<string>.Error(productResponse.Message);
        }

        var newOrder = Order.CreateNewOrder(request, productResponse.ResponseObject.Price);
        await _orderRepository.AddOrderAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        return ResponseWrapper<string>.Success("Order placed successfully");
    }
}
