using Core;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerModel> CreateCustomer(CustomerModel customer)
    {
        Customer toReturn = await _customerRepository.AddCustomer(customer.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerRepository.DeleteCustomer(id);
    }

    public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
    {
        var allCustomers = await _customerRepository.GetAllCustomers();
        return allCustomers.ToModels();
    }

    public async Task<CustomerModel?> GetCustomerById(int id)
    {
        Customer? customer = await _customerRepository.GetCustomerById(id);
        if (customer == null)
        {
            return null;
        }
        return customer.ToModel();
    }

    public async Task<CustomerWithFKsModel?> GetCustomerWithAddress(int id)
    {
        Customer? customer = await _customerRepository.GetCustomerWithAddress(id);
        if (customer == null)
        {
            return null;
        }
        return customer.FKsToModel();
    }

    public async Task UpdateCustomer(CustomerModel customerModel)
    {
        await _customerRepository.UpdateCustomer(customerModel.ToEntity());
    }
}

