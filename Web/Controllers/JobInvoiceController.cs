using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult<IEnumerable<JobInvoiceModel>>> GetAllJobInvoices()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _jobInvoiceService.GetAllJobInvoices());
    }

    [HttpGet("{id:int}", Name = "GetJobInvoiceById")]
    public async Task<ActionResult<JobInvoiceModel?>> GetJobInvoiceById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _jobInvoiceService.GetJobInvoiceById(id));
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

    [HttpDelete("{id:int}", Name = "DeleteJobInvoice")]
    public async Task<ActionResult<bool>> DeleteJobInvoice(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _jobInvoiceService.DeleteJobInvoice(id));
    }
}