namespace Core;

public interface IJobInvoiceRepository
{
    Task<JobInvoice?> GetJobInvoiceById(int id);
    Task<IEnumerable<JobInvoice>> GetAllJobInvoices();
    Task<JobInvoice> AddJobInvoice(JobInvoice jobInvoice);
    Task UpdateJobInvoice(JobInvoice jobInvoice);
    Task<bool> DeleteJobInvoice(int id);
}
