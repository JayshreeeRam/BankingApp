
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Repository
{
   

    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingContext _repo;

        public TransactionRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _repo.Transactions
                        .Include(t => t.Account)
                        .Include(t => t.Sender)
                        .Include(t => t.Receiver)
                        .ToList();
        }

        public Transaction? GetById(int id)
        {
            return _repo.Transactions
                        .Include(t => t.Account)
                        .Include(t => t.Sender)
                        .Include(t => t.Receiver)
                        .FirstOrDefault(t => t.TransactionId == id);
        }

        public Transaction Add(Transaction transaction)
        {
            _repo.Transactions.Add(transaction);
            _repo.SaveChanges();
            return transaction;
        }

        public Transaction? Update(int id, Transaction transaction)
        {
            var existing = _repo.Transactions.Find(id);
            if (existing == null) return null;

            _repo.Entry(existing).CurrentValues.SetValues(transaction);
            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var transaction = _repo.Transactions.Find(id);
            if (transaction == null) return false;

            _repo.Transactions.Remove(transaction);
            _repo.SaveChanges();
            return true;
        }

        public IEnumerable<Transaction> GetByAccount(int accountId)
        {
            return _repo.Transactions
                        .Where(t => t.AccountId == accountId)
                        .Include(t => t.Account)
                        .Include(t => t.Sender)
                        .Include(t => t.Receiver)
                        .ToList();
        }
    }
}
