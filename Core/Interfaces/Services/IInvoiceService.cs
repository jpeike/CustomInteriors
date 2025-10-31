namespace Core;

public interface IInvoiceService
{
    Task<InvoiceModel?> GetInvoiceById(int id);
    Task<IEnumerable<InvoiceModel>> GetAllInvoices();
    Task<InvoiceModel> CreateInvoice(InvoiceModel invoice);
    Task UpdateInvoice(InvoiceModel invoice);
    Task<bool> DeleteInvoice(int id);
}
