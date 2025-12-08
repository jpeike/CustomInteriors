using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult<IEnumerable<EmailModel>>> GetAllEmails()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _emailService.GetAllEmails());
    }

    [HttpGet("{id:int}", Name = "GetEmailById")]
    public async Task<ActionResult<EmailModel?>> GetEmailById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _emailService.GetEmailById(id));
    }

    [HttpPost("", Name = "CreateEmail")]
    public async Task<ActionResult<EmailModel>> CreateEmail([FromBody] EmailModel emailModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        //emailModel.CreatedOn = DateTime.UtcNow;
        return Ok(await _emailService.CreateEmail(emailModel));
    }

    [HttpPut("", Name = "UpdateEmail")]
    public async Task<ActionResult> UpdateEmail([FromBody] EmailModel emailModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _emailService.UpdateEmail(emailModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteEmail")]
    public async Task<ActionResult<bool>> DeleteEmail(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _emailService.DeleteEmail(id));
    }
}