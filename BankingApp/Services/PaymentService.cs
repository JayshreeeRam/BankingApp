using BankingApp.DTOs;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BankingContext _context;

        public PaymentService(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<PaymentDto> GetAll()
        {
            // Project payments to DTOs, safely handling nulls
            return _context.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .AsNoTracking()
                .Select(p => new PaymentDto
                {
                    ClientId = p.ClientId,
                    BeneficiaryId = p.BeneficiaryId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentStatus = p.PaymentStatus
                })
                .ToList();
        }

        public PaymentDto? GetById(int id)
        {
            var p = _context.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .AsNoTracking()
                .FirstOrDefault(x => x.PaymentId == id);

            if (p == null) return null;

            return new PaymentDto
            {
                ClientId = p.ClientId,
                BeneficiaryId = p.BeneficiaryId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentStatus = p.PaymentStatus
            };
        }
        

        public Payment Add(PaymentDto dto)
        {
            

            var payment = new Payment
            {
                ClientId = dto.ClientId,
                BeneficiaryId = dto.BeneficiaryId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                PaymentStatus = dto.PaymentStatus
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment? Update(int id, PaymentDto dto)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null) return null;

            payment.ClientId = dto.ClientId;
             payment.BeneficiaryId = dto.BeneficiaryId;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentStatus = dto.PaymentStatus;

            _context.SaveChanges();
            return payment;
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
