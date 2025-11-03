using Core;

namespace Infrastructure;

public class QuoteRequestService : IQuoteRequestService
{
    private readonly IQuoteRequestRepository _quoteRequestRepository;

    public QuoteRequestService(IQuoteRequestRepository quoteRequestRepository)
    {
        _quoteRequestRepository = quoteRequestRepository;
    }

    public async Task<QuoteRequestModel> CreateQuoteRequest(QuoteRequestModel quoteRequest)
    {
        QuoteRequest toReturn = await _quoteRequestRepository.AddQuoteRequest(quoteRequest.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteQuoteRequest(int id)
    {
        return await _quoteRequestRepository.DeleteQuoteRequest(id);
    }

    public async Task<IEnumerable<QuoteRequestModel>> GetAllQuoteRequests()
    {
        var allQuoteRequests = await _quoteRequestRepository.GetAllQuoteRequests();
        return allQuoteRequests.ToModels();
    }

    public async Task<QuoteRequestModel?> GetQuoteRequestById(int id)
    {
        QuoteRequest? quoteRequest = await _quoteRequestRepository.GetQuoteRequestById(id);
        return quoteRequest?.ToModel();
    }

    public async Task UpdateQuoteRequest(QuoteRequestModel quoteRequest)
    {
        await _quoteRequestRepository.UpdateQuoteRequest(quoteRequest.ToEntity());
    }
}

