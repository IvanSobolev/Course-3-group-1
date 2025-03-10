using Microsoft.AspNetCore.Mvc;
using Solution.Model.DTO;
using Solution.Model.Entity;
using Solution.Model.Structures;
using Solution.Service.Interface;

namespace Solution.Controller;

[ApiController]
[Route("users")]
public class UserController(IUserManager userManager) : ControllerBase
{
    private readonly IUserManager _userManager = userManager;
    
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetUserAsync(string id)
    {
        User? user = await _userManager.GetUserAsync(id);
        if (user == null)
        {
            return NotFound("user not found");
        }

        return Ok(user);
    }
    
    [HttpPost("get/")]
    public async Task<IActionResult> GetUsersAsync([FromBody] FilterViewModel fwm)
    {
        return Ok(await _userManager.GetUsersAsync(fwm));
    }
    
    [HttpPost("add/")]
    public async Task<IActionResult> AddUsersAsync([FromBody] NewUserDataDto user)
    {
        var result = await _userManager.AddNewUserAsync(user);
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.ErrorMessage);
    }
    
    [HttpPut("update/")]
    public async Task<IActionResult> AddUsersAsync([FromBody] UpdateUserDataDto user)
    {
        var result = await _userManager.UpdateUserAsync(user);
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.ErrorMessage);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> AddUsersAsync(string id)
    {
        var result = await _userManager.DeleteUserAsync(id);
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.ErrorMessage);
    }
}