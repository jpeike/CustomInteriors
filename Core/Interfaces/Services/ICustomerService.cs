namespace Core;


public interface ICustomerService
{
    Task<CustomerModel?> GetCustomerById(int id, bool includeDetails = false);
    Task<IEnumerable<CustomerModel>> GetAllCustomers(bool includeDetails = false);
    Task<CustomerModel> CreateCustomer(CustomerModel customer);
    Task UpdateCustomer(CustomerModel customerModel);
    Task<bool> DeleteCustomer(int id);
}

