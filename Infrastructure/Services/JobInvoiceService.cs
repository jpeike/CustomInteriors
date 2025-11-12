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

    public async Task<bool> DeleteJobInvoice(int id)
    {
        return await _jobInvoiceRepository.DeleteJobInvoice(id);
    }

    public async Task<IEnumerable<JobInvoiceModel>> GetAllJobInvoices()
    {
        var allJobInvoices = await _jobInvoiceRepository.GetAllJobInvoices();
        return allJobInvoices.ToModels();
    }

    public async Task<JobInvoiceModel?> GetJobInvoiceById(int id)
    {
        JobInvoice? jobInvoice = await _jobInvoiceRepository.GetJobInvoiceById(id);
        return jobInvoice?.ToModel();
    }

    public async Task UpdateJobInvoice(JobInvoiceModel jobInvoice)
    {
        await _jobInvoiceRepository.UpdateJobInvoice(jobInvoice.ToEntity());
    }
}

