using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetAllCustomers([FromQuery] bool includeDetails = false)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _customerService.GetAllCustomers(includeDetails));
    }

    [HttpGet("{id:int}", Name = "GetCustomerById")]
    public async Task<ActionResult<CustomerModel?>> GetCustomerById(int id, [FromQuery] bool includeDetails = false)
    {
        if (id <= 0) return BadRequest();
        return await _customerService.GetCustomerById(id);
    }

    [HttpPost("", Name = "CreateCustomer")]
    public async Task<ActionResult<CustomerModel>> CreateCustomer([FromBody] CustomerModel customerModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return await _customerService.CreateCustomer(customerModel);
    }

    [HttpPut("", Name = "UpdateCustomer")]
    public async Task<ActionResult> UpdateCustomer([FromBody] CustomerModel customerModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _customerService.UpdateCustomer(customerModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteCustomer")]
    public async Task<ActionResult<bool>> DeleteCustomer(int id)
    {
        if (id <= 0) return BadRequest();
        return await _customerService.DeleteCustomer(id);
    }
}