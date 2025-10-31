using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("", Name = "GetAllInvoices")]
    public async Task<IEnumerable<InvoiceModel>> GetAllInvoices()
    {
        return await _invoiceService.GetAllInvoices();
    }

    [HttpGet("{id:int}", Name = "GetInvoiceById")]
    public async Task<InvoiceModel?> GetInvoiceById(int id)
    {
        return await _invoiceService.GetInvoiceById(id);
    }

    [HttpPost("", Name = "CreateInvoice")]
    public async Task<InvoiceModel> CreateInvoice([FromBody] InvoiceModel invoiceModel)
    {
        InvoiceModel invoice = new()
        {
            InvoiceId = invoiceModel.InvoiceId,
            DateIssued = invoiceModel.DateIssued,
            Method = invoiceModel.Method,
            SellerDetails = invoiceModel.SellerDetails,
        };

        return await _invoiceService.CreateInvoice(invoice);
    }

    [HttpPut("", Name = "UpdateInvoice")]
    public async Task UpdateInvoice([FromBody] InvoiceModel invoiceModel)
    { 
        await _invoiceService.UpdateInvoice(invoiceModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteInvoice")]
    public async Task<bool> DeleteInvoice(int id)
    {
        return await _invoiceService.DeleteInvoice(id);
    }
}

