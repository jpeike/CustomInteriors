namespace Core;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentById(int id);
    Task<IEnumerable<Payment>> GetAllPayments();
    Task<Payment> AddPayment(Payment payment);
    Task UpdatePayment(Payment payment);
    Task<bool> DeletePayment(int id);
}
