namespace Core;

public interface IJobRepository
{
    Task<Job?> GetJobById(int id);
    Task<IEnumerable<Job>> GetAllJobs();
    Task<Job> AddJob(Job job);
    Task UpdateJob(Job job);
    Task<bool> DeleteJob(int id);
}
