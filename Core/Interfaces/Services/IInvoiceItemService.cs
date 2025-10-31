namespace Core;

public interface IInvoiceItemService
{
    Task<InvoiceItemModel?> GetInvoiceItemById(int id);
    Task<IEnumerable<InvoiceItemModel>> GetAllInvoiceItems();
    Task<InvoiceItemModel> CreateInvoiceItem(InvoiceItemModel invoiceItem);
    Task UpdateInvoiceItem(InvoiceItemModel invoiceItem);
    Task<bool> DeleteInvoiceItem(int id);
}
