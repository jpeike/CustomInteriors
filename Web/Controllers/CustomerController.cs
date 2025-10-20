using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet()]
    public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
    {
        return await _customerService.GetAllCustomers();
    }

    [HttpGet("{id:int}")]

    public async Task<CustomerModel?> GetCustomerById(int id)
    {
        return await _customerService.GetCustomerById(id);
    }

    [HttpGet("withChildren/{id:int}")]

    public async Task<CustomerWithFKsModel?> GetCustomerWithAddress(int id)
    {
        return await _customerService.GetCustomerWithAddress(id);
    }


    [HttpPost("CreateCustomer")]
    public async Task<CustomerModel> CreateCustomer([FromBody] CustomerModel customerModel)
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

    [HttpPut("UpdateCustomer")]
    public async Task UpdateCustomer([FromBody] CustomerModel customerModel)
    {
        await _customerService.UpdateCustomer(customerModel);
    }

    [HttpDelete("DeleteCustomer/{id:int}")]
    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerService.DeleteCustomer(id);
    }

}

