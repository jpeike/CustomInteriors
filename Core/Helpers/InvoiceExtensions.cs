namespace Core;

public static class InvoiceExtensions
{
    public static InvoiceModel ToModel(this Invoice entity) => new InvoiceModel
    {
        InvoiceId = entity.InvoiceId,
        DateIssued = entity.DateIssued,
        Method = entity.Method,
        SellerDetails = entity.SellerDetails,
        // JobInvoices = entity.JobInvoices.Select(j => j.ToModel()).ToList(),
        // InvoiceItems = entity.InvoiceItems.Select(i => i.ToModel()).ToList(),
        // Payments = entity.Payments.Select(p => p.ToModel()).ToList(),
    };

    public static IEnumerable<InvoiceModel> ToModels(this IEnumerable<Invoice> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static Invoice ToEntity(this InvoiceModel model) => new Invoice
    {
        InvoiceId = model.InvoiceId,
        DateIssued = model.DateIssued,
        Method = model.Method,
        SellerDetails = model.SellerDetails,
        // JobInvoices = model.JobInvoices.Select(j => j.ToEntity()).ToList(),
        // InvoiceItems = model.InvoiceItems.Select(i => i.ToEntity()).ToList(),
        // Payments = model.Payments.Select(p => p.ToEntity()).ToList(),
    };
}