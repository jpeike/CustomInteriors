namespace Core
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int PostalCode { get; set; }
        public string? Country { get; set; }
        public string AddressType { get; set; } = null!;
    }
}
