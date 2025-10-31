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

    [JsonIgnore] public Customer Customer { get; set; }
    [JsonIgnore] public ICollection<JobInvoice>? JobInvoices { get; set; } = new List<JobInvoice>();
    [JsonIgnore] public ICollection<QuoteRequest>? QuoteRequests { get; set; } = new List<QuoteRequest>();
    [JsonIgnore] public ICollection<JobAssignment>? JobAssignments { get; set; } = new List<JobAssignment>();
}