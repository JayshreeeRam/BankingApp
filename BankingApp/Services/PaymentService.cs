using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BankingContext _repo;

        public PaymentService(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<PaymentDto> GetAll()
        {
            return _repo.Payments
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
            var p = _repo.Payments
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

        public PaymentDto Add(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                ClientId = dto.ClientId,
                BeneficiaryId = dto.BeneficiaryId,
                Amount = dto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Pending
            };

            _repo.Payments.Add(payment);
            _repo.SaveChanges();

            return new PaymentDto
            {
                ClientId = payment.ClientId,
                BeneficiaryId = payment.BeneficiaryId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus
            };
        }

        public PaymentDto? ApprovePayment(int id)
        {
            var payment = _repo.Payments
                .Include(p => p.Client)               // sender client
                    .ThenInclude(c => c.User)        // sender user
                .Include(p => p.Beneficiary)
                    .ThenInclude(b => b.Client)      // receiver client
                        .ThenInclude(c => c.User)    // receiver user
                .FirstOrDefault(p => p.PaymentId == id);

            if (payment == null)
                return null;

            // Sender's account
            var senderAccount = _repo.Accounts
                .FirstOrDefault(a => a.AccountNumber == payment.Client.AccountNo);
            if (senderAccount == null || senderAccount.Balance < payment.Amount)
                return null; // insufficient funds

            // Receiver's account
            var receiverAccount = _repo.Accounts
                .FirstOrDefault(a => a.AccountNumber == payment.Beneficiary.Client.AccountNo);
            if (receiverAccount == null)
                return null;

            // Deduct & credit
            senderAccount.Balance -= payment.Amount;
            receiverAccount.Balance += payment.Amount;

            payment.PaymentStatus = PaymentStatus.Approved;

            // Get sender and receiver names dynamically
            string senderName = payment.Client.User?.Username ?? payment.Client.Name;
            string receiverName = payment.Beneficiary.Client.User?.Username ?? payment.Beneficiary.Client.Name;

            // Create debit transaction (sender -> receiver)
            _repo.Transactions.Add(new Transaction
            {
                AccountId = senderAccount.AccountId,
                Amount = payment.Amount,
             TransactionDate = DateTime.UtcNow.ToLocalTime(),
                TransactionType = TransactionType.Debit,
                TransactionStatus = TransactionStatus.Success,
                SenderId = payment.ClientId,
                ReceiverId = payment.BeneficiaryId,
                SenderName = senderName,
                ReceiverName = receiverName
            });

            // Create credit transaction (receiver receives)
            _repo.Transactions.Add(new Transaction
            {
                AccountId = receiverAccount.AccountId,
                Amount = payment.Amount,
             TransactionDate = DateTime.UtcNow.ToLocalTime(),
                TransactionType = TransactionType.Credit,
                TransactionStatus = TransactionStatus.Success,
                SenderId = payment.ClientId,
                ReceiverId = payment.BeneficiaryId,
                SenderName = senderName,
                ReceiverName = receiverName
            });

            _repo.SaveChanges();

            return new PaymentDto
            {
                ClientId = payment.ClientId,
                BeneficiaryId = payment.BeneficiaryId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus
            };
        }






        public PaymentDto? Update(int id, PaymentDto dto)
        {
            var payment = _repo.Payments.Find(id);
            if (payment == null) return null;

            // Optional: Check if payment is already approved
            if (payment.PaymentStatus == PaymentStatus.Approved)
                throw new InvalidOperationException("Cannot update an approved payment.");

            payment.ClientId = dto.ClientId;
            payment.BeneficiaryId = dto.BeneficiaryId;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentStatus = dto.PaymentStatus;

            _repo.SaveChanges();

            return new PaymentDto
            {
                ClientId = payment.ClientId,
                BeneficiaryId = payment.BeneficiaryId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus
            };
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
