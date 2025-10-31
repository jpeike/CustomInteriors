using System.Text.Json.Serialization;

namespace Core;

public class QuoteRequestModel
{
    public int QuoteId { get; set; }
    public int CustomerId { get; set; }
    public DateTime? RequestDate { get; set; }
    public string? DescriptionOfWork { get; set; }
    public decimal? EstimatedPrice { get; set; }

    public required CustomerModel Customer { get; set; }
    public required JobModel Job { get; set; }
}