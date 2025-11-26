using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("", Name = "GetAllCustomers")]
    public async Task<IEnumerable<CustomerModel>> GetAllCustomers([FromQuery] bool includeDetails = false)
    {
        return await _customerService.GetAllCustomers(includeDetails);
    }

    [HttpGet("{id:int}", Name = "GetCustomerById")]
    public async Task<CustomerModel?> GetCustomerById(int id, [FromQuery] bool includeDetails = false)
    {
        return await _customerService.GetCustomerById(id, includeDetails);
    }

    [HttpPost("", Name = "CreateCustomer")]
    public async Task<CustomerModel> CreateCustomer([FromBody] CustomerModel customerModel)
    {
        return await _customerService.CreateCustomer(customerModel);
    }

    [HttpPut("", Name = "UpdateCustomer")]
    public async Task UpdateCustomer([FromBody] CustomerModel customerModel)
    {
        await _customerService.UpdateCustomer(customerModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteCustomer")]
    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerService.DeleteCustomer(id);
    }
}