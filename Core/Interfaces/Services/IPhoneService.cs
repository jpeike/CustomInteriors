namespace Core;

public interface IPhoneService
{
    Task<PhoneModel?> GetPhoneById(int id);
    Task<IEnumerable<PhoneModel>> GetAllPhones();
    Task<PhoneModel> AddPhone(PhoneModel phone);
    Task UpdatePhone(PhoneModel phone);
    Task<bool> DeletePhone(int id);
}
