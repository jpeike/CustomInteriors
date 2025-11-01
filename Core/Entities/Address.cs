using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core
{
    public class Address
    {
        public required int AddressId { get; set; }
        public required int CustomerId { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required int PostalCode { get; set; }
        public string? Country { get; set; }
        public required string AddressType { get; set; }

        // navigational properties should be not nullable and forced to null if the fk is required
        // or nullable if the fk is nullable
        [JsonIgnore] public Customer Customer { get; set; } = null!;
    }
}