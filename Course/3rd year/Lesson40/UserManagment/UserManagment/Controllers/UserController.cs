using Microsoft.AspNetCore.Mvc;
using UserManagment.Models;
using UserManagment.Services.Interface;

namespace UserManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUserAsync(string username, string email)
    {
        await userService.AddUserAsync(username, email);
        return Ok($"User {username} created.");
    }

    [HttpDelete("RemoveUser")]
    public async Task<IActionResult> RemoveUserAsync(int userId)
    {
        await _userService.DeleteUserAsync(userId);
        return Content($"User with ID {userId} removed.");
    }

    [HttpGet("ShowUser")]
    public async Task<IActionResult> ShowUser(int userId)
    {
        var user = await _userService.GetUserAsync(userId);
        if (user != null)
        {
            return Content($"User: {user.Username}, Email: {user.Email}");
        }
        else
        {
            return Content("User not found.");
        }
    }

    [HttpGet("ListUsers")]
    public async Task<IActionResult> ListUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        var userList = string.Join("<br/>", users.Select(u => $"User: {u.Username}, Email: {u.Email}"));
        return Content(userList);
    }
}
