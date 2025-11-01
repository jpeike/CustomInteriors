namespace Core;

public class UserModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedOn { get; set; }

    // public CustomerModel? Customer { get; set; }
    // public ICollection<JobAssignmentModel> JobAssignments { get; set; } =  new List<JobAssignmentModel>();
}

