using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IClientRepository _clientRepo;

        public PaymentService(
            IPaymentRepository paymentRepo,
            IAccountRepository accountRepo,
            ITransactionRepository transactionRepo,
            IClientRepository clientRepo)
        {
            _paymentRepo = paymentRepo;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _clientRepo = clientRepo;
        }

        public IEnumerable<PaymentDto> GetAll()
        {
            return _paymentRepo.GetAll().Select(p => MapToDto(p)).ToList();
        }

        public PaymentDto? GetById(int id)
        {
            var payment = _paymentRepo.GetById(id);
            return payment == null ? null : MapToDto(payment);
        }

        public PaymentDto Add(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                PaymentId = dto.PaymentId,
                ClientId = dto.ClientId,
                BeneficiaryId = dto.BeneficiaryId,
                Amount = dto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Pending
            };

            var created = _paymentRepo.Add(payment);
            return MapToDto(created);
        }

        public PaymentDto? ApprovePayment(int id)
        {
            var payment = _paymentRepo.GetById(id);
            if (payment == null) return null;

            // Get sender's account
            var senderAccount = _accountRepo.GetByClientId(payment.ClientId);
            if (senderAccount == null || senderAccount.Balance < payment.Amount)
                return null; 

            // Get receiver's account
            var receiverAccount = _accountRepo.GetByClientId(payment.BeneficiaryId);
            if (receiverAccount == null) return null;

            // Deduct & credit
            senderAccount.Balance -= payment.Amount;
            receiverAccount.Balance += payment.Amount;

            _accountRepo.Update(senderAccount.AccountId, senderAccount);
            _accountRepo.Update(receiverAccount.AccountId, receiverAccount);

            payment.PaymentStatus = PaymentStatus.Approved;
            _paymentRepo.Update(payment.PaymentId, payment);

            // Fetch clients for names
            var sender = _clientRepo.GetById(payment.ClientId);
            var receiver = _clientRepo.GetById(payment.BeneficiaryId);

            // Create debit transaction
            _transactionRepo.Add(new Transaction
            {
                AccountId = senderAccount.AccountId,
                Amount = payment.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Debit,
                TransactionStatus = TransactionStatus.Success,
                SenderId = payment.ClientId,
                ReceiverId = payment.BeneficiaryId,
                SenderName = sender?.Name ?? "",
                ReceiverName = receiver?.Name ?? ""
            });

            // Create credit transaction
            _transactionRepo.Add(new Transaction
            {
                AccountId = receiverAccount.AccountId,
                Amount = payment.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Credit,
                TransactionStatus = TransactionStatus.Success,
                SenderId = payment.ClientId,
                ReceiverId = payment.BeneficiaryId,
                SenderName = sender?.Name ?? "",
                ReceiverName = receiver?.Name ?? ""
            });

            return MapToDto(payment);
        }

        public PaymentDto? Update(int id, PaymentDto dto)
        {
            var payment = _paymentRepo.GetById(id);
            if (payment == null) return null;

            if (payment.PaymentStatus == PaymentStatus.Approved)
                throw new InvalidOperationException("Cannot update an approved payment.");
            payment.PaymentId = dto.PaymentId;
            payment.ClientId = dto.ClientId;
            payment.BeneficiaryId = dto.BeneficiaryId;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentStatus = dto.PaymentStatus;

            var updated = _paymentRepo.Update(id, payment);
            return MapToDto(updated);
        }

        //public bool Delete(int id)
        //{
        //    return _paymentRepo.Delete(id);
        //}

        private PaymentDto MapToDto(Payment p)
        {
            return new PaymentDto
            {
                PaymentId=p.PaymentId,
                ClientId = p.ClientId,
                BeneficiaryId = p.BeneficiaryId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentStatus = p.PaymentStatus
            };
        }
    }
}
