namespace Core;

public interface IInvoiceItemRepository
{
    Task<InvoiceItem?> GetInvoiceItemById(int id);
    Task<IEnumerable<InvoiceItem>> GetAllInvoiceItems();
    Task<InvoiceItem> AddInvoiceItem(InvoiceItem invoiceItem);
    Task UpdateInvoiceItem(InvoiceItem invoiceItem);
    Task<bool> DeleteInvoiceItem(int id);
}
