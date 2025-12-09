using System.ComponentModel.DataAnnotations;

namespace Core;

public class InvoiceItemModel
{
    public int ItemId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int InvoiceId { get; set; }
    
    [StringLength(255)]
    public string? Description { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? Amount { get; set; }
    
    [Range(typeof(decimal), "0.00", "999999999999.99")]
    public decimal? Price { get; set; }
}