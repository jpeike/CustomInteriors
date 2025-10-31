namespace Core;

public interface IQuoteRequestService
{
    Task<QuoteRequestModel?> GetQuoteRequestById(int id);
    Task<IEnumerable<QuoteRequestModel>> GetAllQuoteRequests();
    Task<QuoteRequestModel> AddQuoteRequest(QuoteRequestModel quoteRequest);
    Task UpdateQuoteRequest(QuoteRequestModel quoteRequest);
    Task<bool> DeleteQuoteRequest(int id);
}
