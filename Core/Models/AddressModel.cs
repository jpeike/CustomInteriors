using System.Text.Json.Serialization;

namespace Core
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required int PostalCode { get; set; }
        public string? Country { get; set; }
        public required string AddressType { get; set; }

       public CustomerModel Customer { get; set; } //required
    }
}
