using System;
using System.Text.Json.Serialization;

namespace Core;

public class Email
{
    public int EmailId { get; set; } // Primary Key
    public int CustomerId { get; set; } // Foreign Key -> Customer(CustomerId)
    public required string EmailAddress { get; set; }
    public required string EmailType { get; set; }
    public DateTime CreatedOn { get; set; } // Defaults to GETDATE() in SQL
    [JsonIgnore] public Customer Customer { get; set; } = null!; // Navigation property
}