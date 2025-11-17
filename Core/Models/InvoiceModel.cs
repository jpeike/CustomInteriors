using System.ComponentModel.DataAnnotations;

namespace Core;

public class InvoiceModel
{
    public int InvoiceId { get; set; }
    
    [DateBeforeNow]
    public DateTime? DateIssued { get; set; }
    
    [StringLength(100)]
    public string? Method { get; set; }
    
    [StringLength(255)]
    public string? SellerDetails { get; set; }
}