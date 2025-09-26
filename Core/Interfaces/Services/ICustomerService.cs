namespace Core;


public interface ICustomerService
{
    Task<CustomerModel?> GetCustomerById(int id);
    Task<IEnumerable<CustomerModel>> GetAllCustomers();
    Task<CustomerModel> CreateCustomer(Customer customer);
    Task UpdateCustomer(CustomerModel customerModel);
    Task<bool> DeleteCustomer(int id);
}

