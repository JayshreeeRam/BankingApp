using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IPaymentRepository _PaymentRepository;

        public PaymentService(IPaymentRepository PaymentRepository)
        {
            _PaymentRepository = PaymentRepository;
        }
        public Payment Add(Payment Payment)
        {
            return _PaymentRepository.Add(Payment);
        }
       
        public IEnumerable<Payment> GetAll()
        {
            return _PaymentRepository.GetAll();
        }
        public Payment GetById(int id)
        {
            return _PaymentRepository.GetById(id);
        }
      
    }
}


   