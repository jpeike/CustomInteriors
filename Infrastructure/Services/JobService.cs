using Core;

namespace Infrastructure;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobModel> CreateJob(JobModel job)
    {
        Job toReturn = await _jobRepository.AddJob(job.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteJob(int id)
    {
        return await _jobRepository.DeleteJob(id);
    }

    public async Task<IEnumerable<JobModel>> GetAllJobs()
    {
        var allJobs = await _jobRepository.GetAllJobs();
        return allJobs.ToModels();
    }

    public async Task<JobModel?> GetJobById(int id)
    {
        Job? job = await _jobRepository.GetJobById(id);
        return job?.ToModel();
    }

    public async Task UpdateJob(JobModel job)
    {
        await _jobRepository.UpdateJob(job.ToEntity());
    }
}

