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
    public async Task<IEnumerable<PhoneModel>> GetAllPhones()
    {
        return await _phoneService.GetAllPhones();
    }

    [HttpGet("{id:int}", Name = "GetPhoneById")]
    public async Task<PhoneModel?> GetPhoneById(int id)
    {
        return await _phoneService.GetPhoneById(id);
    }

    [HttpPost("", Name = "CreatePhone")]
    public async Task<PhoneModel> CreatePhone([FromBody] PhoneModel phoneModel)
    {
        return await _phoneService.CreatePhone(phoneModel);
    }

    [HttpPut("", Name = "UpdatePhone")]
    public async Task UpdatePhone([FromBody] PhoneModel phoneModel)
    {
        await _phoneService.UpdatePhone(phoneModel);
    }

    [HttpDelete("{id:int}", Name = "DeletePhone")]
    public async Task<bool> DeletePhone(int id)
    {
        return await _phoneService.DeletePhone(id);
    }
}