using OrderService.Domain.Aggregates.OrderAggregate.DTO.Request;
using OrderService.Domain.GenericModels;

namespace OrderService.Domain.Aggregates.OrderAggregate;

public class Order : Entity<Guid>
{
    public Order() : base(Guid.NewGuid())
    {
          
    }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public static Order CreateNewOrder(PostOrderRequest newOrderRequest, decimal price)
    {
        return new Order
        {
            ProductId = Guid.Parse(newOrderRequest.ProductId),
            Quantity = newOrderRequest.Quantity,
            TotalPrice = newOrderRequest.Quantity * price
        };
    }
}
