namespace Core;

public interface IPhoneRepository
{
    Task<Phone?> GetPhoneById(int id);
    Task<IEnumerable<Phone>> GetAllPhones();
    Task<Phone> AddPhone(Phone phone);
    Task UpdatePhone(Phone phone);
    Task<bool> DeletePhone(int id);
}
