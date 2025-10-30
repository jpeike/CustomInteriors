using System.Text.Json.Serialization;

namespace Core;

public class Payment
{
    public int PaymentId { get; set; }
    public int InvoiceId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal? AmountPaid { get; set; }
    public string? Method { get; set; }
    
    [JsonIgnore] Invoice Invoice { get; set; }
}