using Core;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _dbContext.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public async Task<Customer> AddCustomer(Customer user)
    {
        _dbContext.Customers.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateCustomer(Customer user)
    {
        _dbContext.Customers.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        Customer? user = await _dbContext.Customers.FindAsync(id);
        if (user != null)
        {
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}

