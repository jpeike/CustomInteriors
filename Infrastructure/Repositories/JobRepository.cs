using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class JobRepository : IJobRepository
{
    private readonly AppDbContext _dbContext;

    public JobRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Job?> GetJobById(int id)
    {
        return await _dbContext.Jobs.FindAsync(id);
    }

    public async Task<IEnumerable<Job>> GetAllJobs()
    {
        return await _dbContext.Jobs.ToListAsync();
    }

    public async Task<Job> AddJob(Job job)
    {
        _dbContext.Jobs.Add(job);
        await _dbContext.SaveChangesAsync();
        return job;
    }

    public async Task UpdateJob(Job job)
    {
        _dbContext.Jobs.Update(job);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteJob(int id)
    {
        Job? job = await _dbContext.Jobs.FindAsync(id);
        if (job != null)
        {
            _dbContext.Remove(job);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}