namespace Core;

public interface IJobAssignmentService
{
    Task<JobAssignmentModel?> GetJobAssignmentById(int id);
    Task<IEnumerable<JobAssignmentModel>> GetAllJobAssignments();
    Task<JobAssignmentModel> AddJobAssignment(JobAssignmentModel jobAssignment);
    Task UpdateJobAssignment(JobAssignmentModel jobAssignment);
    Task<bool> DeleteJobAssignment(int id);
}
