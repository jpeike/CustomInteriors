using System.ComponentModel.DataAnnotations;

namespace Core;

public class PaymentModel
{
    public int PaymentId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int InvoiceId { get; set; }
    
    public DateTime? PaymentDate { get; set; }
    
    [Range(typeof(decimal), "0.00", "999999999.99")]
    public decimal? AmountPaid { get; set; }
    
    [StringLength(100)]
    public string? Method { get; set; }
}