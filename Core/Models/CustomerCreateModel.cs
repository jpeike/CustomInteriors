using System.ComponentModel.DataAnnotations;

public class CustomerCreateModel
{
    [Required]
    [StringLength(100)]
    [RegularExpression("^[a-zA-Z]+$")]
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
}