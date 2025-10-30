using System.Text.Json.Serialization;

namespace Core;

public class QuoteRequest
{
    public int QuoteId { get; set; }
    public int CustomerId { get; set; }
    public DateTime? RequestDate { get; set; }
    public string? DescriptionOfWork { get; set; }
    public decimal? EstimatedPrice { get; set; }
    
    [JsonIgnore] Customer Customer { get; set; }
}