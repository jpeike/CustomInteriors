using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PhoneRepository : IPhoneRepository
{
    private readonly AppDbContext _dbContext;

    public PhoneRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Phone?> GetPhoneById(int id)
    {
        return await _dbContext.Phones.FindAsync(id);
    }

    public async Task<IEnumerable<Phone>> GetAllPhones()
    {
        return await _dbContext.Phones.ToListAsync();
    }

    public async Task<Phone> AddPhone(Phone phone)
    {
        _dbContext.Phones.Add(phone);
        await _dbContext.SaveChangesAsync();
        return phone;
    }

    public async Task UpdatePhone(Phone phone)
    {
        _dbContext.Phones.Update(phone);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeletePhone(int id)
    {
        Phone? phone = await _dbContext.Phones.FindAsync(id);
        if (phone != null)
        {
            _dbContext.Remove(phone);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}