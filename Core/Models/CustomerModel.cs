namespace Core;

public class CustomerModel
{
    public int CustomerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CustomerType { get; set; }
    public string? PrefferedContactMethod { get; set; }
    public string? CompanyName { get; set; }
    public string? Status { get; set; }
    public string? CustomerNotes { get; set; }
    public ICollection<AddressModel>? Addresses { get; set; }
    public ICollection<EmailModel>? Emails { get; set; } = new List<EmailModel>();
    public ICollection<PhoneModel>? Phones { get; set; } = new List<PhoneModel>();
    public ICollection<JobModel>? Jobs { get; set; } = new List<JobModel>();
    public ICollection<QuoteRequestModel>? QuoteRequests { get; set; } = new List<QuoteRequestModel>();
    public UserModel? User { get; set; }
}