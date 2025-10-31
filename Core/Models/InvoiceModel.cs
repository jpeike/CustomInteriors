using System.Text.Json.Serialization;

namespace Core;

public class InvoiceModel
{
    public int InvoiceId { get; set; }
    public DateTime? DateIssued { get; set; }
    public string? Method { get; set; }
    public string? SellerDetails { get; set; }

    public ICollection<JobInvoiceModel> JobInvoices { get; set; } = new List<JobInvoiceModel>();
    public ICollection<InvoiceItemModel> InvoiceItems { get; set; } = new List<InvoiceItemModel>();
    public ICollection<PaymentModel>? Payments { get; set; } = new List<PaymentModel>();
}