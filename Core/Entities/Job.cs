using System.Text.Json.Serialization;

namespace Core;

public class Job
{
    public int JobId { get; set; }
    public int CustomerId { get; set; }
    public string? JobDescription { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; }
    
    [JsonIgnore] Customer Customer { get; set; }
}