using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Contract;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Domain.Aggregates.OrderAggregate.DTO.Request;
using OrderService.Domain.DTOs;
using OrderService.SharedKernel;
using System.Net.Mime;

namespace OrderService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("AllOrders")]
    [ProducesResponseType(typeof(ResponseWrapper<IEnumerable<Order>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Order>>> AllOrders()
    {
        var result = await _orderService.GetAllOrders();

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("Order/{id}")]
    [ProducesResponseType(typeof(ResponseWrapper<Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Order>> Order(string id)
    {
        var result = await _orderService.GetOrderById(id);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("AllProducts")]
    [ProducesResponseType(typeof(ResponseWrapper<IEnumerable<ProductDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> AllProducts()
    {
        var result = await _orderService.GetAllProducts();

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("PostOrder")]
    [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseWrapper<string>>> PostOrder(PostOrderRequest request)
    {
        var result = await _orderService.PostOrder(request);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }
}