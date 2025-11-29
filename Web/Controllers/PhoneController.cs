using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/phones")]
public class PhoneController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public PhoneController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpGet("", Name = "GetAllPhones")]
    public async Task<ActionResult<IEnumerable<PhoneModel>>> GetAllPhones()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _phoneService.GetAllPhones());
    }

    [HttpGet("{id:int}", Name = "GetPhoneById")]
    public async Task<ActionResult<PhoneModel?>> GetPhoneById(int id)
    {
        if (id <= 0) return BadRequest();
        return await _phoneService.GetPhoneById(id);
    }

    [HttpPost("", Name = "CreatePhone")]
    public async Task<ActionResult<PhoneModel>> CreatePhone([FromBody] PhoneModel phoneModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return await _phoneService.CreatePhone(phoneModel);
    }

    [HttpPut("", Name = "UpdatePhone")]
    public async Task<ActionResult> UpdatePhone([FromBody] PhoneModel phoneModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _phoneService.UpdatePhone(phoneModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeletePhone")]
    public async Task<ActionResult<bool>> DeletePhone(int id)
    {
        if (id <= 0) return BadRequest();
        return await _phoneService.DeletePhone(id);
    }
}