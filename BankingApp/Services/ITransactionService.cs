using BankingApp.Models;

namespace BankingApp.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetAll();
        Transaction Add(Transaction transaction);
        Transaction? Update(int id, Transaction transaction);
        bool Delete(int id);
        Transaction? Find(int id);
        List<Transaction> GetByAccount(int accountId);
    }
}
