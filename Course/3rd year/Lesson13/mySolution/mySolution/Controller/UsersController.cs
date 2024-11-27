using Microsoft.AspNetCore.Mvc;
using mySolution.Manager.Interface;
using mySolution.Models;

namespace mySolution.Controller;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserManager userManager) : ControllerBase
{
    private readonly IUserManager _userManager = userManager;
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _userManager.GetAllUserAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userManager.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        await _userManager.AddUserAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        await _userManager.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userManager.DeleteUserAsync(id);
        return NoContent();
    }
}