using System.Text.Json.Serialization;

namespace Core;

public class InvoiceItem
{
    public int ItemId { get; set; }
    public int  InvoiceId { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    
    [JsonIgnore] public Invoice? Invoice { get; set; }
}