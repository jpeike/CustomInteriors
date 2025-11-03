using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/emails")]
public class EmailsController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("", Name = "GetAllEmails")]
    public async Task<IEnumerable<EmailModel>> GetAllEmails()
    {
        return await _emailService.GetAllEmails();
    }

    [HttpGet("{id:int}", Name = "GetEmailById")]
    public async Task<EmailModel?> GetEmailById(int id)
    {
        return await _emailService.GetEmailById(id);
    }

    [HttpPost("", Name = "CreateEmail")]
    public async Task<EmailModel> CreateEmail([FromBody] EmailModel emailModel)
    {
        emailModel.CreatedOn = DateTime.UtcNow;
        return await _emailService.CreateEmail(emailModel);
    }

    [HttpPut("", Name = "UpdateEmail")]
    public async Task UpdateEmail([FromBody] EmailModel emailModel)
    { 
        await _emailService.UpdateEmail(emailModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteEmail")]
    public async Task<bool> DeleteEmail(int id)
    {
        return await _emailService.DeleteEmail(id);
    }
}

