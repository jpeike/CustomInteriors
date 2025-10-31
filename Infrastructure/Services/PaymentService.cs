using Core;

namespace Infrastructure;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentModel> CreatePayment(PaymentModel payment)
    {
        Payment toReturn = await _paymentRepository.AddPayment(payment.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeletePayment(int id)
    {
        return await _paymentRepository.DeletePayment(id);
    }

    public async Task<IEnumerable<PaymentModel>> GetAllPayments()
    {
        var allPayments = await _paymentRepository.GetAllPayments();
        return allPayments.ToModels();
    }

    public async Task<PaymentModel?> GetPaymentById(int id)
    {
        Payment? payment = await _paymentRepository.GetPaymentById(id);
        return payment?.ToModel();
    }

    public async Task UpdatePayment(PaymentModel payment)
    {
        await _paymentRepository.UpdatePayment(payment.ToEntity());
    }
}

