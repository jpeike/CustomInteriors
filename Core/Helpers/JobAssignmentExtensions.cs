namespace Core;

public static class JobAssignmentExtensions
{
    public static JobAssignmentModel ToModel(this JobAssignment entity) => new JobAssignmentModel
    {
        JobAssignmentId = entity.JobAssignmentId,
        JobId = entity.JobId,
        AssignmentDate = entity.AssignmentDate,
        RoleOnJob = entity.RoleOnJob,
        // Job = entity.Job?.ToModel(),
        // User = entity.User?.ToModel()
    };

    public static IEnumerable<JobAssignmentModel> ToModels(this IEnumerable<JobAssignment> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static JobAssignment ToEntity(this JobAssignmentModel model) => new JobAssignment
    {
        JobAssignmentId = model.JobAssignmentId,
        JobId = model.JobId,
        AssignmentDate = model.AssignmentDate,
        RoleOnJob = model.RoleOnJob,
        // Job = model.Job?.ToEntity(),
        // User = model.User?.ToEntity()
    };
}