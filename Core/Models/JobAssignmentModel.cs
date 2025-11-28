using System.ComponentModel.DataAnnotations;

namespace Core;

public class JobAssignmentModel
{
    public int JobAssignmentId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int JobId { get; set; }
    
    [DateBeforeNow]
    public DateTime? AssignmentDate { get; set; }
    
    [StringLength(100)]
    public string? RoleOnJob { get; set; }
}