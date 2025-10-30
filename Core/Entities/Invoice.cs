namespace Core;

public class Invoice
{
    public int InvoiceId { get; set; }
    public DateTime? DateIssued { get; set; }
    public string? Method {  get; set; }
    public string? SellerDetails { get; set; }
}