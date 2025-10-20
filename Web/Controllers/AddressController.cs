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

    [HttpGet("")]
    public async Task<IEnumerable<AddressModel>> GetAllAddresses()
    {
        return await _addressService.GetAllAddresses();
    }

    [HttpGet("{customerId:int}")]
    public async Task<IEnumerable<AddressModel>> GetAddressesByCustomerId(int customerId)
    {
        return await _addressService.GetAddressesByCustomerId(customerId);
    }

    [HttpPost("")]
    public async Task<AddressModel> CreateAddress([FromBody] AddressModel addressModel)
    {
        AddressModel address = new()
        {
            CustomerId = addressModel.CustomerId,
            Street = addressModel.Street,
            City = addressModel.City,
            State = addressModel.State,
            PostalCode = addressModel.PostalCode,
            Country = addressModel.Country,
            AddressType = addressModel.AddressType
        };

        return await _addressService.CreateAddress(address);
    }

    [HttpPut("")]
    public async Task UpdateAddress([FromBody] AddressModel addressModel)
    {
        await _addressService.UpdateAddress(addressModel);
    }

    [HttpDelete("{addressId:int}")]
    public async Task<bool> DeleteAddress(int addressId)
    {
        return await _addressService.DeleteAddress(addressId);
    }
}