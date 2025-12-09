using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web;

[ApiController]
[Authorize(Policy = Policies.EmployeeOrAdmin)]
[Route("api/jobAssignments")]
public class JobAssignmentController : ControllerBase
{
    private readonly IJobAssignmentService _jobAssignmentService;

    public JobAssignmentController(IJobAssignmentService jobAssignmentService)
    {
        _jobAssignmentService = jobAssignmentService;
    }

    [HttpGet("", Name = "GetAllJobAssignments")]
    public async Task<ActionResult<IEnumerable<JobAssignmentModel>>> GetAllJobAssignments()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _jobAssignmentService.GetAllJobAssignments());
    }

    [HttpGet("{id:int}", Name = "GetJobAssignmentById")]
    public async Task<ActionResult<JobAssignmentModel?>> GetJobAssignmentById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _jobAssignmentService.GetJobAssignmentById(id));
    }

    [HttpPost("", Name = "CreateJobAssignment")]
    public async Task<ActionResult<JobAssignmentModel>> CreateJobAssignment([FromBody] JobAssignmentModel jobAssignmentModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _jobAssignmentService.CreateJobAssignment(jobAssignmentModel));
    }

    [HttpPut("", Name = "UpdateJobAssignment")]
    public async Task<ActionResult> UpdateJobAssignment([FromBody] JobAssignmentModel jobAssignmentModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _jobAssignmentService.UpdateJobAssignment(jobAssignmentModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteJobAssignment")]
    public async Task<ActionResult<bool>> DeleteJobAssignment(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _jobAssignmentService.DeleteJobAssignment(id));
    }
}