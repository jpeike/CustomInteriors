using System.Text.Json.Serialization;

namespace Core;

public class JobAssignment
{
    public int JobId { get; set; }
    public int UserId { get; set; }
    public DateTime? AssignmentDate { get; set; }
    public string? RoleOnJob { get; set; }

    [JsonIgnore] public User? User { get; set; }
    [JsonIgnore] public Job? Job { get; set; }
}