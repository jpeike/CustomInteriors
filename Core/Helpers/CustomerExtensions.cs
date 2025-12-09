namespace Core;

public static class CustomerExtensions
{
    public static CustomerModel ToModel(this Customer customer, bool includeDetails = false) => new CustomerModel
    {
        CustomerId = customer.CustomerId,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        CustomerType = customer.CustomerType,
        PrefferedContactMethod = customer.PrefferedContactMethod,
        CompanyName = customer.CompanyName,
        Status = customer.Status,
        CustomerNotes = customer.CustomerNotes,
        Addresses = includeDetails ? customer.Addresses?.Select(a => a.ToModel()).ToList() : null,
        Emails = includeDetails ? customer.Emails?.Select(e => e.ToModel()).ToList() : null,
        Phones = includeDetails ? customer.Phones?.Select(e => e.ToModel()).ToList() : null

    };

    public static IEnumerable<CustomerModel> ToModels(this IEnumerable<Customer> customer, bool includeDetails = false)
    {
        return customer.Select(u => u.ToModel(includeDetails));
    }

    public static Customer ToEntity(this CustomerCreateModel model) => new Customer
    {
        FirstName = model.FirstName,
        LastName = model.LastName,
        CustomerType = model.CustomerType,
        PrefferedContactMethod = model.PrefferedContactMethod,
        CompanyName = model.CompanyName,
        Status = model.Status,
        CustomerNotes = model.CustomerNotes
    };

    public static Customer UpdateToEntity(this CustomerUpdateModel model) => new Customer
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

