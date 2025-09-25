namespace Core;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerById(int id);
    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<Customer> AddCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task<bool> DeleteCustomer(int id);
}
