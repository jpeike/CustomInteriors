using System.Text.Json.Serialization;

namespace Core;

public class QuoteRequestModel
{
    public int QuoteId { get; set; }
    public int JobId { get; set; }
    public DateTime? RequestDate { get; set; }
    public string? DescriptionOfWork { get; set; }
    public decimal? EstimatedPrice { get; set; }

    //public required CustomerModel Customer { get; set; }
    // public JobModel Job { get; set; } //required
}