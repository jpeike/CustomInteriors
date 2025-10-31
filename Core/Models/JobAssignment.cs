using System.Text.Json.Serialization;

namespace Core;

public class JobAssignmentModel
{
    public int JobId { get; set; }
    public int UserId { get; set; }
    public DateTime? AssignmentDate { get; set; }
    public string? RoleOnJob { get; set; }

    public UserModel? User { get; set; }
    public JobModel? Job { get; set; }
}