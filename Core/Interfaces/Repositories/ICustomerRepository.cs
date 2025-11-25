namespace Core;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerById(int id, bool includeDetails = false);
    Task<IEnumerable<Customer>> GetAllCustomers(bool includeDetails = false);
    Task<Customer> AddCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task<bool> DeleteCustomer(int id);
}
