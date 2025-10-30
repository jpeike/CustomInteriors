using System.Text.Json.Serialization;

namespace Core;

public class JobInvoice
{
    public int JobId { get; set; }
    public int InvoiceId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public float PercentageOfInvoice { get; set; }

    [JsonIgnore] Job Job { get; set; }
    [JsonIgnore] Invoice Invoice { get; set; }
}