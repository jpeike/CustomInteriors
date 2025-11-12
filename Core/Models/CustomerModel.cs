using System.ComponentModel.DataAnnotations;

namespace Core;

public class CustomerModel
{
    public int CustomerId { get; set; }
    
    [Required]
    [StringLength(100)]
    [RegularExpression("^[a-zA-Z]+$")] // simple alpha only regex check
    public required string FirstName { get; set; }
    
    [Required]
    [StringLength(100)]
    [RegularExpression("^[a-zA-Z]+$")]
    public required string LastName { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string CustomerType { get; set; }
    
    [StringLength(100)]
    public string? PrefferedContactMethod { get; set; }
    
    [StringLength(100)]
    public string? CompanyName { get; set; }
    
    [StringLength(100)]
    public string? Status { get; set; }
    
    [StringLength(65535)]
    public string? CustomerNotes { get; set; }
    
    // todo eventually remove
    public ICollection<AddressModel>? Addresses { get; set; }
    // public ICollection<EmailModel>? Emails { get; set; } = new List<EmailModel>();
    // public ICollection<PhoneModel>? Phones { get; set; } = new List<PhoneModel>();
    // public ICollection<JobModel>? Jobs { get; set; } = new List<JobModel>();
    // //public ICollection<QuoteRequestModel>? QuoteRequests { get; set; } = new List<QuoteRequestModel>();
    // public UserModel? User { get; set; }
}