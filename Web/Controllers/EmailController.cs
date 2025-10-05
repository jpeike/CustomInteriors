using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/[controller]")]
public class EmailsController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    [ActionName("GetAllEmails")]
    public async Task<IEnumerable<EmailModel>> GetAllEmails()
    {
        return await _emailService.GetAllEmails();
    }

    [HttpGet("{id:int}")]
    [ActionName("GetEmailById")]
    public async Task<EmailModel?> GetEmailById(int id)
    {
        return await _emailService.GetEmailById(id);
    }

    [HttpPost]
    [ActionName("CreateEmail")]
    public async Task<EmailModel> CreateEmail([FromBody] CreateEmailRequest emailModel)
    {
        var email = new Email
        {
            EmailAddress = emailModel.EmailAddress,
            EmailType = emailModel.EmailType,
            CustomerId = emailModel.CustomerId,
            CreatedOn = DateTime.UtcNow
        };

        return await _emailService.CreateEmail(email);
    }

    [HttpPut("{id:int}")]
    [ActionName("UpdateEmail")]
    public async Task UpdateEmail(int id, [FromBody] EmailModel emailModel)
    {
        // you can enforce id match here if desired:
        if (emailModel.EmailID != null && emailModel.EmailID != id)
        {
            throw new ArgumentException("Email ID mismatch between route and body");
        }

        await _emailService.UpdateEmail(emailModel);
    }

    [HttpDelete("{id:int}")]
    [ActionName("DeleteEmail")]
    public async Task<bool> DeleteEmail(int id)
    {
        return await _emailService.DeleteEmail(id);
    }
}
