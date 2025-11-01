using System.Text.Json.Serialization;

namespace Core;

public class PaymentModel
{
    public int PaymentId { get; set; }
    public int InvoiceId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal? AmountPaid { get; set; }
    public string? Method { get; set; }

    // public InvoiceModel Invoice { get; set; }
}