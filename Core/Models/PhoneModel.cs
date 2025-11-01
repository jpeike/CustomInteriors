using System.Text.Json.Serialization;

namespace Core;

public class PhoneModel
{
    public int PhoneId { get; set; }
    public int CustomerId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneType { get; set; }

    // public CustomerModel Customer { get; set; }
}