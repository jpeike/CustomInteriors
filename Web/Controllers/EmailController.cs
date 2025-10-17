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

    [HttpGet()]
    public async Task<IEnumerable<EmailModel>> GetAllEmails()
    {
        return await _emailService.GetAllEmails();
    }

    [HttpGet("{id:int}")]

    public async Task<EmailModel?> GetEmailById(int id)
    {
        return await _emailService.GetEmailById(id);
    }

    [HttpPost("Create")]
    public async Task<EmailModel> CreateEmail([FromBody] CreateEmailRequest emailModel)
    {
        EmailModel email = new()
        {
            EmailAddress = emailModel.EmailAddress,
            EmailType = emailModel.EmailType,
            CustomerId = emailModel.CustomerId,
            CreatedOn = DateTime.UtcNow
        };

        return await _emailService.CreateEmail(email);
    }

    [HttpPut("Update")]
    public async Task UpdateEmail([FromBody] EmailModel emailModel)
    { 
        await _emailService.UpdateEmail(emailModel);
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<bool> DeleteEmail(int id)
    {
        return await _emailService.DeleteEmail(id);
    }

}

