using Application.Order;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/orders")]
public class OrderController
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<OrderView>> GetAllOrders()
    {
        return await _service.GetOrdersAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<OrderView> Get(int id)
    {
        return await _service.GetOrderAsync(id);
    }

    [HttpPost]
    public async Task<OrderView> AddOrder([FromBody] OrderModel orderModel)
    {
        return await _service.SaveOrderAsync(orderModel);
    }

    [HttpDelete("{id:int}")]
    public async Task DeleteOrder(int id)
    {
        await _service.DeleteOrderAsync(id);
    }
}
