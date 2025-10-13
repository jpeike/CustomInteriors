using Core;

namespace Infrastructure.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<AddressModel> CreateAddress(AddressModel address)
    {
        Address toReturn = await _addressRepository.AddAddress(address.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteAddress(int addressId)
    {
        return await _addressRepository.DeleteAddress(addressId);
    }

    public async Task<IEnumerable<AddressModel>> GetAllAddresses()
    {
        var allAddresses = await _addressRepository.GetAllAddresses();
        return allAddresses.ToModels();
    }

    public async Task<IEnumerable<AddressModel>> GetAddressesByCustomerId(int customerId)
    {
        var addresses = await _addressRepository.GetAddressesByCustomerId(customerId);
        
        return addresses.ToModels();
    }

    public async Task UpdateAddress(AddressModel addressModel)
    {
        await _addressRepository.UpdateAddress(addressModel.ToEntity());
    }
}

