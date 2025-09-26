namespace Core;

public interface IAddressService
{
    Task<IEnumerable<AddressModel>> GetAddressesByCustomerId(int CustomerId);
    Task<AddressModel> CreateAddress(Address address);

    Task<IEnumerable<AddressModel>> GetAllAddresses();
    Task UpdateAddress(AddressModel addressModel);
    Task<bool> DeleteAddress(int AddressId);
}
