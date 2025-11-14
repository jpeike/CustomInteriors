using System.ComponentModel.DataAnnotations;

namespace Core;

public class PhoneModel
{
    public int PhoneId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
    
    [Phone]
    [StringLength(100)]
    public string? PhoneNumber { get; set; }
    
    [StringLength(100)]
    public string? PhoneType { get; set; }
}