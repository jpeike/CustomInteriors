using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web;

[ApiController]
[Authorize(Policy = Policies.AdminOnly)]
[Route("api/jobInvoices")]
public class JobInvoiceController : ControllerBase
{
    private readonly IJobInvoiceService _jobInvoiceService;

    public JobInvoiceController(IJobInvoiceService jobInvoiceService)
    {
        _jobInvoiceService = jobInvoiceService;
    }

    [HttpGet("", Name = "GetAllJobInvoices")]
    public async Task<ActionResult<IEnumerable<JobInvoiceModel>>> GetAllJobInvoices()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _jobInvoiceService.GetAllJobInvoices());
    }

    [HttpGet("by-id", Name = "GetJobInvoiceById")]
    public async Task<ActionResult<JobInvoiceModel?>> GetJobInvoiceById([FromQuery] int jobId, [FromQuery] int invoiceId)
    {
        if (jobId <= 0 || invoiceId <= 0) return BadRequest();
        return Ok(await _jobInvoiceService.GetJobInvoiceById(jobId, invoiceId));
    }

    [HttpPost("", Name = "CreateJobInvoice")]
    public async Task<ActionResult<JobInvoiceModel>> CreateJobInvoice([FromBody] JobInvoiceModel jobInvoiceModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _jobInvoiceService.CreateJobInvoice(jobInvoiceModel));
    }

    [HttpPut("", Name = "UpdateJobInvoice")]
    public async Task<ActionResult> UpdateJobInvoice([FromBody] JobInvoiceModel jobInvoiceModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _jobInvoiceService.UpdateJobInvoice(jobInvoiceModel);
        return NoContent();
    }

    [HttpDelete("", Name = "DeleteJobInvoice")]
    public async Task<ActionResult<bool>> DeleteJobInvoice([FromQuery] int jobId, [FromQuery] int invoiceId)
    {
        if (jobId <= 0 || invoiceId <= 0) return BadRequest();
        return Ok(await _jobInvoiceService.DeleteJobInvoice(jobId, invoiceId));
    }
}