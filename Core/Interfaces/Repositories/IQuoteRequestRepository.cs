namespace Core;

public interface IQuoteRequestRepository
{
    Task<QuoteRequest?> GetQuoteRequestById(int id);
    Task<IEnumerable<QuoteRequest>> GetAllQuoteRequests();
    Task<QuoteRequest> AddQuoteRequest(QuoteRequest quoteRequest);
    Task UpdateQuoteRequest(QuoteRequest quoteRequest);
    Task<bool> DeleteQuoteRequest(int id);
}
