//using BankingApp.Data;
//using BankingApp.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BankingApp.Repositories
//{
//public class TransactionRepository : ITransactionRepository
//{
//    private readonly BankingContext _ctx;

//    public TransactionRepository(BankingContext ctx) => _ctx = ctx;

//        public List<Transaction> GetAll()
//        {
//            return _ctx.Transaction
//                .Include(t => t.Account)
//                .ToList();
//        }

//        public Transaction Add(Transaction transaction)
//        {
//            _ctx.Transaction.Add(transaction);
//            _ctx.SaveChanges();
//            return transaction;
//        }

//        public Transaction? Update(int id, Transaction transaction)
//        {
//            var existing = _ctx.Transaction.Find(id);
//            if (existing == null) return null;

//            existing.TransactionType = transaction.TransactionType;
//            existing.TransactionDate = transaction.TransactionDate;
//            existing.Amount = transaction.Amount;
//            existing.Status = transaction.Status;
//            existing.AccountId = transaction.AccountId;

//            _ctx.SaveChanges();
//            return existing;
//        }

//        public bool Delete(int id)
//        {
//            var existing = _ctx.Transaction.Find(id);
//            if (existing == null) return false;

//            _ctx.Transaction.Remove(existing);
//            _ctx.SaveChanges();
//            return true;
//        }

//        public Transaction? Find(int id)
//        {
//            return _ctx.Transaction
//                .Include(t => t.Account)
//                .FirstOrDefault(t => t.TransactionId == id);
//        }

//        public List<Transaction> GetByAccount(int accountId)
//        {
//            return _ctx.Transaction
//                .Where(t => t.AccountId == accountId)
//                .ToList();
//        }
//    }
//}
