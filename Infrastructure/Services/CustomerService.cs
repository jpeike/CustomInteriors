using Core;

namespace Infrastructure;

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

    public async Task<IEnumerable<CustomerModel>> GetAllCustomers(bool includeDetails = false)
    {
        var allCustomers = await _customerRepository.GetAllCustomers(includeDetails);
        return allCustomers.ToModels(includeDetails);
    }

    public async Task<CustomerModel?> GetCustomerById(int id, bool includeDetails = false)
    {
        Customer? customer = await _customerRepository.GetCustomerById(id, includeDetails);
        if (customer == null)
        {
            return null;
        }
        return customer.ToModel(includeDetails);
    }

    public async Task UpdateCustomer(CustomerModel customerModel)
    {
        await _customerRepository.UpdateCustomer(customerModel.ToEntity());
    }
}

