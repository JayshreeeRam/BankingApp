using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly BankingContext _context;

        public PaymentRepository(BankingContext context)
        {
            _context = context;
        }
        Payment IPaymentRepository.Add(Payment Payment)
        {
            _context.Payments.Add(Payment);
            _context.SaveChanges();
            return Payment;
        }
         
        
        IEnumerable<Payment> IPaymentRepository.GetAll()
        {
            return _context.Payments.ToList();
        }
        Payment IPaymentRepository.GetById(int id)
        {
            return _context.Payments.Find(id);
        }
      
    }
}