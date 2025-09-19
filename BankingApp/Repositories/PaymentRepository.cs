using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BankingContext _context;

        public PaymentRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetAll()
        {
            // Return all payments including self-transfers (BeneficiaryId can be null)
            return _context.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary) // EF will handle null automatically
                .ToList();
        }

        public Payment? GetById(int id)
        {
            return _context.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .FirstOrDefault(p => p.PaymentId == id);
        }

        public Payment Add(Payment payment)
        {
            // Ensure BeneficiaryId is null if self-transfer
           

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment Update(int id, Payment payment)
        {
            var existing = _context.Payments.Find(id);
            if (existing == null) return null!;

            // Handle self-transfer update
           

            _context.Entry(existing).CurrentValues.SetValues(payment);
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return true;
        }
    }
}
