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
    public async Task<IEnumerable<JobModel>> GetAllJobs()
    {
        return await _jobService.GetAllJobs();
    }

    [HttpGet("{id:int}", Name = "GetJobById")]
    public async Task<JobModel?> GetJobById(int id)
    {
        return await _jobService.GetJobById(id);
    }

    [HttpPost("", Name = "CreateJob")]
    public async Task<JobModel> CreateJob([FromBody] JobModel jobModel)
    {
        return await _jobService.CreateJob(jobModel);
    }

    [HttpPut("", Name = "UpdateJob")]
    public async Task UpdateJob([FromBody] JobModel jobModel)
    { 
        await _jobService.UpdateJob(jobModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteJob")]
    public async Task<bool> DeleteJob(int id)
    {
        return await _jobService.DeleteJob(id);
    }
}

