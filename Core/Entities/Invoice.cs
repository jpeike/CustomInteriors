using System.Text.Json.Serialization;

namespace Core;

public class Invoice
{
    public int InvoiceId { get; set; }
    public DateTime? DateIssued { get; set; }
    public string? Method { get; set; }
    public string? SellerDetails { get; set; }

    [JsonIgnore] public ICollection<JobInvoice> JobInvoices { get; set; }  = null!;
    [JsonIgnore] public ICollection<InvoiceItem> InvoiceItems { get; set; } = null!;
    [JsonIgnore] public ICollection<Payment>? Payments { get; set; }
}