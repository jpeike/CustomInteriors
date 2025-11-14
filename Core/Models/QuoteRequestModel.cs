using System.ComponentModel.DataAnnotations;

namespace Core;

public class QuoteRequestModel
{
    public int QuoteId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int JobId { get; set; }
    
    [DateBeforeNow]
    public DateTime? RequestDate { get; set; }
    
    [StringLength(10000)]
    public string? DescriptionOfWork { get; set; }
    
    [Range(typeof(decimal), "0.00", "999999999.99")]
    public decimal? EstimatedPrice { get; set; }
}