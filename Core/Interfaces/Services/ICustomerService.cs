namespace Core.Interfaces.Services;


public interface ICustomerService
{
    Task<CustomerModel?> GetCustomerById(int id);
    Task<IEnumerable<CustomerModel>> GetAllCustomers();
    Task<CustomerModel> CreateCustomer(CustomerModel customer);
    Task UpdateCustomer(CustomerModel customerModel);
    Task<bool> DeleteCustomer(int id);

    Task<CustomerWithFKsModel?> GetCustomerWithAddress(int customerId);
}

