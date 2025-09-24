namespace Core;

public class EmailModel
{
    public int EmailID { get; set; }
    public int CustomerID { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string EmailType { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}
