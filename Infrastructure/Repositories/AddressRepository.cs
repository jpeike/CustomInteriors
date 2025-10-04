using Core;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _dbContext;

    public AddressRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Address>> GetAddressesByCustomerId(int customerId)
    {
        return await _dbContext.Addresses
                       .Where(a => a.CustomerId == customerId)
                       .ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddresses()
    {
        return await _dbContext.Addresses.ToListAsync();

    }

    public async Task<Address> AddAddress(Address address)
    {
        _dbContext.Addresses.Add(address);
        await _dbContext.SaveChangesAsync();
        return address;
    }

    public async Task UpdateAddress(Address address)
    {
        _dbContext.Addresses.Update(address);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAddress(int addressId)
    {
        Address? address = await _dbContext.Addresses.FindAsync(addressId);
        if (address != null)
        {
            _dbContext.Remove(address);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}

