using System;

namespace Core;

public class Email
{
    public int EmailID { get; set; }            // Primary Key
    public int CustomerID { get; set; }         // Foreign Key -> Customer(CustomerID)
    public string EmailAddress { get; set; } = null!; // Required, non-nullable
    public string EmailType { get; set; } = null!;     // Required, non-nullable
    public DateTime CreatedOn { get; set; }     // Defaults to GETDATE() in SQL
}
