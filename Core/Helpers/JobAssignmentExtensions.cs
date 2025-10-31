namespace Core;

public static class JobAssignmentExtensions
{
    public static JobAssignmentModel ToModel(this JobAssignment entity) => new JobAssignmentModel
    {
        JobId = entity.JobId,
        UserId = entity.UserId,
        AssignmentDate = entity.AssignmentDate,
        RoleOnJob = entity.RoleOnJob,
        Job = entity.Job?.ToModel(),
        User = entity.User?.ToModel()
    };

    public static IEnumerable<JobAssignmentModel> ToModels(this IEnumerable<JobAssignment> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static JobAssignment ToEntity(this JobAssignmentModel model) => new JobAssignment
    {
        JobId = model.JobId,
        UserId = model.UserId,
        AssignmentDate = model.AssignmentDate,
        RoleOnJob = model.RoleOnJob,
        Job = model.Job?.ToEntity(),
        User = model.User?.ToEntity()
    };
}