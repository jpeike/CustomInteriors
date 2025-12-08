using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class JobInvoiceRepository : IJobInvoiceRepository
{
    private readonly AppDbContext _dbContext;

    public JobInvoiceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JobInvoice?> GetJobInvoiceById(int jobId, int invoiceId)
    {
        return await _dbContext.JobInvoices.FindAsync(jobId, invoiceId);
    }

    public async Task<IEnumerable<JobInvoice>> GetAllJobInvoices()
    {
        return await _dbContext.JobInvoices.ToListAsync();
    }

    public async Task<JobInvoice> AddJobInvoice(JobInvoice jobInvoice)
    {
        _dbContext.JobInvoices.Add(jobInvoice);
        await _dbContext.SaveChangesAsync();
        return jobInvoice;
    }

    public async Task UpdateJobInvoice(JobInvoice jobInvoice)
    {
        _dbContext.JobInvoices.Update(jobInvoice);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteJobInvoice(int jobId, int invoiceId)
    {
        JobInvoice? jobInvoice = await _dbContext.JobInvoices.FindAsync(jobId, invoiceId);
        if (jobInvoice != null)
        {
            _dbContext.Remove(jobInvoice);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}