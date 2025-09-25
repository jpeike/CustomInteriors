using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

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

    [HttpPost("Create")]
<<<<<<< HEAD
    public async Task<CustomerModel> CreateCustomer([FromBody] CustomerModel customerModel)
    {
        var customer = new Customer
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
=======
    public async Task<CustomerModel> CreateCustomer([FromBody] Customer customerModel)
    {
        return await _customerService.CreateCustomer(customerModel);
>>>>>>> develop
    }

    [HttpPut("Update")]
    public async Task UpdateCustomer([FromBody] CustomerModel customerModel)
    {
        await _customerService.UpdateCustomer(customerModel);
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerService.DeleteCustomer(id);
    }

}

