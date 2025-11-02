using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class JobAssignmentRepository : IJobAssignmentRepository
{
    private readonly AppDbContext _dbContext;

    public JobAssignmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JobAssignment?> GetJobAssignmentById(int id)
    {
        return await _dbContext.JobAssignments.FindAsync(id);
    }

    public async Task<IEnumerable<JobAssignment>> GetAllJobAssignments()
    {
        return await _dbContext.JobAssignments.ToListAsync();
    }

    public async Task<JobAssignment> AddJobAssignment(JobAssignment jobAssignment)
    {
        _dbContext.JobAssignments.Add(jobAssignment);
        await _dbContext.SaveChangesAsync();
        return jobAssignment;
    }

    public async Task UpdateJobAssignment(JobAssignment jobAssignment)
    {
        _dbContext.JobAssignments.Update(jobAssignment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteJobAssignment(int id)
    {
        JobAssignment? jobAssignment = await _dbContext.JobAssignments.FindAsync(id);
        if (jobAssignment != null)
        {
            _dbContext.Remove(jobAssignment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}