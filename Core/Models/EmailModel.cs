namespace Core;

public class EmailModel
{
    public int EmailId { get; set; }
    public int CustomerId { get; set; }
    public required string EmailAddress { get; set; }
    public required string EmailType { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public required CustomerModel Customer { get; set; }
}