using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/jobInvoices")]
public class JobInvoiceController : ControllerBase
{
    private readonly IJobInvoiceService _jobInvoiceService;

    public JobInvoiceController(IJobInvoiceService jobInvoiceService)
    {
        _jobInvoiceService = jobInvoiceService;
    }

    [HttpGet("", Name = "GetAllJobInvoices")]
    public async Task<IEnumerable<JobInvoiceModel>> GetAllJobInvoices()
    {
        return await _jobInvoiceService.GetAllJobInvoices();
    }

    [HttpGet("{id:int}", Name = "GetJobInvoiceById")]
    public async Task<JobInvoiceModel?> GetJobInvoiceById(int id)
    {
        return await _jobInvoiceService.GetJobInvoiceById(id);
    }

    [HttpPost("", Name = "CreateJobInvoice")]
    public async Task<JobInvoiceModel> CreateJobInvoice([FromBody] JobInvoiceModel jobInvoiceModel)
    {
        return await _jobInvoiceService.CreateJobInvoice(jobInvoiceModel);
    }

    [HttpPut("", Name = "UpdateJobInvoice")]
    public async Task UpdateJobInvoice([FromBody] JobInvoiceModel jobInvoiceModel)
    { 
        await _jobInvoiceService.UpdateJobInvoice(jobInvoiceModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteJobInvoice")]
    public async Task<bool> DeleteJobInvoice(int id)
    {
        return await _jobInvoiceService.DeleteJobInvoice(id);
    }
}

