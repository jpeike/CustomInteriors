using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _dbContext;

    public InvoiceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Invoice?> GetInvoiceById(int id)
    {
        return await _dbContext.Invoices.FindAsync(id);
    }

    public async Task<IEnumerable<Invoice>> GetAllInvoices()
    {
        return await _dbContext.Invoices.ToListAsync();
    }

    public async Task<Invoice> AddInvoice(Invoice invoice)
    {
        _dbContext.Invoices.Add(invoice);
        await _dbContext.SaveChangesAsync();
        return invoice;
    }

    public async Task UpdateInvoice(Invoice invoice)
    {
        _dbContext.Invoices.Update(invoice);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteInvoice(int id)
    {
        Invoice? invoice = await _dbContext.Invoices.FindAsync(id);
        if (invoice != null)
        {
            _dbContext.Remove(invoice);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}