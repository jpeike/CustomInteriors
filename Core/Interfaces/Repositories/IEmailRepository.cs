namespace Core;

public interface IEmailRepository
{
    Task<Email?> GetEmailById(int id);
    Task<IEnumerable<Email>> GetAllEmails();
    Task<Email> AddEmail(Email email);
    Task UpdateEmail(Email email);
    Task<bool> DeleteEmail(int id);
}
