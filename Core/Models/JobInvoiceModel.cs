using System.ComponentModel.DataAnnotations;

namespace Core;

public class JobInvoiceModel
{
    [Required]
    [Range(1, int.MaxValue)]
    public int JobId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int InvoiceId { get; set; }
    
    [DateBeforeNow]
    public DateTime? CreatedDate { get; set; }
    
    [Range(0.0, 100.0)]
    public float? PercentageOfInvoice { get; set; }
}