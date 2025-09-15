namespace Core;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = null!; // this "=null!" means that we are ignoring the warning that we would otherwise get saying we know this isnt going to be null
    public string Email { get; set; } = null!;
    public DateTime CreatedOn { get; set; }

}

