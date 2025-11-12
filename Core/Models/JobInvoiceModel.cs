using System.Text.Json.Serialization;

namespace Core;

public class JobInvoiceModel
{
    public int JobId { get; set; }
    public int InvoiceId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public float? PercentageOfInvoice { get; set; }

   // public JobModel Job { get; set; }
   // public InvoiceModel Invoice { get; set; }
}