using System.Net;

namespace Core;

    public static class AddressExtensions
    {
        public static AddressModel ToModel(this Address address) => new AddressModel
        {
            AddressId = address.AddressId,
            CustomerId = address.CustomerId,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
            AddressType = address.AddressType,
            Customer = address.Customer.ToModel()
        };

        public static IEnumerable<AddressModel> ToModels(this IEnumerable<Address> addresses)
        {
            return addresses.Select(a => a.ToModel());
        }

        public static Address ToEntity(this AddressModel model) => new Address
        {
            AddressId = model.AddressId,
            CustomerId = model.CustomerId,
            Street = model.Street,
            City = model.City,
            State = model.State,
            PostalCode = model.PostalCode,
            Country = model.Country,
            AddressType = model.AddressType,
            Customer = model.Customer.ToEntity()
        };
    }

