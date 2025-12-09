using Core;

namespace Infrastructure;

public class JobInvoiceService : IJobInvoiceService
{
    private readonly IJobInvoiceRepository _jobInvoiceRepository;

    public JobInvoiceService(IJobInvoiceRepository jobInvoiceRepository)
    {
        _jobInvoiceRepository = jobInvoiceRepository;
    }

    public async Task<JobInvoiceModel> CreateJobInvoice(JobInvoiceModel jobInvoice)
    {
        JobInvoice toReturn = await _jobInvoiceRepository.AddJobInvoice(jobInvoice.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteJobInvoice(int jobId, int invoiceId)
    {
        return await _jobInvoiceRepository.DeleteJobInvoice(jobId, invoiceId);
    }

    public async Task<IEnumerable<JobInvoiceModel>> GetAllJobInvoices()
    {
        var allJobInvoices = await _jobInvoiceRepository.GetAllJobInvoices();
        return allJobInvoices.ToModels();
    }

    public async Task<JobInvoiceModel?> GetJobInvoiceById(int jobId, int invoiceId)
    {
        JobInvoice? jobInvoice = await _jobInvoiceRepository.GetJobInvoiceById(jobId, invoiceId);
        return jobInvoice?.ToModel();
    }

    public async Task UpdateJobInvoice(JobInvoiceModel jobInvoice)
    {
        await _jobInvoiceRepository.UpdateJobInvoice(jobInvoice.ToEntity());
    }
}

