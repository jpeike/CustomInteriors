namespace Core;


public interface ICustomerService
{
    Task<CustomerModel?> GetCustomerById(int id, bool includeDetails = false);
    Task<IEnumerable<CustomerModel>> GetAllCustomers(bool includeDetails = false);
    Task<CustomerModel> CreateCustomer(CustomerCreateModel customer);
    Task UpdateCustomer(CustomerUpdateModel customerModel);
    Task<bool> DeleteCustomer(int id);
}

