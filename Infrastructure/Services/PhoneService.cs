using Core;

namespace Infrastructure;

public class PhoneService : IPhoneService
{
    private readonly IPhoneRepository _phoneRepository;

    public PhoneService(IPhoneRepository phoneRepository)
    {
        _phoneRepository = phoneRepository;
    }

    public async Task<PhoneModel> CreatePhone(PhoneModel phone)
    {
        Phone toReturn = await _phoneRepository.AddPhone(phone.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeletePhone(int id)
    {
        return await _phoneRepository.DeletePhone(id);
    }

    public async Task<IEnumerable<PhoneModel>> GetAllPhones()
    {
        var allPhones = await _phoneRepository.GetAllPhones();
        return allPhones.ToModels();
    }

    public async Task<PhoneModel?> GetPhoneById(int id)
    {
        Phone? phone = await _phoneRepository.GetPhoneById(id);
        return phone?.ToModel();
    }

    public async Task UpdatePhone(PhoneModel phone)
    {
        await _phoneRepository.UpdatePhone(phone.ToEntity());
    }
}

