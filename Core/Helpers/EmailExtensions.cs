namespace Core;

public static class EmailExtensions
{
    public static EmailModel ToModel(this Email email) => new EmailModel
    {
        EmailId = email.EmailId,
        CustomerId = email.CustomerId,
        EmailAddress = email.EmailAddress,
        EmailType = email.EmailType,
        // CreatedOn = email.CreatedOn,
        // Customer = email.Customer.ToModel()
    };

    public static IEnumerable<EmailModel> ToModels(this IEnumerable<Email> emails)
    {
        return emails.Select(e => e.ToModel());
    }

    public static Email ToEntity(this EmailModel model) => new Email
    {
        EmailId = model.EmailId,
        CustomerId = model.CustomerId,
        EmailAddress = model.EmailAddress,
        EmailType = model.EmailType,
        CreatedOn = DateTime.UtcNow,
        // CreatedOn = model.CreatedOn,
        // Customer = model.Customer.ToEntity()
    };
}
