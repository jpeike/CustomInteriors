using System.Collections.Generic;
using System.Threading.Tasks;
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
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<ActionResult<UserModel?>> GetUserById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _userService.GetUserById(id));
    }

    [HttpPost("", Name = "CreateUser")]
    public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel userModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _userService.CreateUser(userModel));
    }

    [HttpPut("", Name = "UpdateUser")]
    public async Task<ActionResult> UpdateUser([FromBody] UserModel userModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _userService.UpdateUser(userModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _userService.DeleteUser(id));
    }
}

