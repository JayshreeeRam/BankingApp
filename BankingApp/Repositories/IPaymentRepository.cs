using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
        Payment Add(Payment Payment);
       
    }
}
