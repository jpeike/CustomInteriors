using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web;

[ApiController]
[Authorize(Policy = Policies.AdminOnly)]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("", Name = "GetAllPayments")]
    public async Task<ActionResult<IEnumerable<PaymentModel>>> GetAllPayments()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _paymentService.GetAllPayments());
    }

    [HttpGet("{id:int}", Name = "GetPaymentById")]
    public async Task<ActionResult<PaymentModel?>> GetPaymentById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _paymentService.GetPaymentById(id));
    }

    [HttpPost("", Name = "CreatePayment")]
    public async Task<ActionResult<PaymentModel>> CreatePayment([FromBody] PaymentModel paymentModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _paymentService.CreatePayment(paymentModel));
    }

    [HttpPut("", Name = "UpdatePayment")]
    public async Task<ActionResult> UpdatePayment([FromBody] PaymentModel paymentModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _paymentService.UpdatePayment(paymentModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeletePayment")]
    public async Task<ActionResult<bool>> DeletePayment(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _paymentService.DeletePayment(id));
    }
}