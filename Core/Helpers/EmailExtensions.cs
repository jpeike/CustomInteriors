namespace Core;

public static class EmailExtensions
{
    public static EmailModel ToModel(this Email email) => new EmailModel
    {
        EmailID = email.EmailID,
        CustomerID = email.CustomerID,
        EmailAddress = email.EmailAddress,
        EmailType = email.EmailType,
        CreatedOn = email.CreatedOn
    };

    public static IEnumerable<EmailModel> ToModels(this IEnumerable<Email> emails)
    {
        return emails.Select(e => e.ToModel());
    }

    public static Email ToEntity(this EmailModel model) => new Email
    {
        EmailID = model.EmailID,
        CustomerID = model.CustomerID,
        EmailAddress = model.EmailAddress,
        EmailType = model.EmailType,
        CreatedOn = model.CreatedOn
    };
}
