using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult<IEnumerable<QuoteRequestModel>>> GetAllQuoteRequests()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _quoteRequestService.GetAllQuoteRequests());
    }

    [HttpGet("{id:int}", Name = "GetQuoteRequestById")]
    public async Task<ActionResult<QuoteRequestModel?>> GetQuoteRequestById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _quoteRequestService.GetQuoteRequestById(id));
    }

    [HttpPost("", Name = "CreateQuoteRequest")]
    public async Task<ActionResult<QuoteRequestModel>> CreateQuoteRequest([FromBody] CreateQuoteRequestModel quoteRequestModel)
    {
        var toAdd = new QuoteRequestModel();
        toAdd.DescriptionOfWork = quoteRequestModel.DescriptionOfWork;
        toAdd.EstimatedPrice = quoteRequestModel.EstimatedPrice;
        toAdd.JobId = quoteRequestModel.JobId;
        toAdd.QuoteId = 0;
        toAdd.RequestDate = DateTime.Now;

        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _quoteRequestService.CreateQuoteRequest(toAdd));
    }

    [HttpPut("", Name = "UpdateQuoteRequest")]
    public async Task<ActionResult> UpdateQuoteRequest([FromBody] QuoteRequestModel quoteRequestModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _quoteRequestService.UpdateQuoteRequest(quoteRequestModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteQuoteRequest")]
    public async Task<ActionResult<bool>> DeleteQuoteRequest(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _quoteRequestService.DeleteQuoteRequest(id));
    }
}