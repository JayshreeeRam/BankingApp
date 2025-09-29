using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingContext _context;

        public TransactionRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions
                           .AsNoTracking()
                           .OrderByDescending(t => t.TransactionDate)
                           .ToList();
        }

        public Transaction? GetById(int id)
        {
            return _context.Transactions
                           .AsNoTracking()
                           .FirstOrDefault(t => t.TransactionId == id);
        }

        public Transaction Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges(); 
            return transaction;
        }

        public Transaction? Update(int id, Transaction transaction)
        {
            var existing = _context.Transactions.Find(id);
            if (existing == null) return null;

            existing.AccountId = transaction.AccountId;
            existing.TransactionType = transaction.TransactionType;
            existing.Amount = transaction.Amount;
            existing.TransactionStatus = transaction.TransactionStatus;
            existing.TransactionDate = transaction.TransactionDate;
            existing.SenderId = transaction.SenderId;
            existing.ReceiverId = transaction.ReceiverId;
            existing.SenderName = transaction.SenderName;
            existing.ReceiverName = transaction.ReceiverName;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Transaction> GetByAccount(int accountId)
        {
            return _context.Transactions
                           .AsNoTracking()
                           .Where(t => t.AccountId == accountId)
                           .OrderByDescending(t => t.TransactionDate)
                           .ToList();
        }

        public IEnumerable<Transaction> GetByClientId(int clientId)
        {
            return _context.Transactions
                           .AsNoTracking()
                           .Where(t => t.SenderId == clientId || t.ReceiverId == clientId)
                           .OrderByDescending(t => t.TransactionDate)
                           .ToList();
        }

        public IEnumerable<Transaction> GetByEmployeeId(int employeeId)
        {
            return _context.Transactions
                           .AsNoTracking()
                           .Where(t => t.ReceiverId == employeeId)
                           .OrderByDescending(t => t.TransactionDate)
                           .ToList();
        }
    }
}
