namespace Core;

public static class JobInvoiceExtensions
{
    public static JobInvoiceModel ToModel(this JobInvoice entity) => new JobInvoiceModel
    {
        JobId = entity.JobId,
        InvoiceId = entity.InvoiceId,
        CreatedDate = entity.CreatedDate,
        PercentageOfInvoice = entity.PercentageOfInvoice,
        // Job = entity.Job.ToModel(),
        // Invoice = entity.Invoice.ToModel()
    };

    public static IEnumerable<JobInvoiceModel> ToModels(this IEnumerable<JobInvoice> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static JobInvoice ToEntity(this JobInvoiceModel model) => new JobInvoice
    {
        JobId = model.JobId,
        InvoiceId = model.InvoiceId,
        CreatedDate = model.CreatedDate,
        PercentageOfInvoice = model.PercentageOfInvoice,
        // Job = model.Job.ToEntity(),
        // Invoice = model.Invoice.ToEntity()
    };
}