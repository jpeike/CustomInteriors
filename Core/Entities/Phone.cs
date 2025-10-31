using System.Text.Json.Serialization;

namespace Core;

public class Phone
{
    public int PhoneId { get; set; }
    public int CustomerId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneType { get; set; }

    [JsonIgnore] public Customer Customer { get; set; }
}