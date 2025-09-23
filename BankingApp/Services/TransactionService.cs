using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankingContext _context;

        public TransactionService(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<TransactionDto> GetAll()
        {
            return _context.Transactions
                .Include(t => t.Account)
                    .ThenInclude(a => a.Client)
                .AsNoTracking()
                .Select(MapToDto)
                .ToList();
        }

        public TransactionDto? GetById(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Account)
                    .ThenInclude(a => a.Client)
                .AsNoTracking()
                .FirstOrDefault(t => t.TransactionId == id);

            if (transaction == null) return null;

            return MapToDto(transaction);
        }

        public Transaction Add(TransactionDto dto)
        {
            // Fetch the client and employee info for names
            var sender = _context.Clients
                .Include(c => c.User)
                .FirstOrDefault(c => c.ClientId == dto.SenderId);

            var receiverEmployee = _context.Employees
                .Include(e => e.Client)
                .ThenInclude(c => c.User)
                .FirstOrDefault(e => e.EmployeeId == dto.ReceiverId);

            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionStatus = dto.TransactionStatus,
                TransactionDate = dto.TransactionDate,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                SenderName = sender?.User?.Username , 
                ReceiverName = receiverEmployee?.Name ,
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }


        public Transaction? Update(int id, TransactionDto dto)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null) return null;

            transaction.AccountId = dto.AccountId;
            transaction.TransactionType = dto.TransactionType;
            transaction.Amount = dto.Amount;
            transaction.TransactionStatus = dto.TransactionStatus;
            transaction.TransactionDate = dto.TransactionDate;
            transaction.SenderId = dto.SenderId;
            transaction.ReceiverId = dto.ReceiverId;
            transaction.ReceiverName = dto.ReceiverName;
            transaction.SenderName = dto.SenderName;

            _context.SaveChanges();
            return transaction;
        }


        public bool Delete(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<TransactionDto> GetByAccount(int accountId)
        {
            return _context.Transactions
                .Include(t => t.Account)
                    .ThenInclude(a => a.Client)
                .Where(t => t.AccountId == accountId)
                .AsNoTracking()
                .Select(MapToDto)
                .ToList();
        }

        private TransactionDto MapToDto(Transaction t)
        {
            return new TransactionDto
            {
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
