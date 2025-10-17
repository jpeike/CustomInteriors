using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<UserModel?> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpPost("")]
    public async Task<UserModel> CreateUser([FromBody] User userModel)
    {
        return await _userService.CreateUser(userModel);
    }

    [HttpPut("")]
    public async Task UpdateUser([FromBody] UserModel userModel)
    {
        await _userService.UpdateUser(userModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }
}

