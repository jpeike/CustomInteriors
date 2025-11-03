using Core;

namespace Infrastructure;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<InvoiceModel> CreateInvoice(InvoiceModel invoice)
    {
        Invoice toReturn = await _invoiceRepository.AddInvoice(invoice.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteInvoice(int id)
    {
        return await _invoiceRepository.DeleteInvoice(id);
    }

    public async Task<IEnumerable<InvoiceModel>> GetAllInvoices()
    {
        var allInvoices = await _invoiceRepository.GetAllInvoices();
        return allInvoices.ToModels();
    }

    public async Task<InvoiceModel?> GetInvoiceById(int id)
    {
        Invoice? invoice = await _invoiceRepository.GetInvoiceById(id);
        return invoice?.ToModel();
    }

    public async Task UpdateInvoice(InvoiceModel invoice)
    {
        await _invoiceRepository.UpdateInvoice(invoice.ToEntity());
    }
}

