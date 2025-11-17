using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet("", Name = "GetAllJobs")]
    public async Task<ActionResult<IEnumerable<JobModel>>> GetAllJobs()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _jobService.GetAllJobs());
    }

    [HttpGet("{id:int}", Name = "GetJobById")]
    public async Task<ActionResult<JobModel?>> GetJobById(int id)
    {
        if (id <= 0) return BadRequest();
        return await _jobService.GetJobById(id);
    }

    [HttpPost("", Name = "CreateJob")]
    public async Task<ActionResult<JobModel>> CreateJob([FromBody] JobModel jobModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return await _jobService.CreateJob(jobModel);
    }

    [HttpPut("", Name = "UpdateJob")]
    public async Task<ActionResult> UpdateJob([FromBody] JobModel jobModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _jobService.UpdateJob(jobModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteJob")]
    public async Task<ActionResult<bool>> DeleteJob(int id)
    {
        if (id <= 0) return BadRequest();
        return await _jobService.DeleteJob(id);
    }
}