namespace Core;

public interface IInvoiceRepository
{
    Task<Invoice?> GetInvoiceById(int id);
    Task<IEnumerable<Invoice>> GetAllInvoices();
    Task<Invoice> AddInvoice(Invoice invoice);
    Task UpdateInvoice(Invoice invoice);
    Task<bool> DeleteInvoice(int id);
}
