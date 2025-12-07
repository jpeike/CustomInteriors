using System.ComponentModel.DataAnnotations;

namespace Core;

public class EmailModel
{
    public int EmailId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
    
    [Required]
    [StringLength(255)]
    [EmailAddress]
    public required string EmailAddress { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string EmailType { get; set; }
    
    // remove from model and let entity handle date
    //[DateBeforeNow]
    //public DateTime CreatedOn { get; set; }
}