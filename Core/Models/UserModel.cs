using System.ComponentModel.DataAnnotations;

namespace Core;

public class UserModel
{
    public int Id { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string Username { get; set; }
    
    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }
    
    [DateBeforeNow]
    public DateTime CreatedOn { get; set; }
}

