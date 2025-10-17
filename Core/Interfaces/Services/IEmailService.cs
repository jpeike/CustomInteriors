namespace Core;


public interface IEmailService
{
    Task<EmailModel?> GetEmailById(int id);
    Task<IEnumerable<EmailModel>> GetAllEmails();
    Task<EmailModel> CreateEmail(EmailModel email);
    Task UpdateEmail(EmailModel emailModel);
    Task<bool> DeleteEmail(int id);
}

