namespace Core;

public interface IJobInvoiceService
{
    Task<JobInvoiceModel?> GetJobInvoiceById(int jobId, int invoiceId);
    Task<IEnumerable<JobInvoiceModel>> GetAllJobInvoices();
    Task<JobInvoiceModel> CreateJobInvoice(JobInvoiceModel jobInvoice);
    Task UpdateJobInvoice(JobInvoiceModel jobInvoice);
    Task<bool> DeleteJobInvoice(int jobId, int invoiceId);
}
