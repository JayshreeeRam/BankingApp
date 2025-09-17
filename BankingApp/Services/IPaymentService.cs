using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
        Payment Add(Payment Payment);
       
    }
}
