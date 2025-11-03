using Core;

namespace Infrastructure;

public class JobAssignmentService : IJobAssignmentService
{
    private readonly IJobAssignmentRepository _jobAssignmentRepository;

    public JobAssignmentService(IJobAssignmentRepository jobAssignmentRepository)
    {
        _jobAssignmentRepository = jobAssignmentRepository;
    }

    public async Task<JobAssignmentModel> CreateJobAssignment(JobAssignmentModel jobAssignment)
    {
        JobAssignment toReturn = await _jobAssignmentRepository.AddJobAssignment(jobAssignment.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteJobAssignment(int id)
    {
        return await _jobAssignmentRepository.DeleteJobAssignment(id);
    }

    public async Task<IEnumerable<JobAssignmentModel>> GetAllJobAssignments()
    {
        var allJobAssignments = await _jobAssignmentRepository.GetAllJobAssignments();
        return allJobAssignments.ToModels();
    }

    public async Task<JobAssignmentModel?> GetJobAssignmentById(int id)
    {
        JobAssignment? jobAssignment = await _jobAssignmentRepository.GetJobAssignmentById(id);
        return jobAssignment?.ToModel();
    }

    public async Task UpdateJobAssignment(JobAssignmentModel jobAssignment)
    {
        await _jobAssignmentRepository.UpdateJobAssignment(jobAssignment.ToEntity());
    }
}

