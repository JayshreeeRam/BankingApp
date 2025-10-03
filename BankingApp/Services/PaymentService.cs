using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IBeneficiaryRepository beneficiaryRepository;

        public PaymentService(
            IPaymentRepository paymentRepo,
            IAccountRepository accountRepo,
            ITransactionRepository transactionRepo,
            IClientRepository clientRepo,
            IBeneficiaryRepository beneficiaryRepository)
        {
            _paymentRepo = paymentRepo;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _clientRepo = clientRepo;
            this.beneficiaryRepository = beneficiaryRepository;
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
                //PaymentId = dto.PaymentId,
                ClientId = dto.ClientId,
                BeneficiaryId = dto.BeneficiaryId,
                Amount = dto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Pending
            };

            var created = _paymentRepo.Add(payment);
            return MapToDto(created);
        }
        // In your service class
        public PaymentDto RejectPayment(int id, string remark)
        {
            var payment = _paymentRepo.GetById(id);

            if (payment == null)
                throw new ArgumentException($"Payment with id {id} not found.");

            if (payment.PaymentStatus != PaymentStatus.Pending)
                throw new InvalidOperationException($"Cannot reject payment with status {payment.PaymentStatus}. Only pending payments can be rejected.");

            payment.PaymentStatus = PaymentStatus.Rejected;
            payment.remark = remark;
            payment.PaymentDate = DateTime.UtcNow;

            _paymentRepo.Update(payment.PaymentId, payment);
            //_paymentRepo.SaveChanges();

            return MapToDto(payment);
        }
        public PaymentDto ApprovePayment(int id)
        {
            var payment = _paymentRepo.GetById(id);
            Console.WriteLine($"[Service] Approving payment with id: {id}");

            if (payment == null)
            {
                throw new ArgumentException($"Payment with id {id} not found.");
            }
            Console.WriteLine($"[Debug] Payment: ClientId={payment.ClientId}, BeneficiaryId={payment.BeneficiaryId}");
            // Get the beneficiary to find the actual client ID
            var beneficiary = beneficiaryRepository.GetById(payment.BeneficiaryId);
            if (beneficiary == null)
            {
                throw new InvalidOperationException($"Beneficiary with id {payment.BeneficiaryId} not found.");
            }

            var senderAccount = _accountRepo.GetByClientId(payment.ClientId);
            if (senderAccount == null)
            {
                throw new InvalidOperationException($"Sender account for client {payment.ClientId} not found.");
            }

            if (senderAccount.Balance < payment.Amount)
            {
                throw new InvalidOperationException($"Insufficient funds. Balance: {senderAccount.Balance}, Required: {payment.Amount}");
            }

            // Use the client ID from the beneficiary, not the beneficiary ID directly
            var receiverAccount = _accountRepo.GetByClientId(beneficiary.ClientId);
            if (receiverAccount == null)
            {
                throw new InvalidOperationException($"Receiver account for beneficiary {payment.BeneficiaryId} (client {beneficiary.ClientId}) not found.");
            }

            // Rest of your approval logic...
            senderAccount.Balance -= payment.Amount;
            receiverAccount.Balance += payment.Amount;

            Console.WriteLine($"[ApprovePayment] Updated balances: Sender({senderAccount.AccountId})={senderAccount.Balance}, Receiver({receiverAccount.AccountId})={receiverAccount.Balance}");

            payment.PaymentStatus = PaymentStatus.Approved;
            _accountRepo.Update(senderAccount.AccountId, senderAccount);
            _accountRepo.Update(receiverAccount.AccountId, receiverAccount);
            _paymentRepo.Update(payment.PaymentId, payment);

            // Create transactions
            var sender = _clientRepo.GetById(payment.ClientId);
            var receiver = _clientRepo.GetById(beneficiary.ClientId); // Use beneficiary's client ID

            _transactionRepo.Add(new Transaction
            {
                AccountId = senderAccount.AccountId,
                Amount = payment.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Debit,
                TransactionStatus = TransactionStatus.Success,
                SenderId = payment.ClientId,
                ReceiverId = beneficiary.ClientId, // Use the actual client ID
                SenderName = sender?.Name ?? "",
                ReceiverName = receiver?.Name ?? ""
            });

            _transactionRepo.Add(new Transaction
            {
                AccountId = receiverAccount.AccountId,
                Amount = payment.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Credit,
                TransactionStatus = TransactionStatus.Success,
                SenderId = payment.ClientId,
                ReceiverId = beneficiary.ClientId, // Use the actual client ID
                SenderName = sender?.Name ?? "",
                ReceiverName = receiver?.Name ?? ""
            });

            Console.WriteLine($"[ApprovePayment] Payment {id} approved successfully.");
            return MapToDto(payment);
        }


        public PaymentDto? Update(int id, PaymentDto dto)
        {
            var payment = _paymentRepo.GetById(id);
            if (payment == null) return null;

            if (payment.PaymentStatus == PaymentStatus.Approved)
                throw new InvalidOperationException("Cannot update an approved payment.");
            //payment.PaymentId = dto.PaymentId;
            payment.ClientId = dto.ClientId;
            payment.BeneficiaryId = dto.BeneficiaryId;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentStatus = dto.PaymentStatus;
            payment.remark = dto.Remark;

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
                PaymentStatus = p.PaymentStatus,
                Remark = p.remark
            };
        }
    }
}
