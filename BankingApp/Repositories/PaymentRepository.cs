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
            // Return all payments including self-transfers (BeneficiaryId can be null)
            return _repo.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary) // EF will handle null automatically
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
            // Ensure BeneficiaryId is null if self-transfer
           

            _repo.Payments.Add(payment);
            _repo.SaveChanges();
            return payment;
        }

        public Payment Update(int id, Payment payment)
        {
            var existing = _repo.Payments.Find(id);
            if (existing == null) return null!;

            // Handle self-transfer update
           

            _repo.Entry(existing).CurrentValues.SetValues(payment);
            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var payment = _repo.Payments.Find(id);
            if (payment == null) return false;

            _repo.Payments.Remove(payment);
            _repo.SaveChanges();
            return true;
        }
    }
}
