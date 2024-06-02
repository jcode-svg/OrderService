using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.Aggregates.OrderAggregate.DTO.Request;

public class PostOrderRequest
{
    public string ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }
}
