namespace Core;

public static class CustomerWithFKsExtensions
{
    public static CustomerWithFKsModel FKsToModel(this Customer customer) => new CustomerWithFKsModel
    {
        CustomerId = customer.CustomerId,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        CustomerType = customer.CustomerType,
        PrefferedContactMethod = customer.PrefferedContactMethod,
        CompanyName = customer.CompanyName,
        Status = customer.Status,
        CustomerNotes = customer.CustomerNotes,
        Addresses = customer.Addresses
    };

    public static IEnumerable<CustomerWithFKsModel> FksToModels(this IEnumerable<Customer> customer)
    {
        return customer.Select(u => u.FKsToModel());
    }

    public static Customer FKsToEntity(this CustomerWithFKsModel model) => new Customer
    {
        CustomerId = model.CustomerId,
        FirstName = model.FirstName,
        LastName = model.LastName,
        CustomerType = model.CustomerType,
        PrefferedContactMethod = model.PrefferedContactMethod,
        CompanyName = model.CompanyName,
        Status = model.Status,
        CustomerNotes = model.CustomerNotes,
        Addresses = model.Addresses
    };
}

