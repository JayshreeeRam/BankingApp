using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
        Transaction Add(Transaction transaction);
        Transaction? Update(int id, Transaction transaction);
        bool Delete(int id);
        Transaction? Find(int id);
        List<Transaction> GetByAccount(int accountId);
    }
}
