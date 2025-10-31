using System.Text.Json.Serialization;

namespace Core;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!; // this "=null!" means that we are ignoring the warning that we would otherwise get saying we know this isnt going to be null
    public string? Email { get; set; } // The question mark means we might not have an email; ie: its possible for this to be null
    public DateTime CreatedOn { get; set; }

    [JsonIgnore] // Swagger and JSON serialization skip this, we need it to compare the hash, but we want to ensure no matter what it does not get logged or exposed. 
    public string PasswordHash { get; set; } = null!;
    
    [JsonIgnore] public Customer? Customer { get; set; }
    [JsonIgnore] public ICollection<JobAssignment> JobAssignments { get; set; } =  new List<JobAssignment>();
}



