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
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int PostalCode { get; set; }
        public string? Country { get; set; }
        public string AddressType { get; set; } = null!;

        [JsonIgnore]
        public Customer Customer { get; set; } = null!;
    }
}
