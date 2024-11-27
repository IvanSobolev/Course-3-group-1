using Microsoft.AspNetCore.Mvc;
using mySolution.Manager.Interface;
using mySolution.Models;

namespace mySolution.Controller;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderManager orderManager) : ControllerBase
{
    private readonly IOrderManager _orderManager = orderManager;
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderManager.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderManager.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        await _orderManager.AddOrderAsync(order);

        return Ok(order.Id);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order order)
    {
        if (id != order.Id)
            return BadRequest();

        await _orderManager.UpdateOrderAsync(order);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderManager.DeleteOrderAsync(id);
        return Ok();
    }
}