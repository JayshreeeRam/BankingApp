using System;
using System.Linq;
using BankingApp.Models;

namespace BankingApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingContext _context;

        public TransactionRepository(BankingContext context)
        {
            _context = context;
        }

        public Transaction? GetById(int id) =>
            _context.Transactions.FirstOrDefault(t => t.TransactionId == id);

        public IEnumerable<Transaction> GetByAccountId(int accountId) =>
            _context.Transactions.Where(t => t.AccountId == accountId).ToList();

        public Transaction Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void SoftDelete(Transaction transaction)
        {
            transaction.TransactionStatus = Enums.TransactionStatus.Success; // example soft delete
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }
    }
}
