namespace Core;

public static class JobExtensions
{
    public static JobModel ToModel(this Job entity) => new JobModel
    {
        JobId = entity.JobId,
        CustomerId = entity.CustomerId,
        JobDescription = entity.JobDescription,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        Status = entity.Status,
        Customer = entity.Customer.ToModel()
    };

    public static IEnumerable<JobModel> ToModels(this IEnumerable<Job> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static Job ToEntity(this JobModel model) => new Job
    {
        JobId = model.JobId,
        CustomerId = model.CustomerId,
        JobDescription = model.JobDescription,
        StartDate = model.StartDate,
        EndDate = model.EndDate,
        Status = model.Status,
        Customer = model.Customer.ToEntity()
    };
}