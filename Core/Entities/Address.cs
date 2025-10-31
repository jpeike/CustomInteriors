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
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required int PostalCode { get; set; }
        public string? Country { get; set; }
        public required string AddressType { get; set; }

        public required Customer Customer { get; set; }
    }
}