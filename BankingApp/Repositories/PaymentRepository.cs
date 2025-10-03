using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BankingContext _repo;

        public PaymentRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Payment> GetAll()
        {
             return _repo.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary) 
                .ToList();
        }

        public Payment? GetById(int id)
        {
            return _repo.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .FirstOrDefault(p => p.PaymentId == id);
        }



        public Payment Add(Payment payment)
        {
            
            _repo.Payments.Add(payment);
            _repo.SaveChanges();
            return payment;
        }

        //public Payment Update(int id, Payment payment)
        //{
        //    var existing = _repo.Payments.Find(id);
        //    if (existing == null) return null!;

        //    _repo.Entry(existing).CurrentValues.SetValues(payment);
        //    _repo.SaveChanges();
        //    return existing;
        //}

        public Payment Update(int id, Payment payment)
        {
            var existing = _repo.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .FirstOrDefault(p => p.PaymentId == id);

            if (existing == null) return null!;

            _repo.Entry(existing).CurrentValues.SetValues(payment);
            _repo.SaveChanges();  // << This should already save changes
            return existing;
        }






        //public bool Delete(int id)
        //{
        //    var payment = _repo.Payments.Find(id);
        //    if (payment == null) return false;

        //    _repo.Payments.Remove(payment);
        //    _repo.SaveChanges();
        //    return true;
        //}
    }
}
