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

    [HttpGet("")]
    public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
    {
        return await _customerService.GetAllCustomers();
    }

    [HttpGet("{id:int}")]
    public async Task<CustomerModel?> GetCustomerById(int id)
    {
        return await _customerService.GetCustomerById(id);
    }

    // todo look at this again, not really restful but I dont want to change too much with this one yet
    [HttpGet("with-addresses/{id:int}")]
    public async Task<CustomerWithFKsModel?> GetCustomerWithAddress(int id)
    {
        return await _customerService.GetCustomerWithAddress(id);
    }


    [HttpPost("")]
    public async Task<CustomerModel> CreateCustomer([FromBody] Customer customerModel)
    {
        CustomerModel customer = new()
        {
            FirstName = customerModel.FirstName,
            LastName = customerModel.LastName,
            CustomerType = customerModel.CustomerType,
            PrefferedContactMethod = customerModel.PrefferedContactMethod,
            CompanyName = customerModel.CompanyName,
            Status = customerModel.Status,
            CustomerNotes = customerModel.CustomerNotes
        };

        return await _customerService.CreateCustomer(customer);
    }

    [HttpPut("")]
    public async Task UpdateCustomer([FromBody] CustomerModel customerModel)
    {
        await _customerService.UpdateCustomer(customerModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerService.DeleteCustomer(id);
    }
}