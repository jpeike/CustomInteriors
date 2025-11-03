using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class QuoteRequestRepository : IQuoteRequestRepository
{
    private readonly AppDbContext _dbContext;

    public QuoteRequestRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<QuoteRequest?> GetQuoteRequestById(int id)
    {
        return await _dbContext.QuoteRequests.FindAsync(id);
    }

    public async Task<IEnumerable<QuoteRequest>> GetAllQuoteRequests()
    {
        return await _dbContext.QuoteRequests.ToListAsync();
    }

    public async Task<QuoteRequest> AddQuoteRequest(QuoteRequest quoteRequest)
    {
        _dbContext.QuoteRequests.Add(quoteRequest);
        await _dbContext.SaveChangesAsync();
        return quoteRequest;
    }

    public async Task UpdateQuoteRequest(QuoteRequest quoteRequest)
    {
        _dbContext.QuoteRequests.Update(quoteRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteQuoteRequest(int id)
    {
        QuoteRequest? quoteRequest = await _dbContext.QuoteRequests.FindAsync(id);
        if (quoteRequest != null)
        {
            _dbContext.Remove(quoteRequest);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}