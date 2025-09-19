using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;

        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<TransactionDto> GetAll()
        {
            return _repo.GetAll().Select(MapToDto);
        }

        public TransactionDto? GetById(int id)
        {
            var transaction = _repo.GetById(id);
            if (transaction == null) return null;
            return MapToDto(transaction);
        }

        public Transaction Add(TransactionDto dto)
        {
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionStatus = dto.TransactionStatus,
                TransactionDate = dto.TransactionDate
            };
            return _repo.Add(transaction);
        }

        public Transaction? Update(int id, TransactionDto dto)
        {
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionStatus = dto.TransactionStatus,
                TransactionDate = dto.TransactionDate
            };
            return _repo.Update(id, transaction);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public IEnumerable<TransactionDto> GetByAccount(int accountId)
        {
            return _repo.GetByAccount(accountId).Select(MapToDto);
        }

        private TransactionDto MapToDto(Transaction t)
        {
            return new TransactionDto
            {
                AccountId = t.AccountId,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                TransactionStatus = t.TransactionStatus,
                TransactionDate = t.TransactionDate
            };
        }
    }
}
