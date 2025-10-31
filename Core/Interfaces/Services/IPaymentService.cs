namespace Core;

public interface IPaymentService
{
    Task<PaymentModel?> GetPaymentById(int id);
    Task<IEnumerable<PaymentModel>> GetAllPayments();
    Task<PaymentModel> CreatePayment(PaymentModel payment);
    Task UpdatePayment(PaymentModel payment);
    Task<bool> DeletePayment(int id);
}
