using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web;

[ApiController]
[Authorize(Policy = Policies.AdminOnly)]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("", Name = "GetAllInvoices")]
    public async Task<ActionResult<IEnumerable<InvoiceModel>>> GetAllInvoices()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _invoiceService.GetAllInvoices());
    }

    [HttpGet("{id:int}", Name = "GetInvoiceById")]
    public async Task<ActionResult<InvoiceModel?>> GetInvoiceById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _invoiceService.GetInvoiceById(id));
    }

    [HttpPost("", Name = "CreateInvoice")]
    public async Task<ActionResult<InvoiceModel>> CreateInvoice([FromBody] InvoiceModel invoiceModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _invoiceService.CreateInvoice(invoiceModel));
    }

    [HttpPut("", Name = "UpdateInvoice")]
    public async Task<ActionResult> UpdateInvoice([FromBody] InvoiceModel invoiceModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _invoiceService.UpdateInvoice(invoiceModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteInvoice")]
    public async Task<ActionResult<bool>> DeleteInvoice(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _invoiceService.DeleteInvoice(id));
    }
}