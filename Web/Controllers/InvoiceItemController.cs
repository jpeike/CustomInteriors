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
    public async Task<IEnumerable<InvoiceItemModel>> GetAllInvoiceItems()
    {
        return await _invoiceItemService.GetAllInvoiceItems();
    }

    [HttpGet("{id:int}", Name = "GetInvoiceItemById")]
    public async Task<InvoiceItemModel?> GetInvoiceItemById(int id)
    {
        return await _invoiceItemService.GetInvoiceItemById(id);
    }

    [HttpPost("", Name = "CreateInvoiceItem")]
    public async Task<InvoiceItemModel> CreateInvoiceItem([FromBody] InvoiceItemModel invoiceItemModel)
    {
        return await _invoiceItemService.CreateInvoiceItem(invoiceItemModel);
    }

    [HttpPut("", Name = "UpdateInvoiceItem")]
    public async Task UpdateInvoiceItem([FromBody] InvoiceItemModel invoiceItemModel)
    { 
        await _invoiceItemService.UpdateInvoiceItem(invoiceItemModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteInvoiceItem")]
    public async Task<bool> DeleteInvoiceItem(int id)
    {
        return await _invoiceItemService.DeleteInvoiceItem(id);
    }
}

