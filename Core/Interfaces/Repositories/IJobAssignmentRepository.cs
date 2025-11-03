namespace Core;

public interface IJobAssignmentRepository
{
    Task<JobAssignment?> GetJobAssignmentById(int id);
    Task<IEnumerable<JobAssignment>> GetAllJobAssignments();
    Task<JobAssignment> AddJobAssignment(JobAssignment jobAssignment);
    Task UpdateJobAssignment(JobAssignment jobAssignment);
    Task<bool> DeleteJobAssignment(int id);
}
