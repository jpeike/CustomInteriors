using System.Text.Json.Serialization;

namespace Core;

public class Customer
{
    public int CustomerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CustomerType { get; set; }
    public string? PrefferedContactMethod { get; set; }
    public string? CompanyName { get; set; }
    public string? Status { get; set; }
    public string? CustomerNotes { get; set; }
    public ICollection<Address>? Addresses { get; set; }
    [JsonIgnore] public ICollection<Email>? Emails { get; set; } = new List<Email>();
    [JsonIgnore] public ICollection<Phone>? Phones { get; set; } = new List<Phone>();
    [JsonIgnore] public ICollection<Job>? Jobs { get; set; } = new List<Job>();
    [JsonIgnore] public ICollection<QuoteRequest>? QuoteRequests { get; set; } = new List<QuoteRequest>();
    [JsonIgnore] public User? User { get; set; }
}