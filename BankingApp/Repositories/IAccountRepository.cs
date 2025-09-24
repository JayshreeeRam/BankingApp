using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account? GetById(int id);
        public Account? GetByClientId(int clientId);
        Account Add(Account account);
        Account? Update(int id, Account account);
        bool Delete(int id);
        bool AccountNumberExists(string accountNumber);

    }
}
