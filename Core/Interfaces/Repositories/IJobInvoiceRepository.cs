namespace Core;

public interface IJobInvoiceRepository
{
    Task<JobInvoice?> GetJobInvoiceById(int jobId, int invoiceId);
    Task<IEnumerable<JobInvoice>> GetAllJobInvoices();
    Task<JobInvoice> AddJobInvoice(JobInvoice jobInvoice);
    Task UpdateJobInvoice(JobInvoice jobInvoice);
    Task<bool> DeleteJobInvoice(int jobId, int invoiceId);
}
