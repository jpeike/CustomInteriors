namespace Core;

public static class InvoiceItemExtensions
{
    public static InvoiceItemModel ToModel(this InvoiceItem entity) => new InvoiceItemModel
    {
        ItemId = entity.ItemId,
        InvoiceId = entity.InvoiceId,
        Description = entity.Description,
        Amount = entity.Amount,
        Price = entity.Price,
        // Invoice = entity.Invoice.ToModel()
    };

    public static IEnumerable<InvoiceItemModel> ToModels(this IEnumerable<InvoiceItem> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static InvoiceItem ToEntity(this InvoiceItemModel model) => new InvoiceItem
    {
        ItemId = model.ItemId,
        InvoiceId = model.InvoiceId,
        Description = model.Description,
        Amount = model.Amount,
        Price = model.Price,
        // Invoice = model.Invoice.ToEntity()
    };
}