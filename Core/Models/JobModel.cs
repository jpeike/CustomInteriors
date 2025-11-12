using System.ComponentModel.DataAnnotations;

namespace Core;

public class JobModel
{
    public int JobId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
    
    [StringLength(255)]
    public string? JobDescription { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [StringLength(100)]
    public string? Status { get; set; }
}