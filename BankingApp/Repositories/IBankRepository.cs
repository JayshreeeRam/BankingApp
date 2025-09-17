using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IBankRepository
    {
        IEnumerable<Bank> GetAll();
        Bank GetById(int id);
        Bank Add(Bank bank);
        Bank Update(Bank bank);
        void Delete(int id);


    }
}
