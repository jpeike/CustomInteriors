namespace Core;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAddressesByCustomerId(int CustomerId);
    Task<IEnumerable<Address>> GetAllAddresses();
    Task<Address> AddAddress(Address address);
    Task UpdateAddress(Address address);
    Task<bool> DeleteAddress(int AddressId);
}
