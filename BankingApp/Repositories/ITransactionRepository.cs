using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface ITransactionRepository
    {
        Transaction? GetById(int id);
        IEnumerable<Transaction> GetByAccountId(int accountId);
        Transaction Add(Transaction transaction);
        void Update(Transaction transaction);
        void SoftDelete(Transaction transaction);
    }
}
