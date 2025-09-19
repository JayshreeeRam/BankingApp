using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment? GetById(int id);
        Payment Add(Payment payment);
        Payment Update(int id, Payment payment);
        bool Delete(int id);
    }
}
