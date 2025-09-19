
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
                           .Include(t => t.Account) // navigation property
                           .ToList();
        }

        public Transaction? GetById(int id)
        {
            return _context.Transactions
                           .Include(t => t.Account)
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

            _context.Entry(existing).CurrentValues.SetValues(transaction);
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
                           .Where(t => t.AccountId == accountId)
                           .Include(t => t.Account)
                           .ToList();
        }
    }
}
