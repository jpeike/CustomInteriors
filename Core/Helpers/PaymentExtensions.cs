namespace Core;

public static class PaymentExtensions
{
    public static PaymentModel ToModel(this Payment entity) => new PaymentModel
    {
        PaymentId = entity.PaymentId,
        InvoiceId = entity.InvoiceId,
        PaymentDate = entity.PaymentDate,
        AmountPaid = entity.AmountPaid,
        Method = entity.Method,
        Invoice = entity.Invoice.ToModel()
    };

    public static IEnumerable<PaymentModel> ToModels(this IEnumerable<Payment> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static Payment ToEntity(this PaymentModel model) => new Payment
    {
        PaymentId = model.PaymentId,
        InvoiceId = model.InvoiceId,
        PaymentDate = model.PaymentDate,
        AmountPaid = model.AmountPaid,
        Method = model.Method,
        Invoice = model.Invoice.ToEntity()
    };
}