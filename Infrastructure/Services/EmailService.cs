using Core;

namespace Infrastructure;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;

    public EmailService(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }

    public async Task<EmailModel> CreateEmail(EmailModel email)
    {
        Email toReturn = await _emailRepository.AddEmail(email.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteEmail(int id)
    {
        return await _emailRepository.DeleteEmail(id);
    }

    public async Task<IEnumerable<EmailModel>> GetAllEmails()
    {
        var allEmails = await _emailRepository.GetAllEmails();
        return allEmails.ToModels();
    }

    public async Task<EmailModel?> GetEmailById(int id)
    {
        Email? email = await _emailRepository.GetEmailById(id);
        if (email == null)
        {
            return null;
        }
        return email.ToModel();
    }

    public async Task UpdateEmail(EmailModel emailModel)
    {
        await _emailRepository.UpdateEmail(emailModel.ToEntity());
    }
}

