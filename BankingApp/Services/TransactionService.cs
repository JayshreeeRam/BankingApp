using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public TransactionService(
            ITransactionRepository transactionRepo,
            IClientRepository clientRepo,
            IEmployeeRepository employeeRepo)
        {
            _transactionRepo = transactionRepo;
            _clientRepo = clientRepo;
            _employeeRepo = employeeRepo;
        }

        public IEnumerable<TransactionDto> GetAll()
        {
            return _transactionRepo.GetAll().Select(MapToDto).ToList();
        }

        public TransactionDto? GetById(int id)
        {
            var transaction = _transactionRepo.GetById(id);
            return transaction == null ? null : MapToDto(transaction);
        }

        public TransactionDto Add(TransactionDto dto)
        {
            var sender = _clientRepo.GetById(dto.SenderId);
            var receiver = _employeeRepo.GetById(dto.ReceiverId);

            if (sender == null || sender.User == null)
                throw new Exception("Sender not found or invalid");

            if (receiver == null)
                throw new Exception("Receiver not found");

            var transaction = new Transaction
            {
                TransactionId = dto.TransactionId,
                AccountId = dto.AccountId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionStatus = dto.TransactionStatus,
                TransactionDate = dto.TransactionDate,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                SenderName = sender.User.Username,
                ReceiverName = receiver.Name
            };

            var saved = _transactionRepo.Add(transaction);
            return MapToDto(saved);
        }

        //public TransactionDto? Update(int id, TransactionDto dto)
        //{
        //    var transaction = _transactionRepo.GetById(id);
        //    if (transaction == null) return null;
        //    transaction.TransactionId = id;
        //    transaction.AccountId = dto.AccountId;
        //    transaction.TransactionType = dto.TransactionType;
        //    transaction.Amount = dto.Amount;
        //    transaction.TransactionStatus = dto.TransactionStatus;
        //    transaction.TransactionDate = dto.TransactionDate;
        //    transaction.SenderId = dto.SenderId;
        //    transaction.ReceiverId = dto.ReceiverId;
        //    transaction.SenderName = dto.SenderName ?? transaction.SenderName;
        //    transaction.ReceiverName = dto.ReceiverName ?? transaction.ReceiverName;

        //    var updated = _transactionRepo.Update(id, transaction);
        //    return updated == null ? null : MapToDto(updated);
        //}

        //public bool Delete(int id)
        //{
        //    return _transactionRepo.Delete(id);
        //}

        public IEnumerable<TransactionDto> GetByAccount(int accountId)
        {
            return _transactionRepo.GetByAccount(accountId).Select(MapToDto).ToList();
        }

        public IEnumerable<TransactionDto> GetByClientId(int clientId)
        {
            return _transactionRepo.GetByClientId(clientId).Select(MapToDto).ToList();
        }

        private TransactionDto MapToDto(Transaction t)
        {
            return new TransactionDto
            {
                TransactionId = t.TransactionId,
                AccountId = t.AccountId,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                TransactionStatus = t.TransactionStatus,
                TransactionDate = t.TransactionDate,
                SenderId = t.SenderId,
                SenderName = t.SenderName,
                ReceiverId = t.ReceiverId,
                ReceiverName = t.ReceiverName
            };
        }

       

    }
}
