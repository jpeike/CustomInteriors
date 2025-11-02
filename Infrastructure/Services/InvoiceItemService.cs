using Core;

namespace Infrastructure;

public class InvoiceItemService : IInvoiceItemService
{
    private readonly IInvoiceItemRepository _invoiceItemRepository;

    public InvoiceItemService(IInvoiceItemRepository invoiceItemRepository)
    {
        _invoiceItemRepository = invoiceItemRepository;
    }

    public async Task<InvoiceItemModel> CreateInvoiceItem(InvoiceItemModel invoiceItem)
    {
        InvoiceItem toReturn = await _invoiceItemRepository.AddInvoiceItem(invoiceItem.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteInvoiceItem(int id)
    {
        return await _invoiceItemRepository.DeleteInvoiceItem(id);
    }

    public async Task<IEnumerable<InvoiceItemModel>> GetAllInvoiceItems()
    {
        var allInvoiceItems = await _invoiceItemRepository.GetAllInvoiceItems();
        return allInvoiceItems.ToModels();
    }

    public async Task<InvoiceItemModel?> GetInvoiceItemById(int id)
    {
        InvoiceItem? invoiceItem = await _invoiceItemRepository.GetInvoiceItemById(id);
        return invoiceItem?.ToModel();
    }

    public async Task UpdateInvoiceItem(InvoiceItemModel invoiceItem)
    {
        await _invoiceItemRepository.UpdateInvoiceItem(invoiceItem.ToEntity());
    }
}

