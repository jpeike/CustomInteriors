namespace Core;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!; // this "=null!" means that we are ignoring the warning that we would otherwise get saying we know this isnt going to be null
    public string LastName { get; set; } = null!; // this "=null!" means that we are ignoring the warning that we would otherwise get saying we know this isnt going to be null
    public string CustomerType { get; set; } = null!;
    public string? PrefferedContactMethod { get; set; }
    public string? CompanyName { get; set; }
    public string? Status { get; set; }
    public string? CustomerNotes { get; set; }
    public ICollection<Address> Addresses { get; set; } = null!;
}
