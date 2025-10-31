using System.Text.Json.Serialization;

namespace Core;

public class InvoiceItemModel
{
    public int ItemId { get; set; }
    public int InvoiceId { get; set; }
    public string? Description { get; set; }
    public int? Amount { get; set; }
    public decimal? Price { get; set; }

   public required InvoiceModel Invoice { get; set; }
}