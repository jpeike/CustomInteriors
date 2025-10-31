namespace Core;

public interface IJobInvoiceService
{
    Task<JobInvoiceModel?> GetJobInvoiceById(int id);
    Task<IEnumerable<JobInvoiceModel>> GetAllJobInvoices();
    Task<JobInvoiceModel> AddJobInvoice(JobInvoiceModel jobInvoice);
    Task UpdateJobInvoice(JobInvoiceModel jobInvoice);
    Task<bool> DeleteJobInvoice(int id);
}
