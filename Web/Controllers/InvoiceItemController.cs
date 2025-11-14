using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/invoiceItems")]
public class InvoiceItemController : ControllerBase
{
    private readonly IInvoiceItemService _invoiceItemService;

    public InvoiceItemController(IInvoiceItemService invoiceItemService)
    {
        _invoiceItemService = invoiceItemService;
    }

    [HttpGet("", Name = "GetAllInvoiceItems")]
    public async Task<ActionResult<IEnumerable<InvoiceItemModel>>> GetAllInvoiceItems()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _invoiceItemService.GetAllInvoiceItems());
    }

    [HttpGet("{id:int}", Name = "GetInvoiceItemById")]
    public async Task<ActionResult<InvoiceItemModel?>> GetInvoiceItemById(int id)
    {
        if (id <= 0) return BadRequest();
        return await _invoiceItemService.GetInvoiceItemById(id);
    }

    [HttpPost("", Name = "CreateInvoiceItem")]
    public async Task<ActionResult<InvoiceItemModel>> CreateInvoiceItem([FromBody] InvoiceItemModel invoiceItemModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return await _invoiceItemService.CreateInvoiceItem(invoiceItemModel);
    }

    [HttpPut("", Name = "UpdateInvoiceItem")]
    public async Task<ActionResult> UpdateInvoiceItem([FromBody] InvoiceItemModel invoiceItemModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _invoiceItemService.UpdateInvoiceItem(invoiceItemModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteInvoiceItem")]
    public async Task<ActionResult<bool>> DeleteInvoiceItem(int id)
    {
        if (id <= 0) return BadRequest();
        return await _invoiceItemService.DeleteInvoiceItem(id);
    }
}