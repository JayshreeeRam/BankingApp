using BankingApp.Models;
using System.Collections.Generic;

namespace BankingApp.Repository
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();  // ✅ This must exist
        Transaction? GetById(int id);
        Transaction Add(Transaction transaction);
        Transaction? Update(int id, Transaction transaction);
        bool Delete(int id);
        IEnumerable<Transaction> GetByAccount(int accountId);
    }
}
