using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("", Name = "GetAllPayments")]
    public async Task<IEnumerable<PaymentModel>> GetAllPayments()
    {
        return await _paymentService.GetAllPayments();
    }

    [HttpGet("{id:int}", Name = "GetPaymentById")]
    public async Task<PaymentModel?> GetPaymentById(int id)
    {
        return await _paymentService.GetPaymentById(id);
    }

    [HttpPost("", Name = "CreatePayment")]
    public async Task<PaymentModel> CreatePayment([FromBody] PaymentModel paymentModel)
    {
        PaymentModel payment = new()
        {
            PaymentId = paymentModel.PaymentId,
            InvoiceId = paymentModel.InvoiceId,
            PaymentDate = paymentModel.PaymentDate,
            AmountPaid = paymentModel.AmountPaid,
            Method = paymentModel.Method,
        };

        return await _paymentService.CreatePayment(payment);
    }

    [HttpPut("", Name = "UpdatePayment")]
    public async Task UpdatePayment([FromBody] PaymentModel paymentModel)
    { 
        await _paymentService.UpdatePayment(paymentModel);
    }

    [HttpDelete("{id:int}", Name = "DeletePayment")]
    public async Task<bool> DeletePayment(int id)
    {
        return await _paymentService.DeletePayment(id);
    }
}

