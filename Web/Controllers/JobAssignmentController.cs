using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/jobAssignments")]
public class JobAssignmentController : ControllerBase
{
    private readonly IJobAssignmentService _jobAssignmentService;

    public JobAssignmentController(IJobAssignmentService jobAssignmentService)
    {
        _jobAssignmentService = jobAssignmentService;
    }

    [HttpGet("", Name = "GetAllJobAssignments")]
    public async Task<IEnumerable<JobAssignmentModel>> GetAllJobAssignments()
    {
        return await _jobAssignmentService.GetAllJobAssignments();
    }

    [HttpGet("{id:int}", Name = "GetJobAssignmentById")]
    public async Task<JobAssignmentModel?> GetJobAssignmentById(int id)
    {
        return await _jobAssignmentService.GetJobAssignmentById(id);
    }

    [HttpPost("", Name = "CreateJobAssignment")]
    public async Task<JobAssignmentModel> CreateJobAssignment([FromBody] JobAssignmentModel jobAssignmentModel)
    {
        JobAssignmentModel jobAssignment = new()
        {
            JobId = jobAssignmentModel.JobId,
            UserId = jobAssignmentModel.UserId,
            AssignmentDate = jobAssignmentModel.AssignmentDate,
            RoleOnJob = jobAssignmentModel.RoleOnJob,
        };

        return await _jobAssignmentService.CreateJobAssignment(jobAssignment);
    }

    [HttpPut("", Name = "UpdateJobAssignment")]
    public async Task UpdateJobAssignment([FromBody] JobAssignmentModel jobAssignmentModel)
    { 
        await _jobAssignmentService.UpdateJobAssignment(jobAssignmentModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteJobAssignment")]
    public async Task<bool> DeleteJobAssignment(int id)
    {
        return await _jobAssignmentService.DeleteJobAssignment(id);
    }
}

