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

    public async Task<Customer?> GetCustomerById(int id, bool includeDetails = false)
    {
        if (includeDetails)
        {
            return await _dbContext.Customers
                      .Include(c => c.Addresses)
                      .Include(c => c.Emails)
                      .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        return await _dbContext.Customers.Include(c => c.Emails).FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers(bool includeDetails = false)
    {
        if (includeDetails)
        {
            return await _dbContext.Customers
                       .Include(c => c.Addresses)
                       .Include(c => c.Emails)
                       .Include(c => c.Phones)
                       .ToListAsync();
        }

        return await _dbContext.Customers.ToListAsync();
    }

    public async Task<Customer> AddCustomer(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomer(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        Customer? customer = await _dbContext.Customers.FindAsync(id);
        if (customer != null)
        {
            _dbContext.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

}

