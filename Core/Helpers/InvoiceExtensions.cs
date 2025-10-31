namespace Core;

public class InvoiceExtensions
{
    public static CustomerModel ToModel(this Customer customer) => new CustomerModel
    {
        CustomerId = customer.CustomerId,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        CustomerType = customer.CustomerType,
        PrefferedContactMethod = customer.PrefferedContactMethod,
        CompanyName = customer.CompanyName,
        Status = customer.Status,
        CustomerNotes = customer.CustomerNotes,
        Emails = customer.Emails.Select(x =>x.ToModel()).ToList()
    };

    public static IEnumerable<CustomerModel> ToModels(this IEnumerable<Customer> customer)
    {
        return customer.Select(u => u.ToModel());
    }

    public static Customer ToEntity(this CustomerModel model) => new Customer
    {
        CustomerId = model.CustomerId,
        FirstName = model.FirstName,
        LastName = model.LastName,
        CustomerType = model.CustomerType,
        PrefferedContactMethod = model.PrefferedContactMethod,
        CompanyName = model.CompanyName,
        Status = model.Status,
        CustomerNotes = model.CustomerNotes
    };
}