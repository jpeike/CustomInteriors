using Core;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public class EmailRepository : IEmailRepository
{
    private readonly AppDbContext _dbContext;

    public EmailRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Email?> GetEmailById(int id)
    {
        return await _dbContext.Emails.FindAsync(id);
    }

    public async Task<IEnumerable<Email>> GetAllEmails()
    {
        return await _dbContext.Emails.ToListAsync();
    }

    public async Task<Email> AddEmail(Email email)
    {
        _dbContext.Emails.Add(email);
        await _dbContext.SaveChangesAsync();
        return email;
    }

    public async Task UpdateEmail(Email email)
    {
        _dbContext.Emails.Update(email);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteEmail(int id)
    {
        Email? email = await _dbContext.Emails.FindAsync(id);
        if (email != null)
        {
            _dbContext.Remove(email);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}

