using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// https://www.geeksforgeeks.org/system-design/difference-between-rest-api-and-rpc-api/
//https://restfulapi.net/resource-naming/

namespace Web;

[ApiController]
[Route("api/addresses")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    // this is how you specify the name of the method in the ts client -> Name = "nameIWant"
    [HttpGet("", Name = "GetAllAddresses")]
    public async Task<ActionResult<IEnumerable<AddressModel>>> GetAllAddresses()
    {
        return Ok(await _addressService.GetAllAddresses());
    }

    // todo add lookup by address id
    [HttpGet("{id:int}", Name = "GetAddressesByCustomerId")]
    public async Task<ActionResult<IEnumerable<AddressModel>>> GetAddressesByCustomerId(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _addressService.GetAddressesByCustomerId(id));
    }

    [HttpPost("", Name = "CreateAddress")]
    public async Task<ActionResult<AddressModel>> CreateAddress([FromBody] AddressModel addressModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _addressService.CreateAddress(addressModel));
    }

    [HttpPut("", Name = "UpdateAddress")]
    public async Task<ActionResult> UpdateAddress([FromBody] AddressModel addressModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _addressService.UpdateAddress(addressModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteAddress")]
    public async Task<ActionResult<bool>> DeleteAddress(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _addressService.DeleteAddress(id));
    }
}