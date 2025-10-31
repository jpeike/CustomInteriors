using System;
using System.Text.Json.Serialization;

namespace Core;

public class Email
{
    public int EmailID { get; set; } // Primary Key
    public int CustomerId { get; set; } // Foreign Key -> Customer(CustomerId)
    public string EmailAddress { get; set; } = null!; // Required, non-nullable
    public string EmailType { get; set; } = null!; // Required, non-nullable
    public DateTime CreatedOn { get; set; } // Defaults to GETDATE() in SQL
    [JsonIgnore] public Customer Customer { get; set; } = null!; // Navigation property
}