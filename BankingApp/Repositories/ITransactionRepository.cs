using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        Transaction? GetById(int id);
        Transaction Add(Transaction transaction);
        //Transaction? Update(int id, Transaction transaction);
        //bool Delete(int id);
        IEnumerable<Transaction> GetByAccount(int accountId);
    }

}
