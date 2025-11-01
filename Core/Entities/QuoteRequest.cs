using System.Text.Json.Serialization;

namespace Core;

public class QuoteRequest
{
    public int QuoteId { get; set; }
    public int JobId { get; set; }
    public DateTime? RequestDate { get; set; }
    public string? DescriptionOfWork { get; set; }
    public decimal? EstimatedPrice { get; set; }

    [JsonIgnore] public Job Job { get; set; } = null!;
}