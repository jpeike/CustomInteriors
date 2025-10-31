using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class InvoiceItemRepository : IInvoiceItemRepository
{
    private readonly AppDbContext _dbContext;

    public InvoiceItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InvoiceItem?> GetInvoiceItemById(int id)
    {
        return await _dbContext.InvoiceItems.FindAsync(id);
    }

    public async Task<IEnumerable<InvoiceItem>> GetAllInvoiceItems()
    {
        return await _dbContext.InvoiceItems.ToListAsync();
    }

    public async Task<InvoiceItem> AddInvoiceItem(InvoiceItem invoiceItem)
    {
        _dbContext.InvoiceItems.Add(invoiceItem);
        await _dbContext.SaveChangesAsync();
        return invoiceItem;
    }

    public async Task UpdateInvoiceItem(InvoiceItem invoiceItem)
    {
        _dbContext.InvoiceItems.Update(invoiceItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteInvoiceItem(int id)
    {
        InvoiceItem? invoiceItem = await _dbContext.InvoiceItems.FindAsync(id);
        if (invoiceItem != null)
        {
            _dbContext.Remove(invoiceItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}