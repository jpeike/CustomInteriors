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

    [HttpGet("", Name = "GetAllUsers")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<UserModel?> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpPost("", Name = "CreateUser")]
    public async Task<UserModel> CreateUser([FromBody] UserModel userModel)
    {
        return await _userService.CreateUser(userModel);
    }

    [HttpPut("", Name = "UpdateUser")]
    public async Task UpdateUser([FromBody] UserModel userModel)
    {
        await _userService.UpdateUser(userModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public async Task<bool> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }
}

