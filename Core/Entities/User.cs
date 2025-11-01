using System.Text.Json.Serialization;

namespace Core;

public class User
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedOn { get; set; }

    [JsonIgnore] // Swagger and JSON serialization skip this, we need it to compare the hash, but we want to ensure no matter what it does not get logged or exposed. 
    public string PasswordHash { get; set; }
    
    [JsonIgnore] public Customer? Customer { get; set; }
    //[JsonIgnore] public ICollection<JobAssignment>? JobAssignments { get; set; }
}



