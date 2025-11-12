namespace Core;

public interface IPhoneService
{
    Task<PhoneModel?> GetPhoneById(int id);
    Task<IEnumerable<PhoneModel>> GetAllPhones();
    Task<PhoneModel> CreatePhone(PhoneModel phone);
    Task UpdatePhone(PhoneModel phone);
    Task<bool> DeletePhone(int id);
}
