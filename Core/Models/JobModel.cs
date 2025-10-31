using System.Text.Json.Serialization;

namespace Core;

public class JobModel
{
    public int JobId { get; set; }
    public int CustomerId { get; set; }
    public string? JobDescription { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; }

   public CustomerModel Customer { get; set; } //required
   public ICollection<JobInvoiceModel>? JobInvoices { get; set; } = new List<JobInvoiceModel>();
   public ICollection<QuoteRequestModel>? QuoteRequests { get; set; } = new List<QuoteRequestModel>();
   public ICollection<JobAssignmentModel>? JobAssignments { get; set; } = new List<JobAssignmentModel>();
}