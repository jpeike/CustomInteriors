using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _dbContext;

    public PaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Payment?> GetPaymentById(int id)
    {
        return await _dbContext.Payments.FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetAllPayments()
    {
        return await _dbContext.Payments.ToListAsync();
    }

    public async Task<Payment> AddPayment(Payment payment)
    {
        _dbContext.Payments.Add(payment);
        await _dbContext.SaveChangesAsync();
        return payment;
    }

    public async Task UpdatePayment(Payment payment)
    {
        _dbContext.Payments.Update(payment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeletePayment(int id)
    {
        Payment? payment = await _dbContext.Payments.FindAsync(id);
        if (payment != null)
        {
            _dbContext.Remove(payment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}