using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/quoteRequests")]
public class QuoteRequestController : ControllerBase
{
    private readonly IQuoteRequestService _quoteRequestService;

    public QuoteRequestController(IQuoteRequestService quoteRequestService)
    {
        _quoteRequestService = quoteRequestService;
    }

    [HttpGet("", Name = "GetAllQuoteRequests")]
    public async Task<IEnumerable<QuoteRequestModel>> GetAllQuoteRequests()
    {
        return await _quoteRequestService.GetAllQuoteRequests();
    }

    [HttpGet("{id:int}", Name = "GetQuoteRequestById")]
    public async Task<QuoteRequestModel?> GetQuoteRequestById(int id)
    {
        return await _quoteRequestService.GetQuoteRequestById(id);
    }

    [HttpPost("", Name = "CreateQuoteRequest")]
    public async Task<QuoteRequestModel> CreateQuoteRequest([FromBody] QuoteRequestModel quoteRequestModel)
    {
        QuoteRequestModel quoteRequest = new()
        {
            QuoteId = quoteRequestModel.QuoteId,
            JobId = quoteRequestModel.JobId,
            RequestDate = quoteRequestModel.RequestDate,
            DescriptionOfWork = quoteRequestModel.DescriptionOfWork,
            EstimatedPrice = quoteRequestModel.EstimatedPrice,
        };

        return await _quoteRequestService.CreateQuoteRequest(quoteRequest);
    }

    [HttpPut("", Name = "UpdateQuoteRequest")]
    public async Task UpdateQuoteRequest([FromBody] QuoteRequestModel quoteRequestModel)
    {
        await _quoteRequestService.UpdateQuoteRequest(quoteRequestModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteQuoteRequest")]
    public async Task<bool> DeleteQuoteRequest(int id)
    {
        return await _quoteRequestService.DeleteQuoteRequest(id);
    }
}