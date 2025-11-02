namespace Core;

public interface IJobService
{
    Task<JobModel?> GetJobById(int id);
    Task<IEnumerable<JobModel>> GetAllJobs();
    Task<JobModel> CreateJob(JobModel job);
    Task UpdateJob(JobModel job);
    Task<bool> DeleteJob(int id);
}
